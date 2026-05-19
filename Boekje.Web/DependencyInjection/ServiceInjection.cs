using Boekje.Domain.Entities;
using Boekje.Domain.Services;
using Boekje.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Web.DependencyInjection;

public static class ServiceInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<AuthService>();

        services.AddScoped<BudgetService>();

        services.AddScoped<TransactionService>();

        services.AddScoped<DashboardService>();

        services.AddScoped<CurrentUserService>();

        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();

        return services;
    }
}