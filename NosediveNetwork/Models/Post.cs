using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NosediveNetwork.Services;

namespace NosediveNetwork.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }
        
        public DateTime Timestamp { get; set; }

        public string TextContent { get; set; }
        public Picture PictureContent { get; set; }

        public string UserId { get; set; }
        public string CircleId { get; set; }

        public override string ToString()
        {
            return string.Format($"PostId {PostId}, Circle ID {CircleId}, UserId {UserId}, Timestamp {Timestamp}");
        }
    }
}
