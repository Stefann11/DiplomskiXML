using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class HashTagAdapter : ITarget
    {
        private readonly HashTagAdaptee hashTagAdaptee;

        public HashTagAdapter(HashTagAdaptee hashTagAdaptee)
        {
            this.hashTagAdaptee = hashTagAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return hashTagAdaptee.ConvertSqlDataReaderToHashTag(dataRow);
        }
    }
}