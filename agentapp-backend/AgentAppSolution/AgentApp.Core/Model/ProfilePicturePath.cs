using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace AgentApp.Core.Model
{
    public class ProfilePicturePath : ValueObject
    {
        private readonly string path;

        private ProfilePicturePath(string path)
        {
            this.path = path;
        }

        public static Result<ProfilePicturePath> Create(string path)
        {
            //if (!File.Exists(path)) return Result.Failure<ProfileImagePath>("Image does not exist on given path");
            return Result.Success(new ProfilePicturePath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public override string ToString()
        {
            return this.path;
        }

        public static implicit operator string(ProfilePicturePath profileImagePath) => profileImagePath.path;
    }
}