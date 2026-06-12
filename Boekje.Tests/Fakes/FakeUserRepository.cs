using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;

namespace Boekje.Tests.Fakes;

public class FakeUserRepository : IUserRepository
{
    public List<User> Users { get; } = new();

    public Task<User?> GetByEmailAsync(string email)
    {
        User? user =
            Users.FirstOrDefault(x =>
                x.Email == email);

        return Task.FromResult<User?>(user);
    }

    public Task AddAsync(User user)
    {
        user.SetId(Users.Count + 1);

        Users.Add(user);

        return Task.CompletedTask;
    }

    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(
            Users.ToList());
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? user =
            Users.FirstOrDefault(x =>
                x.Id == id);

        return Task.FromResult<User?>(user);
    }

    public Task<bool> UpdateAsync(User user)
    {
        var existing =
            Users.FirstOrDefault(x =>
                x.Id == user.Id);

        if (existing == null)
        {
            return Task.FromResult(false);
        }

        Users.Remove(existing);
        Users.Add(user);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var existing =
            Users.FirstOrDefault(x =>
                x.Id == id);

        if (existing == null)
        {
            return Task.FromResult(false);
        }

        Users.Remove(existing);

        return Task.FromResult(true);
    }
}