using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public static class CurrentUser
    {
        public static string CurrentUserName { get; set; }
        public static void setUser(string Name) => CurrentUserName = Name;
    }
}
