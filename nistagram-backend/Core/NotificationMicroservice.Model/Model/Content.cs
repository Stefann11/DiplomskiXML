using System;

namespace NotificationMicroservice.Core.Model
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public Content()
        {
        }

        public Content(Guid id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}