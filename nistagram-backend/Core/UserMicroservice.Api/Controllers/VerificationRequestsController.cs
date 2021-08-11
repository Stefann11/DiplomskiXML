using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.Factories;
using UserMicroservice.Core.Interface.Repository;
using UserMicroservice.Core.Model;
using UserMicroservice.Core.Services;
using UserMicroservice.DataAccess.Implementation;

namespace UserMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationRequestsController : Controller
    {
        private readonly VerificationRequestService _verificationRequestService;
        private readonly VerificationRequestFactory _verificationRequestFactory;
        private readonly VerificationRequestViewFactory _verificationRequestViewFactory;
        private readonly IUserRepository _userRepository;
        private readonly IVerificationRequestRepository _verificationRequestRepository;

        public VerificationRequestsController(VerificationRequestService verificationRequestService, IUserRepository userRepository,
            IVerificationRequestRepository verificationRequestRepository, VerificationRequestFactory verificationRequestFactory,
            VerificationRequestViewFactory verificationRequestViewFactory)
        {
            _verificationRequestService = verificationRequestService;
            _userRepository = userRepository;
            _verificationRequestRepository = verificationRequestRepository;
            _verificationRequestFactory = verificationRequestFactory;
            _verificationRequestViewFactory = verificationRequestViewFactory;
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public IActionResult Create(DTOs.VerificationRequest verificationRequest)
        {
            Guid id = Guid.NewGuid();
            Result<VerificationRequestFirstName> firstName = VerificationRequestFirstName.Create(verificationRequest.FirstName);
            Result<VerificationRequestLastName> lastName = VerificationRequestLastName.Create(verificationRequest.LastName);
            Result<DocumentImagePath> documentImagePath = DocumentImagePath.Create(verificationRequest.DocumentImagePath);
            if (!Enum.IsDefined(typeof(Categories), verificationRequest.Category)) return BadRequest("Category doesn't exist.");
            Result result = Result.Combine(firstName, lastName, documentImagePath);
            if (result.IsFailure) return BadRequest(result.Error);

            Maybe<RegisteredUser> user = _userRepository.GetById(verificationRequest.RegisteredUserId);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            if (_verificationRequestService.Create(VerificationRequest.Create(id, user.Value, firstName.Value, lastName.Value,
                (Categories)Enum.Parse(typeof(Categories), verificationRequest.Category), documentImagePath.Value, false).Value).IsFailure)
                return BadRequest("Couldn't create verification request");

            return Created(this.Request.Path + id, "");
        }

        [HttpGet]
        public IActionResult Search([FromQuery(Name = "is-approved")] string isApproved)
        {
            if (Request.Query.Count == 0) return BadRequest();
            if (String.IsNullOrEmpty(isApproved)) return BadRequest();
            return Ok(_verificationRequestRepository.GetBy(isApproved)
                .Select(verificationRequest => _verificationRequestViewFactory.Create(verificationRequest)));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] DTOs.VerificationRequest verificationRequest)
        {
            Result<VerificationRequestFirstName> firstName = VerificationRequestFirstName.Create(verificationRequest.FirstName);
            Result<VerificationRequestLastName> lastName = VerificationRequestLastName.Create(verificationRequest.LastName);
            Result<DocumentImagePath> documentImagePath = DocumentImagePath.Create(verificationRequest.DocumentImagePath);
            Result<Category> category = Category.Create(verificationRequest.Category);
            if (!Enum.IsDefined(typeof(Categories), verificationRequest.Category)) return BadRequest("Category doesn't exist.");
            Result result = Result.Combine(firstName, lastName, documentImagePath);
            if (result.IsFailure) return BadRequest(result.Error);

            Maybe<RegisteredUser> user = _userRepository.GetById(verificationRequest.RegisteredUserId);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            var editResult = _verificationRequestService.VerifyUserAsync(VerificationRequest.Create(verificationRequest.Id, user.Value,
                firstName.Value, lastName.Value, (Categories)Enum.Parse(typeof(Categories), verificationRequest.Category),
                documentImagePath.Value, verificationRequest.IsApproved).Value);
            if (editResult.Result.IsFailure) return BadRequest(editResult.Result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _verificationRequestService.Delete(id);
            if (result.IsFailure) return BadRequest(result.Error);
            return NoContent();
        }
    }
}