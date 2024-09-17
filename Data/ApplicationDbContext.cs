using API_Farm.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Farm.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<AnimalType> AnimalTypes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
