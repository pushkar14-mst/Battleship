using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
namespace server.Models
{
    public class PlayersModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string? Id { get; set; }

        [BsonElement("game")]
        [JsonProperty("game")]
        public Game? Game { get; set; }
        
    }

    public class Game
    {
        [JsonProperty("gameId")]
        [BsonElement("gameId")]
        public string? GameId { get; set; }
        [JsonProperty("players")]
        [BsonElement("players")]
        public List<Player>? Players { get; set; }
    }
    public class Player
    {
        [JsonProperty("playerName")]
        [BsonElement("playerName")]
        public string? PlayerName { get; set; }

        [JsonProperty("fleetPlacements")]
        [BsonElement("fleetPlacements")]
        public List<List<int>>? FleetPlacements { get; set; }
    }
}

