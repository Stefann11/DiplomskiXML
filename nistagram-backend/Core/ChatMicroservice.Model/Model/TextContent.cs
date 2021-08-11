using CSharpFunctionalExtensions;

namespace ChatMicroservice.Core.Model
{
    public class TextContent : MessageContent
    {
        private readonly string text;

        private TextContent(string text)
        {
            this.text = text;
        }

        public static Result<TextContent> Create(string text)
        {
            if (text.Length > 200) return Result.Failure<TextContent>("Text cannot contain more than 200 characters");
            return Result.Success(new TextContent(text));
        }

        public static implicit operator string(TextContent textContent) => textContent.text;
    }
}