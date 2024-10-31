using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Migrations;

namespace WindowsFormsApp.View
{
    public partial class formConnection : Form
    {
        public formConnection()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Tạo chuỗi kết nối từ các thông tin người dùng nhập
            string connectionString = $"Server={txtServerName.Text};Database={txtDatabaseName.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";

            // Mở App.config để cập nhật chuỗi kết nối
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["MyDbContext"].ConnectionString = connectionString;
            config.ConnectionStrings.ConnectionStrings["MyDbContext"].ProviderName = "System.Data.SqlClient";

            // Cập nhật trạng thái đã cấu hình
            config.AppSettings.Settings["IsConfigured"].Value = "true";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");

            // Xác nhận thông tin lưu thành công và đóng form
            MessageBox.Show("Đã lưu đường dẫn cơ sở dữ liệu.");
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
