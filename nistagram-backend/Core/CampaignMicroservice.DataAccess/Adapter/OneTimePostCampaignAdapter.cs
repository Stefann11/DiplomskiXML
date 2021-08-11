using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class OneTimePostCampaignAdapter/* : ITarget*/
    {
        private readonly OneTimePostCampaignAdaptee oneTimePostCampaignAdaptee;

        public OneTimePostCampaignAdapter(OneTimePostCampaignAdaptee oneTimePostCampaignAdaptee)
        {
            this.oneTimePostCampaignAdaptee = oneTimePostCampaignAdaptee;
        }

        /* public object ConvertSql(DataRow dataRow)
         {
             return oneTimePostCampaignAdaptee.ConvertSqlDataReaderToOneTimePostCampaign(dataRow);
         }*/
    }
}