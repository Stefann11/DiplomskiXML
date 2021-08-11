using ReportMicroservice.DataAccess.Adaptee;
using ReportMicroservice.DataAccess.Target;
using System.Data;

namespace ReportMicroservice.DataAccess.Adapter
{
    public class ReportAdapter : ITarget
    {
        private readonly ReportAdaptee reportAdaptee;

        public ReportAdapter(ReportAdaptee reportAdaptee)
        {
            this.reportAdaptee = reportAdaptee;
        }

        public object ConvertSql(DataRow dataRow)
        {
            return reportAdaptee.ConvertSqlDataReaderToReport(dataRow);
        }
    }
}