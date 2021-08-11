using CSharpFunctionalExtensions;
using System;

namespace CampaignMicroservice.Core.Model
{
    public class Story : Content
    {
        private Story(Guid id) : base(id)
        {
        }

        public static Result<Story> Create(Guid id)
        {
            return Result.Success(new Story(id));
        }
    }
}