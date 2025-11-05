
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Ticketing.Domain.Data
{
    public class JsonFileRepository<T> : IJsonRepository<T> where T : class
    {
        private readonly string _path;
        private readonly SemaphoreSlim _sem = new SemaphoreSlim(1, 1);
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public JsonFileRepository(string path)
        {
            _path = path;
            var dir = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
        }

        private async Task<List<T>> LoadAsync()
        {
            await _sem.WaitAsync();
            try
            {
                if (!File.Exists(_path)) return new List<T>();
                var text = await File.ReadAllTextAsync(_path);
                if (string.IsNullOrWhiteSpace(text)) return new List<T>();
                return JsonSerializer.Deserialize<List<T>>(text, _jsonOptions) ?? new List<T>();
            }
            finally { _sem.Release(); }
        }

        private async Task SaveAsync(List<T> items)
        {
            await _sem.WaitAsync();
            try
            {
                var text = JsonSerializer.Serialize(items, _jsonOptions);
                await File.WriteAllTextAsync(_path, text);
            }
            finally { _sem.Release(); }
        }

        public async Task<List<T>> GetAllAsync() => await LoadAsync();

        public async Task<T?> GetAsync(Func<T, bool> predicate)
        {
            var all = await LoadAsync();
            return all.FirstOrDefault(predicate);
        }

        public async Task AddAsync(T entity)
        {
            var all = await LoadAsync();
            all.Add(entity);
            await SaveAsync(all);
        }

        public async Task UpdateAsync(Func<T, bool> predicate, Action<T> updater)
        {
            var all = await LoadAsync();
            var item = all.FirstOrDefault(predicate);
            if (item != null)
            {
                updater(item);
                await SaveAsync(all);
            }
        }

        public async Task DeleteAsync(Func<T, bool> predicate)
        {
            var all = await LoadAsync();
            var remaining = all.Where(x => !predicate(x)).ToList();
            await SaveAsync(remaining);
        }

        public async Task<List<T>> WhereAsync(Func<T, bool> predicate)
        {
            var all = await LoadAsync();
            return all.Where(predicate).ToList();
        }
    }
}