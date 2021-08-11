using PostMicroservice.Core.Model;
using System.Data;

namespace PostMicroservice.DataAccess.Adaptee
{
    public class HashTagAdaptee
    {
        public HashTag ConvertSqlDataReaderToHashTag(DataRow dataRow)
        {
            return HashTag.Create(hashTagText: HashTagText.Create(dataRow[0].ToString().Trim()).Value).Value;
        }
    }
}