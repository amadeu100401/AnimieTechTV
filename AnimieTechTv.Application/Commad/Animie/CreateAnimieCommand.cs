using MediatR;

namespace AnimieTechTv.Application.Commad.Animie;

public class CreateAnimieCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Resume { get; set; } = string.Empty;
}
