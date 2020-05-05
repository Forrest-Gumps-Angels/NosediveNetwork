using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NosediveNetwork.Models
{
    public class Circle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircleId { get; set;  }
        public string UserId { get; set; }
        public string PostId { get; set; }

        public override string ToString()
        {
            return string.Format($"Circle ID {CircleId}, UserId {UserId}, PostId {PostId}");
        }
    }
}
