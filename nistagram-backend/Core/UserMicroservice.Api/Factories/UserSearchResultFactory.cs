using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class UserSearchResultFactory
    {
        public UserSearchResult Create(Core.Model.RegisteredUser registeredUser)
        {
            return new UserSearchResult()
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username,
                EmailAddress = registeredUser.EmailAddress,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                DateOfBirth = registeredUser.DateOfBirth,
                PhoneNumber = registeredUser.PhoneNumber,
                Gender = registeredUser.Gender,
                WebsiteAddress = registeredUser.WebsiteAddress,
                Bio = registeredUser.Bio,
                Password = registeredUser.Password,
                IsPrivate = registeredUser.IsPrivate,
                IsAcceptingMessages = registeredUser.IsAcceptingMessages,
                IsAcceptingTags = registeredUser.IsAcceptingTags
            };
        }
    }
}