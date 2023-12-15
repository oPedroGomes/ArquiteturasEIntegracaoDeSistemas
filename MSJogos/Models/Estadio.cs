using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class PagingEstadio
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class ParametersEstadio
    {
        public string country { get; set; }
    }

    public class ResponseEstadio
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int capacity { get; set; }
        public string surface { get; set; }
        public string image { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Estadio
    {
        public string get { get; set; }
        public ParametersEstadio parameters { get; set; }
        public List<object> errors { get; set; }
        public int results { get; set; }
        public PagingEstadio paging { get; set; }
        public List<ResponseEstadio> response { get; set; }
    }


}
