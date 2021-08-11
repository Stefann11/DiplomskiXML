using Microsoft.AspNetCore.Http;

namespace PostMicroservice.Core.Model.File
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}