using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories.Animie;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.Infrastructure.DataAccess.Repositories;

public class AnimieRepository : IAnimieReadOnlyRepository
{
    private readonly AnimieTechTvDbContext _context;

    public AnimieRepository(AnimieTechTvDbContext context) => _context = context;

    public async Task<Animies> GetAllAnimies() => await _context.Animies.FirstAsync();
}
