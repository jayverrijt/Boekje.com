using Boekje.Data.Repositories;
using Boekje.Domain.Interfaces;

namespace Boekje.Web.DependencyInjection;

public static class RepositoryInjection
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository>(provider =>
            new UserRepository(configuration));

        services.AddScoped<IBudgetRepository>(provider =>
            new BudgetRepository(configuration));

        services.AddScoped<ITransactionRepository>(provider =>
            new TransactionRepository(configuration));

        return services;
    }
}