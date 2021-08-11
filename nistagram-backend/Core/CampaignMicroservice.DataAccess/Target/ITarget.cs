using System.Data;

namespace CampaignMicroservice.DataAccessTarget
{
    public interface ITarget
    {
        public object ConvertSql(DataRow dataRow);
    }
}