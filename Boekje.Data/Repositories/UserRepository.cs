using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class UserRepository
    : IUserRepository
{
    private readonly string
        _connectionString;

    public UserRepository(
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

    public Task<User?> GetByEmailAsync(
        string email)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using (var command =
               connection.CreateCommand())
        {
            command.CommandText =
                @"SELECT id,
                         name,
                         email,
                         password
                  FROM user
                  WHERE email = @email";

            command.Parameters.AddWithValue(
                "@email",
                email);

            using (var reader =
                   command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var user =
                        new User(
                            reader.GetString(
                                "name"),

                            reader.GetString(
                                "email"),

                            reader.GetString(
                                "password"));

                    user.SetId(
                        reader.GetInt32(
                            "id"));

                    return Task.FromResult<User?>(
                        user);
                }
            }
        }

        return Task.FromResult<User?>(
            null);
    }

    public Task AddAsync(
        User user)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            connection.CreateCommand();

        command.CommandText =
            @"INSERT INTO user
                (
                    name,
                    email,
                    password
                )
              VALUES
                (
                    @name,
                    @email,
                    @password
                )";

        command.Parameters.AddWithValue(
            "@name",
            user.Name);

        command.Parameters.AddWithValue(
            "@email",
            user.Email);

        command.Parameters.AddWithValue(
            "@password",
            user.PasswordHash);

        command.ExecuteNonQuery();

        return Task.CompletedTask;
    }

    public Task<List<User>>
        GetAllAsync()
    {
        var users =
            new List<User>();

        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using (var command =
               connection.CreateCommand())
        {
            command.CommandText =
                @"SELECT id,
                         name,
                         email,
                         password
                  FROM user";

            using (var reader =
                   command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user =
                        new User(
                            reader.GetString(
                                "name"),

                            reader.GetString(
                                "email"),

                            reader.GetString(
                                "password"));

                    user.SetId(
                        reader.GetInt32(
                            "id"));

                    users.Add(user);
                }
            }
        }

        return Task.FromResult(
            users);
    }

    public Task<User?> GetByIdAsync(
        int id)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using (var command =
               connection.CreateCommand())
        {
            command.CommandText =
                @"SELECT id,
                         name,
                         email,
                         password
                  FROM user
                  WHERE id = @id";

            command.Parameters.AddWithValue(
                "@id",
                id);

            using (var reader =
                   command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var user =
                        new User(
                            reader.GetString(
                                "name"),

                            reader.GetString(
                                "email"),

                            reader.GetString(
                                "password"));

                    user.SetId(
                        reader.GetInt32(
                            "id"));

                    return Task.FromResult<User?>(
                        user);
                }
            }
        }

        return Task.FromResult<User?>(
            null);
    }

    public Task<bool> UpdateAsync(
        User user)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            connection.CreateCommand();

        command.CommandText =
            @"UPDATE user
              SET name = @name,
                  email = @email,
                  password = @password
              WHERE id = @id";

        command.Parameters.AddWithValue(
            "@id",
            user.Id);

        command.Parameters.AddWithValue(
            "@name",
            user.Name);

        command.Parameters.AddWithValue(
            "@email",
            user.Email);

        command.Parameters.AddWithValue(
            "@password",
            user.PasswordHash);

        var affected =
            command.ExecuteNonQuery();

        return Task.FromResult(
            affected > 0);
    }

    public Task<bool> DeleteAsync(
        int id)
    {
        using var connection =
            new MySqlConnection(
                _connectionString);

        connection.Open();

        using var command =
            connection.CreateCommand();

        command.CommandText =
            @"DELETE FROM user
              WHERE id = @id";

        command.Parameters.AddWithValue(
            "@id",
            id);

        var affected =
            command.ExecuteNonQuery();

        return Task.FromResult(
            affected > 0);
    }
}