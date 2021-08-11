using System;

namespace UserMicroservice.Core.Model
{
    public abstract class User
    {
        public Guid Id { get; }
        public Username Username { get; }
        public EmailAddress EmailAddress { get; }

        public User()
        {
        }

        public User(Guid id, Username username, EmailAddress emailAddress)
        {
            Id = id;
            Username = username;
            EmailAddress = emailAddress;
        }
    }
}