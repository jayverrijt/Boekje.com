using Boekje.Web;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["ConnectionStrings:DefaultConnection"] =
    $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
    "Pooling=false;";

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();