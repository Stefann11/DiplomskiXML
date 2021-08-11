using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Target;
using System.Data;

namespace PostMicroservice.DataAccess.Adapter
{
    public class ContentPathAdapter : ITarget

    {
        private readonly ContentPathAdaptee contentPathAdaptee;

        public ContentPathAdapter(ContentPathAdaptee contentPathAdaptee)
        {
            this.contentPathAdaptee = contentPathAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return contentPathAdaptee.ConvertSqlDataReaderToContentPath(dataRow);
        }
    }
}