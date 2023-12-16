using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace BusinessProject.Pages
{
    public class HealthChecksModel : PageModel
    {
        public bool MSDirecoes { get; set; }
        public bool MSEstacionamento { get; set; }
        public bool MSLazer { get; set; }
        public bool MSJogos { get; set; }
        public bool MSTempo { get; set; }
        public bool MSGateway { get; set; }

        string ipDirecoes = "http://localhost:44372/";
        string ipEstacionamento = "http://localhost:44305/";
        string iplazer = "http://localhost:44377";
        string ipTempo = "http://localhost:44314/";
        string ipJogos = "https://localhost:44331/";

        public void OnGet()
        {
            MSDirecoes = PingHost("localhost", 44372);
            MSEstacionamento = PingHost("localhost", 44305);
            MSLazer = PingHost("localhost", 44377);
            MSTempo = PingHost("localhost", 44314);
            MSJogos = PingHost("localhost", 44331);
        }
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }
    }
}
