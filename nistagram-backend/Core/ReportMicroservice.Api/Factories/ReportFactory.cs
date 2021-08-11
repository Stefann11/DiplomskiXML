using ReportMicroservice.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ReportMicroservice.Api.Factories
{
    public class ReportFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public ReportFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public Report Create(Core.Model.Report report)
        {
            return new Report
            {
                Id = report.Id,
                TimeStamp = report.TimeStamp,
                ReportReason = report.ReportReason,
                RegisteredUser = registeredUserFactory.Create(report.RegisteredUser),
                Type = report.Content.GetType().Name,
                Content = new Content { Id = report.Content.Id },
                ReportAction = report.ReportAction
            };
        }

        public List<Report> CreateReports(IEnumerable<Core.Model.Report> reports)
        {
            return (from Core.Model.Report report in reports
                    select Create(report)).ToList();
        }
    }
}