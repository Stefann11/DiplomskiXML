using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace StoryMicroservice.Core.Model
{
    public class Highlights
    {
        public Guid Id { get; }
        public HighlightName HighlightName { get; }

        public IEnumerable<Story> Stories { get; }

        public RegisteredUser RegisteredUser { get; }

        public Highlights(Guid id, HighlightName highlightName, IEnumerable<Story> stories, RegisteredUser registeredUser)
        {
            Id = id;
            HighlightName = highlightName;
            Stories = stories;
            RegisteredUser = registeredUser;
        }

        public static Result<Highlights> Create(Guid id, HighlightName highlightName, IEnumerable<Story> stories, RegisteredUser registeredUser)
        {
            return Result.Success(new Highlights(id, highlightName, stories, registeredUser));
        }
    }
}