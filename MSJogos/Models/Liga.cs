using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{   
    public class CountryLiga
    {
        public string name { get; set; }
        public string code { get; set; }
        public string flag { get; set; }
    }

    public class CoverageLiga
    {
        public FixturesLiga fixtures { get; set; }
        public bool standings { get; set; }
        public bool players { get; set; }
        public bool top_scorers { get; set; }
        public bool top_assists { get; set; }
        public bool top_cards { get; set; }
        public bool injuries { get; set; }
        public bool predictions { get; set; }
        public bool odds { get; set; }
    }

    public class FixturesLiga
    {
        public bool events { get; set; }
        public bool lineups { get; set; }
        public bool statistics_fixtures { get; set; }
        public bool statistics_players { get; set; }
    }

    public class LeagueLiga
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string logo { get; set; }
    }

    public class PagingLiga
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class ParametersLiga
    {
        public string country { get; set; }
    }

    public class ResponseLiga
    {
        public LeagueLiga league { get; set; }
        public CountryLiga country { get; set; }
        public List<SeasonLiga> seasons { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Liga
    {
        public string get { get; set; }
        public ParametersLiga parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public PagingLiga paging { get; set; }
        public List<ResponseLiga> response { get; set; }
    }

    public class SeasonLiga
    {
        public int year { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool current { get; set; }
        public CoverageLiga coverage { get; set; }
    }


}
