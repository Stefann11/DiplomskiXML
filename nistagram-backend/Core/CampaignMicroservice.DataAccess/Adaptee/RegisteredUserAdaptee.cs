using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class RegisteredUserAdaptee
    {
        public RegisteredUser ConvertSqlDataReaderToUserModel(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            if (dataRow[4].ToString().Equals("Agent", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToAgent(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers);
            }
            else if (dataRow[4].ToString().Equals("VerifiedUser", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToVerifiedUser(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers);
            }
            return ConvertSqlDataReaderToRegisteredUser(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers);
        }

        public RegisteredUser ConvertSqlDataReaderToRegisteredUser(DataRow dataRow,
             IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
             IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
             IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            return RegisteredUser.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    FirstName.Create(dataRow[9].ToString()).Value,
                                    LastName.Create(dataRow[10].ToString()).Value,
                                    DateTime.Parse(dataRow[2].ToString()),
                                    Gender.Create(dataRow[3].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[6].ToString()).Value,
                                    bool.Parse(dataRow[7].ToString()),
                                    blockedByUsers,
                                    blockedUsers,
                                    following,
                                    followers,
                                    mutedByUsers,
                                    mutedUsers,
                                    bool.Parse(dataRow[11].ToString())).Value;
        }

        public RegisteredUser ConvertSqlDataReaderToVerifiedUser(DataRow dataRow,
             IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
             IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
             IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            return VerifiedUser.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    FirstName.Create(dataRow[9].ToString()).Value,
                                    LastName.Create(dataRow[10].ToString()).Value,
                                    DateTime.Parse(dataRow[2].ToString()),
                                    Gender.Create(dataRow[3].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[6].ToString()).Value,
                                    bool.Parse(dataRow[7].ToString()),
                                    blockedByUsers,
                                    blockedUsers,
                                    following,
                                    followers,
                                    mutedByUsers,
                                    mutedUsers,
                                    bool.Parse(dataRow[11].ToString()),
                                    Category.Create(dataRow[5].ToString()).Value).Value;
        }

        public RegisteredUser ConvertSqlDataReaderToAgent(DataRow dataRow,
             IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
             IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
             IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers)
        {
            return Agent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    FirstName.Create(dataRow[9].ToString()).Value,
                                    LastName.Create(dataRow[10].ToString()).Value,
                                    DateTime.Parse(dataRow[2].ToString()),
                                    Gender.Create(dataRow[3].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[6].ToString()).Value,
                                    bool.Parse(dataRow[7].ToString()),
                                    blockedByUsers,
                                    blockedUsers,
                                    following,
                                    followers,
                                    mutedByUsers,
                                    mutedUsers,
                                    bool.Parse(dataRow[11].ToString()),
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value).Value;
        }
    }
}