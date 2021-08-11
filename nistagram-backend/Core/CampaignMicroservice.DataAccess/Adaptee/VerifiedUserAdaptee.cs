using CampaignMicroservice.Core.Model;
using System;
using System.Data;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class VerifiedUserAdaptee
    {
        /*public VerifiedUser ConvertSqlDataReaderToVerifiedUser(DataRow dataRow)
        {
            return VerifiedUser.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    category: Category.Create(dataRow[1].ToString().Trim()).Value).Value;
        }*/
    }
}