using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.Core.Interface
{
    public interface ISeenByRepository
    {
        public void Save(Guid exposureDateId, Guid registeredUserId);
    }
}