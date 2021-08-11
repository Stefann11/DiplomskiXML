using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.IO;

namespace PostMicroservice.Core.Model
{
    public class ContentPath : ValueObject
    {
        private readonly string path;

        private ContentPath(string path)
        {
            this.path = path;
        }

        public static Result<ContentPath> Create(string path)
        {
            //if (!File.Exists(path)) return Result.Failure<ContentPath>("File does not exist on given path");
            return Result.Success(new ContentPath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public override string ToString()
        {
            return this.path;
        }

        public static implicit operator string(ContentPath contentPath) => contentPath.path;
    }
}