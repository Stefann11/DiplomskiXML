using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationMicroservice.Api.Factories;
using NotificationMicroservice.Core.Interface.Repository;
using NotificationMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly NotificationFactory notificationFactory;

        public NotificationsController(INotificationRepository notificationRepository,
            NotificationFactory notificationFactory, IRegisteredUserRepository registeredUserRepository,
            IContentRepository contentRepository)
        {
            _notificationRepository = notificationRepository;
            _registeredUserRepository = registeredUserRepository;
            this.notificationFactory = notificationFactory;
            _contentRepository = contentRepository;
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult Save(DTOs.Notification notification)
        {
            Guid id = Guid.NewGuid();
            _contentRepository.Save(new Content(notification.ContentId, notification.Type));
            if (_notificationRepository.Save(new Notification(id, DateTime.Now, notification.ContentId, notification.RegisteredUser.Id)) == null)
                return BadRequest();
            return Created(this.Request.Path + "/" + id, "");
        }

        [HttpPost("following")]
        [Authorize(Roles = "RegisteredUser, Agent, VerifiedUser")]
        public IActionResult GetNotificationsForFollowing(DTOs.NotificationUsers notificationUsers)
        {
            List<RegisteredUser> users = (notificationUsers.RegisteredUsers.Select(registeredUser =>
                _registeredUserRepository.GetById(registeredUser.Id).Value)).ToList();
            RegisteredUser loggedUser = _registeredUserRepository.GetById(notificationUsers.LoggedUser.Id).Value;
            return Ok(notificationFactory.CreateNotifications(_notificationRepository.GetNotificationsForFollowing(users, loggedUser)));
        }
    }
}