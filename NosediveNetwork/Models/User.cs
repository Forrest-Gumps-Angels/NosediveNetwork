using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NosediveNetwork.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public List<string> BlockedUsers { get; set; }

        public List<string> Friends { get; set; }
        public List<string> CircleId { get; set; }

        public override string ToString()
        {
            return string.Format("Book({0}, {1}, {2}, {3})", Id, Name, Gender, Age);
        }
    }
}
