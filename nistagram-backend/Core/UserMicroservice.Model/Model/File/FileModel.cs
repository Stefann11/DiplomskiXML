using Microsoft.AspNetCore.Http;

namespace UserMicroservice.Core.Model.File
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}