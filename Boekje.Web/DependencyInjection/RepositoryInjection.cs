using Boekje.Data.Repositories;
using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Boekje.Domain.Rules;
using Boekje.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Web.DependencyInjection;

public static class RepositoryInjection
{
    public static void AddRepositories(
        this IServiceCollection services)
    {
        /*
         * Repositories
         */

        services.AddScoped<
            IUserRepository,
            UserRepository>();

        services.AddScoped<
            IBudgetRepository,
            BudgetRepository>();

        services.AddScoped<
            IExpenseRepository,
            ExpenseRepository>();

        services.AddScoped<
            ISavingRepository,
            SavingRepository>();

        services.AddScoped<
            ICategoryRepository,
            CategoryRepository>();

        services.AddScoped<
            ITransactionRepository,
            TransactionRepository>();

        /*
         * Budget Rules
         */

        services.AddScoped<
            IBudgetRule,
            RentBudgetRule>();

        services.AddScoped<
            IBudgetRule,
            SavingsBudgetRule>();

        /*
         * Services
         */

        services.AddScoped<
            BudgetQueryService>();

        services.AddScoped<
            BudgetCommandService>();

        services.AddScoped<
            BudgetRuleEngine>();

        services.AddScoped<
            BudgetService>();

        services.AddScoped<
            AuthService>();

        services.AddScoped<
            TransactionService>();

        /*
         * Password hashing
         */

        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();
    }
}