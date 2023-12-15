using MongoDB.Driver;
using MSJogos.Models;
using Newtonsoft.Json;
using System.Web;

namespace MSJogos.Servicos
{
    public class JogosService
    {
        private readonly IMongoCollection<ResponseLiga> _ligas;
        private readonly IMongoCollection<ResponseEstadio> _estadios;
        private readonly IMongoCollection<ResponseJogo> _jogos;
        private readonly IMongoCollection<ResponseEquipa> _equipas;
        private readonly IMongoCollection<ResponseTopScorers> _topScorers;
        private readonly IMongoCollection<LogEquipas> _logEquipas;
        private readonly IMongoCollection<LogJogos> _logJogos;

        private readonly IConfiguration _configuration;
        
        public JogosService()
        {            

        }

        public JogosService(IMongoClient mongoClient, IConfiguration configuration)
        {
            _ligas = mongoClient
                .GetDatabase(configuration.GetSection("AtlasSettings").GetSection("DatabaseName").Value)
                .GetCollection<ResponseLiga>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameLigas").Value);

            _estadios = mongoClient
               .GetDatabase(configuration.GetSection("AtlasSettings").GetSection("DatabaseName").Value)
               .GetCollection<ResponseEstadio>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameEstadios").Value);

            _jogos = mongoClient
           .GetDatabase(configuration.GetSection("AtlasSettings").GetSection("DatabaseName").Value)
           .GetCollection<ResponseJogo>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameJogos").Value);

            _equipas = mongoClient
         .GetDatabase(configuration.GetSection("AtlasSettings").GetSection("DatabaseName").Value)
         .GetCollection<ResponseEquipa>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameEquipas").Value);

            _topScorers = mongoClient
         .GetDatabase(configuration.GetSection("AtlasSettings").GetSection("DatabaseName").Value)
         .GetCollection<ResponseTopScorers>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameTopScorers").Value);


            _logEquipas = mongoClient.GetDatabase(configuration.GetSection("AtlasSettings").GetSection("LogDatabaseName").Value)
                .GetCollection<LogEquipas>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameLogEquipas").Value);


            _logJogos = mongoClient.GetDatabase(configuration.GetSection("AtlasSettings").GetSection("LogDatabaseName").Value)
             .GetCollection<LogJogos>(configuration.GetSection("AtlasSettings").GetSection("CollectionNameLogJogos").Value);


            _configuration = configuration;
        }


        public async Task AtualizaCoordenadasEquipas()
        {
            var update1 = Builders<ResponseEquipa>.Update
                        .Set(r => r.venue.Latitude,"")
                        .Set(r => r.venue.Longitude, "");

            await _equipas.UpdateManyAsync(Builders<ResponseEquipa>.Filter.Empty,update1);


            var filtro = (Builders<ResponseEquipa>.Filter.Eq(r => r.venue.Latitude, ""));                      

            var equipas = _equipas.Find(filtro).ToList();

            foreach(var equipa in equipas)
            {
                var httpClient = new HttpClient();
                var builder = new UriBuilder(_configuration.GetSection("GOOGLE_MAPS_URL").Value);

                string adress = string.Empty;

                if (!string.IsNullOrEmpty(equipa.venue.name))
                    adress += equipa.venue.name + ",";

                if (!string.IsNullOrEmpty(equipa.venue.address))
                    adress += equipa.venue.address + ",";

                if (!string.IsNullOrEmpty(equipa.venue.city))
                    adress += equipa.venue.city;

                var query = HttpUtility.ParseQueryString(builder.Query);
                query["address"] = adress;
                query["key"] = _configuration.GetSection("GOOGLE_MAPS_API_KEY").Value;
                builder.Query = query.ToString();
                string url = builder.ToString();

                try
                {

                    var responseMessage = await httpClient.GetAsync(url);
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    //_logger.LogInformation($"Resposta do servidor: {content}");

                  var  location = JsonConvert.DeserializeObject<Geolocation>(content);


                    if (location.results.Count > 0)
                    {
                        equipa.venue.Latitude = location.results[0].geometry.location.lat.ToString();
                        equipa.venue.Longitude = location.results[0].geometry.location.lng.ToString();

                        var update = Builders<ResponseEquipa>.Update
                            .Set(r => r.venue.Latitude, equipa.venue.Latitude)
                            .Set(r => r.venue.Longitude, equipa.venue.Longitude);

                        await _equipas.UpdateOneAsync(r => r.team.id == equipa.team.id, update);
                    }

                }
                catch (Exception ex)
                {
                    //_logger.LogError($"Erro do servidor: {ex.Message}");
                  
                }

            }
        }


        public async Task AtualizaLiga(Liga liga)
        {
            //Primeiro apaga
            await _ligas.DeleteManyAsync(Builders<ResponseLiga>.Filter.Eq(r=>r.country.name , "Portugal"));

            //Depois insere
            foreach(var item in liga.response)
            {
                await _ligas.InsertOneAsync(item);
            }
            

        }
        public async Task AtualizaEstadios(Estadio estadio)
        {
            //Primeiro apaga
            await _estadios.DeleteManyAsync(Builders<ResponseEstadio>.Filter.Eq(r => r.country, "Portugal"));

            //Depois insere
            foreach (var item in estadio.response)
            {
                await _estadios.InsertOneAsync(item);

            }

        }   
        public async Task AtualizaJogos(Jogo jogo)
        {
            //Primeiro apaga
            await _jogos.DeleteManyAsync(Builders<ResponseJogo>.Filter.Eq(r => r.league.id, jogo.response[0].league.id));

            //Depois insere
            foreach (var item in jogo.response)
            {
                await _jogos.InsertOneAsync(item);

            }

        }
        public async Task AtualizaEquipas(Equipa equipa)
        {
            //Primeiro apaga
            await _equipas.DeleteManyAsync(Builders<ResponseEquipa>.Filter.Eq(r => r.team.country, equipa.parameters.league));

            //Depois insere
            foreach (var item in equipa.response)
            {
                await _equipas.InsertOneAsync(item);

            }

        }

        public async Task AtualizaTopScorers(TopScorers topScorers)
        {
            //Primeiro apaga
            await _topScorers.DeleteManyAsync(Builders<ResponseTopScorers>.Filter.Empty);

            //Depois insere
            foreach (var item in topScorers.response)
            {
                await _topScorers.InsertOneAsync(item);
            }
        }
        public async Task<List<ResponseJogo>> GetJogos(string equipa,DateTime? dataFim)
        {
            var filtro = (Builders<ResponseJogo>.Filter.Eq(r => r.teams.home.name, equipa)
                       | Builders<ResponseJogo>.Filter.Eq(r => r.teams.away.name, equipa));

            if (string.IsNullOrEmpty(equipa))
            {
                filtro = Builders<ResponseJogo>.Filter.Empty;
            }
            else
            {
                filtro = (Builders<ResponseJogo>.Filter.Eq(r => r.teams.home.name, equipa)
                       | Builders<ResponseJogo>.Filter.Eq(r => r.teams.away.name, equipa));
            }


            if (dataFim.HasValue)
            {
                var criteriaFilter = Builders<ResponseJogo>.Filter.Gte(r => r.fixture.date, DateTime.Now) 
                & Builders<ResponseJogo>.Filter.Lte(r => r.fixture.date, dataFim);
                filtro = filtro & criteriaFilter;
            }
            else
            {
                var criteriaFilter = Builders<ResponseJogo>.Filter.Gte(r => r.fixture.date, DateTime.Now);
                filtro = filtro & criteriaFilter;
            }          

            var jogos = await _jogos.Find(filtro).SortBy(r=>r.fixture.date).ThenBy(r=>r.league.id).Limit(5).ToListAsync();
            jogos = jogos.OrderBy(r => r.fixture.date).ToList();

            return jogos;
        }

        public async Task<ResponseJogo> GetJogoById(int fixtureId)
        {
            var filtro = (Builders<ResponseJogo>.Filter.Eq(r => r.fixture.id,fixtureId));
            var jogo = await _jogos.Find(filtro).FirstOrDefaultAsync();
            return jogo;

        }

        public async Task<ResponseEquipa> GetEquipaById(int IdEquipa)
        {
            var filtro = (Builders<ResponseEquipa>.Filter.Eq(r => r.team.id, IdEquipa));
            var equipa = await _equipas.Find(filtro).FirstOrDefaultAsync();
            return equipa;
        }

        public async Task<List<ResponseEstadio>> GetEstadio(string IdEquipa)
        {
            var filtro = (Builders<ResponseEstadio>.Filter.Eq(r => r.id, Convert.ToInt32(IdEquipa)));
              
            var estadios = await _estadios.Find(filtro).ToListAsync();
            return estadios;
        }


        public async Task<List<ResponseTopScorers>> GetTopScorers()
        {
            var topScorers = await _topScorers.Find(_ => true).ToListAsync();
            return topScorers;
        }


        public async Task<List<ResponseLiga>> GetLigas()
        {
            var estadios = await _ligas.Find(_=>true).ToListAsync();
            return estadios;
        }


        //---------------------------------------------------

        public async Task SaveLogEquipas(LogEquipas logEquipas)
        {
            await _logEquipas.InsertOneAsync(logEquipas);
        }

        public async Task SaveLogJogos(LogJogos logJogos)
        {
            await _logJogos.InsertOneAsync(logJogos);
        }

        public async Task<List<LogEquipas>> GetLogEquipas()
        {
            var logs = await _logEquipas.Find(_ => true).ToListAsync();
            return logs;
        }

        public async Task<List<LogJogos>> GetLogJogos()
        {
            var logs = await _logJogos.Find(_ => true).ToListAsync();
            return logs;
        }


    }
}
