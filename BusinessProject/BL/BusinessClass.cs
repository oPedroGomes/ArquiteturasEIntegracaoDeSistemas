using MSDirecoes.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using MSLazer.Services;

namespace BusinessProject.BL
{
    public class BusinessClass
    {
        private readonly DirecoesAPI _direcoesAPI;
        private readonly EstacionamentoAPI _estacionamentoAPI;
        private readonly LazerAPI _lazerAPI;
        private readonly TempoAPI _tempoAPI;
        public BusinessClass(IHttpClientFactory httpFactory)
        {
            _direcoesAPI = new DirecoesAPI("https://localhost:44372/", httpFactory.CreateClient());
            _estacionamentoAPI = new EstacionamentoAPI("https://localhost:44305/", httpFactory.CreateClient());
            _lazerAPI = new LazerAPI("https://localhost:44377/", httpFactory.CreateClient());
             _tempoAPI = new TempoAPI("https://localhost:44314/", httpFactory.CreateClient());
            //

        }

        public async Task<GoogleMapDirectionModel> GetDirections(Coordinates coordinates)
        {
            var directionModel = await _direcoesAPI.DirectionsAsync(coordinates);
            return directionModel;
        }

        public async Task<PlacesResponseLazer> GetPlaces(SearchParametersLazer SearchParameters)
        {
            var placesResponse = await _lazerAPI.GetPlacesLazerAsync(SearchParameters);
            return placesResponse;
        }
        public async Task<ParkingResponse> GetParkingDirections(SearchParameters SearchParameters)
        {
            var parkingResponse = await _estacionamentoAPI.GetPlacesParkAsync(SearchParameters);
            return parkingResponse;
        }
        public async Task<WeatherForecast> GetWeather(string latitude,string longitude)
        {
            var weatherResponse = await _tempoAPI.WeatherAsync(latitude,longitude);
            return weatherResponse;
        }
    }
}
