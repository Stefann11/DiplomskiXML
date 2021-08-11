using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class Comment
    {
        public Guid Id { get; }
        public DateTime TimeStamp { get; }
        public CommentText CommentText { get; }
        public RegisteredUser RegisteredUser { get; }
        public IEnumerable<RegisteredUser> TaggedUsers { get; }

        private Comment(Guid id, DateTime timeStamp, CommentText commentText, RegisteredUser registeredUser, IEnumerable<RegisteredUser> taggedUsers)
        {
            Id = id;
            TimeStamp = timeStamp;
            CommentText = commentText;
            RegisteredUser = registeredUser;
            TaggedUsers = taggedUsers;
        }

        public static Result<Comment> Create(Guid id, DateTime timeStamp, CommentText commentText, RegisteredUser registeredUser, IEnumerable<RegisteredUser> taggedUsers)
        {
            return Result.Success(new Comment(id, timeStamp, commentText, registeredUser, taggedUsers));
        }
    }
}