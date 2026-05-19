using Boekje.Domain.Entities;
using Boekje.Domain.Interfaces;
using Boekje.Domain.Rules;
using Boekje.Domain.Services;
using Boekje.Web.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Boekje.Web;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        /* ========================================
           HTTP CONTEXT
        ======================================== */

        services.AddHttpContextAccessor();

        /* ========================================
           DEPENDENCY INJECTION
        ======================================== */

        services.AddRepositories();

        services.AddScoped<
            IPasswordHasher<User>,
            PasswordHasher<User>>();

        services.AddScoped<
            IBudgetRule,
            RentBudgetRule>();

        services.AddScoped<
            IBudgetRule,
            SavingsBudgetRule>();

        services.AddApplicationServices();

        /* ========================================
           SESSION
        ======================================== */

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromDays(7);

            options.Cookie.HttpOnly = true;

            options.Cookie.IsEssential = true;
        });

        /* ========================================
           MVC
        ======================================== */

        services.AddControllersWithViews();
    }

    public void Configure(
        WebApplication app,
        IWebHostEnvironment env)
    {
        /* ========================================
           ERROR HANDLING
        ======================================== */

        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        /* ========================================
           MIDDLEWARE
        ======================================== */

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthorization();

        /* ========================================
           ROUTING
        ======================================== */

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}