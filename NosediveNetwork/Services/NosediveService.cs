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

            database.DropCollection(settings.CircleCollectionName);
            database.DropCollection(settings.PostCollectionName);
            database.DropCollection(settings.UserCollectionName);

            _Users = database.GetCollection<User>(settings.UserCollectionName); //"Users"
            _Posts = database.GetCollection<Post>(settings.PostCollectionName); //"Posts"
            _Circles = database.GetCollection<Circle>(settings.CircleCollectionName); //"Circles"
        }

        public User GetUser(string name) => _Users.Find(user => user.Name == name).FirstOrDefault();
        public Circle GetCircle(string name) => _Circles.Find(circle => circle.Name == name).FirstOrDefault();


        //public List<Post> GetPosts(string user_id) => _Posts.Find(post => post.UserId == user_id).ToList();

        public List<Post> Feed(User user)
        {
            //var tempFeed = _Circles.Find(new BsonDocument("CircleId", )).ToList();
            //tempCircles.Find(new BsonDocument(""));

            // filter = Builders<Post>.Filter.ElemMatch(x => x.Tags, x => x.Name == "test");

            //return _Posts.Find(Builders<Post>.Filter.ElemMatch(post => post.ci)).ToList();

            return _Posts.Find(Builders<Post>.Filter.eq(x => user.CircleId.Any(x.CircleId))).ToList();
            //return _Posts.Find(new BsonDocument("UserId", user_id)).ToList();
        }

        public List<Post> Wall(User user)
        {
            return _Posts.Find(new BsonDocument("UserId", user.Id)).ToList();
        }

        public User CreateUser(User user)
        {
            _Users.InsertOne(user);
            return user;
        }

        public void CreateTextPost(User user, string content, Circle circle)
        {
            var post = new Post()
            {
                Timestamp = DateTime.Now,
                UserId = user.Id,
                TextContent = content,
                CircleId = circle.CircleId
            };
            _Posts.InsertOne(post);
        }

        public void CreatePicturePost(User user, Picture content, Circle circle = null)
        {
            var post = new Post()
            {
                Timestamp = DateTime.Now,
                UserId = user.Id,
                PictureContent = content,
                CircleId = circle.CircleId
            };
            _Posts.InsertOne(post);
        }

        public void CreateCircle(User user, string name)
        {
            var circle = new Circle()
            {
                Name = name
            };
            _Circles.InsertOne(circle);
            CircleAddUser(circle, user);
        }

        public void CreateComment(Post post, User user, string content)
        {
            var comment = new Comment()
            {
                Content = content,
                Timestamp = DateTime.Now,
                UserId = user.Id
            };
            _Posts.UpdateOne(new BsonDocument("Post", post.PostId),
                Builders<Post>.Update.Push("Comments", comment));
        }

        public void CircleAddUser(Circle circle, User user)
        {
            _Circles.UpdateOne(new BsonDocument("CircleId", circle.CircleId), Builders<Circle>.Update.Push("UsersId", user.Id));
        }

        public void UserAddFriend(User user, User friend)
        {
            _Users.UpdateOne(new BsonDocument("Id", user.Id), Builders<User>.Update.Push("Friends", friend.Id));
        }
    }
}