using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class ExposureDateAdaptee
    {
        public ExposureDate ConvertSqlDataReaderToExposureDate(DataRow dataRow, IEnumerable<RegisteredUser> registeredUsers)
        {
            return ExposureDate.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    DateTime.Parse(dataRow[1].ToString().Trim()),
                    registeredUsers).Value;
        }
    }
}