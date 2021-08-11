using CSharpFunctionalExtensions;
using System;

namespace ReportMicroservice.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; }
        public Username Username { get; }

        private RegisteredUser(Guid id, Username username)
        {
            Id = id;
            Username = username;
        }

        public static Result<RegisteredUser> Create(Guid id, Username username)
        {
            return Result.Success(new RegisteredUser(id, username));
        }
    }
}