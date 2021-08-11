using System;
using System.Collections.Generic;

namespace PostMicroservice.Api.DTOs
{
    public class PostAlbum : Post
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public IEnumerable<RegisteredUser> Likes { get; set; }
        public IEnumerable<RegisteredUser> Dislikes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Location Location { get; set; }
        public IEnumerable<RegisteredUser> TaggedUsers { get; set; }
        public IEnumerable<HashTag> HashTags { get; set; }
        public bool IsBanned { get; set; }
        public IEnumerable<string> ContentPaths { get; set; }
    }
}