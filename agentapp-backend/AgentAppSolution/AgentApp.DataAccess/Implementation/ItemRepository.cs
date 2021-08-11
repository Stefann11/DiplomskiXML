using AgentApp.Core.Interface.Repository;
using AgentApp.Core.Model;
using AgentApp.DataAccess.Adaptee;
using AgentApp.DataAccess.Adapter;
using AgentApp.DataAccess.Target;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace AgentApp.DataAccess.Implementation
{
    public class ItemRepository : Repository, IItemRepository
    {
        public ITarget _itemAdapter = new ItemAdapter(new ItemAdaptee());

        public ItemRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Item> GetAll()
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT id, name, item_image_path, price, available_count ");
            queryBuilder.Append("FROM dbo.Item ");

            List<SqlParameter> parameters = new List<SqlParameter>();

            string query = queryBuilder.ToString();

            DataTable dataTable = ExecuteQuery(query, parameters);

            return (from DataRow dataRow in dataTable.Rows
                    select (Item)_itemAdapter.ConvertSql(dataRow)).ToList();
        }

        public Maybe<Item> GetById(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("SELECT id, name, item_image_path, price, available_count ");
            queryBuilder.Append("FROM dbo.Item ");
            queryBuilder.Append("WHERE id = @Id");

            string query = queryBuilder.ToString();

            SqlParameter parameterId = new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id };

            List<SqlParameter> parameters = new List<SqlParameter>() { parameterId };

            DataTable dataTable = ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                return (Item)_itemAdapter.ConvertSql(dataTable.Rows[0]);
            }
            return Maybe<Item>.None;
        }

        public Item Save(Item item)
        {
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO dbo.Item ");
            queryBuilder.Append("(id, name, item_image_path, price, available_count ) ");
            queryBuilder.Append("VALUES (@id, @name, @item_image_path, @price, @available_count);");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = item.Id },
                new SqlParameter("@name", SqlDbType.NVarChar) { Value = item.Name.ToString() },
                new SqlParameter("@item_image_path", SqlDbType.NVarChar) { Value = item.ItemImagePath.ToString() },
                new SqlParameter("@price", SqlDbType.Float) { Value = item.Price.ToString() },
                new SqlParameter("@available_count", SqlDbType.Int) { Value =  item.AvailableCount.ToString() }
            };

            ExecuteQuery(query, parameters);

            return item;
        }

        public Item Edit(Item item)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Item ");
            queryBuilder.Append("SET name = @name, item_image_path = @item_image_path, " +
                "price = @price, available_count = @available_count ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = item.Id },
                new SqlParameter("@name", SqlDbType.NVarChar) { Value = item.Name.ToString() },
                new SqlParameter("@item_image_path", SqlDbType.NVarChar) { Value = item.ItemImagePath.ToString() },
                new SqlParameter("@price", SqlDbType.Float) { Value = item.Price.ToString() },
                new SqlParameter("@available_count", SqlDbType.Int) { Value = item.AvailableCount.ToString() }
            };

            ExecuteQuery(query, parameters);

            return item;
        }

        public void Delete(Guid id)
        {
            StringBuilder queryBuilder = new StringBuilder("DELETE FROM dbo.Item ");
            queryBuilder.Append("WHERE id = @id ");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
             {
                 new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = id },
             };

            ExecuteQuery(query, parameters);
        }

        public Item Buy(Item item, int quantity)
        {
            StringBuilder queryBuilder = new StringBuilder("UPDATE dbo.Item ");
            queryBuilder.Append("SET available_count = @available_count ");
            queryBuilder.Append("WHERE id = @id;");

            string query = queryBuilder.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", SqlDbType.UniqueIdentifier) { Value = item.Id },
                new SqlParameter("@available_count", SqlDbType.Int) { Value = quantity }
            };

            ExecuteQuery(query, parameters);

            return item;
        }
    }
}