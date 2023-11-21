namespace MSLazer.Services
{
    public class ParkingResponse
    {
        public List<Place> places { get; set; }

    }
    public class DisplayName
    {
        public string text { get; set; }
        public string languageCode { get; set; }
    }

    public class Place
    {
        public double rating { get; set; }
        public string businessStatus { get; set; }
        public string priceLevel { get; set; }
        public DisplayName displayName { get; set; }
    }  

}
