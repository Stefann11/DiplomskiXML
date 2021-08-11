using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class ItemImagePath : ValueObject
    {
        private readonly string path;

        private ItemImagePath(string path)
        {
            this.path = path;
        }

        public static Result<ItemImagePath> Create(string path)
        {
            //if (!File.Exists(path)) return Result.Failure<ProfileImagePath>("Image does not exist on given path");
            return Result.Success(new ItemImagePath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public override string ToString()
        {
            return this.path;
        }

        public static implicit operator string(ItemImagePath profileImagePath) => profileImagePath.path;
    }
}