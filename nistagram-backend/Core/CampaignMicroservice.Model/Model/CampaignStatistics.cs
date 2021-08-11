using CSharpFunctionalExtensions;

namespace CampaignMicroservice.Core.Model
{
    public class CampaignStatistics
    {
        public LikesCount LikesCount { get; }
        public DislikesCount DislikesCount { get; }
        public ExposureCount ExposureCount { get; }
        public ClickCount ClickCount { get; }

        private CampaignStatistics(LikesCount likesCount, DislikesCount dislikesCount,
            ExposureCount exposureCount, ClickCount clickCount)
        {
            LikesCount = likesCount;
            DislikesCount = dislikesCount;
            ExposureCount = exposureCount;
            ClickCount = clickCount;
        }

        public static Result<CampaignStatistics> Create(LikesCount likesCount, DislikesCount dislikesCount,
            ExposureCount exposureCount, ClickCount clickCount)
        {
            return Result.Success(new CampaignStatistics(likesCount, dislikesCount,
            exposureCount, clickCount));
        }
    }
}