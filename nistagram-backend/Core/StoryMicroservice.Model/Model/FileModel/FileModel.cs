using Microsoft.AspNetCore.Http;

namespace StoryMicroservice.Core.Model.FileModel
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}