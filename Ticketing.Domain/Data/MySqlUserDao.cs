using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Ticketing.Domain.Data
{
    public class MySqlUserDao : IJsonRepository<User>
    {
        private readonly string _connStr;

        public MySqlUserDao(string connectionString)
        {
            _connStr = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            EnsureDatabaseAndTables();
        }

        private void EnsureDatabaseAndTables()
        {
            var builder = new MySqlConnectionStringBuilder(_connStr);
            var db = builder.Database ?? "ticketing";
            // Conexión al servidor (sin base de datos) para crear la base si hace falta
            var serverBuilder = new MySqlConnectionStringBuilder(_connStr) { Database = string.Empty };
            using var serverConn = new MySqlConnection(serverBuilder.ToString());
            serverConn.Open();
            using (var cmd = serverConn.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {db};";
                cmd.ExecuteNonQuery();
            }

            // Crear tabla users
            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var create = conn.CreateCommand();
            create.CommandText =
                @"CREATE TABLE IF NOT EXISTS users(
                    Id VARCHAR(36) NOT NULL PRIMARY KEY,
                    Name VARCHAR(200) NOT NULL,
                    Email VARCHAR(200) NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    Role VARCHAR(50) NOT NULL
                );";
            create.ExecuteNonQuery();
        }

        public async Task AddAsync(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            //Parametrización de Consultas: Evitar Inyecciones SQL
            cmd.CommandText = "INSERT INTO users (Id, Name, Email, PasswordHash, Role) VALUES (@Id,@Name,@Email,@PasswordHash,@Role);";
            cmd.Parameters.AddWithValue("@Id", entity.Id);
            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Email", entity.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", entity.Role);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Func<User, bool> predicate)
        {
            var all = await GetAllAsync();
            var targets = all.Where(predicate).ToList();
            if (!targets.Any()) return;

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            foreach (var t in targets)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM users WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<User?> GetAsync(Func<User, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(predicate);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var list = new List<User>();
            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Email, PasswordHash, Role FROM users;";
            using var rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                list.Add(new User
                {
                    Id = rdr.GetString(rdr.GetOrdinal("Id")),
                    Name = rdr.GetString(rdr.GetOrdinal("Name")),
                    Email = rdr.GetString(rdr.GetOrdinal("Email")),
                    PasswordHash = rdr.GetString(rdr.GetOrdinal("PasswordHash")),
                    Role = rdr.GetString(rdr.GetOrdinal("Role"))
                });
            }
            return list;
        }


        public async Task<List<User>> WhereAsync(Func<User, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.Where(predicate).ToList();
        }

        public async Task UpdateAsync(Func<User, bool> predicate, Action<User> updater)
        {
            var all = await GetAllAsync();
            var targets = all.Where(predicate).ToList();
            if (!targets.Any()) return;

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();
            foreach (var t in targets)
            {
                // aplicar cambios en memoria
                updater(t);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE users SET Name=@Name, Email=@Email, PasswordHash=@PasswordHash, Role=@Role WHERE Id=@Id;";
                cmd.Parameters.AddWithValue("@Id", t.Id);
                cmd.Parameters.AddWithValue("@Name", t.Name);
                cmd.Parameters.AddWithValue("@Email", t.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", t.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", t.Role);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}