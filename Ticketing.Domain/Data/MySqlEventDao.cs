using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Ticketing.Domain.Data
{
    public class MySqlEventDao : IJsonRepository<Event>
    {
        private readonly string _connStr;

        public MySqlEventDao(string connectionString)
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
                @"CREATE TABLE IF NOT EXISTS events(
                    Id VARCHAR(36) NOT NULL PRIMARY KEY,
                    Name VARCHAR(200) NOT NULL,
                    Date DATETIME NOT NULL,
                    Venue VARCHAR(200) NOT NULL,
                    Type VARCHAR(100) NOT NULL,
                    TicketsAvailable INT NOT NULL,
                    Price DECIMAL(10,2) NOT NULL,
                    Description TEXT
                );";
            create.ExecuteNonQuery();
        }

        public async Task AddAsync(Event entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO events (Id, Name, Date, Venue, Type, TicketsAvailable, Price, Description)
                                VALUES (@Id,@Name,@Date,@Venue,@Type,@TicketsAvailable,@Price,@Description);";
            //Parametrización de Consultas: Evitar Inyecciones SQL
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Venue", entity.Venue);
            cmd.Parameters.AddWithValue("@Type", entity.Type);
            cmd.Parameters.AddWithValue("@TicketsAvailable", entity.TicketsAvailable);
            cmd.Parameters.AddWithValue("@Price", entity.Price);
            cmd.Parameters.AddWithValue("@Description", entity.Description);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Func<Event, bool> predicate)
        {
            var all = await GetAllAsync();
            var targets = all.Where(predicate).ToList();
            if (!targets.Any()) return;

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            foreach (var t in targets)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM events WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Event?> GetAsync(Func<Event, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(predicate);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var list = new List<Event>();
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Date, Venue, Type, TicketsAvailable, Price, Description FROM events;";
            using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                list.Add(new Event
                {
                    Id = rdr.GetString(rdr.GetOrdinal("Id")),
                    Name = rdr.GetString(rdr.GetOrdinal("Name")),
                    Date = rdr.GetDateTime(rdr.GetOrdinal("Date")),
                    Venue = rdr.GetString(rdr.GetOrdinal("Venue")),
                    Type = rdr.GetString(rdr.GetOrdinal("Type")),
                    TicketsAvailable = rdr.GetInt32(rdr.GetOrdinal("TicketsAvailable")),
                    Price = rdr.GetDecimal(rdr.GetOrdinal("Price")),
                    Description = !rdr.IsDBNull(rdr.GetOrdinal("Description"))
                                    ? rdr.GetString(rdr.GetOrdinal("Description"))
                                    : ""
                });
            }
            return list;
        }

        public async Task<List<Event>> WhereAsync(Func<Event, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.Where(predicate).ToList();
        }

        public async Task UpdateAsync(Func<Event, bool> predicate, Action<Event> updater)
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
                cmd.CommandText = @"UPDATE events SET Name=@Name, Date=@Date, Venue=@Venue, Type=@Type,
                                    TicketsAvailable=@TicketsAvailable, Price=@Price, Description=@Description WHERE Id=@Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                cmd.Parameters.AddWithValue("@Name", t.Name);
                cmd.Parameters.AddWithValue("@Date", t.Date);
                cmd.Parameters.AddWithValue("@Venue", t.Venue);
                cmd.Parameters.AddWithValue("@Type", t.Type);
                cmd.Parameters.AddWithValue("@TicketsAvailable", t.TicketsAvailable);
                cmd.Parameters.AddWithValue("@Price", t.Price);
                cmd.Parameters.AddWithValue("@Description", t.Description);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
