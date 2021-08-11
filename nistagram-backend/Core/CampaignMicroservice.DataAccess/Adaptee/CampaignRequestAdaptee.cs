using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class CampaignRequestAdaptee
    {
        public CampaignRequest ConvertSqlDataReaderToCampaignRequest(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates,
        IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
        IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified, IEnumerable<RegisteredUser> mutedByUsersVerified,
        IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            if (dataRow[31].ToString().Equals("OneTimePostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToCampaignRequestWithOneTimePostCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates.FirstOrDefault(), blockedByUsersVerified, blockedUsersVerified, followingVerified, followersVerified, mutedByUsersVerified,
                    mutedUsersVerified);
            }
            else if (dataRow[31].ToString().Equals("OneTimeStoryCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToCampaignRequestWithOneTimeStoryCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates.FirstOrDefault(), blockedByUsersVerified, blockedUsersVerified, followingVerified, followersVerified, mutedByUsersVerified,
                    mutedUsersVerified);
            }
            else if (dataRow[31].ToString().Equals("RecurringPostCampaign", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToCampaignRequestWithRecurringPostCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                    mutedUsers, ads, exposureDates, blockedByUsersVerified, blockedUsersVerified, followingVerified, followersVerified, mutedByUsersVerified,
                    mutedUsersVerified);
            }
            return ConvertSqlDataReaderToCampaignRequestWithRecurringStoryCampaign(dataRow, blockedByUsers, blockedUsers, following, followers, mutedByUsers,
                     mutedUsers, ads, exposureDates, blockedByUsersVerified, blockedUsersVerified, followingVerified, followersVerified, mutedByUsersVerified,
                    mutedUsersVerified);
        }

        public CampaignRequest ConvertSqlDataReaderToCampaignRequestWithOneTimePostCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, ExposureDate exposureDate,
        IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
        IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified, IEnumerable<RegisteredUser> mutedByUsersVerified,
        IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            return CampaignRequest.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    isApproved: bool.Parse(dataRow[1].ToString().Trim()),
                    campaign: OneTimePostCampaign.Create(Guid.Parse(dataRow[13].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[14].ToString()),
                                                                    DateTime.Parse(dataRow[15].ToString()),
                                                                    Gender.Create(dataRow[16].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[17].ToString()),
                                                            Username.Create(dataRow[18].ToString()).Value,
                                                            FirstName.Create(dataRow[19].ToString()).Value,
                                                            LastName.Create(dataRow[20].ToString()).Value,
                                                            DateTime.Parse(dataRow[21].ToString()),
                                                            Gender.Create(dataRow[22].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[23].ToString()).Value,
                                                            bool.Parse(dataRow[24].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[25].ToString()),
                                                            WebsiteAddress.Create(dataRow[26].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[27].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[28].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[29].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[30].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value,
                    verifiedUser: VerifiedUser.Create(Guid.Parse(dataRow[2].ToString()),
                                    Username.Create(dataRow[3].ToString()).Value,
                                    FirstName.Create(dataRow[4].ToString()).Value,
                                    LastName.Create(dataRow[5].ToString()).Value,
                                    DateTime.Parse(dataRow[6].ToString()),
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[8].ToString()).Value,
                                    /*WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Category.Create(dataRow[5].ToString()).Value,*/
                                    bool.Parse(dataRow[9].ToString()),
                                    blockedByUsersVerified,
                                    blockedUsersVerified,
                                    followingVerified,
                                    followersVerified,
                                    mutedByUsersVerified,
                                    mutedUsersVerified,
                                    bool.Parse(dataRow[10].ToString()),
                                    Category.Create(dataRow[11].ToString().Trim()).Value).Value,
                    CampaignRequestAction.Create(dataRow[12].ToString()).Value).Value;
        }

        public CampaignRequest ConvertSqlDataReaderToCampaignRequestWithOneTimeStoryCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, ExposureDate exposureDate,
        IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
        IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified, IEnumerable<RegisteredUser> mutedByUsersVerified,
        IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            return CampaignRequest.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    isApproved: bool.Parse(dataRow[1].ToString().Trim()),
                    campaign: OneTimeStoryCampaign.Create(Guid.Parse(dataRow[13].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[14].ToString()),
                                                                    DateTime.Parse(dataRow[15].ToString()),
                                                                    Gender.Create(dataRow[16].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[17].ToString()),
                                                            Username.Create(dataRow[18].ToString()).Value,
                                                            FirstName.Create(dataRow[19].ToString()).Value,
                                                            LastName.Create(dataRow[20].ToString()).Value,
                                                            DateTime.Parse(dataRow[21].ToString()),
                                                            Gender.Create(dataRow[22].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[23].ToString()).Value,
                                                            bool.Parse(dataRow[24].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[25].ToString()),
                                                            WebsiteAddress.Create(dataRow[26].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[27].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[28].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[29].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[30].ToString())).Value).Value,
                                               exposureDate,
                                               ads).Value,
                     verifiedUser: VerifiedUser.Create(Guid.Parse(dataRow[2].ToString()),
                                    Username.Create(dataRow[3].ToString()).Value,
                                    FirstName.Create(dataRow[4].ToString()).Value,
                                    LastName.Create(dataRow[5].ToString()).Value,
                                    DateTime.Parse(dataRow[6].ToString()),
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[8].ToString()).Value,
                                    /*WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Category.Create(dataRow[5].ToString()).Value,*/
                                    bool.Parse(dataRow[9].ToString()),
                                    blockedByUsersVerified,
                                    blockedUsersVerified,
                                    followingVerified,
                                    followersVerified,
                                    mutedByUsersVerified,
                                    mutedUsersVerified,
                                    bool.Parse(dataRow[10].ToString()),
                                    Category.Create(dataRow[11].ToString().Trim()).Value).Value,
                    CampaignRequestAction.Create(dataRow[12].ToString()).Value).Value;
        }

        public CampaignRequest ConvertSqlDataReaderToCampaignRequestWithRecurringPostCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates,
        IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
        IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified, IEnumerable<RegisteredUser> mutedByUsersVerified,
        IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[34].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[34].ToString());
            return CampaignRequest.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    isApproved: bool.Parse(dataRow[1].ToString().Trim()),
                    campaign: RecurringPostCampaign.Create(Guid.Parse(dataRow[13].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[14].ToString()),
                                                                    DateTime.Parse(dataRow[15].ToString()),
                                                                    Gender.Create(dataRow[16].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[17].ToString()),
                                                            Username.Create(dataRow[18].ToString()).Value,
                                                            FirstName.Create(dataRow[19].ToString()).Value,
                                                            LastName.Create(dataRow[20].ToString()).Value,
                                                            DateTime.Parse(dataRow[21].ToString()),
                                                            Gender.Create(dataRow[22].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[23].ToString()).Value,
                                                            bool.Parse(dataRow[24].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[25].ToString()),
                                                            WebsiteAddress.Create(dataRow[26].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[27].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[28].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[29].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[30].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[32].ToString()),
                                               DateTime.Parse(dataRow[33].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value,
                    verifiedUser: VerifiedUser.Create(Guid.Parse(dataRow[2].ToString()),
                                    Username.Create(dataRow[3].ToString()).Value,
                                    FirstName.Create(dataRow[4].ToString()).Value,
                                    LastName.Create(dataRow[5].ToString()).Value,
                                    DateTime.Parse(dataRow[6].ToString()),
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[8].ToString()).Value,
                                    /*WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Category.Create(dataRow[5].ToString()).Value,*/
                                    bool.Parse(dataRow[9].ToString()),
                                    blockedByUsersVerified,
                                    blockedUsersVerified,
                                    followingVerified,
                                    followersVerified,
                                    mutedByUsersVerified,
                                    mutedUsersVerified,
                                    bool.Parse(dataRow[10].ToString()),
                                    Category.Create(dataRow[11].ToString().Trim()).Value).Value,
                    CampaignRequestAction.Create(dataRow[12].ToString()).Value).Value;
        }

        public CampaignRequest ConvertSqlDataReaderToCampaignRequestWithRecurringStoryCampaign(DataRow dataRow, IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> blockedUsers,
        IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers, IEnumerable<RegisteredUser> mutedByUsers,
        IEnumerable<RegisteredUser> mutedUsers, IEnumerable<Ad> ads, IEnumerable<ExposureDate> exposureDates,
        IEnumerable<RegisteredUser> blockedByUsersVerified, IEnumerable<RegisteredUser> blockedUsersVerified,
        IEnumerable<RegisteredUser> followingVerified, IEnumerable<RegisteredUser> followersVerified, IEnumerable<RegisteredUser> mutedByUsersVerified,
        IEnumerable<RegisteredUser> mutedUsersVerified)
        {
            DateTime dateOfChange = new DateTime();
            if (!dataRow[34].ToString().Equals("")) dateOfChange = DateTime.Parse(dataRow[34].ToString());
            return CampaignRequest.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    isApproved: bool.Parse(dataRow[1].ToString().Trim()),
                    campaign: RecurringStoryCampaign.Create(Guid.Parse(dataRow[13].ToString()),
                                              TargetAudience.Create(DateTime.Parse(dataRow[14].ToString()),
                                                                    DateTime.Parse(dataRow[15].ToString()),
                                                                    Gender.Create(dataRow[16].ToString()).Value).Value,
                                               Agent.Create(Guid.Parse(dataRow[17].ToString()),
                                                            Username.Create(dataRow[18].ToString()).Value,
                                                            FirstName.Create(dataRow[19].ToString()).Value,
                                                            LastName.Create(dataRow[20].ToString()).Value,
                                                            DateTime.Parse(dataRow[21].ToString()),
                                                            Gender.Create(dataRow[22].ToString()).Value,
                                                            ProfileImagePath.Create(dataRow[23].ToString()).Value,
                                                            bool.Parse(dataRow[24].ToString()),
                                                            blockedByUsers,
                                                            blockedUsers,
                                                            following,
                                                            followers,
                                                            mutedByUsers,
                                                            mutedUsers,
                                                            bool.Parse(dataRow[25].ToString()),
                                                            WebsiteAddress.Create(dataRow[26].ToString()).Value).Value,
                                               CampaignStatistics.Create(LikesCount.Create(int.Parse(dataRow[27].ToString())).Value,
                                                                         DislikesCount.Create(int.Parse(dataRow[28].ToString())).Value,
                                                                         ExposureCount.Create(int.Parse(dataRow[29].ToString())).Value,
                                                                         ClickCount.Create(int.Parse(dataRow[30].ToString())).Value).Value,
                                               DateTime.Parse(dataRow[32].ToString()),
                                               DateTime.Parse(dataRow[33].ToString()),
                                               exposureDates,
                                               dateOfChange,
                                               ads).Value,
                    verifiedUser: VerifiedUser.Create(Guid.Parse(dataRow[2].ToString()),
                                    Username.Create(dataRow[3].ToString()).Value,
                                    FirstName.Create(dataRow[4].ToString()).Value,
                                    LastName.Create(dataRow[5].ToString()).Value,
                                    DateTime.Parse(dataRow[6].ToString()),
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[8].ToString()).Value,
                                    /*WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Category.Create(dataRow[5].ToString()).Value,*/
                                    bool.Parse(dataRow[9].ToString()),
                                    blockedByUsersVerified,
                                    blockedUsersVerified,
                                    followingVerified,
                                    followersVerified,
                                    mutedByUsersVerified,
                                    mutedUsersVerified,
                                    bool.Parse(dataRow[10].ToString()),
                                    Category.Create(dataRow[11].ToString().Trim()).Value).Value,
                    CampaignRequestAction.Create(dataRow[12].ToString()).Value).Value;
        }
    }
}