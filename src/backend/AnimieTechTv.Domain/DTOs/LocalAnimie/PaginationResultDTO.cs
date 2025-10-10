using AnimieTechTv.Domain.Entities;

namespace AnimieTechTv.Domain.DTOs.LocalAnimie;

public class PaginationResultDTO<T>
{
    public int TotalItem { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IList<T> Items { get; set; } = new List<T>();
};
