using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Api.Factories;
using PostMicroservice.Core.Model;
using PostMicroservice.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        private readonly RegisteredUserFactory registeredUserFactory;

        public UsersController(UserService userService, RegisteredUserFactory registeredUserFactory)
        {
            this.userService = userService;
            this.registeredUserFactory = registeredUserFactory;
        }

        [HttpGet]
        public IActionResult GetTaggable([FromQuery] bool isTaggable)
        {
            return Ok(userService.GetTaggable().ToList().
                Select(user => registeredUserFactory.Create(user)));
        }

        [HttpPost]
        public IActionResult RegisterUser(DTOs.RegisteredUser dto)
        {
            Guid id = dto.Id;

            Result<Username> username = Username.Create(dto.Username);
            Result<FirstName> firstName = FirstName.Create(dto.FirstName);
            Result<LastName> lastName = LastName.Create(dto.LastName);
            Result<ProfileImagePath> profileImagePath = ProfileImagePath.Create("");// ovde treba profilna slika
            Result result = Result.Combine(username, firstName, lastName, profileImagePath);
            if (result.IsFailure) return BadRequest();

            if (userService.Create(Core.Model.RegisteredUser
                                       .Create(dto.Id,
                                          username.Value,
                                          firstName.Value,
                                          lastName.Value,
                                          profileImagePath.Value,
                                          dto.isPrivate,
                                          dto.isAcceptingTags,
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>()).Value).IsFailure) return BadRequest();
            return Created(this.Request.Path + id, "");
        }

        [HttpPut("edit")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Edit(DTOs.RegisteredUser dto)
        {
            Result<Username> username = Username.Create(dto.Username);
            Result<FirstName> firstName = FirstName.Create(dto.FirstName);
            Result<LastName> lastName = LastName.Create(dto.LastName);
            Result<ProfileImagePath> profileImagePath = ProfileImagePath.Create("");

            Result result = Result.Combine(username, firstName, lastName);
            if (result.IsFailure) return BadRequest(result.Error);

            return Ok(userService.Edit((Core.Model.RegisteredUser.Create(dto.Id,
                                          username.Value,
                                          firstName.Value,
                                          lastName.Value,
                                          profileImagePath.Value,
                                          dto.isPrivate,
                                          dto.isAcceptingTags,
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>()).Value)));
        }

        [HttpPut("{id}/profile-picture/{image}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult AddProfilePicture([FromRoute] Guid id, [FromRoute] string image)
        {
            userService.AddProfilePicture(id, image);
            return Ok();
        }
    }
}