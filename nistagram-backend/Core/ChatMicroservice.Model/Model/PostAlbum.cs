using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace ChatMicroservice.Core.Model
{
    public class PostAlbum : Content
    {
        private readonly IEnumerable<ContentPath> contentPaths;
        private Description description;

        public PostAlbum(Guid id, IEnumerable<ContentPath> contentPaths, Description description) : base(id)
        {
            this.contentPaths = contentPaths;
            this.description = description;
        }

        public static Result<PostAlbum> Create(Guid id, IEnumerable<ContentPath> contentPaths, Description description)
        {
            return Result.Success(new PostAlbum(id, contentPaths, description));
        }
    }
}