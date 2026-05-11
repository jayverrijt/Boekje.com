using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);

    // optioneel (later)
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
}