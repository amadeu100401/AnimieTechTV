namespace AnimieTechTv.Domain.Repositories.Animie;

public interface IAnimieDeleteRepository
{
    Task<bool> DeleteAnimieByIdAsync(Guid id);
}
