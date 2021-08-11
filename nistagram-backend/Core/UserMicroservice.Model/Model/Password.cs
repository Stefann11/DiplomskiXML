using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Core.Model
{
    public class Password : ValueObject
    {
        private readonly string password;

        private Password(string password)
        {
            this.password = password;
        }

        public static Result<Password> Create(string password)
        {
            if (password.Length > 50) return Result.Failure<Password>("Password cannot contain more than 50 characters");
            if (password.Length < 5) return Result.Failure<Password>("Password cannot contain less than 5 characters");
            return Result.Success(new Password(password));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return password;
        }

        public override string ToString()
        {
            return this.password;
        }

        public static implicit operator string(Password password) => password.password;
    }
}