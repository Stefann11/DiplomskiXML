using System;

namespace ReportMicroservice.Core.Model
{
    public abstract class Content
    {
        public Guid Id { get; }

        public Content(Guid id)
        {
            Id = id;
        }
    }
}