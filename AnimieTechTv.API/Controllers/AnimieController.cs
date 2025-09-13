using AnimieTechTv.Application.Commad.Animie;
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
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAnimie([FromBody] CreateAnimieCommand request)
    {
        var id = await _mediator.Send(request); 

        return Created(string.Empty ,  id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateAnimie(Guid id, [FromBody] CreateAnimieCommand request)
    {
        return Ok(request);
    }

    //[HttpGet]
    //[ProducesResponseType(typeof(List<CreatedAnimieResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public IActionResult GetAllAnimies()
    //{
    //    return Ok(new List<CreatedAnimieResponse>());
    //}

    //[HttpGet("search")]
    //[ProducesResponseType(typeof(CreatedAnimieResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public IActionResult GetAnimieByFilter([FromQuery] string? name, [FromQuery] string? director)
    //{
    //    return Ok(new CreatedAnimieResponse
    //    {
    //        Name = name ?? "DefaultName",
    //        Director = director ?? "DefaultDirector",
    //        Description = "Example animie"
    //    });
    //}

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteAnimie([FromQuery] string? name, [FromQuery] string? id)
    {
        return Ok();
    }
}
