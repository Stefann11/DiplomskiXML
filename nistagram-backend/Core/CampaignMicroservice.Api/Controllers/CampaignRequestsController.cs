using CampaignMicroservice.Api.Factories;
using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignRequestsController : ControllerBase
    {
        private readonly ICampaignRequestRepository _campaignRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly CampaignRequestFactory _campaignRequestFactory;

        public CampaignRequestsController(ICampaignRequestRepository campaignRequestRepository,
            IUserRepository userRepository, ICampaignRepository campaignRepository,
            CampaignRequestFactory campaignRequestFactory)
        {
            _campaignRequestRepository = campaignRequestRepository;
            _userRepository = userRepository;
            _campaignRepository = campaignRepository;
            _campaignRequestFactory = campaignRequestFactory;
        }

        [HttpPost]
        [Authorize(Roles = "Agent")]
        public IActionResult Create(DTOs.CampaignRequest campaignRequest)
        {
            Guid id = Guid.NewGuid();
            Result<CampaignRequestAction> campaignRequestAction = CampaignRequestAction.Create(campaignRequest.CampaignRequestAction);

            Result result = Result.Combine(campaignRequestAction);
            if (result.IsFailure) return BadRequest();

            Maybe<RegisteredUser> user = _userRepository.GetById(campaignRequest.VerifiedUser.Id);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            Maybe<Campaign> campaign = _campaignRepository.GetById(campaignRequest.Campaign.Id);
            if (campaign.HasNoValue) return BadRequest("Campaign doesn't exist.");

            if (_campaignRequestRepository.Save(CampaignRequest.Create(id, campaignRequest.IsApproved,
                campaign.Value, (VerifiedUser)user.Value, campaignRequestAction.Value).Value) == null)
                return BadRequest("Couldn't create  campaign request");

            campaignRequest.Id = id;
            return Ok(campaignRequest);
        }

        [HttpPut]
        [Authorize(Roles = "Agent, VerifiedUser")]
        public IActionResult Update(DTOs.CampaignRequest campaignRequest)
        {
            Result<CampaignRequestAction> campaignRequestAction = CampaignRequestAction.Create(campaignRequest.CampaignRequestAction);

            Result result = Result.Combine(campaignRequestAction);
            if (result.IsFailure) return BadRequest();

            Maybe<RegisteredUser> user = _userRepository.GetById(campaignRequest.VerifiedUser.Id);
            if (user.HasNoValue) return BadRequest("Registered user doesn't exist.");

            Maybe<Campaign> campaign = _campaignRepository.GetById(campaignRequest.Campaign.Id);
            if (campaign.HasNoValue) return BadRequest("Campaign doesn't exist.");

            if (_campaignRequestRepository.Update(CampaignRequest.Create(campaignRequest.Id, campaignRequest.IsApproved,
                campaign.Value, (VerifiedUser)user.Value, campaignRequestAction.Value).Value) == null)
                return BadRequest("Couldn't create  campaign request");

            return Ok(campaignRequest);
        }

        [HttpGet]
        public IActionResult Search([FromQuery] Guid userId, [FromQuery(Name = "is-approved")] string isApproved,
            [FromQuery] string action)
        {
            if (Request.Query.Count == 0) return BadRequest();
            if (String.IsNullOrEmpty(isApproved)) return BadRequest();
            if (String.IsNullOrEmpty(userId.ToString())) return BadRequest();
            return Ok(_campaignRequestFactory.CreateCampaignRequests(_campaignRequestRepository.GetBy(userId, isApproved, action)));
        }
    }
}