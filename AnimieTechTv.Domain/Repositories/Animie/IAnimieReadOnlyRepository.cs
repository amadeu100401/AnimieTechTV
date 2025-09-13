using AnimieTechTv.Domain.Entities;

namespace AnimieTechTv.Domain.Repositories.Animie;

public interface IAnimieReadOnlyRepository
{
    Task<Animies> GetAllAnimies();
}
