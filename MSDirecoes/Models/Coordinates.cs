namespace MSDirecoes.Models
{
    public class Coordinates
    {
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
    }
    public class Coordinate
    {
        public string Latitute { get; set; }
        public string Longitude { get; set; }
    }
}
