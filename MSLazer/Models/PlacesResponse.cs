namespace MSLazer.Models
{
    public class PlacesResponseLazer
    {
        public List<PlaceLazer> places { get; set; }

    }
    public class DisplayNameLazer
    {
        public string text { get; set; }
        public string languageCode { get; set; }
    }

    public class PlaceLazer
    {
        public double rating { get; set; }
        public string businessStatus { get; set; }
        public string priceLevel { get; set; }
        public DisplayNameLazer displayName { get; set; }
    }

}
