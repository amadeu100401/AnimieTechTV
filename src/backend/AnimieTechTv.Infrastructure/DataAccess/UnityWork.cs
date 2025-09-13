using AnimieTechTv.Domain.Repositories;

namespace AnimieTechTv.Infrastructure.DataAccess;

public class UnityWork : IUnityWork
{
    private readonly AnimieTechTvDbContext _dbContext;

    public UnityWork(AnimieTechTvDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
