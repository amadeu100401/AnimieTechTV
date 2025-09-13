using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories.Animie;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.Infrastructure.DataAccess.Repositories;

public class AnimieRepository : IAnimieReadOnlyRepository, IAnimieWriteOnlyRepository
{
    private readonly AnimieTechTvDbContext _context;

    public AnimieRepository(AnimieTechTvDbContext context) => _context = context;

    public async Task Add(Animies animie) => await _context.AddAsync(animie);

    public async Task<bool> ExistisAnimieWithNameAndDirector(string name, string director) => await _context.Animies.AnyAsync(a => a.Name == name && a.Director == director);

    public async Task<Animies> GetAllAnimies() => await _context.Animies.FirstAsync();
}
