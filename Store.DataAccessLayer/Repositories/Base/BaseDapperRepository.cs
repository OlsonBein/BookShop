using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.DataAccessLayer.Repositories.Base
{
    public class BaseDapperRepository<TEntity> : IBaseDapperRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public BaseDapperRepository()
        {
            _connectionString = DapperConstants.ConnectionString;
            _tableName = typeof(TEntity).Name;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT * FROM  {_tableName}s ";
                var entities = await connection.QueryAsync<TEntity>(sqlQuery);
                return entities.ToList();
            }
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.GetAsync<TEntity>(id);
                return result;
            }
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await CreateEntityAsync(connection, entity);
                return result;
            }
        }

        public async Task<bool> CreateRangeAsync(List<TEntity> entities)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = false;
                foreach (var item in entities)
                {
                    result = await CreateEntityAsync(connection, item);
                    
                }
                return result;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                entity.IsRemoved = true;
                var result = await UpdateEntityAsync(connection, entity);
                return result;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<TEntity> entities)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = false;
                foreach (var item in entities)
                {
                    item.IsRemoved = true;
                    result = await UpdateEntityAsync(connection, item);
                }
                return result;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await UpdateEntityAsync(connection, entity);
                return result;
            }
        }

        public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = false;
                foreach (var item in entities)
                {
                    result = await UpdateEntityAsync(connection, item);                    
                }
                return result;
            }
        }

        private async Task<bool> CreateEntityAsync(SqlConnection connection, TEntity entity)
        {
            var result = await connection.InsertAsync(entity);
            return result > 0;   
        }

        private async Task<bool> UpdateEntityAsync(SqlConnection connection, TEntity entity)
        {
            var result = await connection.UpdateAsync(entity);
            return result;
        }
    }
}
