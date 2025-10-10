namespace AnimieTechTv.Communication.Response.Animie;

public class AnimieBasicInfo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? TitleJapanese { get; set; }
    public string? ImageUrl { get; set; }
    public string? Type { get; set; }
    public int? Episodes { get; set; }
    public string? Status { get; set; }
    public double? Score { get; set; }
    public int? Year { get; set; }
    public List<GenresResponseJson>? Genres { get; set; }
    public string? Synopsis { get; set; }
    public string? TrailerUrl { get; set; }
    public string? Url { get; set; }
}
