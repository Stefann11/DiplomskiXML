using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Services;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Story = StoryMicroservice.Core.DTOs.Story;

namespace StoryMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : Controller
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly StoryFactory storyFactory;
        private readonly HashTagFactory hashTagFactory;
        private readonly StoryService storyService;
        private readonly IWebHostEnvironment _env;

        public StoriesController(IStoryRepository storyRepository, StoryFactory storyFactory, IUserRepository userRepository,
            ILocationRepository locationRepository, IWebHostEnvironment env, HashTagFactory hashTagFactory, StoryService storyService)
        {
            _storyRepository = storyRepository;
            _userRepository = userRepository;
            _locationRepository = locationRepository;
            this.storyFactory = storyFactory;
            this.hashTagFactory = hashTagFactory;
            this.storyService = storyService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Search([FromQuery(Name = "story-owner-id")] string storyOwnerId, [FromQuery(Name = "following-id")] string followingId,
            [FromQuery(Name = "last-24h")] string last24h, [FromQuery(Name = "not-logged-in")] string notLoggedIn)
        {
            if (Request.Query.Count == 0) return BadRequest();
            return Ok(storyFactory.CreateStories(_storyRepository.GetBy(storyOwnerId, followingId, last24h, notLoggedIn)));
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Create(Story story)
        {
            story.Id = Guid.NewGuid().ToString();
            story.TimeStamp = DateTime.Now;
            Maybe<Core.Model.RegisteredUser> registeredUser = _userRepository.GetById(new Guid(story.RegisteredUser.Id));
            if (registeredUser.HasNoValue) return BadRequest("Registered user doesn't exist.");
            if (story.Location.CityName != null || story.Location.Street != null || story.Location.Country != null)
            {
                Maybe<Core.Model.Location> location = _locationRepository.GetById(new Guid(story.Location.Id));
                if (location.HasNoValue) return BadRequest("Location doesn't exist.");
            }
            if (storyService.Create(storyFactory.Create(story, _userRepository.GetUsersByDTO(story.SeenByUsers),
                _userRepository.GetUsersByDTO(story.TaggedUsers), _userRepository.GetUsersById(story.RegisteredUser.MyCloseFriends))).IsFailure) return BadRequest();
            return Ok(story);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(storyFactory.Create(_storyRepository.GetById(id).Value));
        }

        [HttpGet("contents/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var content = storyService.GetImage(_env.WebRootPath, fileName);
            if (content.Type.Equals(".mp4")) content.Type = "video/mp4";
            else content.Type = "image/jpeg";
            FileContentResult fileContentResult = File(content.Bytes, content.Type);
            return Ok(fileContentResult);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("{id}/ban")]
        [Authorize(Roles = "Admin")]
        public IActionResult BanStory(string id)
        {
            _storyRepository.BanStory(id);
            return Ok();
        }
    }
}