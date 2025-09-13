using AnimieTechTv.Domain.Entities;

namespace AnimieTechTv.Domain.Repositories.Animie;

public interface IAnimieWriteOnlyRepository
{
    Task Add(Animies animie);
}
