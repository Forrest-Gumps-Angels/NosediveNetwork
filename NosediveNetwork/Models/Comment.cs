using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class Comment
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        
        public override string ToString()
        {
            return string.Format($"Content: {Content}, Timestamp: {Timestamp}, UserId: {UserId}");
        }
    }
}
