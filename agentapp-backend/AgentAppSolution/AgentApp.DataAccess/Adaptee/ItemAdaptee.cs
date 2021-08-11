using AgentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.DataAccess.Adaptee
{
    public class ItemAdaptee
    {
        public Item ConvertSqlDataReaderToItem(DataRow dataRow)
        {
            return Item.Create(
                            Guid.Parse(dataRow[0].ToString()),
                            Name.Create(dataRow[1].ToString().Trim()).Value,
                            ItemImagePath.Create(dataRow[2].ToString().Trim()).Value,
                            Price.Create(float.Parse(dataRow[3].ToString().Trim())).Value,
                            AvailableCount.Create(int.Parse(dataRow[4].ToString().Trim())).Value).Value;
        }
    }
}