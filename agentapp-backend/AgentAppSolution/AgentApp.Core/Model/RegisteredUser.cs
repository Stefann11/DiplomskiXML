using CSharpFunctionalExtensions;
using System;

namespace AgentApp.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; }
        public Username Username { get; }
        public EmailAddress EmailAddress { get; }
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public DateTime DateOfBirth { get; }
        public PhoneNumber PhoneNumber { get; }
        public Gender Gender { get; }
        public WebsiteAddress WebsiteAddress { get; }
        public Bio Bio { get; }
        public Password Password { get; }
        public ProfilePicturePath ProfilePicturePath { get; }

        protected RegisteredUser(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Gender gender,
            WebsiteAddress websiteAddress, Bio bio, Password password, ProfilePicturePath profilePicturePath)
        {
            Id = id;
            Username = username;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Gender = gender;
            WebsiteAddress = websiteAddress;
            Bio = bio;
            Password = password;
            ProfilePicturePath = profilePicturePath;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber, Gender gender,
            WebsiteAddress websiteAddress, Bio bio, Password password, ProfilePicturePath profilePicturePath)
        {
            return Result.Success(new RegisteredUser(id, username, emailAddress, firstName,
            lastName, dateOfBirth, phoneNumber,
            gender, websiteAddress, bio, password,
            profilePicturePath));
        }
    }
}