using CSharpFunctionalExtensions;

namespace ChatMicroservice.Core.Model
{
    public class ImageContent : MessageContent
    {
        private readonly ImagePath imagePath;

        public ImageContent(ImagePath imagePath)
        {
            this.imagePath = imagePath;
        }

        public static Result<ImageContent> Create(ImagePath imagePath)
        {
            return Result.Success(new ImageContent(imagePath));
        }
    }
}