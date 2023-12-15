using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSJogos.Models;
using MSJogos.Servicos;
using Newtonsoft.Json;

namespace MSJogos.Controllers
{
    public class AdminJogosController : Controller
    {
        private readonly JogosService _jogosService;

        public AdminJogosController(JogosService jogosService)
        {
            _jogosService = jogosService;
        }

        /// <summary>
        /// Causa a atualização de todas as ligas disponiveis, apenas disponivel por um admin
        /// </summary>
        /// <returns></returns> 
        [HttpPut("/updateligas")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]

        public async Task<IActionResult> UpdateLigas()
        {
            try
            {


                //Faz o pedido a API para atualizar as ligas
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/leagues?country=Portugal"),
                    Headers =
                     {
                        { "X-RapidAPI-Key", "bded329172msh9ee31d1971b7f50p185a92jsn0a1420afe7e7" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                     },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    //Com o resultado do pedido, atualiza a base de dados
                    _jogosService.AtualizaLiga(JsonConvert.DeserializeObject<Liga>(body));
                    return Ok();

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }



        [HttpPut("/updateestadios")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AtualizaEstadios()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/venues?country=Portugal"),
                    Headers =
                        {
                            { "X-RapidAPI-Key", "bded329172msh9ee31d1971b7f50p185a92jsn0a1420afe7e7" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    _jogosService.AtualizaEstadios(JsonConvert.DeserializeObject<Estadio>(body));
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("/updatejogos")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizaJogos(int ligaID)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/fixtures?league={ligaID}&season=2023"),
                    Headers =
                             {
                                 { "X-RapidAPI-Key", "bded329172msh9ee31d1971b7f50p185a92jsn0a1420afe7e7" },
                                 { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                             },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    _jogosService.AtualizaJogos(JsonConvert.DeserializeObject<Jogo>(body, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }));
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("/updateequipas")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizaEquipas(int ligaID)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/teams?league={ligaID}&season=2023"),
                    Headers =
                        {
                            { "X-RapidAPI-Key", "bded329172msh9ee31d1971b7f50p185a92jsn0a1420afe7e7" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                   await _jogosService.AtualizaEquipas(JsonConvert.DeserializeObject<Equipa>(body, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }));
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("/topscorers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizaTopScorers(string IdLiga)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/players/topscorers?league={IdLiga}&season=2023"),
                Headers =
                        {
                            { "X-RapidAPI-Key", "bded329172msh9ee31d1971b7f50p185a92jsn0a1420afe7e7" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                await _jogosService.AtualizaTopScorers(JsonConvert.DeserializeObject<TopScorers>(body, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));

                return Ok();
            }
        }


        [HttpPut("/updatecoordenadas")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtualizaCoordenadas()
        {
            try
            {
                await _jogosService.AtualizaCoordenadasEquipas();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/GetLogsJogos")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<LogJogos>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public async Task<IActionResult> GetLogsJogos()
        {
            try
            {
                var jogos = await _jogosService.GetLogJogos();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/GetLogsEquipas")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<LogEquipas>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetLogsEquipas()
        {
            try
            {
                var equipas = await _jogosService.GetLogEquipas();
                return Ok(equipas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
