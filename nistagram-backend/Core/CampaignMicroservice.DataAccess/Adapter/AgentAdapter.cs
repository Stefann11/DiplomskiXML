using CampaignMicroservice.DataAccess.Adaptee;
using CampaignMicroservice.DataAccessTarget;
using System.Data;

namespace CampaignMicroservice.DataAccessAdapter
{
    public class AgentAdapter /*: ITarget*/
    {
        private readonly AgentAdaptee agentAdaptee;

        public AgentAdapter(AgentAdaptee agentAdaptee)
        {
            this.agentAdaptee = agentAdaptee;
        }

        /*public object ConvertSql(DataRow dataRow)
        {
            return agentAdaptee.ConvertSqlDataReaderToAgent(dataRow);
        }*/
    }
}