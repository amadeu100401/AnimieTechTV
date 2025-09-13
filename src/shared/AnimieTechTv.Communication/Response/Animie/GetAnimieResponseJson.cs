using System.Text.Json.Serialization;

namespace AnimieTechTv.Communication.Response.Animie;

public class GetAnimieResponseJson
{
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationResponseJson? Pagination { get; set; }

    public List<AnimieResponseJson> Items { get; set; } = [];
}
