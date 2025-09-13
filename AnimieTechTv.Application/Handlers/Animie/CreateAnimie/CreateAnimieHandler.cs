using AnimieTechTv.Application.Commad.Animie;
using AnimieTechTv.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AnimieTechTv.Application.Handlers.Animie.CreateAnimie;

public class CreateAnimieHandler : IRequestHandler<CreateAnimieCommand, Guid>
{
    public async Task<Guid> Handle(CreateAnimieCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);
        var animie = ToEntity(request);

        return Guid.NewGuid();
    }

    private async Task Validate(CreateAnimieCommand request)
    {

    }

    private Animies ToEntity(CreateAnimieCommand request)
    {
        return new Animies
        {
            Name = request.Name,
            Director = request.Director,
            Resume = request.Resume
        };
    }
}
