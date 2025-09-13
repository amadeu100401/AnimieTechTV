using AnimieTechTv.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.Infrastructure.DataAccess;

public class AnimieTechTvDbContext : DbContext
{
    public AnimieTechTvDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Animies> Animies { get; set; }    
}
