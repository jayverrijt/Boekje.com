using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class ExpenseRepository
    : IExpenseRepository
{
    private readonly string
        _connectionString;

    public ExpenseRepository(
        IConfiguration configuration)
    {
        _connectionString =
            configuration
                .GetConnectionString(
                    "DefaultConnection")
            ?? throw new Exception(
                "Connection string missing.");
    }

    public List<Expense> GetByBudget(
        int budgetId)
    {
        var expenses =
            new List<Expense>();

        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            new MySqlCommand(
                @"SELECT *
                  FROM expense
                  WHERE budget_id = @budgetId",
                connection);

        command.Parameters.AddWithValue(
            "@budgetId",
            budgetId);

        using var reader =
            command.ExecuteReader();

        while (reader.Read())
        {
            expenses.Add(
                new Expense(
                    reader.GetString(
                        "type"),

                    reader.GetDecimal(
                        "amount"),

                    reader.GetString(
                        "name")));
        }

        return expenses;
    }

    public void Insert(
        int budgetId,
        Expense expense)
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

    public void DeleteByBudget(
        int budgetId)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            new MySqlCommand(
                @"DELETE FROM expense
                  WHERE budget_id = @budgetId",
                connection);

        command.Parameters.AddWithValue(
            "@budgetId",
            budgetId);

        command.ExecuteNonQuery();
    }
}