using MediatR;

namespace AnimieTechTv.Application.Commad.Animie.Delete;

public class DeleteAnimieCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteAnimieCommand (Guid id) => Id = id;
}
