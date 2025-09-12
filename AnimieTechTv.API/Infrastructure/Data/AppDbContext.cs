using AnimieTechTv.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.API.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Animie> Animies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animie>().ToTable("Customers");
        base.OnModelCreating(modelBuilder);
    }
}
