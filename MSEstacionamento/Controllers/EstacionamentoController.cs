using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSEstacionamento.Models;
using MSLazer.Services;

namespace MSLazer.Controllers
{
    [ApiController]
    public class EstacionamentoController : ControllerBase
    {
        private readonly ParkingService _parkingService;

        public EstacionamentoController(ParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpPost("/GetPlacesPark")]
        [ProducesResponseType(typeof(ParkingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetPlacesPark(SearchParameters searchParameters)
        {
            try
            {
                var places = await _parkingService.GetParkingDirections(searchParameters);
                return Ok(places);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
