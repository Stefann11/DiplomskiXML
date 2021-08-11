using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class PostAlbum : Post
    {
        private readonly List<ContentPath> contentPaths;
        public IEnumerable<ContentPath> ContentPaths => this.contentPaths;

        private PostAlbum(Guid id, DateTime timeStamp, Description description,
            RegisteredUser registeredUser, IEnumerable<RegisteredUser> likes,
            IEnumerable<RegisteredUser> dislikes, IEnumerable<Comment> comments,
            Location location, IEnumerable<RegisteredUser> taggedUsers, IEnumerable<HashTag> hashTags,
            bool isBanned, IEnumerable<ContentPath> contentPaths)
            : base(id, timeStamp, description, registeredUser, likes, dislikes, comments, location,
                  taggedUsers, hashTags, isBanned)
        {
            this.contentPaths = new List<ContentPath>(contentPaths);
        }

        public static Result<PostAlbum> Create(Guid id, DateTime timeStamp, Description description,
            RegisteredUser registeredUser, IEnumerable<RegisteredUser> likes,
            IEnumerable<RegisteredUser> dislikes, IEnumerable<Comment> comments,
            Location location, IEnumerable<RegisteredUser> taggedUsers, IEnumerable<HashTag> hashTags,
            bool isBanned, IEnumerable<ContentPath> contentPaths)
        {
            return Result.Success(new PostAlbum(id, timeStamp, description,
                                registeredUser, likes, dislikes, comments, location, taggedUsers, hashTags, isBanned, contentPaths));
        }
    }
}