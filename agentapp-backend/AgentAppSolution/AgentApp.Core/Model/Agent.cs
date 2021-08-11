using CSharpFunctionalExtensions;
using System;

namespace AgentApp.Core.Model
{
    public class Agent : RegisteredUser
    {
        private Agent(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, Password password,
            ProfilePicturePath profilePicturePath)
            : base(id, username, emailAddress, firstName,
                   lastName, dateOfBirth, phoneNumber,
                   gender, websiteAddress, bio, password,
                   profilePicturePath)
        {
        }

        public static new Result<Agent> Create(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, Password password,
            ProfilePicturePath profilePicturePath)
        {
            return Result.Success(new Agent(id, username, emailAddress, firstName,
            lastName, dateOfBirth, phoneNumber,
            gender, websiteAddress, bio, password,
            profilePicturePath));
        }
    }
}