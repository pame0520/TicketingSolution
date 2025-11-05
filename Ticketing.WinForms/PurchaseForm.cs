using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticketing.Domain.Models;

namespace Ticketing.WinForms
{
    public partial class PurchaseForm : Form
    {
        private readonly AppServices _services;
        private readonly User _user;
        private readonly Event _event;

        public PurchaseForm(AppServices services, User user, Event ev)
        {
            InitializeComponent();
            _services = services;
            _user = user;
            _event = ev;
        }

        private void PurchaseForm_Load(object sender, EventArgs e)
        {
            Text = "Compra de tiques";
            lblTitle.Text = _event.Name;
            lblStock.Text = _event.TicketsAvailable.ToString();
            lblPrice.Text = _event.Price.ToString("₡#,0.##");

            numQty.Minimum = 1;
            numQty.Maximum = Math.Max(1, Math.Min(10, _event.TicketsAvailable));
            numQty.Value = 1;

            txtCard.PlaceholderText = "Tarjeta de prueba (mín. 8 dígitos)";
        }

        private void txtCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir sólo dígitos y teclas de control (backspace, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private async void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                int qty = (int)numQty.Value;
                string card = txtCard.Text.Trim();

                var purchase = await _services.PurchaseService.BuyAsync(_user.Id!, _event, qty, card);

                // Observer: avisa a quien esté suscrito (lista de eventos, etc.)
                _services.Notifier.Notify($"Se actualizó la disponibilidad de '{_event.Name}'.");

                MessageBox.Show(
                    $"Compra realizada!\nEvento: {_event.Name}\nCantidad: {qty}\nTotal: ₡{purchase.Total:#,0}",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lo sentimos hay un error en la compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
