using Microsoft.AspNetCore.Http;

namespace Boekje.Web.Infrastructure;

public class CurrentUserService
{
    private readonly IHttpContextAccessor
        _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor
            httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor;
    }

    public bool IsAuthenticated
    {
        get
        {
            return !string.IsNullOrEmpty(
                Email);
        }
    }

    public string? Email
    {
        get
        {
            return _httpContextAccessor
                .HttpContext?
                .Session
                .GetString("UserEmail");
        }
    }

    public int UserId
    {
        get
        {
            return _httpContextAccessor
                       .HttpContext?
                       .Session
                       .GetInt32("UserId")
                   ?? 0;
        }
    }
}