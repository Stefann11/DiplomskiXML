using System.Data;

namespace UserMicroservice.DataAccess.Target
{
    public interface ITarget
    {
        public object ConvertSql(DataRow dataRow);
    }
}