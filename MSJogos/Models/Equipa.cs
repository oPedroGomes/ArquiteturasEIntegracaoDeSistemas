using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class PagingEquipa
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class ParametersEquipa
    {
        public string league { get; set; }
        public string season { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class ResponseEquipa
    {
        public TeamEquipa team { get; set; }
        public VenueEquipa venue { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Equipa
    {
        public string get { get; set; }
        public ParametersEquipa parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public PagingEquipa paging { get; set; }
        public List<ResponseEquipa> response { get; set; }
    }

    public class TeamEquipa
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string country { get; set; }
        public int? founded { get; set; }
        public bool national { get; set; }
        public string logo { get; set; }
    }

    public class VenueEquipa
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string city { get; set; }
        public int capacity { get; set; }
        public string surface { get; set; }
        public string image { get; set; }
    }


}
