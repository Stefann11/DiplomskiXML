using PostMicroservice.Core.Model;
using System.Data;

namespace PostMicroservice.DataAccess.Adaptee
{
    public class ContentPathAdaptee
    {
        public ContentPath ConvertSqlDataReaderToContentPath(DataRow dataRow)
        {
            return ContentPath.Create(dataRow[0].ToString().Trim()).Value;
        }
    }
}