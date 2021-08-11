using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.DataAccess.Adaptee
{
    public class AdminAdaptee
    {
        public Admin ConvertSqlDataReaderToAdmin(DataRow dataRow)
        {
            return Admin.Create(Guid.Parse(dataRow[0].ToString()),
                            Username.Create(dataRow[1].ToString()).Value,
                            EmailAddress.Create(dataRow[2].ToString()).Value,
                            Password.Create(dataRow[3].ToString()).Value).Value;
        }
    }
}