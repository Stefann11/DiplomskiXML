using Microsoft.AspNetCore.Http;

namespace AgentApp.Core.Model.File
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}