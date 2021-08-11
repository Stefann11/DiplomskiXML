using CampaignMicroservice.DataAccess.Adaptee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Adapter
{
    public class CampaignUpdateAdapter
    {
        private readonly CampaignUpdateAdaptee campaignUpdateAdaptee;

        public CampaignUpdateAdapter(CampaignUpdateAdaptee campaignUpdateAdaptee)
        {
            this.campaignUpdateAdaptee = campaignUpdateAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return campaignUpdateAdaptee.ConvertSqlDataReaderToCampaignUpdate(dataRow);
        }
    }
}