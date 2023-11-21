namespace MSEstacionamento.Models
{

    public class SearchParameters
    {
        public List<string> includedTypes { get; set; } //restaurant,bar,cafe,night_club
        public int maxResultCount { get; set; } = 5;
        public LocationRestriction locationRestriction { get; set; }
    }
    public class Center
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public class Circle
    {
        public Center center { get; set; }
        public double radius { get; set; }
    }

    public class LocationRestriction
    {
        public Circle circle { get; set; }
    }

}
