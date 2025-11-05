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
    public interface IJsonRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(Func<T, bool> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(Func<T, bool> predicate, Action<T> updater);
        Task DeleteAsync(Func<T, bool> predicate);
        Task<List<T>> WhereAsync(Func<T, bool> predicate);
    }
}
