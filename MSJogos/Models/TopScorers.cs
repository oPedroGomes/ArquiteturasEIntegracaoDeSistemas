using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BirthTopScorers
    {
        public string date { get; set; }
        public string place { get; set; }
        public string country { get; set; }
    }

    public class CardsTopScorers
    {
        public int yellow { get; set; }
        public int yellowred { get; set; }
        public int red { get; set; }
    }

    public class DribblesTopScorers
    {
        public int attempts { get; set; }
        public int success { get; set; }
        public object past { get; set; }
    }

    public class DuelsTopScorers
    {
        public int total { get; set; }
        public int won { get; set; }
    }

    public class FoulsTopScorers
    {
        public int drawn { get; set; }
        public int committed { get; set; }
    }

    public class GamesTopScorers
    {
        public int appearences { get; set; }
        public int lineups { get; set; }
        public int minutes { get; set; }
        public object number { get; set; }
        public string position { get; set; }
        public string rating { get; set; }
        public bool captain { get; set; }
    }

    public class GoalsTopScorers
    {
        public int total { get; set; }
        public int conceded { get; set; }
        public int? assists { get; set; }
        public object saves { get; set; }
    }

    public class LeagueTopScorers
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string logo { get; set; }
        public string flag { get; set; }
        public int season { get; set; }
    }

    public class PagingTopScorers
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class ParametersTopScorers
    {
        public string league { get; set; }
        public string season { get; set; }
    }

    public class PassesTopScorers
    {
        public int total { get; set; }
        public int key { get; set; }
        public int accuracy { get; set; }
    }

    public class PenaltyTopScorers
    {
        public object won { get; set; }
        public object commited { get; set; }
        public int scored { get; set; }
        public int missed { get; set; }
        public object saved { get; set; }
    }

    public class PlayerTopScorers
    {
        public int id { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public BirthTopScorers birth { get; set; }
        public string nationality { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public bool injured { get; set; }
        public string photo { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ResponseTopScorers
    {
        public PlayerTopScorers player { get; set; }
        public List<StatisticTopScorers> statistics { get; set; }
    }

    public class TopScorers
    {
        public string get { get; set; }
        public ParametersTopScorers parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public PagingTopScorers paging { get; set; }
        public List<ResponseTopScorers> response { get; set; }
    }

    public class ShotsTopScorers
    {
        public int total { get; set; }
        public int on { get; set; }
    }

    public class StatisticTopScorers
    {
        public TeamTopScorers team { get; set; }
        public LeagueTopScorers league { get; set; }
        public GamesTopScorers games { get; set; }
        public SubstitutesTopScorers substitutes { get; set; }
        public ShotsTopScorers shots { get; set; }
        public GoalsTopScorers goals { get; set; }
        public PassesTopScorers passes { get; set; }
        public TacklesTopScorers tackles { get; set; }
        public DuelsTopScorers duels { get; set; }
        public DribblesTopScorers dribbles { get; set; }
        public FoulsTopScorers fouls { get; set; }
        public CardsTopScorers cards { get; set; }
        public PenaltyTopScorers penalty { get; set; }
    }

    public class SubstitutesTopScorers
    {
        public int @in { get; set; }
        public int @out { get; set; }
        public int bench { get; set; }
    }

    public class TacklesTopScorers
    {
        public int total { get; set; }
        public int? blocks { get; set; }
        public int? interceptions { get; set; }
    }

    public class TeamTopScorers
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }


}
