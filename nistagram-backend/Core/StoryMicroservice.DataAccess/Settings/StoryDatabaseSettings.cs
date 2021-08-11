using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMicroservice.DataAccess.Implementation
{
    public class StoryDatabaseSettings : IStoryDatabaseSettings
    {
        public string StoriesCollectionName { get; set; }
        public string RegisteredUsersCollectionName { get; set; }
        public string LocationsCollectionName { get; set; }
        public string HighlightsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}