using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Domain.Services;

public class AuthService
{
    private readonly IUserRepository
        _userRepository;

    private readonly IPasswordHasher<User>
        _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository =
            userRepository;

        _passwordHasher =
            passwordHasher;
    }

    public async Task<User?> LoginAsync(
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(
                email))
        {
            throw new Exception(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(
                password))
        {
            throw new Exception(
                "Password is required.");
        }

        var user =
            await _userRepository
                .GetByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        var result =
            _passwordHasher
                .VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    password);

        return result ==
               PasswordVerificationResult.Success
            ? user
            : null;
    }

    public async Task<bool> RegisterAsync(
        string name,
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(
                name))
        {
            throw new Exception(
                "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(
                email))
        {
            throw new Exception(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(
                password))
        {
            throw new Exception(
                "Password is required.");
        }

        var existingUser =
            await _userRepository
                .GetByEmailAsync(email);

        if (existingUser != null)
        {
            return false;
        }

        /*
         * Password hashing now handled
         * through Dependency Injection
         */

        var hashedPassword =
            _passwordHasher
                .HashPassword(
                    null!,
                    password);

        var user =
            new User(
                name,
                email,
                hashedPassword);

        await _userRepository
            .AddAsync(user);

        return true;
    }
}