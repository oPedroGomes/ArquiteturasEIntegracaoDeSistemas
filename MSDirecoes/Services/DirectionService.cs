using Microsoft.Extensions.Configuration;
using MSDirecoes.Models;
using Newtonsoft.Json;
using System.Web;

namespace MSDirecoes.Services
{
    public class DirectionService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public DirectionService(IHttpClientFactory httpFactory,IConfiguration configuration)
        {   
            _httpFactory = httpFactory;
            _configuration = configuration;
        }

        public async  Task<GoogleMapDirectionModel> GetDirectionsByCoordinates(Coordinates coordinates)
        {
            var httpClient = _httpFactory.CreateClient();
            var builder = new UriBuilder(_configuration.GetSection("GOOGLE_MAPS_URL").Value);
            GoogleMapDirectionModel directionModel;

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["origin"] = $"{coordinates.From.Latitute},{coordinates.From.Longitude}";
            query["destination"] = $"{coordinates.To.Latitute} , {coordinates.To.Longitude}";
            query["key"] = _configuration.GetSection("GOOGLE_MAPS_API_KEY").Value;
            query["language"] = "pt-PT";
            builder.Query = query.ToString();
            string url = builder.ToString();

            try
            {

                var responseMessage = await httpClient.GetAsync(url);
                var content = await responseMessage.Content.ReadAsStringAsync();
                //_logger.LogInformation($"Resposta do servidor: {content}");

                directionModel = JsonConvert.DeserializeObject<GoogleMapDirectionModel>(content);

            }
            catch (Exception ex)
            {
                //_logger.LogError($"Erro do servidor: {ex.Message}");
                return null;
            }

            return directionModel;

        }
    }
}
