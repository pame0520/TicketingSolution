using System;
using System.Windows.Forms;
using Ticketing.Domain.Models;

namespace Ticketing.WinForms.Data
{
    public partial class EventForm : Form
    {
        public Event CurrentEvent { get; private set; }

        public EventForm(Event? e = null)
        {
            InitializeComponent();

            // Asegurarse de tener valores por defecto seguros en los controles
            numPrice.DecimalPlaces = 2;
            numPrice.Minimum = 0;
            numPrice.Maximum = 1_000_000;
            numTickets.Minimum = 0;
            numTickets.Maximum = 1000000;

            if (e != null)
            {
                CurrentEvent = e;
                txtName.Text = e.Name;

                // Evitar ArgumentOutOfRangeException al asignar DateTimePicker.Value
                var dateToSet = e.Date;
                if (dateToSet < dtpDate.MinDate) dateToSet = DateTime.Now;
                if (dateToSet > dtpDate.MaxDate) dateToSet = dtpDate.MaxDate;
                dtpDate.Value = dateToSet;

                txtVenue.Text = e.Venue;
                txtType.Text = e.Type;
                numTickets.Value = Math.Max(numTickets.Minimum, Math.Min(numTickets.Maximum, e.TicketsAvailable));
                // Precio es decimal en el modelo: clamar entre min/max de numPrice
                var priceDecimal = e.Price;
                if (priceDecimal < numPrice.Minimum) priceDecimal = numPrice.Minimum;
                if (priceDecimal > numPrice.Maximum) priceDecimal = numPrice.Maximum;
                numPrice.Value = priceDecimal;
                txtDescription.Text = e.Description;
            }
            else
            {
                CurrentEvent = new Event();
                // inicializar controles a valores sensatos
                dtpDate.Value = DateTime.Now;
                numPrice.Value = 0m;
                numTickets.Value = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CurrentEvent.Name = txtName.Text.Trim();
            CurrentEvent.Date = dtpDate.Value;
            CurrentEvent.Venue = txtVenue.Text.Trim();
            CurrentEvent.Type = txtType.Text.Trim();
            CurrentEvent.TicketsAvailable = (int)numTickets.Value;
            // Guardar decimal correctamente
            CurrentEvent.Price = numPrice.Value;
            CurrentEvent.Description = txtDescription.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

