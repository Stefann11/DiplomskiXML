using PostMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace PostMicroservice.DataAccess.Adaptee
{
    public class CommentAdaptee
    {
        public Comment ConvertSqlDataReaderToComment(DataRow dataRow)
        {
            return Comment.Create(id: Guid.Parse(dataRow[0].ToString().Trim()),
                                  timeStamp: DateTime.Parse(dataRow[1].ToString().Trim()),
                                  commentText: CommentText.Create(dataRow[2].ToString().Trim()).Value,
                                  registeredUser: RegisteredUser.Create(
                                        id: Guid.Parse(dataRow[3].ToString().Trim()),
                                        username: Username.Create(dataRow[4].ToString().Trim()).Value,
                                        firstName: FirstName.Create(dataRow[5].ToString().Trim()).Value,
                                        lastName: LastName.Create(dataRow[6].ToString().Trim()).Value,
                                        profileImagePath: ProfileImagePath.Create(dataRow[9].ToString().Trim()).Value,
                                        isPrivate: bool.Parse(dataRow[7].ToString().Trim()),
                                        isAcceptingTags: bool.Parse(dataRow[8].ToString().Trim()),
                                        blockedUsers: new List<RegisteredUser>(),
                                        blockedByUsers: new List<RegisteredUser>(),
                                        following: new List<RegisteredUser>(),
                                        followers: new List<RegisteredUser>()).Value,
                                   taggedUsers: new List<RegisteredUser>()).Value;
        }
    }
}