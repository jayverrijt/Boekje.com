using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IUserRepository
{
    User? GetByEmail(string email);
    void Add(User user);
}