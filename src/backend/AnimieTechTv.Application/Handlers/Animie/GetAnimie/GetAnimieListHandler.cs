using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.Communication.Http;
using AnimieTechTv.Domain.DTOs.AnimieApi;
using MediatR;

namespace AnimieTechTv.Application.Handlers.Animie.GetAnimie;

public class GetAnimieListHandler : IRequestHandler<GetAnimieListCommand, GetAnimieListResponseJson>
{
    private readonly IJikanAPI jikanAPI;

    public GetAnimieListHandler(IJikanAPI jikanAPI)
    {
        this.jikanAPI = jikanAPI;
    }

    public async Task<GetAnimieListResponseJson> Handle(GetAnimieListCommand request, CancellationToken cancellationToken)
    {
        var pagination = new Dictionary<string, int>
        {
            { "page", request.Page },
            { "limit", request.Limit }
        };

        var response = await jikanAPI.GetAllAnimiePaginaded(pagination);

        if (response == null || response.Data == null || response.Data.Count == 0)
        {
            return new GetAnimieListResponseJson
            {
                Data = []
            };
        }

        return new GetAnimieListResponseJson
        {
            Data = [.. response.Data.Select(a => new AnimieBasicInfo
            {
                Id = a.MalId.GetValueOrDefault(),
                Title = a.Title,
                TitleJapanese = a.TitleJapanese,
                ImageUrl = GetImageUrl(a.Images),
                Synopsis = a.Synopsis,
                Type = a.Type,
                Status = a.Status,
                Episodes = a.Episodes.GetValueOrDefault(),
                Genres = GetGenersFromHttpResponse(a.Genres),
                TrailerUrl = GetTraulerUrl(a.Trailer),
                Score = a.Score,
                Year = a.Year,
                Url = a.Url
            })],
            Pagination = response.Pagination == null ? null : new PaginationBasic
            {
                LastVisablePage = response.Pagination.LastVisiblePage,
                HasNextPage = response.Pagination.HasNextPage,
                Count = response.Pagination.Items.Count,
            }
        };
    }

    private string GetImageUrl(ImagesDTO? httpResponse)
    {
        if (httpResponse == null)
            return string.Empty;

        return httpResponse.Webp.ImageUrl ?? httpResponse.Jpg.ImageUrl;
    }

    private List<GenresResponseJson> GetGenersFromHttpResponse(List<GenreDTO>? httpResponse)
    {
        if (httpResponse == null || httpResponse.Count == 0)
            return [];

        return [.. httpResponse.Select(g => new GenresResponseJson
        {
            MalId = g.MalId,
            Name = g.Name,
            Type = g.Type,
            Url = g.Url
        })];
    }

    private string GetTraulerUrl(TrailerDTO? trailerResponse)
    {
        if (trailerResponse == null)
            return string.Empty;

        return trailerResponse.Url ?? string.Empty;
    }
}
