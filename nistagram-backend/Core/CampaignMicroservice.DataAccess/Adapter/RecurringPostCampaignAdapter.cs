using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class RecurringPostCampaignAdapter /*: ITarget*/
    {
        private readonly RecurringPostCampaignAdaptee recurringPostCampaignAdaptee;

        public RecurringPostCampaignAdapter(RecurringPostCampaignAdaptee recurringPostCampaignAdaptee)
        {
            this.recurringPostCampaignAdaptee = recurringPostCampaignAdaptee;
        }

        /* public object ConvertSql(DataRow dataRow)
         {
             return recurringPostCampaignAdaptee.ConvertSqlDataReaderToReccuringStoryCampaign(dataRow);
         }*/
    }
}