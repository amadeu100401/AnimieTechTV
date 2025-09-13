using AnimieTechTv.Communication.Response.Animie;
using MediatR;
using System.Text.Json.Serialization;

namespace AnimieTechTv.Application.Commad.Animie.Update;

public class UpdateAnimieInfoCommand : IRequest<AnimieResponseJson>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Director { get; set; }
    public string? Resume { get; set; }
}
