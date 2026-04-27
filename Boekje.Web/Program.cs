using Boekje.Data.Repositories;
using Boekje.Domain.Interfaces;
using Boekje.Domain.Services;
using Boekje.Web.Infrastructure;
using Boekje.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Connection string ophalen
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Repository
builder.Services.AddScoped<IUserRepository>(provider =>
    new UserRepository(connectionString));

// Services
builder.Services.AddScoped<AuthService>();

// HttpContext (voor CurrentUserService)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<ITransactionRepository>(sp =>
    new TransactionRepository(connectionString));

builder.Services.AddScoped<TransactionService>();

// Sessions (login systeem)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Scopes & Services
builder.Services.AddScoped<DashboardService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Error handling (production)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS
app.UseHttpsRedirection();

// Static files (css/js)
app.UseStaticFiles();

app.UseRouting();

// Sessions (MOET vóór endpoints)
app.UseSession();

// Routing
app.MapDefaultControllerRoute();

app.Run();