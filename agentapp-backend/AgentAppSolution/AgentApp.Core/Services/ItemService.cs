using AgentApp.Core.DTOs;
using AgentApp.Core.Interface.Repository;
using AgentApp.Core.Model;
using AgentApp.Core.Model.File;
using CSharpFunctionalExtensions;
using System;
using System.IO;

namespace AgentApp.Core.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
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
            if (String.IsNullOrWhiteSpace(fileName)) fileName = "trolley.png";
            var type = Path.GetExtension(fileName);
            path = path + "\\images\\" + fileName;
            return new Content() { Bytes = System.IO.File.ReadAllBytes(path), Type = type };
        }

        public Result Buy(Item item, int quantity)
        {
            return quantity > int.Parse(item.AvailableCount)
                ? Result.Failure("Quantity is bigger than available count")
                : Result.Success(_itemRepository.Buy(item, int.Parse(item.AvailableCount) - quantity));
        }
    }
}