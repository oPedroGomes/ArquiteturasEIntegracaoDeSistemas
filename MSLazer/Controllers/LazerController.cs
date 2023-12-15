using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSLazer.Models;
using MSLazer.Services;

namespace MSLazer.Controllers
{
    [ApiController]
    public class LazerController : ControllerBase
    {
        private readonly PlacesService _placesService;

        public LazerController(PlacesService placesService)
        {
            _placesService = placesService;
        }

        [HttpPost("/GetPlacesLazer")]
        [ProducesResponseType(typeof(PlacesResponseLazer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetPlacesLazer(SearchParametersLazer searchParameters)
        {
            try
            {
                var places = await _placesService.GetDirectionsByCoordinates(searchParameters);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
