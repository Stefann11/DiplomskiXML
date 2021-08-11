using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentApp.Api.DTOs
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
    }
}