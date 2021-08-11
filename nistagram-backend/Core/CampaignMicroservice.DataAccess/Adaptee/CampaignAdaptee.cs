using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class CampaignAdaptee
    {
        public Campaign ConvertSqlDataReaderToCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates)
        {
            if (dataRow[18].ToString().Equals("OneTimePostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToOneTimePostCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates.FirstOrDefault());
            }
            else if (dataRow[18].ToString().Equals("OneTimeStoryCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToOneTimeStoryCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates.FirstOrDefault());
            }
            else if (dataRow[18].ToString().Equals("RecurringPostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToRecurringPostCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates);
            }
            return ConvertSqlDataReaderToRecurringStoryCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                     mutedUsers, ads, exposureDates);
        }

        private OneTimePostCampaign ConvertSqlDataReaderToOneTimePostCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> mutedByUsers, IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads,
            ExposureDate exposureDate)
        {
            return OneTimePostCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[4].ToString()),
                                                            Username.Create(dataRow[5].ToString()).Value,
                                                            FirstName.Create(dataRow[6].ToString()).Value,
                                                            LastName.Create(dataRow[7].ToString()).Value,
                                                            DateTime.Parse(dataRow[8].ToString()),
                                                            Gender.Create(dataRow[9].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[10].ToString()).Value,
                                                            bool.Parse(dataRow[11].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[12].ToString()),
                                                            WebsiteAddress.Create(dataRow[13].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[14].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[15].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[16].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[17].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value;
        }

        private OneTimeStoryCampaign ConvertSqlDataReaderToOneTimeStoryCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers,
             IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
             IEnumerable<RegisteredUser> mutedByUsers, IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads,
             ExposureDate exposureDate)
        {
            return OneTimeStoryCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[4].ToString()),
                                                            Username.Create(dataRow[5].ToString()).Value,
                                                            FirstName.Create(dataRow[6].ToString()).Value,
                                                            LastName.Create(dataRow[7].ToString()).Value,
                                                            DateTime.Parse(dataRow[8].ToString()),
                                                            Gender.Create(dataRow[9].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[10].ToString()).Value,
                                                            bool.Parse(dataRow[11].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[12].ToString()),
                                                            WebsiteAddress.Create(dataRow[13].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[14].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[15].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[16].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[17].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value;
        }

        private RecurringPostCampaign ConvertSqlDataReaderToRecurringPostCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers,
           IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
           IEnumerable<RegisteredUser> mutedByUsers, IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads,
           IEnumerable<ExposureDate> exposureDates)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[21].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[21].ToString());
            return RecurringPostCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[4].ToString()),
                                                            Username.Create(dataRow[5].ToString()).Value,
                                                            FirstName.Create(dataRow[6].ToString()).Value,
                                                            LastName.Create(dataRow[7].ToString()).Value,
                                                            DateTime.Parse(dataRow[8].ToString()),
                                                            Gender.Create(dataRow[9].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[10].ToString()).Value,
                                                            bool.Parse(dataRow[11].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[12].ToString()),
                                                            WebsiteAddress.Create(dataRow[13].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[14].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[15].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[16].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[17].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[19].ToString()),
                                               DateTime.Parse(dataRow[20].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value;
        }

        private RecurringStoryCampaign ConvertSqlDataReaderToRecurringStoryCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers,
           IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
           IEnumerable<RegisteredUser> mutedByUsers, IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads,
           IEnumerable<ExposureDate> exposureDates)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[21].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[21].ToString());
            return RecurringStoryCampaign.Create(Guid.Parse(dataRow[0].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[1].ToString()),
                                                                    DateTime.Parse(dataRow[2].ToString()),
                                                                    Gender.Create(dataRow[3].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[4].ToString()),
                                                            Username.Create(dataRow[5].ToString()).Value,
                                                            FirstName.Create(dataRow[6].ToString()).Value,
                                                            LastName.Create(dataRow[7].ToString()).Value,
                                                            DateTime.Parse(dataRow[8].ToString()),
                                                            Gender.Create(dataRow[9].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[10].ToString()).Value,
                                                            bool.Parse(dataRow[11].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[12].ToString()),
                                                            WebsiteAddress.Create(dataRow[13].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[14].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[15].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[16].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[17].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[19].ToString()),
                                               DateTime.Parse(dataRow[20].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value;
        }
    }
}