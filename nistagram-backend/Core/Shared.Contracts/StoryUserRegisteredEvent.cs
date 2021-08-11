using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts
{
    public class StoryUserRegisteredEvent
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImagePath { get; set; }
        public Boolean IsPrivate { get; set; }
        public Boolean IsAcceptingTags { get; set; }
        public IEnumerable<StoryUserRegisteredEvent> Following { get; set; }
        public IEnumerable<StoryUserRegisteredEvent> Followers { get; set; }
    }
}