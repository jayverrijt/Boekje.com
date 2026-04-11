using Boekje.Domain.Entities;

namespace Boekje.Domain.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();
    User GetById(int id);
    User GetByEmail(string email);
    int Add(User user);
    bool Update(User user);
    bool Delete(int id);
}