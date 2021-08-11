using System;

namespace NotificationMicroservice.Core.Model
{
    public class RegisteredUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicturePath { get; set; }

        public RegisteredUser()
        {
        }

        public RegisteredUser(Guid id, string username, string profilePicturePath)
        {
            Id = id;
            Username = username;
            ProfilePicturePath = profilePicturePath;
        }
    }
}