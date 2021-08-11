using CSharpFunctionalExtensions;
using System;

namespace UserMicroservice.Core.Model
{
    public class VerificationRequest
    {
        public Guid Id { get; }
        public RegisteredUser RegisteredUser { get; }
        public VerificationRequestFirstName FirstName { get; }
        public VerificationRequestLastName LastName { get; }
        public Categories Category { get; }
        public DocumentImagePath DocumentImagePath { get; }
        public bool IsApproved { get; }

        private VerificationRequest(Guid id, RegisteredUser registeredUser, VerificationRequestFirstName firstName, VerificationRequestLastName lastName,
            Categories category, DocumentImagePath documentImagePath, bool isApproved)
        {
            Id = id;
            RegisteredUser = registeredUser;
            FirstName = firstName;
            LastName = lastName;
            Category = category;
            DocumentImagePath = documentImagePath;
            IsApproved = isApproved;
        }

        public static Result<VerificationRequest> Create(Guid id, RegisteredUser registeredUser, VerificationRequestFirstName firstName,
            VerificationRequestLastName lastName, Categories category, DocumentImagePath documentImagePath, bool isApproved)
        {
            return Result.Success(new VerificationRequest(id, registeredUser, firstName, lastName, category, documentImagePath, isApproved));
        }
    }
}