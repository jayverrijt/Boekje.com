using Microsoft.AspNetCore.Http;
using Boekje.Domain.Interfaces;

namespace Boekje.Web.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public bool IsAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(Email);
            }
        }
    }
}