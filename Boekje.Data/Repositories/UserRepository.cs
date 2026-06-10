using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Boekje.Data.Repositories;

public class UserRepository : IUserRepository {
    private readonly string
        _connectionString;

    public UserRepository(IConfiguration configuration) {
        try
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
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task<User?> GetByEmailAsync(string email) {
        try
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
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task AddAsync(User user) {
        try
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
        } catch (MySqlException ex) {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task<List<User>> GetAllAsync() {
        try
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
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task<User?> GetByIdAsync(int id) {
        try
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
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task<bool> UpdateAsync(User user) {
        try
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
        catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Task<bool> DeleteAsync(int id) {
        try
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
        } catch (MySqlException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}