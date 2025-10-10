using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class GenreDTO
{
    [JsonPropertyName("mal_id")]
    public int MalId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}
