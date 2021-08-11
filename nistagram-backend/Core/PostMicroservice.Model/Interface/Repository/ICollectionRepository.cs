using CSharpFunctionalExtensions;
using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Interface.Repository
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        public IEnumerable<Collection> GetByUserId(Guid userid);

        public Result AddPostToCollection(Guid id, Guid postId);
    }
}