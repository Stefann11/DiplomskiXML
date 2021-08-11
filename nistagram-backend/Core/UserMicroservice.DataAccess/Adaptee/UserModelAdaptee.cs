using System;
using System.Collections.Generic;
using System.Data;
using UserMicroservice.Core.Model;

namespace UserMicroservice.DataAccess.Adaptee
{
    public class UserModelAdaptee
    {
        public User ConvertSqlDataReaderToUserModel(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            if (dataRow[13].ToString().Equals("agentapp", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToApprovedAgent(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
            }
            else if (dataRow[13].ToString().Equals("Agent", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToAgent(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
            }
            else if (dataRow[13].ToString().Equals("agentden", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToDeniedAgent(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
            }
            else if (dataRow[13].ToString().Equals("agentuna", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToUnapprovedAgent(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
            }
            else if (dataRow[13].ToString().Equals("VerifiedUser", StringComparison.InvariantCultureIgnoreCase))
            {
                return ConvertSqlDataReaderToVerifiedUser(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
            }
            return ConvertSqlDataReaderToRegisteredUser(dataRow,
             blockedUsers, blockedByUsers,
             mutedUsers, mutedByUsers,
             following, followers,
             myCloseFriends, closeFriendsTo);
        }

        private RegisteredUser ConvertSqlDataReaderToRegisteredUser(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return RegisteredUser.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString())).Value;
        }

        private VerifiedUser ConvertSqlDataReaderToVerifiedUser(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return VerifiedUser.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString()),
                                    Category.Create(dataRow[14].ToString()).Value).Value;
        }

        private Agent ConvertSqlDataReaderToAgent(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return Agent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString())).Value;
        }

        private ApprovedAgent ConvertSqlDataReaderToApprovedAgent(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return ApprovedAgent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString())).Value;
        }

        private UnapprovedAgent ConvertSqlDataReaderToUnapprovedAgent(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return UnapprovedAgent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString())).Value;
        }

        private DeniedAgent ConvertSqlDataReaderToDeniedAgent(DataRow dataRow,
            IEnumerable<RegisteredUser> blockedUsers, IEnumerable<RegisteredUser> blockedByUsers,
            IEnumerable<RegisteredUser> mutedUsers, IEnumerable<RegisteredUser> mutedByUsers,
            IEnumerable<RegisteredUser> following, IEnumerable<RegisteredUser> followers,
            IEnumerable<RegisteredUser> myCloseFriends, IEnumerable<RegisteredUser> closeFriendsTo)
        {
            return DeniedAgent.Create(Guid.Parse(dataRow[0].ToString()),
                                    Username.Create(dataRow[1].ToString()).Value,
                                    EmailAddress.Create(dataRow[2].ToString()).Value,
                                    FirstName.Create(dataRow[3].ToString()).Value,
                                    LastName.Create(dataRow[4].ToString()).Value,
                                    DateTime.Parse(dataRow[5].ToString()),
                                    PhoneNumber.Create(dataRow[6].ToString()).Value,
                                    Gender.Create(dataRow[7].ToString()).Value,
                                    WebsiteAddress.Create(dataRow[8].ToString()).Value,
                                    Bio.Create(dataRow[9].ToString()).Value,
                                    bool.Parse(dataRow[10].ToString()),
                                    bool.Parse(dataRow[11].ToString()),
                                    bool.Parse(dataRow[12].ToString()),
                                    Password.Create(dataRow[15].ToString()).Value,
                                    ProfileImagePath.Create(dataRow[16].ToString()).Value,
                                    blockedUsers, blockedByUsers,
                                     mutedUsers, mutedByUsers,
                                     following, followers,
                                     myCloseFriends, closeFriendsTo,
                                    bool.Parse(dataRow[17].ToString())).Value;
        }
    }
}