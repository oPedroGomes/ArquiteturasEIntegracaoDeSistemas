using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MSTempo.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace MSTempo
{
    public class Hourly
    {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<int> weather_code { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string weather_code { get; set; }
    }

    public class WeatherForecast
    {
        private readonly IOptions<WeatherSettings> _weatherSettings;
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<WeatherForecast> _logger;

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }



        public WeatherForecast(IOptions<WeatherSettings> weatherSettings,
                               IHttpClientFactory httpFactory,
                               ILogger<WeatherForecast> logger)
        {
            _weatherSettings = weatherSettings;
            _httpFactory = httpFactory;
            _logger = logger;

        }



        public async Task<WeatherForecast> GetTempo(string latitude,string longitude,int forecastDays)
        {
            var httpClient = _httpFactory.CreateClient();
            var builder = new UriBuilder(_weatherSettings.Value.WeatherAPIUrl);
            WeatherForecast weather;

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["latitude"] = latitude.ToString();
            query["longitude"] = longitude.ToString();
            query["hourly"] = _weatherSettings.Value.DefaultFilters;
            query["forecast_days"] = forecastDays.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();
            try
            {

                var responseMessage = await httpClient.GetAsync(url);
                var content = await responseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation($"Resposta do servidor: {content}");

                 weather = JsonConvert.DeserializeObject<WeatherForecast>(content);
                
            }catch(Exception ex)
            {
                _logger.LogError($"Erro do servidor: {ex.Message}");
                return null;
            }

             return weather;


        }


    }

}