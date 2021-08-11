using CampaignMicroservice.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace CampaignMicroservice.Core.Interface
{
    public interface ICampaignRepository
    {
        public void Save(Campaign campaign);

        public void Update(Campaign campaign);

        public void Remove(Guid id);

        public Maybe<Campaign> GetById(Guid id);

        public IEnumerable<Campaign> GetBy(Guid agentId, Guid clientId);

        public void SaveCampaignUpdate(CampaignUpdate campaignUpdate);

        public IEnumerable<CampaignUpdate> GetAllCampaignUpdates();

        public void UpdateCampaignUpdate(Guid id);

        public void UpdateWithoutType(CampaignUpdate campaign);

        public void Delete(Guid id);
    }
}