using AnimieTechTv.Domain.DTOs;
using AnimieTechTv.Domain.Entities;

namespace AnimieTechTv.Domain.Repositories.Animie;

public interface IAnimieReadOnlyRepository
{
    Task<PaginationResultDTO<Animies>> GetAllAnimies(PaginationDTO pagination);

    Task<List<Animies>> GetAnimieByFilter(GetAnimieFilterDTO filter);

    Task<bool> ExistisAnimieWithNameAndDirector(string name, string director);
}
