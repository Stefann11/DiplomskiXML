using PostMicroservice.Api.DTOs;
using System.Collections.Generic;

namespace PostMicroservice.Api.Factories
{
    public class PostSingleFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;
        private readonly LocationFactory locationFactory;
        private readonly CommentFactory commentFactory;
        private readonly HashTagFactory hashTagFactory;

        public PostSingleFactory(RegisteredUserFactory registeredUserFactory, LocationFactory locationFactory,
            CommentFactory commentFactory, HashTagFactory hashTagFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
            this.locationFactory = locationFactory;
            this.commentFactory = commentFactory;
            this.hashTagFactory = hashTagFactory;
        }

        public PostSingle Create(Core.Model.PostSingle post)
        {
            return new PostSingle
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
                ContentPath = post.ContentPath
            };
        }
    }
}