using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class SavingRepository : ISavingRepository {
    private readonly string
        _connectionString;

    public SavingRepository(IConfiguration configuration) {
        try
        {
            _connectionString =
                configuration
                    .GetConnectionString(
                        "DefaultConnection")
                ?? throw new Exception(
                    "Connection string missing.");
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public List<Saving> GetByBudget(int budgetId) {
        try
        {
            var savings =
                new List<Saving>();

            using var connection =
                new MySqlConnection(
                    _connectionString);

            connection.Open();

            using var command =
                new MySqlCommand(
                    @"SELECT *
                  FROM saving
                  WHERE budget_id = @budgetId",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            using var reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                savings.Add(
                    new Saving(
                        reader.GetDecimal(
                            "amount"),

                        reader.GetString(
                            "name")));
            }

            return savings;
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void Insert(int budgetId, Saving saving) {
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

            command.Parameters.AddWithValue(
                "@amount",
                saving.Amount);

            command.Parameters.AddWithValue(
                "@name",
                saving.Name);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
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
                    @"DELETE FROM saving
                  WHERE budget_id = @budgetId",
                    connection);

            command.Parameters.AddWithValue(
                "@budgetId",
                budgetId);

            command.ExecuteNonQuery();
        }
        catch (MySqlException ex) {
            throw new InvalidOperationException(ex.Message);
        }
    }
}