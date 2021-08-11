using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.Core.Interface.Repository
{
    public interface IVerificationRequestRepository : IRepository<VerificationRequest>
    {
        public IEnumerable<VerificationRequest> GetBy(string isApproved);
    }
}