using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.Infrastructure.DataAccess;

public class AnimieTechTvDbContext : DbContext
{
    public AnimieTechTvDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Animies> Animies { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Animies>(entity =>
        {
            entity.ToTable("animies", SchemaNames.CATALOG_SCHEMA);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Director).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Resume).HasMaxLength(1000);
        });
    }
}
