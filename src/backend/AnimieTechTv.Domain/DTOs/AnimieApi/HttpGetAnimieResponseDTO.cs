using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class HttpGetAnimieResponseDTO
{
    [JsonPropertyName("data")]
    public List<AnimeDTO> Data { get; set; } = [];

    [JsonPropertyName("pagination")]
    public PaginationAnimieDTO Pagination { get; set; }
}
