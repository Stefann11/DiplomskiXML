using CampaignMicroservice.Api.Factories;
using CampaignMicroservice.Core.Interface;
using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CampaignMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdRepository _adRepository;
        private readonly DTOAdFactory _dtoAdFactory;
        private readonly IUserRepository _userRepository;
        private readonly AdFactory _adFactory;

        public AdsController(IAdRepository adRepository, DTOAdFactory dtoAdFactory,
            IUserRepository userRepository, AdFactory adFactory)
        {
            _adRepository = adRepository;
            _dtoAdFactory = dtoAdFactory;
            _userRepository = userRepository;
            _adFactory = adFactory;
        }

        [HttpPost]
        [Authorize(Roles = "Agent, VerifiedUser")]
        public IActionResult Save(DTOs.CreateAdDto dto)
        {
            dto.Ad.Id = Guid.NewGuid();
            RegisteredUser profileOwner = _userRepository.GetById(dto.Ad.ProfileOwnerId).Value;
            Ad ad = (_adFactory.Create(dto.Ad, profileOwner));

            _adRepository.Save(ad, dto.CampaignId);

            return Ok(dto.Ad);
        }

        [HttpGet]
        public IActionResult Search([FromQuery] Guid contentId)
        {
            if (Request.Query.Count == 0) return BadRequest();
            if (String.IsNullOrEmpty(contentId.ToString())) return BadRequest();
            var result = _adRepository.GetByContentId(contentId);
            if (result.HasNoValue) return BadRequest();
            return Ok(_dtoAdFactory.Create(result.Value));
        }
    }
}