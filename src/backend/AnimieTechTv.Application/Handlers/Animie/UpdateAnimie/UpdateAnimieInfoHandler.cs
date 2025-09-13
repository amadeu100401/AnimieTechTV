using AnimieTechTv.Application.Commad.Animie.Update;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using MediatR;

namespace AnimieTechTv.Application.Handlers.Animie.UpdateAnimie;

public class UpdateAnimieInfoHandler : IRequestHandler<UpdateAnimieInfoCommand, AnimieResponseJson>
{

    private readonly IAnimieReadOnlyRepository _animieRepository;
    private readonly IUnityWork _unityWork;

    public UpdateAnimieInfoHandler(IAnimieReadOnlyRepository animieRepository, IUnityWork unityWork)
    {
        _animieRepository = animieRepository;
        _unityWork = unityWork;
    }

    public async Task<AnimieResponseJson> Handle(UpdateAnimieInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await GetAnimieEntity(request.Id);

        entity.Name = request.Name ?? entity.Name;
        entity.Director = request.Director ?? entity.Director;
        entity.Resume = request.Resume ?? entity.Resume;

        entity.UpdateTimestamp();

        await _unityWork.Commit();

        return new AnimieResponseJson
        {
            AnimieIdentification = entity.Id,
            Name = entity.Name,
            Director = entity.Director,
            Resume = entity.Resume
        };
    }

    private async Task<Animies> GetAnimieEntity(Guid id)
    {
        var entity = await _animieRepository.GetByIdAsync(id);

        return entity ?? throw new AnimieTechTVException(ResourceMessageExceptions.ANIMIE_NOT_FOUNDED_BY_ID);
    }
}
