using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationMicroservice.Api.DTOs
{
    public class NotificationUsers
    {
        public RegisteredUser LoggedUser { get; set; }
        public List<RegisteredUser> RegisteredUsers { get; set; }
    }
}