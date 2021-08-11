using ReportMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportMicroservice.DataAccess.Adaptee
{
    public class RegisteredUserAdaptee
    {
        public RegisteredUser ConvertSqlDataReaderToRegisteredUser(DataRow dataRow)
        {
            return RegisteredUser.Create(Guid.Parse(dataRow[0].ToString()),
                            Username.Create(dataRow[1].ToString().Trim()).Value).Value;
        }
    }
}