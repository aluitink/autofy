using Autofy.Public.Api.Controllers;
using Autofy.Public.Api.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofy.Public.Api.DataProvider
{
    public class MongoDBDataProvider: IDataProvider
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        public MongoDBDataProvider(string connectionString)
        {
            _client = new MongoClient(new MongoUrl(connectionString));
            _database = _client.GetDatabase("autoengine");
        }

        public string CreateActivity(Activity activity)
        {
            var collection = _database.GetCollection<Activity>("activity");
            collection.InsertOne(activity);
            return activity.Id;
        }

        public Activity RetrieveActivity(string id)
        {
            var collection = _database.GetCollection<Activity>("activity");
            var result = collection.Find(a => a.Id == id);
            return result.FirstOrDefault<Activity>();
        }
    }
}
