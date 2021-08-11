using AgentApp.DataAccess.Adaptee;
using AgentApp.DataAccess.Target;
using System.Data;

namespace AgentApp.DataAccess.Adapter
{
    public class ItemAdapter : ITarget
    {
        private readonly ItemAdaptee itemAdaptee;

        public ItemAdapter(ItemAdaptee itemAdaptee)
        {
            this.itemAdaptee = itemAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return itemAdaptee.ConvertSqlDataReaderToItem(dataRow);
        }
    }
}