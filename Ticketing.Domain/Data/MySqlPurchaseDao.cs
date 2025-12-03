using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Ticketing.Domain.Data
{
    public class MySqlPurchaseDao : IJsonRepository<Purchase>
    {
        private readonly string _connStr;

        public MySqlPurchaseDao(string connectionString)
        {
            _connStr = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            EnsureDatabaseAndTables();
        }

        private void EnsureDatabaseAndTables()
        {
            var builder = new MySqlConnectionStringBuilder(_connStr);
            var db = builder.Database ?? "ticketing";
            var serverBuilder = new MySqlConnectionStringBuilder(_connStr) { Database = string.Empty };
            using var serverConn = new MySqlConnection(serverBuilder.ToString());
            serverConn.Open();
            using (var cmd = serverConn.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {db};";
                cmd.ExecuteNonQuery();
            }

            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var create = conn.CreateCommand();
            create.CommandText =
                @"CREATE TABLE IF NOT EXISTS purchases(
                    Id VARCHAR(36) NOT NULL PRIMARY KEY,
                    UserId VARCHAR(36) NOT NULL,
                    EventId VARCHAR(36) NOT NULL,
                    Quantity INT NOT NULL,
                    Total DECIMAL(10,2) NOT NULL,
                    Date DATETIME NOT NULL,
                    EventName VARCHAR(200)
                );";
            create.ExecuteNonQuery();
        }

        public async Task AddAsync(Purchase entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO purchases (Id, UserId, EventId, Quantity, Total, Date, EventName)
                                VALUES (@Id,@UserId,@EventId,@Quantity,@Total,@Date,@EventName);";
            //Parametrización de Consultas: Evitar Inyecciones SQL
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
            cmd.Parameters.AddWithValue("@Total", entity.Total);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@EventName", entity.EventName);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Func<Purchase, bool> predicate)
        {
            var all = await GetAllAsync();
            var targets = all.Where(predicate).ToList();
            if (!targets.Any()) return;

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            foreach (var t in targets)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM purchases WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Purchase?> GetAsync(Func<Purchase, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(predicate);
        }

        public async Task<List<Purchase>> GetAllAsync()
        {
            var list = new List<Purchase>();
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, UserId, EventId, Quantity, Total, Date, EventName FROM purchases;";
            using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                list.Add(new Purchase
                {
                    Id = rdr.GetString(rdr.GetOrdinal("Id")),
                    UserId = rdr.GetString(rdr.GetOrdinal("UserId")),
                    EventId = rdr.GetString(rdr.GetOrdinal("EventId")),
                    Quantity = rdr.GetInt32(rdr.GetOrdinal("Quantity")),
                    Total = rdr.GetDecimal(rdr.GetOrdinal("Total")),
                    Date = rdr.GetDateTime(rdr.GetOrdinal("Date")),
                    EventName = !rdr.IsDBNull(rdr.GetOrdinal("EventName"))
                                    ? rdr.GetString(rdr.GetOrdinal("EventName"))
                                    : ""
                });
            }
            return list;
        }

        public async Task<List<Purchase>> WhereAsync(Func<Purchase, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.Where(predicate).ToList();
        }

        public async Task UpdateAsync(Func<Purchase, bool> predicate, Action<Purchase> updater)
        {
            var all = await GetAllAsync();
            var targets = all.Where(predicate).ToList();
            if (!targets.Any()) return;

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            foreach (var t in targets)
            {
                updater(t);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE purchases SET UserId=@UserId, EventId=@EventId, Quantity=@Quantity,
                                    Total=@Total, Date=@Date, EventName=@EventName WHERE Id=@Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                cmd.Parameters.AddWithValue("@UserId", t.UserId);
                cmd.Parameters.AddWithValue("@EventId", t.EventId);
                cmd.Parameters.AddWithValue("@Quantity", t.Quantity);
                cmd.Parameters.AddWithValue("@Total", t.Total);
                cmd.Parameters.AddWithValue("@Date", t.Date);
                cmd.Parameters.AddWithValue("@EventName", t.EventName);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}