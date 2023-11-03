using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSJogos.Models;

namespace MSJogos.Controllers
{
    public class JogosController : Controller
    {
        [HttpGet("/jogos")]
        public IActionResult GetJogos()
        {
            return Ok(new { 
                Equipa1 = "Benfica",
                Equipa2 = "Porto"
            });
        }
        [HttpGet("/jogo")]
        public IActionResult GetJogo(int idJogo)
        {
            return Ok(new
            {
                Equipa1 = "Benfica",
                Equipa2 = "Porto"
            });
        }

        //CRUD Admin
        [Authorize]
        [HttpPut("/operation/jogo")]
        public IActionResult UpdateJogo(Jogo jogo)
        {
            //Altera um jogo
            return Ok(new
            {
                Equipa1 = "Benfica",
                Equipa2 = "Porto"
            });
        }
        [Authorize]
        [HttpPost("/operation/jogo")]
        public IActionResult CreateJogo(Jogo jogo)
        {
            //Altera um jogo
            return Ok(new
            {
                Equipa1 = "Benfica",
                Equipa2 = "Porto"
            });
        }

        [Authorize]
        [HttpDelete("/operation/jogo")]
        public IActionResult DeleteJogo(Jogo jogo)
        {
            //Altera um jogo
            return Ok(new
            {
                Equipa1 = "Benfica",
                Equipa2 = "Porto"
            });
        }


    }
}
