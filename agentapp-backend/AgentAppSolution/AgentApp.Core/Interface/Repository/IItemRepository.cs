using AgentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.Core.Interface.Repository
{
    public interface IItemRepository : IRepository<Item>
    {
        public Item Buy(Item item, int quantity);
    }
}