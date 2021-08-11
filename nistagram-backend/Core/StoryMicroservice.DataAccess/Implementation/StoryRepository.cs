using CSharpFunctionalExtensions;
using MongoDB.Driver;
using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryMicroservice.DataAccess.Implementation
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IMongoCollection<Story> _stories;
        private readonly IUserRepository _userRepository;
        private readonly StoryFactory storyFactory;
        private readonly RegisteredUserFactory userFactory;

        public StoryRepository(IStoryDatabaseSettings settings, IUserRepository userRepository, StoryFactory storyFactory,
            RegisteredUserFactory userFactory)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stories = database.GetCollection<Story>(settings.StoriesCollectionName);
            this.storyFactory = storyFactory;
            this.userFactory = userFactory;
            _userRepository = userRepository;
        }

        public IEnumerable<Core.Model.Story> GetAll()
        {
            var stories = _stories.Find(story => true).ToList();
            return stories.Select(story => GetById(new Guid(story.Id)).Value);
        }

        public Maybe<Core.Model.Story> GetById(Guid id)
        {
            var storyDTO = _stories.Find<Story>(story => story.Id.Equals(id)).FirstOrDefault();
            if (storyDTO == null) return Maybe<Core.Model.Story>.None;
            if (storyDTO.IsBanned == true)
            {
                return Maybe<Core.Model.Story>.None;
            }
            var taggedUsers = _userRepository.GetUsersByDTO(storyDTO.TaggedUsers);
            var seenByUsers = _userRepository.GetUsersByDTO(storyDTO.SeenByUsers);
            var myCloseFriends = _userRepository.GetUsersById(storyDTO.RegisteredUser.MyCloseFriends);
            return storyFactory.Create(storyDTO, seenByUsers, taggedUsers, myCloseFriends);
        }

        public Core.Model.Story Save(Core.Model.Story story)
        {
            _stories.InsertOne(storyFactory.Create(story));
            return story;
        }

        public Core.Model.Story Edit(string id, Core.Model.Story storyIn)
        {
            _stories.ReplaceOne(story => story.Id == id, storyFactory.Create(storyIn));
            return storyIn;
        }

        public void Delete(Guid id)
        {
            _stories.DeleteOne(story => story.Id.Equals(id.ToString()));
        }

        public IEnumerable<Core.Model.Story> GetBy(string storyOwnerId, string followingId, string last24h, string notLoggedIn)
        {
            List<Core.Model.Story> allStories = new List<Core.Model.Story>();

            if (!String.IsNullOrWhiteSpace(storyOwnerId))
                allStories.AddRange(storyFactory.CreateStories(_stories.Find(story => story.RegisteredUser.Id.Equals(storyOwnerId)).ToList()));
            List<Core.Model.Story> stories = new List<Core.Model.Story>();
            stories.AddRange(allStories.Where(story => story.IsBanned == false));
            if (!String.IsNullOrWhiteSpace(followingId) && String.IsNullOrWhiteSpace(storyOwnerId))
            {
                var user = _userRepository.GetById(new Guid(followingId)).Value;
                var followingUsers = userFactory.CreateIds(user.Following);
                stories.AddRange(storyFactory.CreateStories(_stories.Find(story => followingUsers.Contains(story.RegisteredUser.Id)).ToList()));
                var tempStories = new List<Core.Model.Story>();
                foreach (var story in stories) if (story.GetType().Name.Equals("CloseFriendStory"))
                        tempStories.Add(Core.Model.CloseFriendStory.Create(story.Id, story.ContentPath,
                     story.TimeStamp, story.Duration, story.Description, _userRepository.GetById(story.RegisteredUser.Id).Value,
                     story.Location, story.SeenByUsers, story.TaggedUsers, story.HashTags, story.IsBanned).Value);
                    else
                        tempStories.Add(Core.Model.Story.Create(story.Id, story.ContentPath,
                    story.TimeStamp, story.Duration, story.Description, _userRepository.GetById(story.RegisteredUser.Id).Value,
                    story.Location, story.SeenByUsers, story.TaggedUsers, story.HashTags, story.IsBanned).Value);
                stories = tempStories;
            }
            if (!String.IsNullOrWhiteSpace(last24h) && last24h.Equals("true"))
            {
                var expiredStories = storyFactory.CreateStories(_stories.Find(story => story.TimeStamp < DateTime.Now.AddDays(-1)).ToList());
                foreach (var story in expiredStories) stories.Remove(story);
                var tempStories = new List<Core.Model.Story>(stories);
                if (!String.IsNullOrWhiteSpace(followingId) && !followingId.Equals(storyOwnerId))
                {
                    foreach (var story in tempStories)
                    {
                        var user = _userRepository.GetById(story.RegisteredUser.Id).Value;
                        if (story.GetType().Name == "CloseFriendStory"
                         && !userFactory.CreateIds(user.MyCloseFriends).Contains(followingId))
                            stories.Remove(story);
                    }
                }
                tempStories = new List<Core.Model.Story>(stories);
                tempStories = tempStories.Distinct().ToList();
                foreach (var story in tempStories) if (userFactory.CreateIds(story.RegisteredUser.BlockedUsers).Contains(followingId) ||
                        userFactory.CreateIds(story.RegisteredUser.BlockedByUsers).Contains(followingId))
                        stories.Remove(story);
            }
            if (!String.IsNullOrWhiteSpace(notLoggedIn) && notLoggedIn.Equals("true"))
            {
                var expiredStories = storyFactory.CreateStories(_stories.Find(story => story.TimeStamp < DateTime.Now.AddDays(-1)).ToList());
                foreach (var story in expiredStories) stories.Remove(story);
                var tempStories = new List<Core.Model.Story>(stories);

                foreach (var story in tempStories)
                {
                    if (story.GetType().Name == "CloseFriendStory")
                        stories.Remove(story);
                }
            }
            return stories;
        }

        public void BanStory(string id)
        {
            var story = GetById(new Guid(id)).Value;
            Story dto = storyFactory.Create(story);
            dto.IsBanned = true;
            _stories.ReplaceOne(story => story.Id == id, dto);
        }
    }
}