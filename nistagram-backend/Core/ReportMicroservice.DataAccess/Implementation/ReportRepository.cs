using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using ReportMicroservice.Core.Interface.Repository;
using ReportMicroservice.Core.Model;
using ReportMicroservice.DataAccess.Adaptee;
using ReportMicroservice.DataAccess.Adapter;
using ReportMicroservice.DataAccess.Target;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace ReportMicroservice.DataAccess.Implementation
{
    public class ReportRepository : Repository, IReportRepository
    {
        public ITarget _reportTarget = new ReportAdapter(new ReportAdaptee());

        public ReportRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Report> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT report.id, report.timestamp, " +
                "report.report_reason, report.type, report.content_id, report.action, r.id, r.username ");
            queryBuilder.Append("FROM dbo.Report AS report, dbo.RegisteredUser AS r ");
            queryBuilder.Append("WHERE report.registered_user_id = r.id AND report.action = \'Created\' " +
                "ORDER BY report.timestamp DESC;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Report)_reportTarget.ConvertSql(dataRow)).ToList();
        }

        public Maybe<Report> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Report Save(Report report)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Report ");
            queryBuilder.Append("(id, timestamp, report_reason, registered_user_id, type, content_id, action) ");
            queryBuilder.Append("VALUES (@id, @timestamp, @report_reason, @registered_user_id, @type, @content_id, @action);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = report.Id },
                new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = report.TimeStamp },
                new SqlParameter("@report_reason", SqlDbType.NVarChar) { Value = report.ReportReason.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = report.RegisteredUser.Id },
                new SqlParameter("@type", SqlDbType.NVarChar) { Value = report.Content.GetType().Name },
                new SqlParameter("@content_id", SqlDbType.UniqueIdentifier) { Value = report.Content.Id },
                new SqlParameter("@action", SqlDbType.NVarChar) { Value = report.ReportAction.ToString() }
            };

            ExecuteQuery(query, parameters);

            return report;
        }

        public Report Edit(Report report)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Report ");
            queryBuilder.Append("SET timestamp = @timestamp, report_reason = @report_reason, " +
                "registered_user_id = @registered_user_id, type = @type, content_id = @content_id, action = @action ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = report.Id },
                new SqlParameter("@timestamp", SqlDbType.NVarChar) { Value = report.TimeStamp },
                new SqlParameter("@report_reason", SqlDbType.NVarChar) { Value = report.ReportReason.ToString() },
                new SqlParameter("@registered_user_id", SqlDbType.UniqueIdentifier) { Value = report.RegisteredUser.Id },
                new SqlParameter("@type", SqlDbType.NVarChar) { Value = report.Content.GetType().Name },
                new SqlParameter("@content_id", SqlDbType.UniqueIdentifier) { Value = report.Content.Id },
                new SqlParameter("@action", SqlDbType.NVarChar) { Value = report.ReportAction.ToString() }
            };

            ExecuteQuery(query, parameters);

            return report;
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}