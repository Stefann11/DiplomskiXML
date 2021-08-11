using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Model
{
    public class ProfileImagePath : ValueObject
    {
        private readonly string path;

        private ProfileImagePath(string path)
        {
            this.path = path;
        }

        public static Result<ProfileImagePath> Create(string path)
        {
            //if (!File.Exists(path)) return Result.Failure<ProfileImagePath>("Image does not exist on given path");
            return Result.Success(new ProfileImagePath(path));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return path;
        }

        public override string ToString()
        {
            return this.path;
        }

        public static implicit operator string(ProfileImagePath profileImagePath) => profileImagePath.path;
    }
}