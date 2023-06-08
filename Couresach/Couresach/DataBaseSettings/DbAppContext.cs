using Microsoft.EntityFrameworkCore;

namespace Couresach;

public class DbAppContext : DbContext
{
    public DbSet<User>? User { get; set; }
    public DbSet<Role>? Role { get; set; }
    
    public DbSet<Order>? Order { get; set; }
    
    public DbSet<Ware>? Ware { get; set; }
    
    public DbSet<Service>? Service { get; set; }
    
    public DbSet<Category>? Category { get; set; }
    public DbSet<Manufacturer>? Manufacturer { get; set; }

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
        modelBuilder.Entity<Order>().HasOne(p => p.UsersEntity)
            .WithMany(p => p.OrderEntity);
        modelBuilder.Entity<Order>().HasOne(p => p.WareEntity)
            .WithMany(p => p.OrderEntity);
        modelBuilder.Entity<Order>().HasOne(p => p.ServiceEntity)
            .WithMany(p => p.OrderEntity);
        modelBuilder.Entity<Ware>().HasOne(p => p.CategoryEntity)
            .WithMany(p => p.WareEntity);
        modelBuilder.Entity<Ware>().HasOne(p => p.ManufacturerEntity)
            .WithMany(p => p.WareEntity);
        modelBuilder.Entity<Ware>().Property(p => p.Image).IsRequired(true)
            .HasDefaultValueSql("C:\\Users\\Константин\\Desktop\\Coursework\\Couresach\\Couresach\\Images\\no_img.jpg");
    }
}