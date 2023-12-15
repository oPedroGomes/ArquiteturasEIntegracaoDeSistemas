namespace MSJogos.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AddressComponentGeolocation
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class GeometryGeolocation
    {
        public LocationGeolocation location { get; set; }
        public string location_type { get; set; }
        public ViewportGeolocation viewport { get; set; }
    }

    public class LocationGeolocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class NortheastGeolocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class PlusCodeGeolocation
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class ResultGeolocation
    {
        public List<AddressComponentGeolocation> address_components { get; set; }
        public string formatted_address { get; set; }
        public GeometryGeolocation geometry { get; set; }
        public string place_id { get; set; }
        public PlusCodeGeolocation plus_code { get; set; }
        public List<string> types { get; set; }
    }

    public class Geolocation
    {
        public List<ResultGeolocation> results { get; set; }
        public string status { get; set; }
    }

    public class SouthwestGeolocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class ViewportGeolocation
    {
        public NortheastGeolocation northeast { get; set; }
        public SouthwestGeolocation southwest { get; set; }
    }


}
