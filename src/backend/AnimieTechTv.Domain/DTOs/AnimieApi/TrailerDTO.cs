using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class TrailerDTO
{
    [JsonPropertyName("youtube_id")]
    public string? YoutubeId { get; set; }
    public string? Url { get; set; }

    [JsonPropertyName("embed_url")]
    public string? EmbedUrl { get; set; }

    [JsonPropertyName("images")]
    public TrailerImagesDTO? Images { get; set; }
}
