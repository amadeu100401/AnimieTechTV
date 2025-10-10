using AnimieTechTv.Communication.Response.Animie;
using MediatR;

namespace AnimieTechTv.Application.Commad.Animie.Get;

public class GetAnimieListCommand : IRequest<GetAnimieListResponseJson>
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;    
}
