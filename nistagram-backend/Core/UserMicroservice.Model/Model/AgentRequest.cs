using CSharpFunctionalExtensions;
using System;

namespace UserMicroservice.Core.Model
{
    public class AgentRequest
    {
        public Guid Id { get; }
        public bool IsApproved { get; }
        public RegisteredUser RegisteredUser { get; }
        public AgentRequestAction AgentRequestAction { get; }
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
        public bool IsPrivate { get; }
        public bool IsAcceptingMessages { get; }
        public bool IsAcceptingTags { get; }

        private AgentRequest(Guid id, bool isApproved, RegisteredUser registeredUser, AgentRequestAction agentRequestAction,
            Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, bool isPrivate, bool isAcceptingMessages, bool isAcceptingTags, Password password)
        {
            Id = id;
            IsApproved = isApproved;
            RegisteredUser = registeredUser;
            AgentRequestAction = agentRequestAction;
            Username = username;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Gender = gender;
            WebsiteAddress = websiteAddress;
            Bio = bio;
            IsPrivate = isPrivate;
            IsAcceptingMessages = isAcceptingMessages;
            IsAcceptingTags = isAcceptingTags;
            Password = password;
        }

        public static Result<AgentRequest> Create(Guid id, bool isApproved, RegisteredUser registeredUser,
            AgentRequestAction agentRequestAction, Username username, EmailAddress emailAddress, FirstName firstName,
            LastName lastName, DateTime dateOfBirth, PhoneNumber phoneNumber,
            Gender gender, WebsiteAddress websiteAddress, Bio bio, bool isPrivate, bool isAcceptingMessages, bool isAcceptingTags, Password password)
        {
            return Result.Success(new AgentRequest(id, isApproved, registeredUser, agentRequestAction, username, emailAddress, firstName,
            lastName, dateOfBirth, phoneNumber,
            gender, websiteAddress, bio, isPrivate, isAcceptingMessages, isAcceptingTags, password));
        }
    }
}