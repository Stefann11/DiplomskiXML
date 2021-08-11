using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class CollectionAdapter : ITarget
    {
        private readonly CollectionAdaptee collectionAdaptee;

        public CollectionAdapter(CollectionAdaptee collectionAdaptee)
        {
            this.collectionAdaptee = collectionAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return collectionAdaptee.ConvertSqlDataReaderToCollection(dataRow);
        }
    }
}