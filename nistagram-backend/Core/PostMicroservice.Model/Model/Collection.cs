using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class Collection
    {
        public Guid Id { get; }
        public CollectionName CollectionName { get; }
        public IEnumerable<Post> Posts { get; }
        public RegisteredUser RegisteredUser { get; }

        public Collection(Guid id, CollectionName collectionName, IEnumerable<Post> posts,
            RegisteredUser registeredUser)
        {
            Id = id;
            CollectionName = collectionName;
            Posts = posts;
            RegisteredUser = registeredUser;
        }

        public static Result<Collection> Create(Guid id, CollectionName collectionName, IEnumerable<Post> posts,
            RegisteredUser registeredUser)
        {
            return Result.Success(new Collection(id, collectionName, posts, registeredUser));
        }
    }
}