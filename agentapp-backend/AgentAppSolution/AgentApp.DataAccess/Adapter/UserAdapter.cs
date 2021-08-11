using AgentApp.Core.Model;
using AgentApp.DataAccess.Adaptee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.DataAccess.Adapter
{
    public class UserAdapter
    {
        private readonly UserAdaptee registeredUserAdaptee;

        public UserAdapter(UserAdaptee registeredUserAdaptee)
        {
            this.registeredUserAdaptee = registeredUserAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return registeredUserAdaptee.ConvertSqlDataReaderToUser(dataRow);
        }
    }
}