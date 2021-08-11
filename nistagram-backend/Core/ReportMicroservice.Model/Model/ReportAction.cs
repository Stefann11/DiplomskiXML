using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportMicroservice.Core.Model
{
    public class ReportAction : ValueObject
    {
        private readonly string name;

        private ReportAction(string name)
        {
            this.name = name;
        }

        public static Result<ReportAction> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return Result.Failure<ReportAction>("Action cannot be empty, or contain only white spaces");
            if (name.Length > 250) return Result.Failure<ReportAction>("Action cannot contain more than 50 characters");
            return Result.Success(new ReportAction(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static implicit operator string(ReportAction username) => username.name;
    }
}