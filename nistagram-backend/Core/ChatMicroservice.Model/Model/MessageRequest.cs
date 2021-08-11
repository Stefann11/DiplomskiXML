using CSharpFunctionalExtensions;
using System;

namespace ChatMicroservice.Core.Model
{
    public class MessageRequest : Message
    {
        private readonly bool isApproved;

        public MessageRequest(Guid id, DateTime timeStamp, MessageContent messageContent, RegisteredUser messageReceiver, RegisteredUser messageSender, bool isApproved) : base(id, timeStamp, messageContent, messageReceiver, messageSender)
        {
            this.isApproved = isApproved;
        }

        public static Result<MessageRequest> Create(Guid id, DateTime timeStamp, MessageContent messageContent, RegisteredUser messageReceiver, RegisteredUser messageSender, bool isApproved)
        {
            return Result.Success(new MessageRequest(id, timeStamp, messageContent, messageReceiver, messageSender, isApproved));
        }
    }
}