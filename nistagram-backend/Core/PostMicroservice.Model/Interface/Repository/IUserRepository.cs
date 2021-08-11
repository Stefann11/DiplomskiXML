using CSharpFunctionalExtensions;
using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Interface.Repository
{
    public interface IUserRepository : IRepository<RegisteredUser>
    {
        public IEnumerable<RegisteredUser> GetTaggable();

        public void AddProfilePicture(Guid id, string image);

        public Maybe<RegisteredUser> GetByUsername(String username);
    }
}