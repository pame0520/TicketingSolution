using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Ticketing.Domain.Models;

namespace Ticketing.Domain.Data
{
    public static class JsonToMySqlMigrator
    {
        /// <summary>
        /// Lee los JSON (users.json, events.json, purchases.json) en dataPath e inserta los registros
        /// en la base MySQL indicada por connectionString. Tras migrar mueve los JSON a Data\backup\.
        /// No borra los archivos originales: los mueve como respaldo.
        /// </summary>
        public static async Task MigrateIfNeededAsync(string dataPath, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(dataPath)) throw new ArgumentNullException(nameof(dataPath));
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            Directory.CreateDirectory(dataPath);
            var backupDir = Path.Combine(dataPath, "backup");
            Directory.CreateDirectory(backupDir);

            var userDao = new MySqlUserDao(connectionString);
            var eventDao = new MySqlEventDao(connectionString);
            var purchaseDao = new MySqlPurchaseDao(connectionString);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Usuarios
            var usersFile = Path.Combine(dataPath, "users.json");
            if (File.Exists(usersFile))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(usersFile);
                    var users = JsonSerializer.Deserialize<List<User>>(json, options) ?? new List<User>();
                    foreach (var u in users)
                    {
                        // Evitar duplicados por email
                        var exists = await userDao.GetAsync(x => x.Email.Equals(u.Email, StringComparison.OrdinalIgnoreCase));
                        if (exists == null)
                        {
                            // conservamos Id y PasswordHash tal como vienen
                            await userDao.AddAsync(u);
                        }
                    }

                    // mover a backup con marca de tiempo
                    var dest = Path.Combine(backupDir, $"users_{DateTime.Now:yyyyMMddHHmmss}.json");
                    File.Move(usersFile, dest, overwrite: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Error migrando '{usersFile}': {ex.Message}", ex);
                }
            }

            // Eventos
            var eventsFile = Path.Combine(dataPath, "events.json");
            if (File.Exists(eventsFile))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(eventsFile);
                    var events = JsonSerializer.Deserialize<List<Event>>(json, options) ?? new List<Event>();
                    foreach (var e in events)
                    {
                        var exists = await eventDao.GetAsync(x => x.Id == e.Id);
                        if (exists == null)
                        {
                            await eventDao.AddAsync(e);
                        }
                    }

                    var dest = Path.Combine(backupDir, $"events_{DateTime.Now:yyyyMMddHHmmss}.json");
                    File.Move(eventsFile, dest, overwrite: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Error migrando '{eventsFile}': {ex.Message}", ex);
                }
            }

            // Compras
            var purchasesFile = Path.Combine(dataPath, "purchases.json");
            if (File.Exists(purchasesFile))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(purchasesFile);
                    var purchases = JsonSerializer.Deserialize<List<Purchase>>(json, options) ?? new List<Purchase>();
                    foreach (var p in purchases)
                    {
                        var exists = await purchaseDao.GetAsync(x => x.Id == p.Id);
                        if (exists == null)
                        {
                            await purchaseDao.AddAsync(p);
                        }
                    }

                    var dest = Path.Combine(backupDir, $"purchases_{DateTime.Now:yyyyMMddHHmmss}.json");
                    File.Move(purchasesFile, dest, overwrite: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Error migrando '{purchasesFile}': {ex.Message}", ex);
                }
            }
        }
    }
}