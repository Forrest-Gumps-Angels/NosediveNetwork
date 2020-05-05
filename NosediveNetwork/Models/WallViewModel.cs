using NosediveNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class WallViewModel
    {
        public NosediveService NosediveService { get; set; }
        public List<Post> WallList { get; set; } = new List<Post>();

        public WallViewModel(NosediveService nosediveService)
        {
            NosediveService = nosediveService;
            WallList = NosediveService.Wall(NosediveService.GetUser("Morten Hansen"));
        }
    }
}
