using StoryMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.Interface.Repository
{
    public interface IHighlightRepositry : IRepository<Highlights>
    {
        public IEnumerable<Core.Model.Highlights> GetBy(string ownerId);

        public Core.Model.Highlights AddStory(string highlightId, Story story);
    }
}