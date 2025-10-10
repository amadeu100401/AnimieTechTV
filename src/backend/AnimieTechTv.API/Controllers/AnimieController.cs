using AnimieTechTv.Application.Commad.Animie.Create;
using AnimieTechTv.Application.Commad.Animie.Delete;
using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Application.Commad.Animie.Update;
using AnimieTechTv.Communication.Response.Animie;
using AnimieTechTv.Domain.DTOs;
using AnimieTechTv.Domain.DTOs.LocalAnimie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimieTechTv.API.Controllers;

public class AnimieController : BaseController
{
    private readonly IMediator _mediator;

    public AnimieController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AnimieResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAnimie([FromBody] CreateAnimieCommand request)
    {
        var response = await _mediator.Send(request); 

        return Created(string.Empty , response);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(GetAnimieListResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnimieList()
    {
        var response = await _mediator.Send(new GetAnimieListCommand());
        return Ok(response);
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

    [HttpGet("filter")]
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAnimie(
        Guid id, 
        [FromBody] UpdateAnimieInfoCommand request)
    {
        request.Id = id;

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAnimie([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("ID inválido");

        var command = new DeleteAnimieCommand(id);

        await _mediator.Send(command);

        return Ok();
    }
}
