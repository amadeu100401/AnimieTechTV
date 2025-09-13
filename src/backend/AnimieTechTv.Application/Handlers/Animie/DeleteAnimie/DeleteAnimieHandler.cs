using AnimieTechTv.Application.Commad.Animie.Delete;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using MediatR;

namespace AnimieTechTv.Application.Handlers.Animie.DeleteAnimie;

public class DeleteAnimieHandler : IRequestHandler<DeleteAnimieCommand, bool>
{
    private readonly IAnimieDeleteRepository _animieDeleteRepository;

    public DeleteAnimieHandler(IAnimieDeleteRepository animieDeleteRepository) => _animieDeleteRepository = animieDeleteRepository;

    public async Task<bool> Handle(DeleteAnimieCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await _animieDeleteRepository.DeleteAnimieByIdAsync(request.Id);

        if (!isDeleted) throw new AnimieTechTVException(ResourceMessageExceptions.ANIMIE_ERROR_ON_DELETED);

        return isDeleted;
    }
}
