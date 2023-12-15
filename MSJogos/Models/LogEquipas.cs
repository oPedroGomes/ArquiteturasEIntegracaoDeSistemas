using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MSJogos.Models
{
    [BsonIgnoreExtraElements]

    public class LogEquipas
    {
        public string DataCria { get; set; }
        public string Email { get; set; }
        public string Equipa { get; set; }

    }
    [BsonIgnoreExtraElements]

    public class LogJogos
    {
        public string DataCria { get; set; }
        public string Email { get; set; }
        public string IdJogo { get; set; }

    }
}
