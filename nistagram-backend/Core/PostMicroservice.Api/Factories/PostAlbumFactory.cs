using PostMicroservice.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Factories
{
    public class PostAlbumFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;
        private readonly LocationFactory locationFactory;
        private readonly CommentFactory commentFactory;
        private readonly HashTagFactory hashTagFactory;

        public PostAlbumFactory(RegisteredUserFactory registeredUserFactory, LocationFactory locationFactory,
            CommentFactory commentFactory, HashTagFactory hashTagFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
            this.locationFactory = locationFactory;
            this.commentFactory = commentFactory;
            this.hashTagFactory = hashTagFactory;
        }

        public PostAlbum Create(Core.Model.PostAlbum post)
        {
            return new PostAlbum
            {
                Id = post.Id,
                TimeStamp = post.TimeStamp,
                Description = post.Description,
                RegisteredUser = registeredUserFactory.Create(post.RegisteredUser),
                Likes = registeredUserFactory.CreateUsers(post.Likes),
                Dislikes = registeredUserFactory.CreateUsers(post.Dislikes),
                Comments = commentFactory.CreateComments(post.Comments),
                Location = locationFactory.Create(post.Location),
                TaggedUsers = registeredUserFactory.CreateUsers(post.TaggedUsers),
                HashTags = hashTagFactory.CreateHashTags(post.HashTags),
                IsBanned = post.IsBanned,
                ContentPaths = Convert(post.ContentPaths)
            };
        }

        private IEnumerable<string> Convert(IEnumerable<Core.Model.ContentPath> contentPaths)
        {
            return (from Core.Model.ContentPath contentPath in contentPaths
                    select contentPath.ToString()).ToList();
        }
    }
}