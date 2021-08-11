using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class RecurringStoryCampaignAdapter /*: ITarget*/
    {
        private readonly RecurringStoryCampaignAdaptee reccuringStoryCampaignAdaptee;

        public RecurringStoryCampaignAdapter(RecurringStoryCampaignAdaptee reccuringStoryCampaignAdaptee)
        {
            this.reccuringStoryCampaignAdaptee = reccuringStoryCampaignAdaptee;
        }

        /*public object ConvertSql(DataRow dataRow)
        {
            return reccuringStoryCampaignAdaptee.ConvertSqlDataReaderToReccuringStoryCampaign(dataRow);
        }*/
    }
}