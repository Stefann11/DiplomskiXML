using PostMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Factories
{
    public class CommentFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public CommentFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public Comment Create(Core.Model.Comment comment)
        {
            return new Comment
            {
                Id = comment.Id,
                TimeStamp = comment.TimeStamp,
                CommentText = comment.CommentText,
                RegisteredUser = registeredUserFactory.Create(comment.RegisteredUser),
                TaggedUsers = Convert(comment.TaggedUsers),
            };
        }

        public IEnumerable<Comment> CreateComments(IEnumerable<Core.Model.Comment> comments)
        {
            return comments.Select(comment => Create(comment)).ToList();
        }

        private IEnumerable<RegisteredUser> Convert(IEnumerable<Core.Model.RegisteredUser> users)
        {
            return users.Select(registeredUser => new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                ProfileImagePath = registeredUser.ProfileImagePath
            }).ToList();
        }
    }
}