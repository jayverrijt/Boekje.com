using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Tests.Fakes;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Boekje.Tests.Services;

public class AuthServiceTests
{
    [Fact]
    public async Task Register_CreatesUser()
    {
        var repo =
            new FakeUserRepository();

        var service =
            new AuthService(
                repo,
                new PasswordHasher<User>());

        var result =
            await service.RegisterAsync(
                "John",
                "john@test.com",
                "Password123");

        Assert.True(result);
        Assert.Single(repo.Users);
    }

    [Fact]
    public async Task Register_DuplicateEmail_ReturnsFalse()
    {
        var repo =
            new FakeUserRepository();

        await repo.AddAsync(
            new User(
                "John",
                "john@test.com",
                "hash"));

        var service =
            new AuthService(
                repo,
                new PasswordHasher<User>());

        var result =
            await service.RegisterAsync(
                "Jane",
                "john@test.com",
                "Password123");

        Assert.False(result);
    }

    [Fact]
    public async Task Register_EmptyName_Throws()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.RegisterAsync(
                "",
                "john@test.com",
                "Password123"));
    }

    [Fact]
    public async Task Register_EmptyEmail_Throws()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.RegisterAsync(
                "John",
                "",
                "Password123"));
    }

    [Fact]
    public async Task Register_EmptyPassword_Throws()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.RegisterAsync(
                "John",
                "john@test.com",
                ""));
    }

    [Fact]
    public async Task Login_ReturnsUser_WhenCredentialsCorrect()
    {
        var repo =
            new FakeUserRepository();

        var hasher =
            new PasswordHasher<User>();

        var hash =
            hasher.HashPassword(
                null!,
                "Password123");

        await repo.AddAsync(
            new User(
                "John",
                "john@test.com",
                hash));

        var service =
            new AuthService(
                repo,
                hasher);

        var result =
            await service.LoginAsync(
                "john@test.com",
                "Password123");

        Assert.NotNull(result);
        Assert.Equal(
            "john@test.com",
            result!.Email);
    }

    [Fact]
    public async Task Login_ReturnsNull_WhenPasswordIncorrect()
    {
        var repo =
            new FakeUserRepository();

        var hasher =
            new PasswordHasher<User>();

        var hash =
            hasher.HashPassword(
                null!,
                "Password123");

        await repo.AddAsync(
            new User(
                "John",
                "john@test.com",
                hash));

        var service =
            new AuthService(
                repo,
                hasher);

        var result =
            await service.LoginAsync(
                "john@test.com",
                "WrongPassword");

        Assert.Null(result);
    }

    [Fact]
    public async Task Login_ReturnsNull_WhenUserDoesNotExist()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        var result =
            await service.LoginAsync(
                "missing@test.com",
                "Password123");

        Assert.Null(result);
    }

    [Fact]
    public async Task Login_EmptyEmail_Throws()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.LoginAsync(
                "",
                "Password123"));
    }

    [Fact]
    public async Task Login_EmptyPassword_Throws()
    {
        var service =
            new AuthService(
                new FakeUserRepository(),
                new PasswordHasher<User>());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.LoginAsync(
                "john@test.com",
                ""));
    }
}