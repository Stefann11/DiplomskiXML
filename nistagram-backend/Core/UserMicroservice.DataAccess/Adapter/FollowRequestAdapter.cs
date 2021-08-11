using System.Data;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Adapter
{
    public class FollowRequestAdapter : ITarget
    {
        private readonly FollowRequestAdaptee followRequestAdaptee;

        public FollowRequestAdapter(FollowRequestAdaptee followRequestAdaptee)
        {
            this.followRequestAdaptee = followRequestAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return followRequestAdaptee.ConvertSqlDataReaderToFollowRequest(dataRow);
        }
    }
}