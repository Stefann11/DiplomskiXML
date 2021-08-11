using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgentApp.Api.DTOs
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ItemImagePath { get; set; }
        public float Price { get; set; }
        public int AvailableCount { get; set; }
    }
}