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
    public class AdAdapter
    {
        private readonly AdAdaptee adAdaptee;

        public AdAdapter(AdAdaptee adAdaptee)
        {
            this.adAdaptee = adAdaptee;
        }

        public object ConvertSql(DataRow dataRow, RegisteredUser registeredUser)
        {
            return adAdaptee.ConvertSqlDataReaderToAd(dataRow, registeredUser);
        }
    }
}