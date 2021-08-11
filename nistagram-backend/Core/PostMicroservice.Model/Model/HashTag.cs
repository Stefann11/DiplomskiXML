using CSharpFunctionalExtensions;

namespace PostMicroservice.Core.Model
{
    public class HashTag
    {
        public HashTagText HashTagText { get; }

        private HashTag(HashTagText hashTagText)
        {
            this.HashTagText = hashTagText;
        }

        public static Result<HashTag> Create(HashTagText hashTagText)
        {
            return Result.Success(new HashTag(hashTagText));
        }
    }
}