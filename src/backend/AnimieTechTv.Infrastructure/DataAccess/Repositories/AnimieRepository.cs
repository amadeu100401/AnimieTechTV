using AnimieTechTv.Domain.DTOs;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories.Animie;
using Microsoft.EntityFrameworkCore;

namespace AnimieTechTv.Infrastructure.DataAccess.Repositories;

public class AnimieRepository : IAnimieReadOnlyRepository, IAnimieWriteOnlyRepository, IAnimieDeleteRepository
{
    private readonly AnimieTechTvDbContext _context;

    public AnimieRepository(AnimieTechTvDbContext context) => _context = context;

    public async Task Add(Animies animie) => await _context.AddAsync(animie);

    public async Task<bool> ExistisAnimieWithNameAndDirector(string name, string director) => await _context.Animies.AnyAsync(a => a.Name == name && a.Director == director);

    public async Task<PaginationResultDTO<Animies>> GetAllAnimies(PaginationDTO pagination)
    {
        var totalCount = await _context.Animies.CountAsync();

        var result = await _context.Animies
        .OrderBy(a => a.Name)
        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
        .Take(pagination.PageSize)
        .ToListAsync();

        return new PaginationResultDTO<Animies>{
            TotalItem = totalCount, 
            PageNumber = pagination.PageNumber, 
            PageSize = pagination.PageSize, 
            Items = result
        };
    }

    public async Task<List<Animies>> GetAnimieByFilter(GetAnimieFilterDTO filter)
    {
        var query = _context.Animies.AsQueryable();

        if (filter.Id.HasValue)
            query = query.Where(a => a.Id == filter.Id.Value);

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(a => a.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Director))
            query = query.Where(a => a.Director.Contains(filter.Director));

        return await query
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<Animies?> GetByIdAsync(Guid Id) => await _context.Animies.FirstOrDefaultAsync(a => a.Id == Id);

    public async Task<bool> DeleteAnimieByIdAsync(Guid id) => await _context.Animies.Where(a => a.Id == id).ExecuteDeleteAsync() > 0;
}
