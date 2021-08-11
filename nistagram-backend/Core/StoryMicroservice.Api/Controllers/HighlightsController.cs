using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Model;
using StoryMicroservice.DataAccess.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlights = StoryMicroservice.Core.DTOs.Highlights;

namespace StoryMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighlightsController : Controller
    {
        private readonly HighlightFactory highlightFactory;
        private readonly StoryFactory storyFactory;
        private readonly IHighlightRepositry _highlightRepository;

        public HighlightsController(HighlightFactory highlightFactory, StoryFactory storyFactory, IHighlightRepositry highlightRepository)
        {
            this.highlightFactory = highlightFactory;
            this.storyFactory = storyFactory;
            _highlightRepository = highlightRepository;
        }

        [HttpGet]
        public IActionResult GetBy([FromQuery(Name = "owner-id")] string ownerId)
        {
            if (Request.Query.Count == 0) return BadRequest();
            return Ok(highlightFactory.CreateHighlights(_highlightRepository.GetBy(ownerId)));
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Save(Highlights highlight)
        {
            highlight.Id = Guid.NewGuid().ToString();
            Result<HighlightName> highlightName = HighlightName.Create(highlight.HighlightName);
            if (highlightName.IsFailure) return BadRequest(highlightName.Error);
            _highlightRepository.Save(highlightFactory.Create(highlight));
            return Created(this.Request.Path + "/" + highlight.Id, "");
        }

        [HttpPost("{id}/stories")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult AddStory([FromRoute] string id, Core.DTOs.Story story)
        {
            _highlightRepository.AddStory(id, storyFactory.Create(story, new List<Core.Model.RegisteredUser>(),
                new List<Core.Model.RegisteredUser>(), new List<Core.Model.RegisteredUser>()));
            return Created(this.Request.Path + "/" + id, "");
        }
    }
}