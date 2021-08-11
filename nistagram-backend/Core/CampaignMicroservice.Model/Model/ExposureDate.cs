using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Model
{
    public class ExposureDate
    {
        public Guid Id { get; }
        public DateTime Time { get; }
        public IEnumerable<RegisteredUser> SeenBy { get; }

        private ExposureDate(Guid id, DateTime time, IEnumerable<RegisteredUser> seenBy)
        {
            Id = id;
            Time = time;
            SeenBy = seenBy;
        }

        public static Result<ExposureDate> Create(Guid id, DateTime time, IEnumerable<RegisteredUser> seenBy)
        {
            return new ExposureDate(id, time, seenBy);
        }
    }
}