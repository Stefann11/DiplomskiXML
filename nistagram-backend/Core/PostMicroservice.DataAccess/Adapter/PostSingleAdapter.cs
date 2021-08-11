using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class PostSingleAdapter : ITarget
    {
        private readonly PostSingleAdaptee postSingleAdaptee;

        public PostSingleAdapter(PostSingleAdaptee postSingleAdaptee)
        {
            this.postSingleAdaptee = postSingleAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return postSingleAdaptee.ConvertSqlDataReaderToPostSingle(dataRow);
        }
    }
}