using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class AiredDTO
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public PropDTO Prop { get; set; }

    [JsonPropertyName("string")]
    public string? String { get; set; }
}
