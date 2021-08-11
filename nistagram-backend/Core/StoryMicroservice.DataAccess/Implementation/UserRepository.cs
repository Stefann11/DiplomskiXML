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
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<RegisteredUser> _users;
        private readonly RegisteredUserFactory userFactory;

        public UserRepository(IStoryDatabaseSettings settings, RegisteredUserFactory userFactory)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<RegisteredUser>(settings.RegisteredUsersCollectionName);
            this.userFactory = userFactory;
        }

        public void Delete(Guid id)
        {
            _users.DeleteOne(user => user.Id.Equals(id.ToString()));
        }

        public Core.Model.RegisteredUser Edit(string id, Core.Model.RegisteredUser userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userFactory.Create(userIn));
            return userIn;
        }

        public IEnumerable<Core.Model.RegisteredUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<Core.Model.RegisteredUser> GetById(Guid id)
        {
            var userDTO = _users.Find<RegisteredUser>(user => user.Id.Equals(id)).FirstOrDefault();
            if (userDTO == null) return Maybe<Core.Model.RegisteredUser>.None;
            var blockedByUsers = GetUsersById(userDTO.BlockedByUsers);
            var blockedUsers = GetUsersById(userDTO.BlockedUsers);
            var followers = GetUsersById(userDTO.Followers);
            var following = GetUsersById(userDTO.Following);
            var closeFriendTo = GetUsersById(userDTO.CloseFriendTo);
            var myCloseFriends = GetUsersById(userDTO.MyCloseFriends);
            return userFactory.Create(userDTO,
                blockedUsers, blockedByUsers, followers, following, closeFriendTo, myCloseFriends);
        }

        public Core.Model.RegisteredUser Save(Core.Model.RegisteredUser user)
        {
            _users.InsertOne(userFactory.Create(user));
            return user;
        }

        public IEnumerable<Core.Model.RegisteredUser> GetUsersById(List<string> ids)
        {
            return userFactory.CreateUsers(_users.Find(user => ids.Contains(user.Id)).ToList());
        }

        public IEnumerable<Core.Model.RegisteredUser> GetUsersByDTO(List<RegisteredUser> users)
        {
            var test = users.Select(user => user.Id);
            return userFactory.CreateUsers(_users.Find(user => test.Contains(user.Id)).ToList());
        }

        public IEnumerable<Core.Model.RegisteredUser> GetBy(string isTaggable)
        {
            List<Core.Model.RegisteredUser> result = new List<Core.Model.RegisteredUser>();
            if (!String.IsNullOrWhiteSpace(isTaggable))
            {
                if (isTaggable.Equals("true"))
                {
                    result = userFactory.CreateUsers(_users.Find(user => user.IsAcceptingTags == true).ToList()).ToList();
                }
            }
            return result;
        }

        public Maybe<Core.Model.RegisteredUser> GetByUsername(string username)
        {
            var userDTO = _users.Find<RegisteredUser>(user => user.Username.Equals(username)).FirstOrDefault();
            if (userDTO == null) return Maybe<Core.Model.RegisteredUser>.None;
            var blockedByUsers = GetUsersById(userDTO.BlockedByUsers);
            var blockedUsers = GetUsersById(userDTO.BlockedUsers);
            var followers = GetUsersById(userDTO.Followers);
            var following = GetUsersById(userDTO.Following);
            var closeFriendTo = GetUsersById(userDTO.CloseFriendTo);
            var myCloseFriends = GetUsersById(userDTO.MyCloseFriends);
            return userFactory.Create(userDTO,
                blockedUsers, blockedByUsers, followers, following, closeFriendTo, myCloseFriends);
        }

        public void Follow(Guid followedById, Guid followingId)
        {
            Core.Model.RegisteredUser followedUser = GetById(followingId).Value;
            Core.Model.RegisteredUser followingUser = GetById(followedById).Value;

            var userDTO = _users.Find<RegisteredUser>(user => user.Id.Equals(followedUser.Id.ToString())).FirstOrDefault();
            var blockedByUsers = GetUsersById(userDTO.BlockedByUsers);
            var blockedUsers = GetUsersById(userDTO.BlockedUsers);
            List<Core.Model.RegisteredUser> followers = followedUser.Followers.ToList();
            followers.Add(followingUser);
            var following = GetUsersById(userDTO.Following);
            var closeFriendTo = GetUsersById(userDTO.CloseFriendTo);
            var myCloseFriends = GetUsersById(userDTO.MyCloseFriends);
            Core.Model.RegisteredUser updatedFollowedUser = userFactory.Create(userDTO,
                blockedUsers, blockedByUsers, followers, following, closeFriendTo, myCloseFriends);

            var userDTO2 = _users.Find<RegisteredUser>(user => user.Id.Equals(followingUser.Id.ToString())).FirstOrDefault();
            var blockedByUsers2 = GetUsersById(userDTO2.BlockedByUsers);
            var blockedUsers2 = GetUsersById(userDTO2.BlockedUsers);
            var followers2 = GetUsersById(userDTO2.Followers);
            List<Core.Model.RegisteredUser> following2 = followingUser.Following.ToList();
            following2.Add(updatedFollowedUser);
            var closeFriendTo2 = GetUsersById(userDTO2.CloseFriendTo);
            var myCloseFriends2 = GetUsersById(userDTO2.MyCloseFriends);
            Core.Model.RegisteredUser updatedFollowingUser = userFactory.Create(userDTO2,
                blockedUsers2, blockedByUsers2, followers2, following2, closeFriendTo2, myCloseFriends2);

            Edit(followingId.ToString(), updatedFollowedUser);
            Edit(followedById.ToString(), updatedFollowingUser);
        }

        public void DeleteFollow(Guid followedById, Guid followingId)
        {
            Core.Model.RegisteredUser followedUser = GetById(followingId).Value;
            Core.Model.RegisteredUser followingUser = GetById(followedById).Value;

            var userDTO = _users.Find<RegisteredUser>(user => user.Id.Equals(followedUser.Id.ToString())).FirstOrDefault();
            var blockedByUsers = GetUsersById(userDTO.BlockedByUsers);
            var blockedUsers = GetUsersById(userDTO.BlockedUsers);
            List<Core.Model.RegisteredUser> followers = followedUser.Followers.ToList();
            if (followers.Contains(followingUser)) followers.Remove(followingUser);
            List<Core.Model.RegisteredUser> following = followedUser.Following.ToList();
            if (following.Contains(followingUser)) following.Remove(followingUser);
            var closeFriendTo = GetUsersById(userDTO.CloseFriendTo);
            var myCloseFriends = GetUsersById(userDTO.MyCloseFriends);
            Core.Model.RegisteredUser updatedFollowedUser = userFactory.Create(userDTO,
                blockedUsers, blockedByUsers, followers, following, closeFriendTo, myCloseFriends);

            var userDTO2 = _users.Find<RegisteredUser>(user => user.Id.Equals(followingUser.Id.ToString())).FirstOrDefault();
            var blockedByUsers2 = GetUsersById(userDTO2.BlockedByUsers);
            var blockedUsers2 = GetUsersById(userDTO2.BlockedUsers);
            List<Core.Model.RegisteredUser> followers2 = followingUser.Followers.ToList();
            if (followers2.Contains(followedUser)) followers2.Remove(followedUser);
            List<Core.Model.RegisteredUser> following2 = followingUser.Following.ToList();
            if (following2.Contains(followedUser)) following2.Remove(followedUser);
            var closeFriendTo2 = GetUsersById(userDTO2.CloseFriendTo);
            var myCloseFriends2 = GetUsersById(userDTO2.MyCloseFriends);
            Core.Model.RegisteredUser updatedFollowingUser = userFactory.Create(userDTO2,
                blockedUsers2, blockedByUsers2, followers2, following2, closeFriendTo2, myCloseFriends2);

            Edit(followingId.ToString(), updatedFollowedUser);
            Edit(followedById.ToString(), updatedFollowingUser);
        }

        public void Block(Guid blockedById, Guid blockingId)
        {
            Core.Model.RegisteredUser blockedUser = GetById(blockingId).Value;
            Core.Model.RegisteredUser blockedByUser = GetById(blockedById).Value;

            var userDTO = _users.Find<RegisteredUser>(user => user.Id.Equals(blockedByUser.Id.ToString())).FirstOrDefault();
            var blockedByUsers = GetUsersById(userDTO.BlockedByUsers);
            List<Core.Model.RegisteredUser> blockedUsers = blockedByUser.BlockedUsers.ToList();
            blockedUsers.Add(blockedUser);
            var followers = GetUsersById(userDTO.Followers);
            var following = GetUsersById(userDTO.Following);
            var closeFriendTo = GetUsersById(userDTO.CloseFriendTo);
            var myCloseFriends = GetUsersById(userDTO.MyCloseFriends);
            Core.Model.RegisteredUser updatedBlockedByUser = userFactory.Create(userDTO,
                blockedUsers, blockedByUsers, followers, following, closeFriendTo, myCloseFriends);

            var userDTO2 = _users.Find<RegisteredUser>(user => user.Id.Equals(blockedUser.Id.ToString())).FirstOrDefault();
            List<Core.Model.RegisteredUser> blockedByUsers2 = blockedUser.BlockedByUsers.ToList();
            blockedByUsers2.Add(updatedBlockedByUser);
            var blockedUsers2 = GetUsersById(userDTO2.BlockedUsers);
            var followers2 = GetUsersById(userDTO2.Followers);
            var following2 = GetUsersById(userDTO2.Following);

            var closeFriendTo2 = GetUsersById(userDTO2.CloseFriendTo);
            var myCloseFriends2 = GetUsersById(userDTO2.MyCloseFriends);
            Core.Model.RegisteredUser updatedBlockedUser = userFactory.Create(userDTO2,
                blockedUsers2, blockedByUsers2, followers2, following2, closeFriendTo2, myCloseFriends2);

            Edit(blockedById.ToString(), updatedBlockedByUser);
            Edit(blockingId.ToString(), updatedBlockedUser);
        }

        public bool AlreadyFollows(Guid followedById, Guid followingId)
        {
            Core.Model.RegisteredUser followedUser = GetById(followingId).Value;
            Core.Model.RegisteredUser followingUser = GetById(followedById).Value;
            if (followedUser.Followers.ToList().Contains(followingUser)) return true;
            if (followingUser.Following.ToList().Contains(followedUser)) return true;
            return false;
        }
    }
}