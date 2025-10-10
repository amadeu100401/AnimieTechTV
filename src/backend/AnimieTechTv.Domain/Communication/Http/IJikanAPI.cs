using AnimieTechTv.Domain.DTOs.AnimieApi;

namespace AnimieTechTv.Domain.Communication.Http;

public interface IJikanAPI
{
    Task<HttpGetAnimieResponseDTO> GetAllAnimiePaginaded(IDictionary<string, int> pagination);
}
