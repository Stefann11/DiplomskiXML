using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using PostMicroservice.Core.Interface.Repository;
using PostMicroservice.Core.Model;
using PostMicroservice.DataAccess.Adaptee;
using PostMicroservice.DataAccess.Adapter;
using PostMicroservice.DataAccess.Target;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMicroservice.DataAccess.Implementation
{
    public class LocationRepository : Repository, ILocationRepository
    {
        public ITarget _locationTarget = new LocationAdapter(new LocationAdaptee());

        public LocationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Location> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT l.id, l.street, l.city_name, l.country ");
            queryBuilder.Append("FROM dbo.Location AS l;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Location)_locationTarget.ConvertSql(dataRow)).ToList();
        }

        public Maybe<Location> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT l.id, l.street, l.city_name, l.country ");
            queryBuilder.Append("FROM dbo.Location AS l ");
            queryBuilder.Append("WHERE l.Id = @Id;");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Location)_locationTarget.ConvertSql(dataTable.Rows[0]);
            }

            return null;
        }

        public Location Save(Location obj)
        {
            throw new NotImplementedException();
        }

        public Location Edit(Location obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetByText(string text)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT distinct id, street, city_name, country ");
            queryBuilder.Append("FROM dbo.Location ");
            queryBuilder.Append("WHERE LOWER(city_name) LIKE LOWER(@Text) OR LOWER(street)LIKE LOWER(@Text)" +
                " OR LOWER(country) LIKE LOWER(@Text);");

            string query = queryBuilder.ToString();

            SqlParameter parameterText = new SqlParameter("@Text", SqlDbType.NVarChar) { Value = "%" + text + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterText };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Location)_locationTarget.ConvertSql(dataRow));
        }

        public IEnumerable<Location> GetCountryByText(string text)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT distinct id, street, city_name, country ");
            queryBuilder.Append("FROM dbo.Location ");
            queryBuilder.Append("WHERE LOWER(country) LIKE LOWER(@Text);");

            string query = queryBuilder.ToString();

            SqlParameter parameterText = new SqlParameter("@Text", SqlDbType.NVarChar) { Value = "%" + text + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterText };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Location)_locationTarget.ConvertSql(dataRow));
        }

        public IEnumerable<Location> GetCityByText(string text)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT distinct id, street, city_name, country ");
            queryBuilder.Append("FROM dbo.Location ");
            queryBuilder.Append("WHERE LOWER(city_name) LIKE LOWER(@Text);");

            string query = queryBuilder.ToString();

            SqlParameter parameterText = new SqlParameter("@Text", SqlDbType.NVarChar) { Value = "%" + text + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterText };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Location)_locationTarget.ConvertSql(dataRow));
        }

        public IEnumerable<Location> GetStreetByText(string text)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT distinct id, street, city_name, country ");
            queryBuilder.Append("FROM dbo.Location ");
            queryBuilder.Append("WHERE LOWER(street) LIKE LOWER(@Text);");

            string query = queryBuilder.ToString();

            SqlParameter parameterText = new SqlParameter("@Text", SqlDbType.NVarChar) { Value = "%" + text + "%" };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterText };

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Location)_locationTarget.ConvertSql(dataRow));
        }
    }
}