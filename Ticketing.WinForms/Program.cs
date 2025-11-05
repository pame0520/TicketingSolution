using System;
using System.IO;
using System.Windows.Forms;

namespace Ticketing.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //  Ruta donde se guardan los archivos JSON
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataPath);

            //  Pasamos esa ruta al constructor
            var services = new AppServices(dataPath);

            // Iniciamos con el login
            Application.Run(new LoginForm(services));
        }
    }
}