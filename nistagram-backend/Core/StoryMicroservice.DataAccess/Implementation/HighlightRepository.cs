using CSharpFunctionalExtensions;
using MongoDB.Driver;
using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.DataAccess.Implementation
{
    public class HighlightRepository : IHighlightRepositry
    {
        private readonly IMongoCollection<Highlights> _highlights;
        private readonly IStoryRepository _storyRepository;
        private readonly HighlightFactory highlightFactory;
        private readonly StoryFactory storyFactory;

        public HighlightRepository(IStoryDatabaseSettings settings, IStoryRepository storyRepository,
            HighlightFactory highlightFactory, StoryFactory storyFactory)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _highlights = database.GetCollection<Highlights>(settings.HighlightsCollectionName);
            _storyRepository = storyRepository;
            this.highlightFactory = highlightFactory;
            this.storyFactory = storyFactory;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Core.Model.Highlights Edit(string id, Core.Model.Highlights highlightIn)
        {
            _highlights.ReplaceOne(highlight => highlight.Id == id, highlightFactory.Create(highlightIn));
            return highlightIn;
        }

        public IEnumerable<Core.Model.Highlights> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<Core.Model.Highlights> GetById(Guid id)
        {
            return highlightFactory.Create(_highlights.Find(highlight => highlight.Id.Equals(id.ToString())).FirstOrDefault());
        }

        public Core.Model.Highlights Save(Core.Model.Highlights highlight)
        {
            _highlights.InsertOne(highlightFactory.Create(highlight));
            return highlight;
        }

        public IEnumerable<Core.Model.Highlights> GetBy(string ownerId)
        {
            List<Core.Model.Highlights> highlights = new List<Core.Model.Highlights>();
            if (!String.IsNullOrWhiteSpace(ownerId))
            {
                highlights = highlightFactory.CreateHighlights(_highlights.Find(highlight => highlight.RegisteredUser.Id.Equals(ownerId)).ToList())
                    .ToList();
            }
            return highlights;
        }

        public Core.Model.Highlights AddStory(string highlightId, Core.Model.Story story)
        {
            var highlight = GetById(new Guid(highlightId)).Value;
            var storyToAdd = _storyRepository.GetById(story.Id).Value;
            var stories = highlight.Stories.ToList();
            stories.Add(storyToAdd);
            Edit(highlightId, Core.Model.Highlights.Create(highlight.Id, highlight.HighlightName, stories, highlight.RegisteredUser).Value);
            return highlight;
        }
    }
}