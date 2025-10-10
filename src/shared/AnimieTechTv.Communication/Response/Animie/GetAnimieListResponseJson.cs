namespace AnimieTechTv.Communication.Response.Animie;

public class GetAnimieListResponseJson
{
    public required List<AnimieBasicInfo> Data { get; set; } = [];
    public PaginationBasic? Pagination { get; set; } = null;
}
