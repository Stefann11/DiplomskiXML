using StoryMicroservice.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace StoryMicroservice.DataAccess.Factories
{
    public class HashTagFactory
    {
        public string Create(Core.Model.HashTag hashTag)
        {
            return hashTag.HashTagText;
        }

        public Core.Model.HashTag Create(string hashTag)
        {
            return Core.Model.HashTag.Create(HashTagText.Create(hashTag).Value).Value;
        }

        public List<string> CreateHashTags(IEnumerable<Core.Model.HashTag> hashTags)
        {
            return hashTags.Select(hashTag => Create(hashTag)).ToList();
        }

        public IEnumerable<Core.Model.HashTag> CreateHashTags(List<string> hashTags)
        {
            return hashTags.Select(hashTag => Create(hashTag)).ToList();
        }
    }
}