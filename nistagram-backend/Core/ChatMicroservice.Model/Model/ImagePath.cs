using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.IO;

namespace ChatMicroservice.Core.Model
{
    public class ImagePath : ValueObject
    {
        private readonly string path;

        private ImagePath(string path)
        {
            this.path = path;
        }

        public static Result<ImagePath> Create(string path)
        {
            if (!File.Exists(path)) return Result.Failure<ImagePath>("File does not exist on given path");
            return Result.Success(new ImagePath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public static implicit operator string(ImagePath imagePath) => imagePath.path;
    }
}