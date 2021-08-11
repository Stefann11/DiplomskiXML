using ReportMicroservice.Core.Model;
using System;
using System.Data;

namespace ReportMicroservice.DataAccess.Adaptee
{
    public class ReportAdaptee
    {
        public Report ConvertSqlDataReaderToReport(DataRow dataRow)
        {
            if (dataRow[3].ToString().Trim().Equals("post", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderWithPost(dataRow);
            }
            else if (dataRow[3].ToString().Trim().Equals("story", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderWithStory(dataRow);
            }
            else
            {
                return ConvertSqlDataReaderWithCampaign(dataRow);
            }
        }

        private Report ConvertSqlDataReaderWithPost(DataRow dataRow)
        {
            return Report.Create(Guid.Parse(dataRow[0].ToString()),
                            DateTime.Parse(dataRow[1].ToString()),
                             ReportReason.Create(dataRow[2].ToString().Trim()).Value,
                             RegisteredUser.Create(
                                 Guid.Parse(dataRow[6].ToString()),
                                 Username.Create(dataRow[7].ToString().Trim()).Value).Value,
                             Post.Create(Guid.Parse(dataRow[4].ToString())).Value,
                             ReportAction.Create(dataRow[5].ToString().Trim()).Value).Value;
        }

        private Report ConvertSqlDataReaderWithStory(DataRow dataRow)
        {
            return Report.Create(Guid.Parse(dataRow[0].ToString()),
                            DateTime.Parse(dataRow[1].ToString()),
                             ReportReason.Create(dataRow[2].ToString().Trim()).Value,
                             RegisteredUser.Create(
                                 Guid.Parse(dataRow[6].ToString()),
                                 Username.Create(dataRow[7].ToString().Trim()).Value).Value,
                             Story.Create(Guid.Parse(dataRow[4].ToString())).Value,
                             ReportAction.Create(dataRow[5].ToString().Trim()).Value).Value;
        }

        private Report ConvertSqlDataReaderWithCampaign(DataRow dataRow)
        {
            return Report.Create(Guid.Parse(dataRow[0].ToString()),
                            DateTime.Parse(dataRow[1].ToString()),
                             ReportReason.Create(dataRow[2].ToString().Trim()).Value,
                             RegisteredUser.Create(
                                 Guid.Parse(dataRow[6].ToString()),
                                 Username.Create(dataRow[7].ToString().Trim()).Value).Value,
                             Campaign.Create(Guid.Parse(dataRow[4].ToString())).Value,
                             ReportAction.Create(dataRow[5].ToString().Trim()).Value).Value;
        }
    }
}