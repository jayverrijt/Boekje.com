using System.Collections.Generic;
using MySqlConnector;
using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<User> GetAll()
    {
        var users = new List<User>();

        using (var conn = new MySqlConnection(_connectionString))
        {
            conn.Open();

            string query = "SELECT Id, Name, Email, PasswordHash FROM Users";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(MapUser(reader));
            }
        }

        return users;
    }

    public User GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "SELECT Id, Name, Email, PasswordHash FROM Users WHERE Id = @Id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        return reader.Read() ? MapUser(reader) : null;
    }

    public User GetByEmail(string email)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "SELECT Id, Name, Email, PasswordHash FROM Users WHERE Email = @Email";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Email", email);

        using var reader = cmd.ExecuteReader();

        return reader.Read() ? MapUser(reader) : null;
    }

    public int Add(User user)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "INSERT INTO Users (Name, Email, PasswordHash) VALUES (@Name, @Email, @PasswordHash)";
        using var cmd = new MySqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    public bool Update(User user)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "UPDATE Users SET Name = @Name, Email = @Email, PasswordHash = @PasswordHash WHERE Id = @Id";
        using var cmd = new MySqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@Id", user.Id);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "DELETE FROM Users WHERE Id = @Id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        return cmd.ExecuteNonQuery() > 0;
    }

    // functie dat herhaling voorkomt en null crashes
    private User MapUser(MySqlDataReader reader)
    {
        return new User
        {
            Id = reader.GetInt32("Id"),

            Name = reader.IsDBNull(reader.GetOrdinal("Name"))
                ? null
                : reader.GetString("Name"),

            Email = reader.GetString("Email"),

            PasswordHash = reader.IsDBNull(reader.GetOrdinal("PasswordHash"))
                ? null
                : reader.GetString("PasswordHash")
        };
    }
}