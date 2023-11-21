using Microsoft.AspNetCore.Mvc;

namespace MSTempo.Controllers
{
    [Route("/api")]
    [ApiController]
 
    public class WeatherForecastController : ControllerBase
    {
        private const int FORECAST_DAYS = 1;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecast _weatherForecast;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecast weatherForecast)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
        }

        [HttpGet("/weather")]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       public async Task<IActionResult> Weather(string latitude,string longitude)
       {
            _logger.LogInformation("Recebeu pedido do tempo");
            var tempo = await _weatherForecast.GetTempo(latitude, longitude, FORECAST_DAYS);
            if (tempo == null)
                return StatusCode(StatusCodes.Status500InternalServerError);            

            return Ok(tempo);
       }

        [HttpGet("/teste")]
        public IActionResult Teste()
        {
            return Ok("Teste");
        }
    }
}