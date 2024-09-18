using API_Farm.Models;
using API_Farm.Seeders;
using Microsoft.EntityFrameworkCore;

namespace API_Farm.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AnimalType> AnimalTypes { get; set; }
    public DbSet<Animal> Animals { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        AnimalTypeSeeder.Seed(modelBuilder);
        AnimalSeeder.Seed(modelBuilder,500);
    }
}
