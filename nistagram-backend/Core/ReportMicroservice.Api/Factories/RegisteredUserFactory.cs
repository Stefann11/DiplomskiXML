using ReportMicroservice.Api.DTOs;

namespace ReportMicroservice.Api.Factories
{
    public class RegisteredUserFactory
    {
        public RegisteredUser Create(Core.Model.RegisteredUser registeredUser)
        {
            return new RegisteredUser
            {
                Id = registeredUser.Id,
                Username = registeredUser.Username
            };
        }
    }
}