using CSharpFunctionalExtensions;
using System;

namespace ChatMicroservice.Core.Model
{
    public class Message
    {
        private readonly Guid id;
        private readonly DateTime timeStamp;
        private readonly MessageContent messageContent;
        private readonly RegisteredUser messageReceiver;
        private readonly RegisteredUser messageSender;

        public Message(Guid id, DateTime timeStamp, MessageContent messageContent, RegisteredUser messageReceiver, RegisteredUser messageSender)
        {
            this.id = id;
            this.timeStamp = timeStamp;
            this.messageContent = messageContent;
            this.messageReceiver = messageReceiver;
            this.messageSender = messageSender;
        }

        public static Result<Message> Create(Guid id, DateTime timeStamp, MessageContent messageContent, RegisteredUser messageReceiver, RegisteredUser messageSender)
        {
            return Result.Success(new Message(id, timeStamp, messageContent, messageReceiver, messageSender));
        }
    }
}