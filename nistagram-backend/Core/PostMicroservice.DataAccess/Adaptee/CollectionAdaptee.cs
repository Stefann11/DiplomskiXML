using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PostMicroservice.DataAccess.Adaptee
{
    public class CollectionAdaptee
    {
        public Collection ConvertSqlDataReaderToCollection(DataRow dataRow)
        {
            return Collection.Create(id: Guid.Parse(dataRow[0].ToString().Trim()),
                                     collectionName: CollectionName.Create(dataRow[1].ToString().Trim()).Value,
                                     posts: new List<Post>(),
                                     registeredUser: RegisteredUser.Create(
                                        id: Guid.Parse(dataRow[2].ToString().Trim()),
                                        username: Username.Create(dataRow[3].ToString().Trim()).Value,
                                        firstName: FirstName.Create(dataRow[4].ToString().Trim()).Value,
                                        lastName: LastName.Create(dataRow[5].ToString().Trim()).Value,
                                        profileImagePath: ProfileImagePath.Create(dataRow[8].ToString().Trim()).Value,
                                        isPrivate: bool.Parse(dataRow[6].ToString().Trim()),
                                        isAcceptingTags: bool.Parse(dataRow[7].ToString().Trim()),
                                        blockedUsers: new List<RegisteredUser>(),
                                        blockedByUsers: new List<RegisteredUser>(),
                                        following: new List<RegisteredUser>(),
                                        followers: new List<RegisteredUser>()).Value).Value;
        }
    }
}