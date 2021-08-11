using CampaignMicroservice.Core.Model;
using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class RegisteredUserAdapter
    {
        private readonly RegisteredUserAdaptee registeredUserAdaptee;

        public RegisteredUserAdapter(RegisteredUserAdaptee registeredUserAdaptee)
        {
            this.registeredUserAdaptee = registeredUserAdaptee;
        }

        public object ConvertSql(DataRow dataRow, IEnumerable<RegisteredUser> blockedUsers,
            IEnumerable<RegisteredUser> blockedByUsers, IEnumerable<RegisteredUser> mutedUsers,
            IEnumerable<RegisteredUser> mutedByUsers, IEnumerable<RegisteredUser> following,
            IEnumerable<RegisteredUser> followers)
        {
            return registeredUserAdaptee.ConvertSqlDataReaderToUserModel(dataRow, blockedUsers,
                blockedByUsers, mutedUsers, mutedByUsers, following, followers);
        }
    }
}