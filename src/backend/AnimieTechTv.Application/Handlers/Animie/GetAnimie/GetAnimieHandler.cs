using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Communication.Response;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.DTOs;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnimieTechTv.Application.Handlers.Animie.GetAnimie;

public class GetAnimieHandler : IRequestHandler<GetAnimieCommand, GetAnimieResponseJson>
{
    private readonly ILogger<GetAnimieHandler> _logger;
    private readonly IAnimieReadOnlyRepository _animieReadOnlyRepository;

    public GetAnimieHandler(ILogger<GetAnimieHandler> logger, IAnimieReadOnlyRepository animieReadOnlyRepository)
    {
        _logger = logger;
        _animieReadOnlyRepository = animieReadOnlyRepository;
    }

    public async Task<GetAnimieResponseJson> Handle(GetAnimieCommand request, CancellationToken cancellationToken)
    {
        var isToGetAll = string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Director);

        var response = isToGetAll ? await GetAllAnimies(request.Pagination) : await GetAnimieByFilter(request);

        return response;
    }

    private async Task<GetAnimieResponseJson> GetAllAnimies(PaginationDTO pagination)
    {
        var paginationResult = await _animieReadOnlyRepository.GetAllAnimies(pagination);

        if (paginationResult is null)
        {
            _logger.LogWarning("No animies found in the repository.");
            return new GetAnimieResponseJson();
        }

        var animies = paginationResult.Items;

        var response = animies.Select(a => new AnimieResponseJson
        {
            AnimieIdentification = a.Id,
            Name = a.Name,
            Director = a.Director,
            Resume = a.Resume
        }).ToList();

        return new GetAnimieResponseJson
        {
            Pagination = new PaginationResponseJson
            {
                TotalItem = paginationResult.TotalItem,
                PageNumber = paginationResult.PageNumber,
                PageSize = paginationResult.PageSize
            },
            Items = response
        };
    }

    private async Task<GetAnimieResponseJson> GetAnimieByFilter(GetAnimieCommand request)
    {
        var filters = new GetAnimieFilterDTO(request.Id, request.Name, request.Director);

        var response = await _animieReadOnlyRepository.GetAnimieByFilter(filters);

        if (response is null)
        {
            _logger.LogWarning(ResourceMessageExceptions.ANIMIE_NOT_FOUNDED_BY_FILTER);
            return new GetAnimieResponseJson();
        }

        return new GetAnimieResponseJson
        {
            Items = [.. response.Select(a => new AnimieResponseJson
            {
                AnimieIdentification = a.Id,
                Name = a.Name,
                Director = a.Director,
                Resume = a.Resume
            })]
        };
    }
}
