using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using Ticketing.Domain.Data;
using Ticketing.Domain.Utils;

namespace Ticketing.Domain.Services
{
    public class EventService
    {
        private readonly IJsonRepository<Event> _repo;

        public EventService(IJsonRepository<Event> repo) => _repo = repo;

        public Task<List<Event>> GetAllAsync() => _repo.GetAllAsync();

       // public Task AddAsync(Event e) => _repo.AddAsync(e);
        public async Task AddAsync(Event e)
        {
            // Generar IDs secuenciales del tipo e1, e2, e3...
            var all = await _repo.GetAllAsync();
            int max = 0;
            foreach (var ev in all)
            {
                var id = ev?.Id;
                if (string.IsNullOrWhiteSpace(id)) continue;
                if ((id.StartsWith("e") || id.StartsWith("E")) && int.TryParse(id.Substring(1), out var n))
                {
                    if (n > max) max = n;
                }
            }

            e.Id = $"e{max + 1}";
            await _repo.AddAsync(e);
        }


        public Task UpdateAsync(Event e) => _repo.UpdateAsync(x => x.Id == e.Id, x =>
        {
            x.Name = e.Name;
            x.Date = e.Date;
            x.Venue = e.Venue;
            x.Type = e.Type;
            x.Price = e.Price;
            x.TicketsAvailable = e.TicketsAvailable;
            x.Description = e.Description;
        });

        // ✅ Método agregado: eliminación de eventos
        public Task DeleteAsync(string id)
        {
            return _repo.DeleteAsync(e => e.Id == id);
        }

        /// <summary>
        /// Búsqueda con filtros. Cambia recursive=true/false para comparar enfoques.
        /// </summary>
        public async Task<List<Event>> SearchAsync(
            string? text, DateTime? date, string? type, string? venue, bool recursive = true)
        {
            var all = await _repo.GetAllAsync();
            var preds = new List<Func<Event, bool>>();

            if (!string.IsNullOrWhiteSpace(text))
                preds.Add(e => e.Name.Contains(text, StringComparison.OrdinalIgnoreCase)
                            || e.Description.Contains(text, StringComparison.OrdinalIgnoreCase));

            if (date.HasValue)
                preds.Add(e => e.Date.Date == date.Value.Date);

            if (!string.IsNullOrWhiteSpace(type))
                preds.Add(e => e.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(venue))
                preds.Add(e => e.Venue.Contains(venue, StringComparison.OrdinalIgnoreCase));

            return recursive
                ? SearchUtils.RecursiveFilter(all, preds)
                : SearchUtils.IterativeFilter(all, preds);
        }
    }
}
