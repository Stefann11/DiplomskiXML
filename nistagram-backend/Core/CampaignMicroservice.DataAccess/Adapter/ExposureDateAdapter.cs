using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class ExposureDateAdapter
    {
        private readonly ExposureDateAdaptee exposureDateAdaptee;

        public ExposureDateAdapter(ExposureDateAdaptee exposureDateAdaptee)
        {
            this.exposureDateAdaptee = exposureDateAdaptee;
        }

        public object ConvertSql(DataRow dataRow, IEnumerable<RegisteredUser> seenByUsers)
        {
            return exposureDateAdaptee.ConvertSqlDataReaderToExposureDate(dataRow, seenByUsers);
        }
    }
}