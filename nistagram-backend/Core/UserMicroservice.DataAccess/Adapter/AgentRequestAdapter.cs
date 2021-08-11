using System.Data;
using UserMicroservice.DataAccess.Adaptee;
using UserMicroservice.DataAccess.Target;

namespace UserMicroservice.DataAccess.Adapter
{
    public class AgentRequestAdapter : ITarget
    {
        private readonly AgentRequestAdaptee agentRequestAdaptee;

        public AgentRequestAdapter(AgentRequestAdaptee agentRequestAdaptee)
        {
            this.agentRequestAdaptee = agentRequestAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return agentRequestAdaptee.ConvertSqlDataReaderToAgentRequest(dataRow);
        }
    }
}