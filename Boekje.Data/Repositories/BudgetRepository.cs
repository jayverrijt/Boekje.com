using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class BudgetRepository : IBudgetRepository {
    private readonly string
        _connectionString;

    public BudgetRepository(IConfiguration configuration) {
        var connectionString =
            configuration.GetConnectionString(
                "DefaultConnection");

        if (string.IsNullOrWhiteSpace(
                connectionString))
        {
            throw new InvalidOperationException(
                "DefaultConnection is not configured.");
        }

        _connectionString =
            connectionString;
    }

    public Budget? GetByUser(
       int userId)
    {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"SELECT id,
                             user_id,
                             income
                      FROM budget
                      WHERE user_id = @userId;
    
                      SELECT e.type,
                             e.amount,
                             e.name
                      FROM expense e
                      INNER JOIN budget b
                          ON e.budget_id = b.id
                      WHERE b.user_id = @userId;
    
                      SELECT s.amount,
                             s.name
                      FROM saving s
                      INNER JOIN budget b
                          ON s.budget_id = b.id
                      WHERE b.user_id = @userId;",
                    connection);

            command.Parameters.AddWithValue(
                "@userId",
                userId);

            using var reader =
                command.ExecuteReader();

            /*
             * BUDGET
             */

            if (!reader.Read())
            {
                return null;
            }

            var budget =
                new Budget(
                    reader.GetInt32(
                        "user_id"),

                    reader.GetDecimal(
                        "income"));

            budget.SetId(
                reader.GetInt32(
                    "id"));

            /*
             * EXPENSES
             */

            reader.NextResult();

            while (reader.Read())
            {
                budget.AddExpense(
                    new Expense(
                        reader.GetString(
                            "type"),

                        reader.GetDecimal(
                            "amount"),

                        reader.GetString(
                            "name")));
            }

            /*
             * SAVINGS
             */

            reader.NextResult();

            while (reader.Read())
            {
                budget.AddSaving(
                    new Saving(
                        reader.GetDecimal(
                            "amount"),

                        reader.GetString(
                            "name")));
            }

            return budget;
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(
                "Failed to load budget.",
                ex);
        }
    }

    public int CreateBudget(Budget budget) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"INSERT INTO budget
                    (
                        user_id,
                        income
                    )
                  VALUES
                    (
                        @userId,
                        @income
                    );

                  SELECT LAST_INSERT_ID();",
                    connection);

            command.Parameters.AddWithValue(
                "@userId",
                budget.UserId);

            command.Parameters.AddWithValue(
                "@income",
                budget.Income);

            return Convert.ToInt32(
                command.ExecuteScalar());
        } catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Failed to create budget.",
                ex);
        }
    }

    public void InsertExpenses(int budgetId, IReadOnlyList<Expense> expenses) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"INSERT INTO expense
                    (
                        budget_id,
                        type,
                        amount,
                        name
                    )
                  VALUES
                    (
                        @budgetId,
                        @type,
                        @amount,
                        @name
                    )",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.Parameters.Add(
                "@type",
                MySqlDbType.VarChar);

            command.Parameters.Add(
                "@amount",
                MySqlDbType.Decimal);

            command.Parameters.Add(
                "@name",
                MySqlDbType.VarChar);

            foreach (var expense in expenses)
            {
                command.Parameters["@type"].Value =
                    expense.Type;

                command.Parameters["@amount"].Value =
                    expense.Amount;

                command.Parameters["@name"].Value =
                    expense.Name;

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to insert expenses.", ex);
        }
    }

    public void InsertSavings(int budgetId, IReadOnlyList<Saving> savings) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"INSERT INTO saving
                    (
                        budget_id,
                        amount,
                        name
                    )
                  VALUES
                    (
                        @budgetId,
                        @amount,
                        @name
                    )",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.Parameters.Add(
                "@amount",
                MySqlDbType.Decimal);

            command.Parameters.Add(
                "@name",
                MySqlDbType.VarChar);

            foreach (var saving in savings)
            {
                command.Parameters["@amount"].Value =
                    saving.Amount;

                command.Parameters["@name"].Value =
                    saving.Name;

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Could not insert to savings .", ex);
        }
    }

    public void InsertCategories(int budgetId, Dictionary<string, decimal> categories) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"INSERT INTO category
                    (
                        budget_id,
                        name,
                        amount
                    )
                  VALUES
                    (
                        @budgetId,
                        @name,
                        @amount
                    )",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.Parameters.Add(
                "@name",
                MySqlDbType.VarChar);

            command.Parameters.Add(
                "@amount",
                MySqlDbType.Decimal);

            foreach (var category in categories)
            {
                command.Parameters["@name"].Value =
                    category.Key;

                command.Parameters["@amount"].Value =
                    category.Value;

                command.ExecuteNonQuery();
            }
        } catch (Exception ex)
        {
            throw new InvalidOperationException("Could not insert categories", ex);
        }
    }

    public Dictionary<string, decimal> GetCategories(
            int budgetId) { try {
            var categories =
                new Dictionary<string, decimal>();

            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"SELECT name,
                         amount
                  FROM category
                  WHERE budget_id = @budgetId",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            using var reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(
                    reader.GetString(
                        "name"),

                    reader.GetDecimal(
                        "amount"));
            }

            return categories;
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void DeleteByBudgetId(int budgetId) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"DELETE FROM expense
                  WHERE budget_id = @budgetId;

                  DELETE FROM saving
                  WHERE budget_id = @budgetId;

                  DELETE FROM category
                  WHERE budget_id = @budgetId;

                  DELETE FROM budget
                  WHERE id = @budgetId;",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.ExecuteNonQuery();
        } catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void DeleteByUserId(int userId) {
        try
        {
            var budget =
                GetByUser(
                    userId);

            if (budget != null)
            {
                DeleteByBudgetId(
                    budget.Id);
            }
        } catch (MySqlException ex)
        {
            throw new InvalidOperationException("Could not delete budget", ex);
        }
    }
}