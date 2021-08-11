using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoryMicroservice.DataAccess.Implementation
{
    public interface IStoryDatabaseSettings
    {
        string StoriesCollectionName { get; set; }
        string RegisteredUsersCollectionName { get; set; }
        string LocationsCollectionName { get; set; }
        string HighlightsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}