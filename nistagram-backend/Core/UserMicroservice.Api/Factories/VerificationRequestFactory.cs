using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class VerificationRequestFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory = new RegisteredUserFactory();

        public VerificationRequest Create(Core.Model.VerificationRequest verificationRequest)
        {
            return new VerificationRequest()
            {
                Id = verificationRequest.Id,
                FirstName = verificationRequest.FirstName,
                LastName = verificationRequest.LastName,
                RegisteredUserId = verificationRequest.RegisteredUser.Id,
                Category = verificationRequest.Category.ToString(),
                DocumentImagePath = verificationRequest.DocumentImagePath,
                IsApproved = verificationRequest.IsApproved
            };
        }
    }
}