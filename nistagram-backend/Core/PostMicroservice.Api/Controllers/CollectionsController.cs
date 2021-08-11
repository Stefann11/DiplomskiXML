using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostMicroservice.Api.Factories;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly CollectionFactory collectionFactory;
        private readonly IUserRepository _userRepository;

        public CollectionsController(ICollectionRepository collectionRepository,
            CollectionFactory collectionFactory, IUserRepository userRepository)
        {
            _collectionRepository = collectionRepository;
            this.collectionFactory = collectionFactory;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetBy([FromQuery] Guid userId)
        {
            return Ok(collectionFactory.CreateCollections(_collectionRepository.GetByUserId(userId)));
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Save(DTOs.Collection collection)
        {
            Result<CollectionName> collectionName = CollectionName.Create(collection.CollectionName);
            Result result = Result.Combine(collectionName);
            RegisteredUser registeredUser = _userRepository.GetById(collection.RegisteredUser.Id).Value;
            Guid id = Guid.NewGuid();
            if (_collectionRepository.Save(Collection.Create(id, collectionName.Value,
                   new List<Post>(), registeredUser).Value) == null) return BadRequest();
            collection.Id = id;
            return Ok(collection);
        }

        [HttpPut("{id}/posts/{postId}")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult AddPostToCollection([FromRoute] Guid id, [FromRoute] Guid postId)
        {
            return Ok(_collectionRepository.AddPostToCollection(id, postId));
        }
    }
}