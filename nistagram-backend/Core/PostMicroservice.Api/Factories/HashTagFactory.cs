using PostMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace PostMicroservice.Api.Factories
{
    public class HashTagFactory
    {
        public HashTag Create(Core.Model.HashTag hashTag)
        {
            return new HashTag
            {
                HashTagText = hashTag.HashTagText
            };
        }

        public IEnumerable<HashTag> CreateHashTags(IEnumerable<Core.Model.HashTag> hashTags)
        {
            return hashTags.Select(hashTag => Create(hashTag)).ToList();
        }
    }
}