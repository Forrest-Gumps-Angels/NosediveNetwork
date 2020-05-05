using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        List<PostModel> Posts { get; set; }

        List<CircleModel> Circles { get; set; }

        public override string ToString()
        {
            return string.Format("Book({0}, {1}, {2}, {3}, {4})", Id, BookName, Price, Category, Author);
        }
    }
}
