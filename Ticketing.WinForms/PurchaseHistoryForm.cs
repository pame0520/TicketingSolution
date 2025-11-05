using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticketing.Domain.Models;

namespace Ticketing.WinForms
{
    public partial class PurchaseHistoryForm : Form
    {
        private readonly AppServices _services;
        private readonly User _user;

        public PurchaseHistoryForm(AppServices services, User user)
        {
            InitializeComponent();
            _services = services;
            _user = user;
        }

        private async void PurchaseHistoryForm_Load(object sender, EventArgs e)
        {
            // 1) Traer compras del usuario
            var items = await _services.PurchaseService.GetByUserAsync(_user.Id);

            // 2) Traer nombres de eventos (si Purchase solo guarda EventId)
            var events = (await _services.EventService.GetAllAsync())
                         .ToDictionary(x => x.Id, x => x.Name);

            // 3) Proyectar a la grilla
            gridPurchases.DataSource = items.Select(p => new
            {
                Evento = events.TryGetValue(p.EventId, out var name) ? name : p.EventId,
                Fecha = p.Date.ToString("yyyy-MM-dd HH:mm"),
                Cantidad = p.Quantity,
                Total = p.Total.ToString("C0", CultureInfo.GetCultureInfo("es-CR"))
            })
            .ToList();
        }

        private void gridPurchases_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
