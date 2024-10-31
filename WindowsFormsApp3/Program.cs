using System;
using System.Configuration;
using System.Windows.Forms;
using WindowsFormsApp.View;

namespace WindowsFormsApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Kiểm tra cấu hình
            string isConfigured = ConfigurationManager.AppSettings["IsConfigured"];
            if (isConfigured == "false")
            {
                // Hiển thị FormDatabaseSetup
                Application.Run(new formConnection());
            }
            else
            {
                // Tiếp tục vào ứng dụng chính
                Application.Run(new formMain());
            }
        }
    }
}
