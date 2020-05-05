using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class WallViewModel
    {
        public List<Post> WallList { get; set; } = new List<Post>();

        public WallViewModel(List<Post> wallList)
        {
            WallList = wallList;
        }
    }
}
