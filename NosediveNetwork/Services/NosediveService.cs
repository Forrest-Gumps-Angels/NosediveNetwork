using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NosediveNetwork.Models;

namespace NosediveNetwork.Services
{
    public class NosediveService
    {
        private readonly IMongoCollection<User> _Users;
        private readonly IMongoCollection<Post> _Posts;
        private readonly IMongoCollection<Circle> _Circles;

        public NosediveService(INosediveNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName); //"NosediveDb"

            _Users = database.GetCollection<User>(settings.UserCollectionName); //"Users"
            _Posts = database.GetCollection<Post>(settings.PostCollectionName); //"Posts"
            _Circles = database.GetCollection<Circle>(settings.CircleCollectionName); //"Circles"
        }

        public User GetUser(string id) => _Users.Find(user => user.Id == id).FirstOrDefault();

        //public List<Post> GetPosts(string user_id) => _Posts.Find(post => post.UserId == user_id).ToList();

        public List<Post> Feed(string user_id)
        {
            return _Posts.Aggregate()
                .Match(p => p.UserId == user_id)
                .Lookup(_Users, )
        }

        public User Create(User user)
        {
            _Users.InsertOne(user);
            return user;
        }

        
    }
}