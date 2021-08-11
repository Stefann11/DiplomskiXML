using CampaignMicroservice.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CampaignMicroservice.DataAccess.Adaptee
{
    public class RecurringStoryCampaignAdaptee
    {
        /*public RecurringStoryCampaign ConvertSqlDataReaderToReccuringStoryCampaign(DataRow dataRow)
        {
            return RecurringStoryCampaign.Create(
                    id: Guid.Parse(dataRow[0].ToString().Trim()),
                    targetAudience: TargetAudience.Create(
                        minDateOfBirth: DateTime.Parse(dataRow[1].ToString().Trim()),
                        maxDateOfBirth: DateTime.Parse(dataRow[2].ToString().Trim()),
                        gender: Gender.Create(dataRow[3].ToString().Trim()).Value,
                        audience: new List<RegisteredUser>()).Value,
                    agent: Agent.Create(id: Guid.Parse(dataRow[4].ToString().Trim())).Value,
                    campaignStatistics: CampaignStatistics.Create(
                        likesCount: LikesCount.Create(int.Parse(dataRow[5].ToString().Trim())).Value,
                        dislikesCount: DislikesCount.Create(int.Parse(dataRow[6].ToString().Trim())).Value,
                        exposureCount: ExposureCount.Create(int.Parse(dataRow[7].ToString().Trim())).Value,
                        clickCount: ClickCount.Create(int.Parse(dataRow[8].ToString().Trim())).Value).Value,
                    startDate: DateTime.Parse(dataRow[9].ToString().Trim()),
                    endDate: DateTime.Parse(dataRow[10].ToString().Trim()),
                    exposureDates: new List<DateTime>(),
                    dateOfChange: DateTime.Parse(dataRow[11].ToString().Trim())).Value;
        }*/
    }
}