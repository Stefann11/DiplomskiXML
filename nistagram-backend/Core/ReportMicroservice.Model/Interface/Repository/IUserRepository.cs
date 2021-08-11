using CSharpFunctionalExtensions;
using ReportMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportMicroservice.Core.Interface.Repository
{
    public interface IUserRepository : IRepository<RegisteredUser>
    {
        public Maybe<RegisteredUser> GetByUsername(String username);
    }
}