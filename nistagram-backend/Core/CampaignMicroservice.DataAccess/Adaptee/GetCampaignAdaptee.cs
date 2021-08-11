using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class GetCampaignAdaptee
    {
        public Campaign ConvertSqlDataReaderToCampaign(DataRow dataRow, Agent agent, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates)
        {
            if (dataRow[9].ToString().Equals("OneTimePostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToOneTimePostCampaign(dataRow, agent, ads, exposureDates.FirstOrDefault());
            }
            else if (dataRow[9].ToString().Equals("OneTimeStoryCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToOneTimeStoryCampaign(dataRow, agent, ads, exposureDates.FirstOrDefault());
            }
            else if (dataRow[9].ToString().Equals("RecurringPostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToRecurringPostCampaign(dataRow, agent, ads, exposureDates);
            }
            return ConvertSqlDataReaderToRecurringStoryCampaign(dataRow, agent, ads, exposureDates);
        }

        private OneTimePostCampaign ConvertSqlDataReaderToOneTimePostCampaign(DataRow dataRow, Agent agent, IEnumerable<Ad> ads,
            ExposureDate exposureDate)
        {
            return OneTimePostCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               agent,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[5].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[6].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[7].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[8].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value;
        }

        private OneTimeStoryCampaign ConvertSqlDataReaderToOneTimeStoryCampaign(DataRow dataRow, Agent agent, IEnumerable<Ad> ads,
             ExposureDate exposureDate)
        {
            return OneTimeStoryCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               agent,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[5].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[6].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[7].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[8].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value;
        }

        private RecurringPostCampaign ConvertSqlDataReaderToRecurringPostCampaign(DataRow dataRow, Agent agent, IEnumerable<Ad> ads,
           IEnumerable<ExposureDate> exposureDates)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[12].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[12].ToString());
            return RecurringPostCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               agent,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[5].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[6].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[7].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[8].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[10].ToString()),
                                               DateTime.Parse(dataRow[11].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value;
        }

        private RecurringStoryCampaign ConvertSqlDataReaderToRecurringStoryCampaign(DataRow dataRow, Agent agent, IEnumerable<Ad> ads,
           IEnumerable<ExposureDate> exposureDates)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[12].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[12].ToString());
            return RecurringStoryCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               agent,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[5].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[6].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[7].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[7].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[10].ToString()),
                                               DateTime.Parse(dataRow[11].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value;
        }
    }
}