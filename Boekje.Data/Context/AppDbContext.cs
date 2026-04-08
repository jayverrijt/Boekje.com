using Boekje.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boekje.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}