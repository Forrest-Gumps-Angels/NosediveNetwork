using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Models
{
    public class NosediveNetworkDatabaseSettings : INosediveNetworkDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string PostCollectionName { get; set; }
        public string CircleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    public interface INosediveNetworkDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string PostCollectionName { get; set; }
        string CircleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
