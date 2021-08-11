using CSharpFunctionalExtensions;
using StoryMicroservice.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.Interface.Repository
{
    public interface IUserRepository : IRepository<Core.Model.RegisteredUser>
    {
        public IEnumerable<Core.Model.RegisteredUser> GetUsersById(List<string> ids);

        public IEnumerable<Core.Model.RegisteredUser> GetUsersByDTO(List<RegisteredUser> users);

        public IEnumerable<Core.Model.RegisteredUser> GetBy(string isTaggable);

        public Maybe<Core.Model.RegisteredUser> GetByUsername(string username);

        public void Follow(Guid followedById, Guid followingId);

        public bool AlreadyFollows(Guid followedById, Guid followingId);

        public void Block(Guid blockedById, Guid blockingId);

        public void DeleteFollow(Guid followedById, Guid followingId);
    }
}