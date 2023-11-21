namespace MSLazer.Models
{

    public class SearchParametersLazer
    {
        public List<string> includedTypes { get; set; } //restaurant,bar,cafe,night_club
        public int maxResultCount { get; set; } = 5;
        public LocationRestrictionLazer locationRestriction { get; set; }
    }
    public class CenterLazer
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public class CircleLazer
    {
        public CenterLazer center { get; set; }
        public double radius { get; set; }
    }

    public class LocationRestrictionLazer
    {
        public CircleLazer circle { get; set; }
    }


}
