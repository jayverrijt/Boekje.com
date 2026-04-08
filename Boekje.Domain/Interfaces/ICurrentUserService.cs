namespace Boekje.Domain.Interfaces;

public interface ICurrentUserService
{
    string? Email { get; }
    bool IsAuthenticated { get; }
}