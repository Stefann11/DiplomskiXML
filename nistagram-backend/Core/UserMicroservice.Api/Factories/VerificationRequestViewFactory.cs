using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class VerificationRequestViewFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory = new RegisteredUserFactory();

        public VerificationRequestView Create(Core.Model.VerificationRequest verificationRequest)
        {
            return new VerificationRequestView()
            {
                Id = verificationRequest.Id,
                FirstName = verificationRequest.FirstName,
                LastName = verificationRequest.LastName,
                RegisteredUser = registeredUserFactory.Create(verificationRequest.RegisteredUser),
                Category = verificationRequest.Category.ToString(),
                DocumentImagePath = verificationRequest.DocumentImagePath,
                IsApproved = verificationRequest.IsApproved
            };
        }
    }
}