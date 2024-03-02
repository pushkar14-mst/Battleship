using System;
using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using server.Data;
namespace server.Data
{
	public class MongoDBServices
	{
		private readonly IMongoCollection<PlayersModel> _playersCollection;
        public MongoDBServices(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playersCollection = database.GetCollection<PlayersModel>(mongoDBSettings.Value.CollectionName);
        }
        public async Task CreateAsync(PlayersModel newGame) =>
        await _playersCollection.InsertOneAsync(newGame);
        public async Task<PlayersModel> RetrieveGameByGameId(string gameId)
        {
            var filter = Builders<PlayersModel>.Filter.Eq("game.gameId", gameId);
            PlayersModel res= await _playersCollection.Find(filter).FirstOrDefaultAsync();
            return res;
        }
        public async Task UpdateGame(PlayersModel game)
        {
            var filter = Builders<PlayersModel>.Filter.Eq("game.gameId", game.Game.GameId);
            await _playersCollection.ReplaceOneAsync(filter, game);
        }
        public async Task UpdateFleet(string gameId, string playerName,List<List<int>> fleetPlacement)
        {
            try
            {
                var filter = Builders<PlayersModel>.Filter.And(
                    Builders<PlayersModel>.Filter.Eq("game.gameId", gameId),
                    Builders<PlayersModel>.Filter.ElemMatch("game.players", Builders<Player>.Filter.Eq("playerName", playerName))
                );

                var update = Builders<PlayersModel>.Update.Set("game.players.$.fleetPlacements", fleetPlacement);

                UpdateResult result = await _playersCollection.UpdateOneAsync(filter, update);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    Console.WriteLine($"Fleet placement updated for player '{playerName}' in game with ID '{gameId}'.");
                }
                else
                {
                    Console.WriteLine($"No player found with the specified gameId '{gameId}' and playerName '{playerName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating fleet placement: {ex.Message}");
                throw;
            }
        }
        
    }
}

