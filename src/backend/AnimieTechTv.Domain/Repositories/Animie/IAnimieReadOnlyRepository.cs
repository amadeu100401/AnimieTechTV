using AnimieTechTv.Domain.DTOs.LocalAnimie;
using AnimieTechTv.Domain.Entities;

namespace AnimieTechTv.Domain.Repositories.Animie;

public interface IAnimieReadOnlyRepository
{
    Task<PaginationResultDTO<Animies>> GetAllAnimies(PaginationDTO pagination);

    Task<Animies?> GetByIdAsync(Guid Id);

    Task<List<Animies>> GetAnimieByFilter(GetAnimieFilterDTO filter);

    Task<bool> ExistisAnimieWithNameAndDirector(string name, string director);
}
