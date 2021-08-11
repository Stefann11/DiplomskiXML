using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Api.Factories;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashTagsController : Controller
    {
        private readonly IHashTagRepository _hashTagRepository;
        private readonly HashTagFactory hashTagFactory;

        public HashTagsController(IHashTagRepository hashTagRepository, HashTagFactory hashTagFactory)
        {
            _hashTagRepository = hashTagRepository;
            this.hashTagFactory = hashTagFactory;
        }

        [HttpGet]
        public IActionResult GetBy([FromQuery] string text)
        {
            return Ok(hashTagFactory.CreateHashTags(_hashTagRepository.GetByText(text)));
        }
    }
}