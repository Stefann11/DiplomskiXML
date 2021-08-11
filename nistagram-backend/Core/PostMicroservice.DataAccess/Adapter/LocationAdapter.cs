using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class LocationAdapter : ITarget
    {
        private readonly LocationAdaptee locationAdaptee;

        public LocationAdapter(LocationAdaptee locationAdaptee)
        {
            this.locationAdaptee = locationAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return locationAdaptee.ConvertSqlDataReaderToLocation(dataRow);
        }
    }
}