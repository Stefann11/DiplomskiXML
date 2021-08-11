using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryMicroservice.Core.DTOs
{
    public class File
    {
        public FileContentResult FileContentResult { get; set; }
        public string Type { get; set; }
    }
}