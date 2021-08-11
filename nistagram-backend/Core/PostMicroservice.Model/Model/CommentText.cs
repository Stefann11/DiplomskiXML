using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace PostMicroservice.Core.Model
{
    public class CommentText : ValueObject
    {
        private readonly string text;

        private CommentText(string text)
        {
            this.text = text;
        }

        public static Result<CommentText> Create(string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return Result.Failure<CommentText>("Comment text cannot be empty");
            if (text.Length > 200) return Result.Failure<CommentText>("Comment cannot contain more than 200 characters.");
            return Result.Success(new CommentText(text));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return text;
        }

        public override string ToString()
        {
            return this.text;
        }

        public static implicit operator string(CommentText commentText) => commentText.text;
    }
}