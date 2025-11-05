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
        private List<Purchase> _items = new();

        public PurchaseHistoryForm(AppServices services, User user)
        {
            InitializeComponent();
            _services = services;
            _user = user;
        }

        private async void PurchaseHistoryForm_Load(object sender, EventArgs e)
        {
            // 1) Traer compras del usuario
            _items = await _services.PurchaseService.GetByUserAsync(_user.Id);

            // 2) Traer nombres de eventos (si Purchase solo guarda EventId)
            var events = (await _services.EventService.GetAllAsync())
                         .ToDictionary(x => x.Id, x => x.Name);

            // 3) Proyectar a la grilla
            gridPurchases.DataSource = _items.Select(p => new
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

        // Añadir este manejador y vincularlo al botón "Descargar tickets" en el diseñador
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurarnos de tener las compras más recientes
                _items = await _services.PurchaseService.GetByUserAsync(_user.Id);
                var events = (await _services.EventService.GetAllAsync())
                             .ToDictionary(x => x.Id, x => x.Name);

                using var sfd = new SaveFileDialog
                {
                    Filter = "CSV|*.csv|Texto|*.txt",
                    FileName = $"tickets_{_user.Email}_{DateTime.Now:yyyyMMddHHmm}.csv",
                    Title = "Guardar tickets"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using var sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                // Encabezado
                sw.WriteLine("Evento,Fecha,Cantidad,Total");

                foreach (var p in _items)
                {
                    var evName = events.TryGetValue(p.EventId, out var n) ? n : p.EventId;
                    var date = p.Date.ToString("yyyy-MM-dd HH:mm");
                    var total = p.Total.ToString("C0", CultureInfo.GetCultureInfo("es-CR"));
                    // Escape básico de comas
                    var safeName = evName.Contains(",") ? $"\"{evName.Replace("\"", "\"\"")}\"" : evName;
                    sw.WriteLine($"{safeName},{date},{p.Quantity},{total}");
                }

                MessageBox.Show("Tickets exportados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exportando tickets: {ex.Message}");
            }
        }
    }
}
