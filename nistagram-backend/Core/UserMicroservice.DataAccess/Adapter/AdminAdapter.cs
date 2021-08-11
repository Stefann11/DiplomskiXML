using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Adapter
{
    public class AdminAdapter : ITarget
    {
        private readonly AdminAdaptee adminAdaptee;

        public AdminAdapter(AdminAdaptee adminAdaptee)
        {
            this.adminAdaptee = adminAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return adminAdaptee.ConvertSqlDataReaderToAdmin(dataRow);
        }
    }
}