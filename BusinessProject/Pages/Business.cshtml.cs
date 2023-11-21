using BusinessProject.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessProject.Pages
{
    public class BusinessModel : PageModel
    {
        private readonly BusinessClass _businessClass;

        public GoogleMapDirectionModel direcoes { get; set; }
        public ParkingResponse parking { get; set; }

        public PlacesResponseLazer lazer{ get; set; }

        public WeatherForecast tempo { get; set; }

        public BusinessModel(BusinessClass businessClass)
        {
            _businessClass = businessClass;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Comentado para não estar a fazer chamadas desnecessárias --------------------------------------

            direcoes = await _businessClass.GetDirections(new Coordinates()
            {
                From = new Coordinate() { Latitute = "41.532051", Longitude = "-8.619053" },
                To = new Coordinate() { Latitute = "41.499340", Longitude = "-8.421150" }
            });

            //Comentado para não estar a fazer chamadas desnecessárias --------------------------------------
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
                            Latitude = 41.532051,
                            Longitude = -8.619053
                        },
                        Radius = 10000
                    }
                }
            });

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
                            Latitude = 41.532051,
                            Longitude = -8.619053
                        },
                        Radius = 10000
                    }

                }
            });


            tempo = await _businessClass.GetWeather("41.532051", "-8.619053");

            return Page();

        }
    }
}
