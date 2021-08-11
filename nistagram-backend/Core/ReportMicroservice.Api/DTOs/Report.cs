using System;

namespace ReportMicroservice.Api.DTOs
{
    public class Report
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ReportReason { get; set; }
        public RegisteredUser RegisteredUser { get; set; }
        public Content Content { get; set; }
        public string Type { get; set; }
        public string ReportAction { get; set; }
    }
}