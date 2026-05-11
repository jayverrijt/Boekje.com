using MySqlConnector;
using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Boekje.Data.Repositories;

public class BudgetRepository : IBudgetRepository
{
    private readonly string _connectionString;

    public BudgetRepository(IConfiguration config)
    {
        var conn = config.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(conn))
            throw new Exception("Connection string not found");

        _connectionString = conn;
    }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    // =========================
    // GET FULL BUDGET
    // =========================
    public Budget? GetByUser(int userId)
    {
        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("SELECT * FROM budget WHERE user_id = @userId", conn);
        cmd.Parameters.AddWithValue("@userId", userId);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            return null;

        var budget = new Budget
        {
            Id = reader.GetInt32("id"),
            UserId = reader.GetInt32("user_id"),
            Income = reader.GetDecimal("income"),
            Expenses = new List<Expense>(),
            Savings = new List<Saving>()
        };

        reader.Close();

        // ===== EXPENSES =====
        var expCmd = new MySqlCommand("SELECT * FROM recurring_expense WHERE budget_id = @bid", conn);
        expCmd.Parameters.AddWithValue("@bid", budget.Id);

        using var expReader = expCmd.ExecuteReader();
        while (expReader.Read())
        {
            budget.Expenses.Add(new Expense
            {
                Type = expReader.GetString("type"),
                Name = expReader.GetString("name"),
                Amount = expReader.GetDecimal("amount")
            });
        }
        expReader.Close();

        // ===== SAVINGS =====
        var savCmd = new MySqlCommand("SELECT * FROM saving WHERE budget_id = @bid", conn);
        savCmd.Parameters.AddWithValue("@bid", budget.Id);

        using var savReader = savCmd.ExecuteReader();
        while (savReader.Read())
        {
            budget.Savings.Add(new Saving
            {
                Name = savReader.GetString("name"),
                Amount = savReader.GetDecimal("amount")
            });
        }
        savReader.Close();

        return budget;
    }

    // =========================
    // GET CATEGORIES
    // =========================
    public Dictionary<string, decimal> GetCategories(int budgetId)
    {
        var result = new Dictionary<string, decimal>();

        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand("SELECT category, amount FROM category_budget WHERE budget_id = @bid", conn);
        cmd.Parameters.AddWithValue("@bid", budgetId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result[reader.GetString("category")] = reader.GetDecimal("amount");
        }

        return result;
    }

    // =========================
    // CREATE
    // =========================
    public int CreateBudget(Budget budget)
    {
        using var conn = GetConnection();
        conn.Open();

        var cmd = new MySqlCommand(
            "INSERT INTO budget (user_id, income) VALUES (@userId, @income); SELECT LAST_INSERT_ID();",
            conn);

        cmd.Parameters.AddWithValue("@userId", budget.UserId);
        cmd.Parameters.AddWithValue("@income", budget.Income);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // =========================
    // DELETE
    // =========================
    public void DeleteByBudgetId(int budgetId)
    {
        using var conn = GetConnection();
        conn.Open();

        var queries = new[]
        {
            "DELETE FROM recurring_expense WHERE budget_id = @id",
            "DELETE FROM saving WHERE budget_id = @id",
            "DELETE FROM category_budget WHERE budget_id = @id"
        };

        foreach (var query in queries)
        {
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", budgetId);
            cmd.ExecuteNonQuery();
        }
    }

    // =========================
    // INSERTS
    // =========================
    public void InsertExpenses(int budgetId, List<Expense> expenses)
    {
        using var conn = GetConnection();
        conn.Open();

        foreach (var e in expenses)
        {
            var cmd = new MySqlCommand(
                "INSERT INTO recurring_expense (budget_id, type, name, amount) VALUES (@bid, @type, @name, @amount)",
                conn);

            cmd.Parameters.AddWithValue("@bid", budgetId);
            cmd.Parameters.AddWithValue("@type", e.Type);
            cmd.Parameters.AddWithValue("@name", e.Name);
            cmd.Parameters.AddWithValue("@amount", e.Amount);

            cmd.ExecuteNonQuery();
        }
    }

    public void InsertSavings(int budgetId, List<Saving> savings)
    {
        using var conn = GetConnection();
        conn.Open();

        foreach (var s in savings)
        {
            var cmd = new MySqlCommand(
                "INSERT INTO saving (budget_id, name, amount) VALUES (@bid, @name, @amount)",
                conn);

            cmd.Parameters.AddWithValue("@bid", budgetId);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@amount", s.Amount);

            cmd.ExecuteNonQuery();
        }
    }

    public void InsertCategories(int budgetId, Dictionary<string, decimal> categories)
    {
        using var conn = GetConnection();
        conn.Open();

        foreach (var c in categories)
        {
            var cmd = new MySqlCommand(
                "INSERT INTO category_budget (budget_id, category, amount) VALUES (@bid, @cat, @amount)",
                conn);

            cmd.Parameters.AddWithValue("@bid", budgetId);
            cmd.Parameters.AddWithValue("@cat", c.Key);
            cmd.Parameters.AddWithValue("@amount", c.Value);

            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteByUserId(int userId)
    {
        using var conn = GetConnection();
        conn.Open();

        // eerst budget id ophalen
        var getCmd = new MySqlCommand("SELECT id FROM budget WHERE user_id = @uid", conn);
        getCmd.Parameters.AddWithValue("@uid", userId);

        var result = getCmd.ExecuteScalar();

        if (result == null)
            return;

        int budgetId = Convert.ToInt32(result);

        // child tables verwijderen
        DeleteByBudgetId(budgetId);

        // budget zelf verwijderen
        var deleteCmd = new MySqlCommand("DELETE FROM budget WHERE id = @id", conn);
        deleteCmd.Parameters.AddWithValue("@id", budgetId);

        deleteCmd.ExecuteNonQuery();
    }
}