using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ticketing.Domain.Data;
using Ticketing.Domain.Models;
using Ticketing.Domain.Patterns;
using Ticketing.Domain.Services;
using Ticketing.Patterns;


namespace Ticketing.WinForms
{
    public class AppServices
    {
        public EventService EventService { get; }
        public UserService UserService { get; }
        public PurchaseService PurchaseService { get; }

        public TicketingFacade Facade { get; }

        // Notifier para la UI
        public INotifier Notifier { get; }

        public AppServices(string dataPath)
        {
            Directory.CreateDirectory(dataPath);

            var usersPath = Path.Combine(dataPath, "users.json");
            var eventsPath = Path.Combine(dataPath, "events.json");
            var purchasesPath = Path.Combine(dataPath, "purchases.json");

            // Usamos el Factory Method para crear repositorios (pasar el tipo de modelo, no la interfaz)
            var userRepo = RepositoryFactory.Create<User>(usersPath);
            var eventRepo = RepositoryFactory.Create<Event>(eventsPath);
            var purchaseRepo = RepositoryFactory.Create<Purchase>(purchasesPath);

            // Servicios clásicos
            UserService = new UserService(userRepo);
            EventService = new EventService(eventRepo);
            PurchaseService = new PurchaseService(purchaseRepo, EventService);

            // Notifier
            Notifier = new Notifier();

            // Facade
            Facade = new TicketingFacade(userRepo, eventRepo, purchaseRepo);
        }
    }
}