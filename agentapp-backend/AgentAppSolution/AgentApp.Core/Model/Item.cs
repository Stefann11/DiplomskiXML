using CSharpFunctionalExtensions;
using System;

namespace AgentApp.Core.Model
{
    public class Item
    {
        public Guid Id { get; }
        public Name Name { get; }
        public ItemImagePath ItemImagePath { get; }
        public Price Price { get; }
        public AvailableCount AvailableCount { get; }

        protected Item(Guid id, Name name, ItemImagePath itemImagePath, Price price,
            AvailableCount availableCount)
        {
            Id = id;
            Name = name;
            ItemImagePath = itemImagePath;
            Price = price;
            AvailableCount = availableCount;
        }

        public static Result<Item> Create(Guid id, Name name, ItemImagePath itemImagePath, Price price,
            AvailableCount availableCount)
        {
            return Result.Success(new Item(id, name, itemImagePath, price, availableCount));
        }
    }
}