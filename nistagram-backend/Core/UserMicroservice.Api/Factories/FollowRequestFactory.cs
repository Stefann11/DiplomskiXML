using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Api.DTOs;

namespace UserMicroservice.Api.Factories
{
    public class FollowRequestFactory
    {
        private readonly RegisteredUserFactory registeredUserFactory;

        public FollowRequestFactory(RegisteredUserFactory registeredUserFactory)
        {
            this.registeredUserFactory = registeredUserFactory;
        }

        public FollowRequest Create(Core.Model.FollowRequest followRequest)
        {
            return new FollowRequest
            {
                Id = followRequest.Id,
                Timestamp = followRequest.Timestamp,
                RequestsFollow = registeredUserFactory.Create(followRequest.RequestsFollow),
                ReceivesFollow = registeredUserFactory.Create(followRequest.ReceivesFollow)
            };
        }
    }
}