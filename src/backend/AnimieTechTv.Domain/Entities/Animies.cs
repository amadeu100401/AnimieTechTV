namespace AnimieTechTv.Domain.Entities;

public class Animies : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Resume { get; set; } = string.Empty;
}
