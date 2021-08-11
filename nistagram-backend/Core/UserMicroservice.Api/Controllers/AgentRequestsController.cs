using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserMicroservice.Api.Factories;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;
using UserMicroservice.Core.Services;

namespace UserMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentRequestsController : ControllerBase
    {
        private readonly AgentRequestFactory agentRequestFactory;
        private readonly IAgentRequestRepository _agentRequestRepository;
        private readonly AgentRequestService agentRequestService;
        private readonly IUserRepository _userRepository;

        public AgentRequestsController(AgentRequestFactory agentRequestFactory,
            IAgentRequestRepository agentRequestRepository, IUserRepository userRepository, AgentRequestService agentRequestService)
        {
            this.agentRequestFactory = agentRequestFactory;
            _agentRequestRepository = agentRequestRepository;
            _userRepository = userRepository;
            this.agentRequestService = agentRequestService;
        }

        [HttpPost]
        public IActionResult Create(DTOs.AgentRequest agentRequest)
        {
            Guid id = Guid.NewGuid();
            Result<Username> username = Username.Create(agentRequest.Username);
            Result<EmailAddress> emailAddress = EmailAddress.Create(agentRequest.EmailAddress);
            Result<FirstName> firstName = FirstName.Create(agentRequest.FirstName);
            Result<LastName> lastName = LastName.Create(agentRequest.LastName);
            Result<PhoneNumber> phoneNumber = PhoneNumber.Create(agentRequest.PhoneNumber);
            Result<Gender> gender = Gender.Create(agentRequest.Gender);
            Result<WebsiteAddress> websiteAddress = WebsiteAddress.Create(agentRequest.WebsiteAddress);
            Result<Bio> bio = Bio.Create(agentRequest.Bio);
            Result<Password> password = Password.Create(agentRequest.Password);

            Result result = Result.Combine(username, emailAddress, firstName, lastName, phoneNumber, gender, websiteAddress, bio, password);
            if (result.IsFailure) return BadRequest();

            Maybe<RegisteredUser> user = _userRepository.GetById(agentRequest.RegisteredUser.Id);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            if (_agentRequestRepository.Save(AgentRequest.Create(id, agentRequest.IsApproved, user.Value,
                AgentRequestAction.Create("created").Value, username.Value, emailAddress.Value,
                firstName.Value, lastName.Value, agentRequest.DateOfBirth, phoneNumber.Value,
                gender.Value, websiteAddress.Value, bio.Value, agentRequest.IsPrivate,
                agentRequest.IsAcceptingMessages, agentRequest.IsAcceptingTags, password.Value).Value) == null)
                return BadRequest("Couldn't create agent request");

            return Created(this.Request.Path + id, "");
        }

        [HttpGet]
        public IActionResult Search([FromQuery(Name = "is-approved")] string isApproved)
        {
            if (Request.Query.Count == 0) return BadRequest();
            if (String.IsNullOrEmpty(isApproved)) return BadRequest();
            return Ok(agentRequestFactory.CreateAgentRequests(_agentRequestRepository.GetBy(isApproved)));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DTOs.AgentRequest agentRequest)
        {
            Result<Username> username = Username.Create(agentRequest.Username);
            Result<EmailAddress> emailAddress = EmailAddress.Create(agentRequest.EmailAddress);
            Result<FirstName> firstName = FirstName.Create(agentRequest.FirstName);
            Result<LastName> lastName = LastName.Create(agentRequest.LastName);
            Result<PhoneNumber> phoneNumber = PhoneNumber.Create(agentRequest.PhoneNumber);
            Result<Gender> gender = Gender.Create(agentRequest.Gender);
            Result<WebsiteAddress> websiteAddress = WebsiteAddress.Create(agentRequest.WebsiteAddress);
            Result<Bio> bio = Bio.Create(agentRequest.Bio);
            Result<Password> password = Password.Create(agentRequest.Password);

            Result result = Result.Combine(username, emailAddress, firstName, lastName, phoneNumber, gender, websiteAddress, bio, password);

            if (result.IsFailure) return BadRequest();
            Maybe<RegisteredUser> user = _userRepository.GetById(agentRequest.RegisteredUser.Id);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            var editResult = agentRequestService.UpdateAgentRequestAsync(AgentRequest.Create(agentRequest.Id, agentRequest.IsApproved,
                user.Value, AgentRequestAction.Create(agentRequest.AgentRequestAction).Value,
                username.Value, emailAddress.Value,
                firstName.Value, lastName.Value, agentRequest.DateOfBirth, phoneNumber.Value,
                gender.Value, websiteAddress.Value, bio.Value, agentRequest.IsPrivate,
                agentRequest.IsAcceptingMessages, agentRequest.IsAcceptingTags, password.Value).Value);
            if (editResult.Result.IsFailure) return BadRequest(editResult.Result.Error);

            return Ok(agentRequest);
        }
    }
}