using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSDirecoes.Models;
using MSDirecoes.Services;

namespace MSDirecoes.Controllers
{
    [Route("/api")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {
        private readonly DirectionService _directionService;

        public DirectionsController(DirectionService directionService)
        {
            _directionService = directionService;
            
            
        }

        [HttpPost]
        [Route("/directions")]
        [Authorize]
        [ProducesResponseType(typeof(GoogleMapDirectionModel), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetDirections([FromBody]Coordinates coordinates)
        {
            var directions = await _directionService.GetDirections(coordinates);

            if(directions == null)
                return BadRequest();

            return Ok(directions);
        }


    }
}
