using System;
using System.IO;
using Ticketing.Domain.Data;
using Ticketing.Domain.Models;
using Ticketing.Domain.Services;
using Ticketing.Patterns;
using Ticketing.Domain.Patterns;

namespace Ticketing.WinForms
{
    public class AppServices
    {
        public EventService EventService { get; }
        public UserService UserService { get; }
        public PurchaseService PurchaseService { get; }

        // Notificador para la UI
        public INotifier Notifier { get; }

        // Facade ya no se usa con MySQL, pero lo dejamos nullable
        public TicketingFacade? Facade { get; }

        public AppServices(string dataPath, string mysqlConnectionString)
        {
            // Por si algún día quieres seguir guardando JSON de respaldo
            Directory.CreateDirectory(dataPath);

            Notifier = new Notifier();

            // Conexión MySQL
            string conn = mysqlConnectionString;

            // DAOs MySQL
            var userRepo = new MySqlUserDao(conn);
            var eventRepo = new MySqlEventDao(conn);
            var purchaseRepo = new MySqlPurchaseDao(conn);

            // Services MySQL
            UserService = new UserService(userRepo);
            EventService = new EventService(eventRepo);
            PurchaseService = new PurchaseService(purchaseRepo, EventService);

            // Con MySQL no usamos Facade JSON
            Facade = null;
        }
    }
}