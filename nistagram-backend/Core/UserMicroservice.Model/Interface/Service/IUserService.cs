using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.Core.Interface.Service
{
    public interface IUserService : IService<RegisteredUser>
    {
        public Result Create(RegisteredUser registeredUser);
    }
}