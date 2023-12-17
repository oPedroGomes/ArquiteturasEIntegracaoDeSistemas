using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace BusinessProject.BL
{
    public class BusinessClass
    {
        private readonly DirecoesAPI _direcoesAPI;
        private readonly EstacionamentoAPI _estacionamentoAPI;
        private readonly LazerAPI _lazerAPI;
        private readonly TempoAPI _tempoAPI;
        private readonly JogosAPI _jogosAPI;

        public BusinessClass(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            _direcoesAPI = new DirecoesAPI("http://localhost:44372/", client);
            _estacionamentoAPI = new EstacionamentoAPI("http://localhost:44305/", client);
            _lazerAPI = new LazerAPI("http://localhost:44377/", client);
             _tempoAPI = new TempoAPI("http://localhost:44314/", client);
            _jogosAPI = new JogosAPI("http://localhost:44331/", client);



        }//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");

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

        public async Task<List<ResponseJogo>> GetJogos(string equipa,DateTime? dataFim)
        {
            var jogosResponse = await _jogosAPI.JogosAsync(equipa, dataFim);
            return jogosResponse.ToList();
        }

        public async Task<ResponseJogo> GetJogoById (int? idJogo,bool saveLog)
        {
            var jogosResponse = await _jogosAPI.JogoAsync(idJogo,saveLog);
            return jogosResponse;
        }

        public async Task<ResponseEquipa> GetEquipaById(int idEquipa)
        {
            var equipaResponse = await _jogosAPI.EquipaAsync(idEquipa);
            return equipaResponse;
        }

        public async Task<List<ResponseTopScorers>> GetTopScorers()
        {
            var jogosResponse = await _jogosAPI.TopscorersAllAsync();
            return jogosResponse.ToList();
        }

        public async Task<List<LogEquipas>> GetLogEquipas()
        {
            var equipasResponse = await _jogosAPI.GetLogsEquipasAsync();
            return equipasResponse.ToList();
        }
        public async Task<List<LogJogos>> GetLogJogos()
        {
            var jogosResponse = await _jogosAPI.GetLogsJogosAsync();
            return jogosResponse.ToList();
        }
    }
}
