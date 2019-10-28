using Dapper;
using Insurance.Policies.Domain.Settings;
using Insurance.Policies.Infraestructure.Factories;
using Insurance.Policies.Infraestructure.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Repositories.Base
{
    public class Db : IDb
    {

        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_settings.Value.DataBaseSettings.StringConnection);
            }
        }
        
        public IOptions<AppSettings> _settings;
        public Db(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public async Task<T> CommandAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<T>> command)
        {

            using (var connection = Connection)
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await command(connection, transaction, 0);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                        throw;
                    }
                }
            }
        }

        public async Task<T> GetAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<T>> command)
        {
            return await CommandAsync(command);
        }

        public async Task<IList<T>> SelectAsync<T>(Func<SqlConnection, SqlTransaction, int, Task<IList<T>>> command)
        {
            return await CommandAsync(command);
        }

        public async Task ExecuteAsync(string sql, object parameters)
        {
            await CommandAsync(async (conn, trn, timeout) =>
            {
                await conn.ExecuteAsync(sql, parameters, trn, timeout);
                return 1;
            });
        }

        public async Task<T> GetFistAsync<T>(string sql, object parameters)
        {
            return await CommandAsync(async (conn, trn, timeout) =>
            {
                T result = await conn.QueryFirstOrDefaultAsync<T>(sql, parameters, trn, timeout);
                return result;
            });
        }

        public async Task<T> GetAsync<T>(string sql, object parameters)
        {
            return await CommandAsync(async (conn, trn, timeout) =>
            {
                T result = await conn.QuerySingleAsync<T>(sql, parameters, trn, timeout);
                return result;
            });
        }

        public async Task<IList<T>> SelectAsync<T>(string sql, object parameters)
        {
            return await CommandAsync<IList<T>>(async (conn, trn, timeout) =>
            {
                var result = (await conn.QueryAsync<T>(sql, parameters, trn, timeout)).ToList();
                return result;
            });
        }

        public void MigrateDataBase()
        {
            try
            {
                var migrater = MigrationFactory.Build(Connection);
                migrater.ExecuteCommand();
            }
            catch (Exception e)
            {
                throw new Exception("Database migration failed."+ e.Message);
            }
        }

    }
}
