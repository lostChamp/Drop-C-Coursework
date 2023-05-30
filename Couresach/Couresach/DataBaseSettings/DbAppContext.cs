using Microsoft.EntityFrameworkCore;

namespace Couresach;

public class DbAppContext : DbContext
{
    public DbSet<User>? User { get; set; }
    public DbSet<Role>? Role { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Username=user;Password=user;Database=coursach"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasOne(p => p.RolesEntity)
            .WithMany(p => p.UsersEntity);
    }
}