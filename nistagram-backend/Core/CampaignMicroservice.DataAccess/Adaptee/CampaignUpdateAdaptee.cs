using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class CampaignUpdateAdaptee
    {
        public CampaignUpdate ConvertSqlDataReaderToCampaignUpdate(DataRow dataRow)
        {
            return CampaignUpdate.Create(Guid.Parse(dataRow[0].ToString()),
                                    Guid.Parse(dataRow[1].ToString()),
                                    TargetAudience.Create(DateTime.Parse(dataRow[3].ToString()), DateTime.Parse(dataRow[4].ToString()), Gender.Create(dataRow[5].ToString()).Value).Value,
                                    DateTime.Parse(dataRow[2].ToString()),
                                    bool.Parse(dataRow[6].ToString())).Value;
        }
    }
}