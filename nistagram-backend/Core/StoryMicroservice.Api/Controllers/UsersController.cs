using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Model;
using StoryMicroservice.Core.Services;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Collections.Generic;
using RegisteredUser = StoryMicroservice.Core.DTOs.RegisteredUser;

namespace StoryMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RegisteredUserFactory userFactory;
        private readonly UserService userService;
        private readonly IUserRepository _userRepository;

        public UsersController(UserService userService, RegisteredUserFactory userFactory, IUserRepository userRepository)
        {
            this.userService = userService;
            this.userFactory = userFactory;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetBy([FromQuery(Name = "is-taggable")] string isTaggable)
        {
            if (Request.Query.Count == 0) return BadRequest();
            return Ok(userFactory.CreateUsers(_userRepository.GetBy(isTaggable)));
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisteredUser dto)
        {
            Result<Username> username = Username.Create(dto.Username);
            Result<FirstName> firstName = FirstName.Create(dto.FirstName);
            Result<LastName> lastName = LastName.Create(dto.LastName);
            Result result = Result.Combine(username, firstName, lastName);
            if (result.IsFailure) return BadRequest();

            if (userService.Create(userFactory.Create(dto, new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>(),
                                          new List<Core.Model.RegisteredUser>())).IsFailure)
                return BadRequest();
            return Created(this.Request.Path + "/" + dto.Id, "");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Edit(RegisteredUser dto, [FromRoute] string id)
        {
            Result<Username> username = Username.Create(dto.Username);
            Result<FirstName> firstName = FirstName.Create(dto.FirstName);
            Result<LastName> lastName = LastName.Create(dto.LastName);

            Result result = Result.Combine(username, firstName, lastName);
            if (result.IsFailure) return BadRequest(result.Error);

            userService.Edit(id, userFactory.Create(dto, _userRepository.GetUsersById(dto.BlockedByUsers),
                _userRepository.GetUsersById(dto.BlockedByUsers), _userRepository.GetUsersById(dto.Followers),
                _userRepository.GetUsersById(dto.Following), _userRepository.GetUsersById(dto.CloseFriendTo)
                , _userRepository.GetUsersById(dto.MyCloseFriends)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(userFactory.Create(_userRepository.GetById(id).Value));
        }

        [HttpPut("{userId}/close-friends/{closeFriendId}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult AddCloseFriend([FromRoute] string userId, [FromRoute] string closeFriendId)
        {
            if (userService.AddCloseFriend(userId, closeFriendId).IsFailure) return BadRequest();
            return NoContent();
        }
    }
}