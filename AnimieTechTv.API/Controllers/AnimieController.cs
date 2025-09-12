using AnimieTechTv.API.Models.Requests;
using AnimieTechTv.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AnimieTechTv.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimieController : ControllerBase
    {
        private readonly ILogger<AnimieController> _logger;

        public AnimieController(ILogger<AnimieController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedAnimieResponse), StatusCodes.Status201Created)]
        public IActionResult CreateAnimie([FromBody] CreateAnimieRequest request)
        {
            var response = new CreatedAnimieResponse
            {
                Name = request.Name,
                Director = request.Director,
                Description = request.Resume
            };

            return CreatedAtAction(nameof(GetAnimieByFilter), new { name = response.Name }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateAnimie(Guid id, [FromBody] CreateAnimieRequest request)
        {
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CreatedAnimieResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllAnimies()
        {
            return Ok(new List<CreatedAnimieResponse>());
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(CreatedAnimieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAnimieByFilter([FromQuery] string? name, [FromQuery] string? director)
        {
            return Ok(new CreatedAnimieResponse
            {
                Name = name ?? "DefaultName",
                Director = director ?? "DefaultDirector",
                Description = "Example animie"
            });
        }
    }
}
