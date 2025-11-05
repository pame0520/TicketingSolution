
using System;

namespace Ticketing.Domain.Models
{
    public class Purchase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public string EventId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string EventName { get; set; } = string.Empty;
    }
}