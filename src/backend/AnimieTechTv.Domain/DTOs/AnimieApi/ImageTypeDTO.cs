using System.Text.Json.Serialization;

namespace AnimieTechTv.Domain.DTOs.AnimieApi;

public class ImageTypeDTO
{
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
    public string SmallImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
}
