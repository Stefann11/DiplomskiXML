using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highlights = StoryMicroservice.Core.DTOs.Highlights;

namespace StoryMicroservice.DataAccess.Factories
{
    public class HighlightFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;
        private readonly StoryFactory storyFactory;

        public HighlightFactory(RegisteredUserFactory registeredUserFactory, StoryFactory storyFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
            this.storyFactory = storyFactory;
        }

        public Highlights Create(Core.Model.Highlights highlight)
        {
            return new Highlights
            {
                Id = highlight.Id.ToString(),
                RegisteredUser = registeredUserFactory.Create(highlight.RegisteredUser),
                HighlightName = highlight.HighlightName,
                Stories = storyFactory.CreateStories(highlight.Stories)
            };
        }

        public Core.Model.Highlights Create(Highlights highlight)
        {
            return Core.Model.Highlights.Create(new Guid(highlight.Id), HighlightName.Create(highlight.HighlightName).Value,
                storyFactory.CreateStories(highlight.Stories),
                registeredUserFactory.Create(highlight.RegisteredUser, new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>(),
                new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>(),
                new List<Core.Model.RegisteredUser>())).Value;
        }

        public IEnumerable<Core.Model.Highlights> CreateHighlights(List<Highlights> highlights)
        {
            return highlights.Select(highlight => Create(highlight));
        }

        public List<Highlights> CreateHighlights(IEnumerable<Core.Model.Highlights> highlights)
        {
            return highlights.Select(location => Create(location)).ToList();
        }
    }
}