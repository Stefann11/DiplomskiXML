using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Adapter
{
    public class GetCampaignAdapter
    {
        private readonly GetCampaignAdaptee campaignAdaptee;

        public GetCampaignAdapter(GetCampaignAdaptee campaignAdaptee)
        {
            this.campaignAdaptee = campaignAdaptee;
        }

        public object ConvertSql(DataRow dataRow, Agent agent, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates)
        {
            return campaignAdaptee.ConvertSqlDataReaderToCampaign(dataRow, agent, ads, exposureDates);
        }
    }
}