using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Domain.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _hasher;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _hasher = new PasswordHasher<User>();
    }

    public bool Register(string email, string password)
    {
        var existing = _userRepository.GetByEmail(email);
        if (existing != null) return false;

        var user = new User { Email = email };

        // 🔐 Hash password
        user.PasswordHash = _hasher.HashPassword(user, password);

        _userRepository.Add(user);
        return true;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);

        // user bestaat niet
        if (user == null)
            return null;

        // oude user zonder password (voorkomt crash)
        if (string.IsNullOrEmpty(user.PasswordHash))
            return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success ? user : null;
    }
}