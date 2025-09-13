using AnimieTechTv.Application.Commad.Animie;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimieTechTv.Application.Handlers.Animie.CreateAnimie;

public class CreateAnimieHandler : IRequestHandler<CreateAnimieCommand, CreateAnimieResponseJson>
{
    private readonly IAnimieReadOnlyRepository _animieReadOnlyRepository;
    private readonly IAnimieWriteOnlyRepository _animieWriteOnlyRepository;
    private readonly IUnityWork _unityWork;
    private readonly ILogger<CreateAnimieHandler> _logger;

    public CreateAnimieHandler(IAnimieReadOnlyRepository animieReadOnlyRepository, IAnimieWriteOnlyRepository animieWriteOnlyRepository, IUnityWork unityWork, ILogger<CreateAnimieHandler> logger)
    {
        _animieReadOnlyRepository = animieReadOnlyRepository;
        _animieWriteOnlyRepository = animieWriteOnlyRepository;
        _unityWork = unityWork;
        _logger = logger;
    }

    public async Task<CreateAnimieResponseJson> Handle(CreateAnimieCommand request, CancellationToken cancellationToken)
    {
        await Validate(request);
        var animie = ToEntity(request);

        await _animieWriteOnlyRepository.Add(animie);

        await _unityWork.Commit();

        return new CreateAnimieResponseJson
        {
            AnimieIdentification = animie.Id,
            Director = animie.Director,
            Name = animie.Name,
            Resume = animie.Resume
        };
    }

    private async Task Validate(CreateAnimieCommand request)
    {

        var animieAlreadyRegistered = await _animieReadOnlyRepository.ExistisAnimieWithNameAndDirector(request.Name, request.Director);

        if (animieAlreadyRegistered)
        {
            string message = ResourceMessageExceptions.ANIMIE_ALREADY_ADDED;

            _logger.LogError(message);
            throw new ErrorOnValidation(message);
        }
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
