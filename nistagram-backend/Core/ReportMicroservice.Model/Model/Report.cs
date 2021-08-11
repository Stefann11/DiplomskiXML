using CSharpFunctionalExtensions;
using System;

namespace ReportMicroservice.Core.Model
{
    public class Report
    {
        public Guid Id { get; }
        public DateTime TimeStamp { get; }
        public ReportReason ReportReason { get; }
        public RegisteredUser RegisteredUser { get; }
        public Content Content { get; }
        public ReportAction ReportAction { get; }

        private Report(Guid id, DateTime timestamp, ReportReason reportReason, RegisteredUser registeredUser,
            Content content, ReportAction action)
        {
            Id = id;
            TimeStamp = timestamp;
            ReportReason = reportReason;
            RegisteredUser = registeredUser;
            Content = content;
            ReportAction = action;
        }

        public static Result<Report> Create(Guid id, DateTime timestamp, ReportReason reportReason,
            RegisteredUser registeredUser, Content content, ReportAction action)
        {
            return Result.Success(new Report(id, timestamp, reportReason, registeredUser, content, action));
        }
    }
}