using BusinessProject.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BusinessProject.Pages
{
    public class StatsModel : PageModel
    {
        public List<LogEquipas> LogEquipas { get; set; }
        public List<LogJogos> LogJogos { get; set; }

        //---------------------------------------------
        public List<string> labelsJogos { get; set; }
        public List<string>  dataJogos { get; set; }
        public List<string> labelsEquipa { get; set; }
        public List<string> dataEquipa { get; set; }

        //---------------------------------------------
        public List<UserStats> DadosUser { get; set; }



        public async Task<IActionResult> OnGet()
        {           

            BusinessClass businessClass = new BusinessClass(Global.BERARER_TOKEN);

            LogEquipas = await businessClass.GetLogEquipas();
            LogJogos = await businessClass.GetLogJogos();



            labelsEquipa = new List<string>();
            labelsEquipa.AddRange(LogEquipas.DistinctBy(x=>x.Equipa).Select(y=>y.Equipa).ToList());
            dataEquipa = new List<string>();
            dataEquipa.AddRange(LogEquipas.GroupBy(x => x.Equipa).Select(y => y.Count().ToString()));



            labelsJogos = new List<string>();
            dataJogos = new List<string>();

            var auxJogos = LogJogos.GroupBy(x => x.IdJogo).Select(y => new { id = y.Key, count = y.Count() }).OrderByDescending(o => o.count).Take(5).ToList();

            foreach(var item in auxJogos)
            {
                dataJogos.Add(item.count.ToString());

                var aux = await businessClass.GetJogoById(Convert.ToInt32(item.id),false);
                labelsJogos.Add(aux.Teams.Home.Name + " - "  + aux.Teams.Away.Name + " | " + aux.Fixture.Date.ToString("dd-MM-yyyy"));
            }


            //Fazer um somatorio quantas vezes um determinado utilizador faz consultas
            var auxUserSum1 = LogJogos.GroupBy(x => x.Email).Select(y => new {email = y.Key, count = y.Count() }).OrderByDescending(o => o.count).ToList();
            var auxUserSum2 = LogEquipas.GroupBy(x => x.Email).Select(y => new { email = y.Key, count = y.Count() }).OrderByDescending(o => o.count).ToList();
            var auxSum = auxUserSum1.Join(auxUserSum2, x => x.email, y => y.email, (x, y) => new { email = x.email, NReg = x.count + y.count }).ToList();


            DadosUser = new List<UserStats>();
            auxSum = auxSum.OrderByDescending(x => x.NReg).ToList();
            foreach (var item in auxSum)
            {
                DadosUser.Add(new UserStats() { Email = item.email, CountPedidos = item.NReg});
            }

            return Page();
        }


    }

    public class UserStats
    {
        public string Email { get; set; }
        public int CountPedidos { get; set;}

    }
}
