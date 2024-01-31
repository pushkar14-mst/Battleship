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
    }
}

