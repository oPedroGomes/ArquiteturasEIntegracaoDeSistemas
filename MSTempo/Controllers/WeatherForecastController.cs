using Microsoft.AspNetCore.Mvc;

namespace MSTempo.Controllers
{
    [Route("/api")]
    [ApiController]
 
    public class WeatherForecastController : ControllerBase
    {      

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecast _weatherForecast;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecast weatherForecast)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
        }

        [HttpGet]
        [Route("/weather")]
       public async Task<IActionResult> Weather()
       {
            _logger.LogInformation("Recebeu pedido do tempo");
            var tempo = await _weatherForecast.GetTempo(50.ToString(), 13.ToString(), 1);
            if (tempo == null)
                return StatusCode(StatusCodes.Status500InternalServerError);            

            return Ok(tempo);
       }
    }
}