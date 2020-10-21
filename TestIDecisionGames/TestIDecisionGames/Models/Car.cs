using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TestIDecisionGames.Models
{
    public class Car
    {
        [BsonId]
        [BsonRequired]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
