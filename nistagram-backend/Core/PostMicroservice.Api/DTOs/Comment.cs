using System;
using System.Collections.Generic;

namespace PostMicroservice.Api.DTOs
{
    public class Comment
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CommentText { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public IEnumerable<RegisteredUser> TaggedUsers { get; set; }
    }
}