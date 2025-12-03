using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticketing.Domain.Models;
using Ticketing.Domain.Patterns;
using Ticketing.Domain.Services;
using Ticketing.WinForms.Data;

namespace Ticketing.WinForms
{
    public partial class EventListForm : Form, IEventObserver
    {
        private readonly AppServices _services = null!;
        private readonly User _currentUser = null!;

        public EventListForm(AppServices services, User user)
        {
            InitializeComponent();
            _services = services;
            _currentUser = user;

            // Control simple de permisos: solo Admin puede CRUD de eventos
            var isAdmin = _currentUser?.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
            btnAddEvent.Visible = isAdmin;
            btnEditEvent.Visible = isAdmin;
            btnDeleteEvent.Visible = isAdmin;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _services.Notifier.Subscribe(this);
            _ = LoadEventsAsync(txtSearch.Text.Trim());
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _services.Notifier.Unsubscribe(this);
            base.OnFormClosed(e);
        }

        public void OnEventUpdated(string message)
        {
            _ = LoadEventsAsync(txtSearch.Text.Trim());
            MessageBox.Show(message, "Notificación");
        }

        private async Task LoadEventsAsync(string? text = null)
        {
            List<Event> data;
            if (string.IsNullOrWhiteSpace(text))
                data = await _services.EventService.GetAllAsync();
            else
                data = await _services.EventService.SearchAsync(text, null, null, null, recursive: true);

            gridEvents.AutoGenerateColumns = true;
            gridEvents.DataSource = data
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    Date = e.Date.ToString("yyyy-MM-dd"),
                    e.Venue,
                    e.Type,
                    e.TicketsAvailable,
                    e.Price
                })
                .ToList();
        }
        // Agregar evento
        private async void btnAddEvent_Click(object sender, EventArgs e)
        {
            using (var frm = new EventForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var newEvent = frm.CurrentEvent;
                    // el Id ahora lo asigna EventService de forma secuencial (e1,e2,...)
                    await _services.EventService.AddAsync(newEvent);
                    await LoadEventsAsync(txtSearch.Text.Trim());
                    MessageBox.Show("Evento agregado correctamente.", "Éxito");
                }
            }
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadEventsAsync(txtSearch.Text.Trim());
        }

        private async void EventListForm_Load(object sender, EventArgs e)
        {
            await LoadEventsAsync("");
        }

        // Doble click abre formulario de compra
        private async void gridEvents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var cell = gridEvents.Rows[e.RowIndex].Cells["Id"]?.Value;
            if (cell == null) return;
            var id = cell.ToString();
            if (string.IsNullOrWhiteSpace(id)) return;

            var all = await _services.EventService.GetAllAsync();
            var selected = all.FirstOrDefault(ev => ev.Id == id);
            if (selected == null) return;

            using (var frm = new PurchaseForm(_services, _currentUser, selected))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadEventsAsync(txtSearch.Text.Trim());
            }
        }



        // Editar evento
        private async void btnEditEvent_Click(object sender, EventArgs e)
        {
            if (gridEvents.CurrentRow == null) return;

            var cell = gridEvents.CurrentRow.Cells["Id"]?.Value;
            if (cell == null) return;
            var id = cell.ToString();
            if (string.IsNullOrWhiteSpace(id)) return;

            var events = await _services.EventService.GetAllAsync();
            var selected = events.FirstOrDefault(ev => ev.Id == id);
            if (selected == null) return;

            using (var frm = new EventForm(selected))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await _services.EventService.UpdateAsync(frm.CurrentEvent);
                    await LoadEventsAsync(txtSearch.Text.Trim());
                    MessageBox.Show("Evento actualizado correctamente.", "Éxito");
                }
            }
        }

        // Eliminar evento
        private async void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (gridEvents.CurrentRow == null) return;

            var cell = gridEvents.CurrentRow.Cells["Id"]?.Value;
            if (cell == null) return;
            var id = cell.ToString();
            if (string.IsNullOrWhiteSpace(id)) return;

            var confirm = MessageBox.Show("¿Seguro que deseas eliminar este evento?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                await _services.EventService.DeleteAsync(id);
                await LoadEventsAsync(txtSearch.Text.Trim());
                MessageBox.Show("Evento eliminado correctamente.", "Éxito");
            }
        }

        private void btnMy_Click(object sender, EventArgs e)
        {
            using var frm = new PurchaseHistoryForm(_services, _currentUser);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
            using (var login = new LoginForm(_services))
            {
                login.ShowDialog();
            }
            Close();
        }
    }
}
