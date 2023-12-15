using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AwayJogo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public bool? winner { get; set; }
    }

    public class ExtratimeJogo
    {
        public object home { get; set; }
        public object away { get; set; }
    }

    public class FixtureJogo
    {
        public int id { get; set; }
        public string referee { get; set; }
        public string timezone { get; set; }
        public DateTime date { get; set; }
        public int timestamp { get; set; }
        public PeriodsJogo periods { get; set; }
        public VenueJogo venue { get; set; }
        public StatusJogo status { get; set; }
    }

    public class FulltimeJogo
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class GoalsJogo
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class HalftimeJogo
    {
        public int home { get; set; }
        public int away { get; set; }
    }

    public class HomeJogo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public bool? winner { get; set; }
    }

    public class LeagueJogo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string logo { get; set; }
        public string flag { get; set; }
        public int season { get; set; }
        public string round { get; set; }
    }

    public class PagingJogo
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class ParametersJogo
    {
        public string league { get; set; }
        public string season { get; set; }
    }

    public class PenaltyJogo
    {
        public object home { get; set; }
        public object away { get; set; }
    }

    public class PeriodsJogo
    {
        public string first { get; set; }
        public string second { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ResponseJogo
    {
        public FixtureJogo fixture { get; set; }
        public LeagueJogo league { get; set; }
        public TeamsJogo teams { get; set; }
        public GoalsJogo goals { get; set; }
        public ScoreJogo score { get; set; }
        public ResponseJogo()
        {
            
        }
    }
    [BsonIgnoreExtraElements]
    public class Jogo
    {
        public string get { get; set; }
        public ParametersJogo parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public PagingJogo paging { get; set; }
        public List<ResponseJogo> response { get; set; }
    }

    public class ScoreJogo
    {
        public HalftimeJogo halftime { get; set; }
        public FulltimeJogo fulltime { get; set; }
        public ExtratimeJogo extratime { get; set; }
        public PenaltyJogo penalty { get; set; }
    }

    public class StatusJogo
    {
        public string @long { get; set; }
        public string @short { get; set; }
        public string elapsed { get; set; }
    }

    public class TeamsJogo
    {
        public HomeJogo home { get; set; }
        public AwayJogo away { get; set; }
    }

    public class VenueJogo
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
    }


}
