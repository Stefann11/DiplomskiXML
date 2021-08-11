using System;
using System.Collections.Generic;

namespace PostMicroservice.Api.DTOs
{
    public class Collection
    {
        public Guid Id { get; set; }
        public string CollectionName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
    }
}