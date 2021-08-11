using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class AdAdaptee
    {
        public Ad ConvertSqlDataReaderToAd(DataRow dataRow, RegisteredUser registeredUser)
        {
            Content content;
            if (dataRow[6].ToString().Equals("Post"))
                content = Post.Create(Guid.Parse(dataRow[1].ToString())).Value;
            else
                content = Story.Create(Guid.Parse(dataRow[1].ToString())).Value;
            return Ad.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    content: content,
                    link: Link.Create(dataRow[2].ToString().Trim()).Value,
                    clickCount: ClickCount.Create(int.Parse(dataRow[3].ToString().Trim())).Value,
                    registeredUser).Value;
        }
    }
}