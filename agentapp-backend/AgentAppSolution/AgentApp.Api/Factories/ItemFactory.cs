using AgentApp.Api.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace AgentApp.Api.Factories
{
    public class ItemFactory
    {
        public Item Create(Core.Model.Item item)
        {
            return new Item
            {
                Id = item.Id,
                Name = item.Name,
                ItemImagePath = item.ItemImagePath,
                Price = float.Parse(item.Price),
                AvailableCount = int.Parse(item.AvailableCount)
            };
        }

        public List<Item> CreateItems(IEnumerable<Core.Model.Item> items)
        {
            return (from Core.Model.Item item in items
                    select Create(item)).ToList();
        }
    }
}