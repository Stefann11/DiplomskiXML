using StoryMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.Interface.Repository
{
    public interface IStoryRepository : IRepository<Story>
    {
        public IEnumerable<Core.Model.Story> GetBy(string storyOwnerId, string followingId, string last24h, string notLoggedIn);

        public void BanStory(string id);
    }
}