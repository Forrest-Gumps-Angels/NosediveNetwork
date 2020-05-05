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

            Seed();
        }

        public List<User> GetAllUsers() => _Users.Find(_ => true).ToList();
        public User GetUser(string name) => _Users.Find(user => user.Name == name).FirstOrDefault();
        public User GetUserFromId(string id) => _Users.Find(user => user.Id == id).FirstOrDefault();
        public Circle GetCircle(string name) => _Circles.Find(circle => circle.Name == name).FirstOrDefault();

        public Post GetPostFromId(string id) => _Posts.Find(post => post.PostId == id).FirstOrDefault();


        //public List<Post> GetPosts(string user_id) => _Posts.Find(post => post.UserId == user_id).ToList();

        public List<Post> Feed(User user)
        {
            //var tempFeed = _Circles.Find(new BsonDocument("CircleId", )).ToList();
            //tempCircles.Find(new BsonDocument(""));

            // filter = Builders<Post>.Filter.ElemMatch(x => x.Tags, x => x.Name == "test");

            //return _Posts.Find(Builders<Post>.Filter.ElemMatch(post => post.ci)).ToList();

            return _Posts.Find(Builders<Post>.Filter
                .Where(x => user.CircleId.Contains(x.CircleId) || user.Friends.Contains(x.UserId))).ToList();
            // user.
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

        public Post CreateTextPost(User user, string content, Circle circle)
        {
            var post = new Post()
            {
                Timestamp = DateTime.Now,
                UserId = user.Id,
                TextContent = content,
                CircleId = circle.CircleId
            };
            _Posts.InsertOne(post);
            return post;
        }

        public Post CreatePicturePost(User user, Picture content, Circle circle = null)
        {
            var post = new Post()
            {
                Timestamp = DateTime.Now,
                UserId = user.Id,
                PictureContent = content,
                CircleId = circle.CircleId
            };
            _Posts.InsertOne(post);
            return post;
        }

        public void CreateCircle(User user, string name)
        {
            var circle = new Circle()
            {
                Name = name
            };
            _Circles.InsertOne(circle);
            
            UserAddCircle(circle, user);
        }

        public void CreateComment(Post post, User user, string content)
        {
            var comment = new Comment()
            {
                Content = content,
                Timestamp = DateTime.Now,
                UserId = user.Id
            };
            _Posts.UpdateOne(Builders<Post>.Filter.Eq("PostId", post.PostId),
                Builders<Post>.Update.Push("Comments", comment));
        }

        public void UserAddCircle(Circle circle, User user)
        {
            _Users.UpdateOne(Builders<User>.Filter.Eq("Id", user.Id), Builders<User>.Update.Push("CircleId", circle.CircleId));
        }

        public void UserAddFriend(User user, User friend)
        {
            _Users.UpdateOne(Builders<User>.Filter.Eq("Id", user.Id), Builders<User>.Update.Push("Friends", friend.Id));
        }

        public void Seed()
        {
            ///////// Creating dummy data ////////////


            // Creating users
            CreateUser(new Models.User() { Name = "Morten Hansen", Age = 22, Gender = "Male" });
            CreateUser(new Models.User() { Name = "Rasmus Føgh", Age = 12, Gender = "Unidentifiable" });
            CreateUser(new Models.User() { Name = "Viktor Lundsgaard", Age = 23, Gender = "Male" });

            // Creating circles
            CreateCircle(GetUser("Morten Hansen"), "Area 51 conspiracy group");
            CreateCircle(GetUser("Viktor Lundsgaard"), "Hot girls group");
            UserAddCircle(GetCircle("Area 51 conspiracy group"), GetUser("Rasmus Føgh"));

            // Creating posts
            var post1 = CreateTextPost(GetUser("Morten Hansen"), "Hej jeg er Morten!", GetCircle("Area 51 conspiracy group"));
            var post2 = CreateTextPost(GetUser("Rasmus Føgh"), "Hej jeg er i tvivl om mit køn!", GetCircle("Area 51 conspiracy group"));
            var post3 = CreateTextPost(GetUser("Viktor Lundsgaard"), "Hej jeg laver mange damer!", GetCircle("Hot girls group"));

            CreateComment(post1, GetUser("Viktor Lundsgaard"), "Super fedt Morten, jeg hedder Viktor!");


            // Adding friends
            UserAddFriend(GetUser("Morten Hansen"), GetUser("Viktor Lundsgaard"));
        }
    }
}