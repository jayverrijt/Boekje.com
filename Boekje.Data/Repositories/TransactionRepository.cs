using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class TransactionRepository
    : ITransactionRepository
{
    private readonly string
        _connectionString;

    public TransactionRepository(
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

    public void AddTransaction(
        Transaction transaction)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            new MySqlCommand(
                @"INSERT INTO transaction
                    (
                        user_id,
                        amount,
                        type,
                        category,
                        description,
                        created_at
                    )
                  VALUES
                    (
                        @userId,
                        @amount,
                        @type,
                        @category,
                        @description,
                        @createdAt
                    )",
                connection);

        command.Parameters.AddWithValue(
            "@userId",
            transaction.UserId);

        command.Parameters.AddWithValue(
            "@amount",
            transaction.Amount);

        command.Parameters.AddWithValue(
            "@type",
            transaction.Type);

        command.Parameters.AddWithValue(
            "@category",
            transaction.Category);

        command.Parameters.AddWithValue(
            "@description",
            transaction.Description);

        command.Parameters.AddWithValue(
            "@createdAt",
            transaction.CreatedAt);

        command.ExecuteNonQuery();
    }

    public List<Transaction>
        GetByUser(int userId)
    {
        var transactions =
            new List<Transaction>();

        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using (var command =
               new MySqlCommand(
                   @"SELECT *
                     FROM transaction
                     WHERE user_id = @userId
                     ORDER BY created_at DESC",
                   connection))
        {
            command.Parameters.AddWithValue(
                "@userId",
                userId);

            using (var reader =
                   command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var transaction =
                        new Transaction(
                            reader.GetInt32(
                                "user_id"),

                            reader.GetDecimal(
                                "amount"),

                            reader.GetString(
                                "type"),

                            reader.GetString(
                                "category"),

                            reader["description"]
                                ?.ToString());

                    transaction.SetId(
                        reader.GetInt32(
                            "id"));

                    transaction.SetCreatedAt(
                        reader.GetDateTime(
                            "created_at"));

                    transactions.Add(
                        transaction);
                }
            }
        }

        return transactions;
    }
}