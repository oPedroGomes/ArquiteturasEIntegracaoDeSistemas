using BusinessProject.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessProject.Pages
{
    public class BusinessModel : PageModel
    {
        private  BusinessClass _businessClass;

        public GoogleMapDirectionModel direcoes { get; set; }
        public ParkingResponse parking { get; set; }
        public PlacesResponseLazer lazer { get; set; }
        public WeatherForecast tempo { get; set; }
        public List<ResponseJogo> Jogos { get; set; }  

        public ResponseJogo DetalheJogo { get; set; }

        [BindProperty]
        public int? IdJogo { get; set; }
        public List<ResponseTopScorers> TopScorers { get; set; }

        public BusinessModel()
        {
        }

        public IActionResult OnGet()
        {
            if(string.IsNullOrEmpty(Global.BERARER_TOKEN))
            {
                return RedirectToPage("SetBearerToken");
            }
            
            _businessClass = new BusinessClass(Global.BERARER_TOKEN);
            return Page();
        }
        public async Task<IActionResult> OnPostDetalhe()
        {
            _businessClass = new BusinessClass(Global.BERARER_TOKEN);

            if (IdJogo == null)
                return RedirectToPage("Business");

            DetalheJogo = await _businessClass.GetJogoById(IdJogo,true);
            ResponseEquipa resp;
            try
            {
             resp = await _businessClass.GetEquipaById(DetalheJogo.Teams.Home.Id);
            }catch(Exception ex)
            {
                resp = null;
            }

            if(resp != null)
            {
                string latitude = resp.Venue.Latitude.Replace(',', '.');
                string longitude = resp.Venue.Longitude.Replace(',', '.');

                try
                {
                    tempo = await _businessClass.GetWeather(latitude.Replace(',', '.'), longitude.Replace(',', '.'));
                }
                catch (Exception ex)
                {
                    tempo = null;
                }

                try
                {
                    direcoes = await _businessClass.GetDirections(new Coordinates()
                    {
                        From = new Coordinate() { Latitute = "41.532051", Longitude = "-8.619053" },
                        To = new Coordinate() { Latitute = latitude, Longitude = longitude }
                    });
                }
                catch (Exception ex)
                {
                    direcoes = null;
                }


                try
                {
                    lazer = await _businessClass.GetPlaces(new SearchParametersLazer()
                    {
                        IncludedTypes = new List<string>() { "restaurant", "bar" },
                        MaxResultCount = 10,
                        LocationRestriction = new LocationRestrictionLazer()
                        {

                            Circle = new CircleLazer()
                            {
                                Center = new CenterLazer()
                                {
                                    Latitude = Double.Parse(latitude.Replace('.', ',')),
                                    Longitude = Double.Parse(longitude.Replace('.', ','))
                                },
                                Radius = 10000
                            }

                        }
                    });

                }
                catch (Exception ex)
                {
                    lazer = null;
                }

                try
                {
                    parking = await _businessClass.GetParkingDirections(new SearchParameters()
                    {
                        IncludedTypes = new List<string>() { "parking" },
                        MaxResultCount = 10,
                        LocationRestriction = new LocationRestriction()
                        {


                            Circle = new Circle()
                            {
                                Center = new Center()
                                {
                                    Latitude = Double.Parse(latitude.Replace('.', ',')),
                                    Longitude = Double.Parse(longitude.Replace('.', ','))
                                },
                                Radius = 10000
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    parking = null;
                }

            }




            try
            {
                TopScorers = await _businessClass.GetTopScorers();
            }
            catch (Exception ex)
            {
                TopScorers = null;
            }



            return Page();
        }
        public async Task<IActionResult> OnPost(string clube)
        {
            _businessClass = new BusinessClass(Global.BERARER_TOKEN);

            try
            {
                Jogos = await _businessClass.GetJogos(clube, null);
            }
            catch (Exception ex)
            {
                Jogos = null;
            }


            

            return Page();

        }

    }
}
