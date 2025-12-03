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
        private readonly MySqlEventDao _repo;

        public EventService(MySqlEventDao repo)
        {
            _repo = repo;
        }

        public Task<List<Event>> GetAllAsync() => _repo.GetAllAsync();

        public Task AddAsync(Event e)
        {
            // ID ya es GUID automáticamente
            return _repo.AddAsync(e);
        }

        public Task UpdateAsync(Event e)
        {
            return _repo.UpdateAsync(x => x.Id == e.Id, x =>
            {
                x.Name = e.Name;
                x.Date = e.Date;
                x.Venue = e.Venue;
                x.Type = e.Type;
                x.Price = e.Price;
                x.TicketsAvailable = e.TicketsAvailable;
                x.Description = e.Description;
            });
        }

        public Task DeleteAsync(string id)
        {
            return _repo.DeleteAsync(e => e.Id == id);
        }

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
