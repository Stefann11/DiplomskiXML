using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Model;

namespace UserMicroservice.DataAccess.Adaptee
{
    public class AgentRequestAdaptee
    {
        public AgentRequest ConvertSqlDataReaderToAgentRequest(DataRow dataRow)
        {
            return AgentRequest.Create(
                            Guid.Parse(dataRow[0].ToString()),
                            bool.Parse(dataRow[1].ToString()),
                            RegisteredUser.Create(
                                new Guid(dataRow[2].ToString()),
                                Username.Create(dataRow[3].ToString()).Value,
                                EmailAddress.Create(dataRow[4].ToString()).Value,
                                FirstName.Create(dataRow[5].ToString()).Value,
                                LastName.Create(dataRow[6].ToString()).Value,
                                DateTime.Parse(dataRow[7].ToString()),
                                PhoneNumber.Create(dataRow[8].ToString()).Value,
                                Gender.Create(dataRow[9].ToString()).Value,
                                WebsiteAddress.Create(dataRow[10].ToString()).Value,
                                Bio.Create(dataRow[11].ToString()).Value,
                                bool.Parse(dataRow[12].ToString()),
                                bool.Parse(dataRow[13].ToString()),
                                bool.Parse(dataRow[14].ToString()),
                                Password.Create(dataRow[16].ToString()).Value,
                                ProfileImagePath.Create(dataRow[15].ToString()).Value,
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                new List<RegisteredUser>(),
                                bool.Parse(dataRow[17].ToString())).Value,
                            AgentRequestAction.Create(dataRow[18].ToString().Trim()).Value,
                            Username.Create(dataRow[19].ToString()).Value,
                            EmailAddress.Create(dataRow[20].ToString()).Value,
                            FirstName.Create(dataRow[21].ToString()).Value,
                            LastName.Create(dataRow[22].ToString()).Value,
                            DateTime.Parse(dataRow[23].ToString()),
                            PhoneNumber.Create(dataRow[24].ToString()).Value,
                            Gender.Create(dataRow[25].ToString()).Value,
                            WebsiteAddress.Create(dataRow[26].ToString()).Value,
                            Bio.Create(dataRow[27].ToString()).Value,
                            bool.Parse(dataRow[28].ToString()),
                            bool.Parse(dataRow[29].ToString()),
                            bool.Parse(dataRow[30].ToString()),
                            Password.Create(dataRow[31].ToString()).Value).Value;
        }
    }
}