using WindowsFormsApp.View;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class formMain : DevComponents.DotNetBar.Office2007RibbonForm
    {
        public static string __UserName;
        public static string __Password;
        public static string __Permision;
        public static int __IdUser;

        public formMain()
        {
            InitializeComponent();
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            if (ShowLoginForm() == 1)
            {
                // Form Login đã thành công, formMain hiển thị
                this.Show();
            }
        }
        private void btnBook_Click(object sender, EventArgs e)
        {
            OpenForm<formBook>();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenForm<formCategory>();
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            OpenForm<formAuthors>();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            OpenForm<formStaff>();
        }

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            OpenForm<formPublisher>();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            OpenForm<formUser>();
        }

        private void btnChangePW_Click(object sender, EventArgs e)
        {
            OpenForm<formChangePassword>();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            OpenForm<formNewBill>();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ShowLoginForm();
        }

        public int ShowLoginForm()
        {
            using (var loginForm = new formLogin())
            {
                DialogResult result = loginForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    __UserName = loginForm._username;
                    __Password = loginForm._password;
                    __Permision = loginForm._permision;
                    __IdUser = loginForm.UserIdLogin;
                    SetPermissions(loginForm._permision);
                    return 1;
                }
                return 0;
            }
        }

        private void SetPermissions(string permision)
        {
            rbUser.Enabled = permision == "manager";
        }

        private void OpenForm<T>() where T : Form, new()
        {
            using (var form = new T())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenForm<formCustomer>();
        }
    }
}

