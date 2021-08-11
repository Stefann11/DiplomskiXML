using System.Data;

namespace PostMicroservice.DataAccess.Target
{
    public interface ITarget
    {
        public object ConvertSql(DataRow dataRow);
    }
}