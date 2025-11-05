using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Domain.Models;
using Ticketing.Domain.Data;


namespace Ticketing.Domain.Services
{
    public class PurchaseService
    {
        private readonly IJsonRepository<Purchase> _repo;
        private readonly EventService _eventService;
        private static readonly SemaphoreSlim _lock = new(1, 1);

        public PurchaseService(IJsonRepository<Purchase> repo, EventService eventService)
        {
            _repo = repo;
            _eventService = eventService;
        }

        // ✅ Este es el método que faltaba
        public async Task<Purchase> BuyAsync(string userId, Event ev, int quantity, string card)
        {
            if (quantity <= 0)
                throw new ArgumentException("Cantidad inválida.");
            if (string.IsNullOrWhiteSpace(card) || card.Length < 8)
                throw new ArgumentException("Tarjeta inválida (mínimo 8 dígitos).");
            if (!card.All(char.IsDigit))
                throw new ArgumentException("La tarjeta debe contener solo dígitos.");

            await _lock.WaitAsync();
            try
            {
                // Verificar stock actualizado
                var all = await _eventService.GetAllAsync();
                var current = all.FirstOrDefault(x => x.Id == ev.Id);
                if (current == null)
                    throw new InvalidOperationException("Evento no encontrado.");
                if (current.TicketsAvailable < quantity)
                    throw new InvalidOperationException("No hay suficientes tiquetes disponibles.");

                // Actualizar stock
                current.TicketsAvailable -= quantity;
                await _eventService.UpdateAsync(current);

                // Crear la compra
                var purchase = new Purchase
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    EventId = current.Id!,
                    Quantity = quantity,
                    Total = current.Price * quantity,
                    Date = DateTime.Now
                };

                await _repo.AddAsync(purchase);
                return purchase;
            }
            finally
            {
                _lock.Release();
            }
        }

        public Task<List<Purchase>> GetByUserAsync(string userId) =>
            _repo.WhereAsync(p => p.UserId == userId);

        public Task<List<Purchase>> GetAllAsync() => _repo.GetAllAsync();
    }
}
