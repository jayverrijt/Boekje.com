using System.Collections.Generic;
using MySqlConnector;
using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddTransaction(Transaction transaction)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var query = @"INSERT INTO transaction 
            (user_id, amount, type, category, description) 
            VALUES (@userId, @amount, @type, @category, @description)";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@userId", transaction.UserId);
        cmd.Parameters.AddWithValue("@amount", transaction.Amount);
        cmd.Parameters.AddWithValue("@type", transaction.Type);
        cmd.Parameters.AddWithValue("@category", transaction.Category);
        cmd.Parameters.AddWithValue("@description", transaction.Description);

        cmd.ExecuteNonQuery();
    }

    public List<Transaction> GetByUser(int userId)
    {
        var list = new List<Transaction>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        var query = "SELECT * FROM transaction WHERE user_id = @userId";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@userId", userId);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Transaction
            {
                Id = reader.GetInt32("id"),
                UserId = reader.GetInt32("user_id"),
                Amount = reader.GetDecimal("amount"),
                Type = reader.GetString("type"),
                Category = reader.GetString("category"),
                Description = reader["description"]?.ToString(),
                CreatedAt = reader.GetDateTime("created_at")
            });
        }

        return list;
    }
}