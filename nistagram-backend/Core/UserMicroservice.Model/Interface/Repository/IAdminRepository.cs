using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.Core.Interface.Repository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Maybe<Admin> GetByUsername(String username);

        public Maybe<User> GetRoleByUsername(String username);
    }
}