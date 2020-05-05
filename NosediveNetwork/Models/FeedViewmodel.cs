using NosediveNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class FeedViewModel
    {
        public NosediveService NosediveService { get; set; }
        public List<Post> FeedList { get; set; } = new List<Post>();

        public FeedViewModel(NosediveService nosediveService)
        {
            NosediveService = nosediveService;
            FeedList = NosediveService.Feed(NosediveService.GetUser(CurrentUser.CurrentUserName));
        }
    }
}
