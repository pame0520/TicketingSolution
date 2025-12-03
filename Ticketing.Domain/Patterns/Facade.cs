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
    // ⛔ ESTA CLASE YA NO SE USA CON MYSQL
    // ✔ Solo se mantiene para compatibilidad antigua con JSON
    // ✔ No genera errores
    // ✔ No se usa en AppServices cuando useMySql = true

    public class TicketingFacade
    {
        // Clase desactivada para MySQL
        // Si se requiere usar JSON, se puede restaurar la implementación original.
    }
}