using CSharpFunctionalExtensions;
using System;

namespace ChatMicroservice.Core.Model
{
    public abstract class Content : MessageContent
    {
        private readonly Guid id;

        public Content(Guid id)
        {
            this.id = id;
        }
    }
}