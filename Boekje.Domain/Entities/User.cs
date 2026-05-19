namespace Boekje.Domain.Entities;

public class User
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public User(
        string name,
        string email,
        string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception(
                "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new Exception(
                "Password hash is required.");
        }

        Name = name;

        Email = email;

        PasswordHash = passwordHash;
    }

    /*
     * Persistence helper
     */

    public void SetId(int id)
    {
        Id = id;
    }

    /*
     * Domain behavior
     */

    public void ChangeName(
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception(
                "Name is required.");
        }

        Name = name;
    }

    public void ChangePassword(
        string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new Exception(
                "Password hash is required.");
        }

        PasswordHash = passwordHash;
    }
}