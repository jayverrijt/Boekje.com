using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly string
        _connectionString;

    public CategoryRepository(IConfiguration configuration) {
        _connectionString =
            configuration
                .GetConnectionString(
                    "DefaultConnection")
            ?? throw new Exception(
                "Connection string missing.");
    }

    public Dictionary<string, decimal> GetByBudget(int budgetId) {
        try
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
        } catch(MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void Insert(int budgetId, string name, decimal amount) {
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

            command.Parameters.AddWithValue(
                "@name",
                name);

            command.Parameters.AddWithValue(
                "@amount",
                amount);

            command.ExecuteNonQuery();
        } catch (MySqlException ex)
        {
            throw new InvalidOperationException("Failed to insert into Category",ex);
        }
    }

    public void DeleteByBudget(int budgetId) {
        try
        {
            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"DELETE FROM category
                  WHERE budget_id = @budgetId",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException("Failed to delete category from budget",ex);
        }
    }
}