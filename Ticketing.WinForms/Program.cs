using System;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Ticketing.Domain.Data;
using System.Threading.Tasks;

namespace Ticketing.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //  Ruta donde se guardan los archivos JSON (seguimos manteniendo la carpeta)
            var dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataPath);

            // Construir cadena de conexión MySQL (credenciales facilitadas)
            //Valores Quemados (Hardcode)
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                UserID = "root",
                Password = "pamela",
                Database = "ticketing",
                SslMode = MySqlSslMode.Disabled
            };
            var connStr = builder.ToString();

            // Ejecutar migración automática (si hay archivos JSON)
            try
            {
                // Ejecuta el task de forma sincrónica dentro de Main
                Task.Run(() => JsonToMySqlMigrator.MigrateIfNeededAsync(dataPath, connStr)).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // No detener la app; informar y continuar. Revisar logs/MessageBox si se quiere mayor visibilidad.
                MessageBox.Show($"Advertencia al migrar JSON a MySQL: {ex.Message}", "Migración", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Pasamos la opción de usar MySQL y la cadena de conexión
            var services = new AppServices(dataPath, connStr);


            // Iniciamos con el login
            Application.Run(new LoginForm(services));
        }
    }
}