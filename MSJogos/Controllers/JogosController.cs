using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MSJogos.Models;
using MSJogos.Servicos;
using System.Security.Claims;

namespace MSJogos.Controllers
{
    public class JogosController : Controller
    {
        private readonly JogosService _jogosService;

        public JogosController(JogosService jogosService)
        {
            _jogosService = jogosService;
        }

        [HttpGet("/jogos")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(List<ResponseJogo>), 200)]
        [Authorize]

        public async Task<IActionResult> GetProximosJogos(string equipa,DateTime? dataFim)
        {
            try
            {
                var jogos = await _jogosService.GetJogos(equipa, dataFim);

                if (!string.IsNullOrEmpty(equipa))
                {
                    var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                    await _jogosService.SaveLogEquipas(
                        new LogEquipas()
                        {   DataCria = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Email = email,
                            Equipa = equipa
                        });
                }

                return Ok(jogos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/estadio")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(ResponseEstadio), 200)]
        [Authorize]
        public async Task<IActionResult> GetEstadioByEquipa(string idEquipa)
        {
            if (string.IsNullOrEmpty(idEquipa))
                return BadRequest("Obrigatório indicar uma equipa!");

            try
            {
                var estadios = await _jogosService.GetEstadio(idEquipa);
                return Ok(estadios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/ligas")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(ResponseLiga), 200)]
        [Authorize]

        public async Task<IActionResult> GetLigas()
        {
            try
            {
                var ligas = await _jogosService.GetLigas();
                return Ok(ligas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/topscorers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(List<ResponseTopScorers>), 200)]
        [Authorize]

        public async Task<IActionResult> GetTopScorers()
        {
            try
            {
                var topScorers = await _jogosService.GetTopScorers();
                return Ok(topScorers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/jogo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(ResponseJogo), 200)]
        [Authorize]

        public async Task<IActionResult> GetJogoById(int fixtureId,bool saveLog = false)
        {
            if (fixtureId == 0)
                return BadRequest("Obrigatório indicar um ID de jogo!");

            try
            {
                var jogo = await _jogosService.GetJogoById(fixtureId);

                if (saveLog)
                {
                    var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
                    await _jogosService.SaveLogJogos(
                        new LogJogos()
                        {
                            DataCria = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            Email = email,
                            IdJogo = jogo.fixture.id.ToString()
                        });
                }
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/equipa")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(Exception))]
        [ProducesResponseType(typeof(ResponseEquipa), 200)]
        [Authorize]

        public async Task<IActionResult> GetEquipaById(int equipaId)
        {
            if (equipaId == 0)
                return BadRequest("Obrigatório indicar um ID de jogo!");

            try
            {
                var equipa = await _jogosService.GetEquipaById(equipaId);



                return Ok(equipa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
