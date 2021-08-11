using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace ReportMicroservice.Core.Model
{
    public class ReportReason : ValueObject
    {
        private readonly string reason;

        private ReportReason(string reason)
        {
            this.reason = reason;
        }

        public static Result<ReportReason> Create(string reason)
        {
            if (String.IsNullOrWhiteSpace(reason)) return Result.Failure<ReportReason>("Reason for reporting cannot be empty.");
            if (reason.Length > 250) return Result.Failure<ReportReason>("Reason for reporting cannot contain more than 250 characters.");
            return Result.Success(new ReportReason(reason));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return reason;
        }

        public override string ToString()
        {
            return this.reason;
        }

        public static implicit operator string(ReportReason reportReason) => reportReason.reason;
    }
}