using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class BudgetRepository
    : IBudgetRepository
{
    private readonly string
        _connectionString;

    public BudgetRepository(
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString(
                "DefaultConnection");

        if (string.IsNullOrWhiteSpace(
                connectionString))
        {
            throw new Exception(
                "Connection string not found.");
        }

        _connectionString =
            connectionString;
    }

    public Budget? GetByUser(
        int userId)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        Budget? budget = null;

        /*
         * GET BUDGET
         */

        using (var budgetCommand =
               new MySqlCommand(
                   @"SELECT *
                     FROM budget
                     WHERE user_id = @userId",
                   connection))
        {
            budgetCommand.Parameters.AddWithValue(
                "@userId",
                userId);

            using (var reader =
                   budgetCommand.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return null;
                }

                budget =
                    new Budget(
                        reader.GetInt32(
                            "user_id"),

                        reader.GetDecimal(
                            "income"));

                budget.SetId(
                    reader.GetInt32(
                        "id"));
            }
        }

        /*
         * GET EXPENSES
         */

        using (var expenseCommand =
               new MySqlCommand(
                   @"SELECT *
                     FROM expense
                     WHERE budget_id = @budgetId",
                   connection))
        {
            expenseCommand.Parameters.AddWithValue(
                "@budgetId",
                budget!.Id);

            using (var expenseReader =
                   expenseCommand.ExecuteReader())
            {
                while (expenseReader.Read())
                {
                    var expense =
                        new Expense(
                            expenseReader.GetString(
                                "type"),

                            expenseReader.GetDecimal(
                                "amount"),

                            expenseReader.GetString(
                                "name"));

                    budget.AddExpense(
                        expense);
                }
            }
        }

        /*
         * GET SAVINGS
         */

        using (var savingCommand =
               new MySqlCommand(
                   @"SELECT *
                     FROM saving
                     WHERE budget_id = @budgetId",
                   connection))
        {
            savingCommand.Parameters.AddWithValue(
                "@budgetId",
                budget.Id);

            using (var savingReader =
                   savingCommand.ExecuteReader())
            {
                while (savingReader.Read())
                {
                    var saving =
                        new Saving(
                            savingReader.GetDecimal(
                                "amount"),

                            savingReader.GetString(
                                "name"));

                    budget.AddSaving(
                        saving);
                }
            }
        }

        return budget;
    }

    public int CreateBudget(
        Budget budget)
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
    }

    public void InsertExpenses(
        int budgetId,
        IReadOnlyList<Expense> expenses)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        foreach (var expense in expenses)
        {
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

            command.Parameters.AddWithValue(
                "@type",
                expense.Type);

            command.Parameters.AddWithValue(
                "@amount",
                expense.Amount);

            command.Parameters.AddWithValue(
                "@name",
                expense.Name);

            command.ExecuteNonQuery();
        }
    }

    public void InsertSavings(
        int budgetId,
        IReadOnlyList<Saving> savings)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        foreach (var saving in savings)
        {
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

            command.Parameters.AddWithValue(
                "@amount",
                saving.Amount);

            command.Parameters.AddWithValue(
                "@name",
                saving.Name);

            command.ExecuteNonQuery();
        }
    }

    public void InsertCategories(
        int budgetId,
        Dictionary<string, decimal>
            categories)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        foreach (var category in categories)
        {
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

            command.Parameters.AddWithValue(
                "@name",
                category.Key);

            command.Parameters.AddWithValue(
                "@amount",
                category.Value);

            command.ExecuteNonQuery();
        }
    }

    public Dictionary<string, decimal>
        GetCategories(
            int budgetId)
    {
        var categories =
            new Dictionary<string, decimal>();

        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            new MySqlCommand(
                @"SELECT *
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

    public void DeleteByBudgetId(
        int budgetId)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        /*
         * DELETE EXPENSES
         */

        using (var deleteExpenses =
               new MySqlCommand(
                   @"DELETE FROM expense
                     WHERE budget_id = @budgetId",
                   connection))
        {
            deleteExpenses.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            deleteExpenses.ExecuteNonQuery();
        }

        /*
         * DELETE SAVINGS
         */

        using (var deleteSavings =
               new MySqlCommand(
                   @"DELETE FROM saving
                     WHERE budget_id = @budgetId",
                   connection))
        {
            deleteSavings.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            deleteSavings.ExecuteNonQuery();
        }

        /*
         * DELETE CATEGORIES
         */

        using (var deleteCategories =
               new MySqlCommand(
                   @"DELETE FROM category
                     WHERE budget_id = @budgetId",
                   connection))
        {
            deleteCategories.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            deleteCategories.ExecuteNonQuery();
        }

        /*
         * DELETE BUDGET
         */

        using (var deleteBudget =
               new MySqlCommand(
                   @"DELETE FROM budget
                     WHERE id = @budgetId",
                   connection))
        {
            deleteBudget.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            deleteBudget.ExecuteNonQuery();
        }
    }

    public void DeleteByUserId(
        int userId)
    {
        var budget =
            GetByUser(userId);

        if (budget != null)
        {
            DeleteByBudgetId(
                budget.Id);
        }
    }
}