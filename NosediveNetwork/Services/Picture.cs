using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Help from: https://www.codeproject.com/articles/708140/uploading-and-viewing-images-with-asp-net-mvc-and


namespace NosediveNetwork.Services
{
    public class Picture
    {
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
    }
}
