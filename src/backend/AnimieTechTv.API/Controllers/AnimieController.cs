using AnimieTechTv.Application.Commad.Animie.Create;
using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimieTechTv.API.Controllers;

public class AnimieController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimieController> _logger;

    public AnimieController(ILogger<AnimieController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnimieResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAnimie([FromBody] CreateAnimieCommand request)
    {
        var response = await _mediator.Send(request); 

        return Created(string.Empty , response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAnimieResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAnimies(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var pagination = new PaginationDTO
        {
            PageNumber = page,
            PageSize = pageSize
        };

        var response = await _mediator.Send(new GetAnimieCommand(pagination));

        return Ok(response);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(GetAnimieResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnimieByFilter(
        [FromQuery] string? id,
        [FromQuery] string? name,
        [FromQuery] string? director)
    {
        Guid? guidId = null;

        if (!string.IsNullOrWhiteSpace(id) && Guid.TryParse(id, out var parsedId))
        {
            guidId = parsedId;
        }

        var response = await _mediator.Send(new GetAnimieCommand(guidId, name, director));

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateAnimie(Guid id, [FromBody] CreateAnimieCommand request)
    {
        return Ok(request);
    }

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteAnimie([FromQuery] string? name, [FromQuery] string? id)
    {
        return Ok();
    }
}
