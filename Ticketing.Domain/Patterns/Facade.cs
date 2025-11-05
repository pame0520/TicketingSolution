using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Data;
using Ticketing.Domain.Models;
using Ticketing.Domain.Services;

namespace Ticketing.Domain.Patterns
{
    public class TicketingFacade
    {
        private readonly UserService _userService;
        private readonly EventService _eventService;
        private readonly PurchaseService _purchaseService;

        // Cambia el constructor para que reciba los repositorios de tipo IJsonRepository
        public TicketingFacade(IJsonRepository<User> userRepo, IJsonRepository<Event> eventRepo, IJsonRepository<Purchase> purchaseRepo)
        {
            _userService = new UserService(userRepo);
            _eventService = new EventService(eventRepo);
            _purchaseService = new PurchaseService(purchaseRepo, _eventService);
        }

        // Métodos expuestos al sistema (simplifica AppServices)

        public Task<User?> LoginAsync(string email, string pass) => _userService.LoginAsync(email, pass);
        public Task<List<Event>> GetEventsAsync() => _eventService.GetAllAsync();
        public Task<List<Purchase>> GetUserPurchasesAsync(string userId) => _purchaseService.GetByUserAsync(userId);
        public Task<Purchase> BuyTicketAsync(User user, Event ev, int qty, string card) => _purchaseService.BuyAsync(user.Id, ev, qty, card);
    }
}