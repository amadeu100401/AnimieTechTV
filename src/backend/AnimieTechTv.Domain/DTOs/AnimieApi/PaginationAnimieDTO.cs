using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class PaginationAnimieDTO
{
    [JsonPropertyName("last_visible_page")]
    public int LastVisiblePage { get; set; }

    [JsonPropertyName("has_next_page")]
    public bool HasNextPage { get; set; }

    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("items")]
    public PaginationItemsDTO Items { get; set; }
}
