using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.DTOs;
using MediatR;

namespace AnimieTechTv.Application.Commad.Animie.Get;

public class GetAnimieCommand : IRequest<GetAnimieResponseJson>
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Director { get; set; }

    public PaginationDTO Pagination { get; set; }

    public GetAnimieCommand(PaginationDTO pagination) => Pagination = pagination;

    public GetAnimieCommand(Guid? id, string? name, string? director) 
    {
        Id = id;
        Name = name;
        Director = director;
    }
}
