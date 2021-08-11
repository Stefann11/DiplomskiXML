using System;
using System.Collections.Generic;

namespace StoryMicroservice.Core.DTOs
{
    public class Story
    {
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Description { get; set; }

        public RegisteredUser RegisteredUser { get; set; }

        public string Type { get; set; }

        public string ContentPath { get; set; }

        public int Duration { get; set; }

        public Location Location { get; set; }
        public List<RegisteredUser> SeenByUsers { get; set; }
        public List<RegisteredUser> TaggedUsers { get; set; }
        public List<string> HashTags { get; set; }
        public bool IsBanned { get; set; }
    }
}