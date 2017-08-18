using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Autofy.Public.Api.Controllers
{

    public class Activity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string WebHookUrl { get; set; }
        public string Name { get; set; }
        public List<string> Actions { get; set; }
    }
}