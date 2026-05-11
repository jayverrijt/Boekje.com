using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Domain.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        return result == PasswordVerificationResult.Success ? user : null;
    }

    public async Task<bool> RegisterAsync(string email, string password)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);

        if (existingUser != null)
            return false;

        var user = new User
        {
            Email = email,
            PasswordHash = _passwordHasher.HashPassword(null, password)
        };

        await _userRepository.AddAsync(user);

        return true;
    }
}