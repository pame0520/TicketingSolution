
using System;

namespace Ticketing.Domain.Models
{
    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int TicketsAvailable { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}