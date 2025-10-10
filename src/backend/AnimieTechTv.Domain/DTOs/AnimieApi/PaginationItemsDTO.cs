using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class PaginationItemsDTO
{
    public int Count { get; set; }
    public int Total { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
}
