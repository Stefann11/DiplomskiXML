using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationMicroservice.Api.Factories;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using NotificationMicroservice.Core.Services;
using System;

namespace NotificationMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationOptionsController : ControllerBase
    {
        private readonly NotificationOptionsFactory notificationOptionsFactory;
        private readonly INotificationOptionsRepository _notificationOptionsRepository;
        private readonly NotificationOptionsService notificationOptionsService;
        private readonly IRegisteredUserRepository _registeredUserRepository;

        public NotificationOptionsController(INotificationOptionsRepository notificationOptionsRepository,
            NotificationOptionsFactory notificationOptionsFactory, NotificationOptionsService notificationOptionsService,
            IRegisteredUserRepository registeredUserRepository)
        {
            _notificationOptionsRepository = notificationOptionsRepository;
            this.notificationOptionsFactory = notificationOptionsFactory;
            this.notificationOptionsService = notificationOptionsService;
            _registeredUserRepository = registeredUserRepository;
        }

        [HttpGet]
        public IActionResult GetBy([FromQuery] Guid loggedUserId, [FromQuery] Guid notificationByUserId)
        {
            return Ok(notificationOptionsService.GetBy(loggedUserId, notificationByUserId));
        }

        [HttpPut]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Edit(DTOs.NotificationOptions notificationOptions)
        {
            return Ok(notificationOptionsFactory.Create(_notificationOptionsRepository.Edit(
                new NotificationOptions(notificationOptions.Id, notificationOptions.IsNotifiedByFollowRequests,
                notificationOptions.IsNotifiedByMessages, notificationOptions.IsNotifiedByPosts, notificationOptions.IsNotifiedByStories,
                notificationOptions.IsNotifiedByComments, notificationOptions.LoggedUser.Id, notificationOptions.NotificationByUser.Id))));
        }
    }
}