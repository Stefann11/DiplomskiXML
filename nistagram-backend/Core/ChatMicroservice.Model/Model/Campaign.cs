using CSharpFunctionalExtensions;
using System;

namespace ChatMicroservice.Core.Model
{
    internal class Campaign : Content
    {
        private Campaign(Guid id) : base(id)
        {
        }

        public static Result<Campaign> Create(Guid id)
        {
            return Result.Success(new Campaign(id));
        }
    }
}