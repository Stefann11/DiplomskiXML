using CSharpFunctionalExtensions;
using StoryMicroservice.Core.DTOs;
using StoryMicroservice.Core.Interface.Repository;
using StoryMicroservice.Core.Model;
using StoryMicroservice.Core.Model.FileModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Story = StoryMicroservice.Core.Model.Story;

namespace StoryMicroservice.Core.Services
{
    public class StoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public Result Create(Story story)
        {
            if (_storyRepository.GetById(story.Id).HasValue) return Result.Failure("Story with that Id already exists.");
            _storyRepository.Save(story);
            return Result.Success(story);
        }

        public string ImageToSave(string path, FileModel file)
        {
            try
            {
                using (Stream stream = new FileStream(path + "\\images\\" + file.FileName, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }
                return file.FileName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Content GetImage(string path, string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName)) fileName = "iconfinder_00-ELASTOFONT-STORE-READY_user-circle_2703062.png";
            var type = Path.GetExtension(fileName);
            path = path + "\\images\\" + fileName;
            return new Content() { Bytes = System.IO.File.ReadAllBytes(path), Type = type };
        }
    }
}