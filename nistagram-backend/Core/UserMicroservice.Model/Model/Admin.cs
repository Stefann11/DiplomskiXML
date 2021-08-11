using CSharpFunctionalExtensions;
using System;

namespace UserMicroservice.Core.Model
{
    public class Admin : User
    {
        public Password Password { get; }

        private Admin(Guid id, Username username, EmailAddress emailAddress, Password password) : base(id, username, emailAddress)
        {
            Password = password;
        }

        public static Result<Admin> Create(Guid id, Username username, EmailAddress emailAddress, Password password)
        {
            return Result.Success(new Admin(id, username, emailAddress, password));
        }
    }
}