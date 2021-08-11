using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class OneTimeStoryCampaignAdapter /*: ITarget*/
    {
        private readonly OneTimeStoryCampaignAdaptee oneTimeStoryCampaignAdaptee;

        public OneTimeStoryCampaignAdapter(OneTimeStoryCampaignAdaptee oneTimeStoryCampaignAdaptee)
        {
            this.oneTimeStoryCampaignAdaptee = oneTimeStoryCampaignAdaptee;
        }

        /*public object ConvertSql(DataRow dataRow)
        {
            return oneTimeStoryCampaignAdaptee.ConvertSqlDataReaderToOneTimeStoryCampaign(dataRow);
        }*/
    }
}