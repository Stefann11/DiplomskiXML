using AgentApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentApp.DataAccess.Adaptee
{
    public class UserAdaptee
    {
        public RegisteredUser ConvertSqlDataReaderToUser(DataRow dataRow)
        {
            if (dataRow[10].ToString().Equals("Agent", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToAgent(dataRow);
            }
            else
            {
                return ConvertSqlDataReaderToRegisteredUser(dataRow);
            }
        }

        public RegisteredUser ConvertSqlDataReaderToAgent(DataRow dataRow)
        {
            return Agent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    Password.Create(dataRow[11].ToString()).Value,
                                    ProfilePicturePath.Create(dataRow[12].ToString()).Value).Value;
        }

        public RegisteredUser ConvertSqlDataReaderToRegisteredUser(DataRow dataRow)
        {
            return RegisteredUser.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    Password.Create(dataRow[11].ToString()).Value,
                                    ProfilePicturePath.Create(dataRow[12].ToString()).Value).Value;
        }
    }
}