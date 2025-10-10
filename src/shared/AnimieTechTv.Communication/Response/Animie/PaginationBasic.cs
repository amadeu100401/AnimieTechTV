namespace AnimieTechTv.Communication.Response.Animie;

public class PaginationBasic
{
    public int CurrentPage { get; set; }
    public int LastVisablePage { get; set; }
    public bool HasNextPage { get; set; }
    public int Count { get; set; }
}
