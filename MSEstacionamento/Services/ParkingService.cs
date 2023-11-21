using Microsoft.Extensions.Configuration;
using MSEstacionamento.Models;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace MSLazer.Services
{
    public class ParkingService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public ParkingService(IHttpClientFactory httpFactory,IConfiguration configuration) 
        {
            _httpFactory = httpFactory;
            _configuration = configuration;
        }
        public async Task<ParkingResponse> GetParkingDirections(SearchParameters SearchParameters)
        {
            var httpClient = _httpFactory.CreateClient();
            var builder = new UriBuilder(_configuration.GetSection("GOOGLE_MAPS_URL").Value);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["key"] = _configuration.GetSection("GOOGLE_MAPS_API_KEY").Value;
            //query["language"] = "pt-PT";
            builder.Query = query.ToString();
            string url = builder.ToString();
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("X-Goog-FieldMask", "places.displayName,places.business_status,places.rating,places.priceLevel");
            request.RequestUri = new Uri(url);
            request.Content = new StringContent(JsonConvert.SerializeObject(SearchParameters), Encoding.UTF8, "application/json");

            try
            {

                var responseUrl = await httpClient.SendAsync(request);
                var content = await responseUrl.Content.ReadAsStringAsync();

                //_logger.LogInformation($"Resposta do servidor: {content}");

                var response = JsonConvert.DeserializeObject<ParkingResponse>(content);
                return response;

            }
            catch (Exception ex)
            {
                //_logger.LogError($"Erro do servidor: {ex.Message}");
                return null;
            }

            return null;

        }

    }
}
