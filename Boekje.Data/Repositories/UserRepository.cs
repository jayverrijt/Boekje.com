using System.Collections.Generic;
using System.Threading.Tasks;
using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, email, password FROM `user` WHERE email = @email";
        cmd.Parameters.AddWithValue("@email", email);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Email = reader.GetString(1),
                PasswordHash = reader.GetString(2)
            };
        }

        return null;
    }


    public async Task AddAsync(User user)
    {
        using var conn = new MySqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO `user` (email, password) VALUES (@email, @password)";
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.PasswordHash);

        await cmd.ExecuteNonQueryAsync();
    }


    public async Task<List<User>> GetAllAsync()
    {
        return new List<User>();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return null;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return false;
    }
}