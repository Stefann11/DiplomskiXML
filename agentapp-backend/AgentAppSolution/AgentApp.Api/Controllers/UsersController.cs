using AgentApp.Api.DTOs;
using AgentApp.Core.Interface.Repository;
using AgentApp.Core.Model;
using AgentApp.Core.Services;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _env;

        public UsersController(UserService userService, IUserRepository userRepository,
            IWebHostEnvironment env)
        {
            this.userService = userService;
            _userRepository = userRepository;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(DTOs.RegisteredUser dto)
        {
            Guid id = Guid.NewGuid();

            Result<Username> username = Username.Create(dto.Username);
            Result<EmailAddress> emailAddress = EmailAddress.Create(dto.EmailAddress);
            Result<FirstName> firstName = FirstName.Create(dto.FirstName);
            Result<LastName> lastName = LastName.Create(dto.LastName);
            Result<PhoneNumber> phoneNumber = PhoneNumber.Create(dto.PhoneNumber);
            Result<Gender> gender = Gender.Create(dto.Gender);
            Result<WebsiteAddress> websiteAddress = WebsiteAddress.Create(dto.WebsiteAddress);
            Result<Bio> bio = Bio.Create(dto.Bio);
            Result<Password> password = Password.Create(dto.Password);

            Result result = Result.Combine(username, emailAddress, firstName, lastName, phoneNumber, gender, websiteAddress, bio, password);
            if (result.IsFailure) return BadRequest();

            Result registrationResult = userService.Create(Core.Model.RegisteredUser
                               .Create(id,
                                  username.Value,
                                  emailAddress.Value,
                                  firstName.Value,
                                  lastName.Value,
                                  dto.DateOfBirth,
                                  phoneNumber.Value,
                                  gender.Value,
                                  websiteAddress.Value,
                                  bio.Value,
                                  password.Value,
                                  ProfilePicturePath.Create("").Value).Value);

            if (registrationResult.IsFailure) return BadRequest();
            dto.Id = id;
            return Ok(/*this.Request.Path + id, ""*/ dto);
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = userService.FindUser(login.Username, login.Password);
            if (user != null)
            {
                var tokenString = userService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }
    }
}