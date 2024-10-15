﻿using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formLogin : Form
    {
        public string _permision;
        public string _username;
        public string _password;
        public int UserIdLogin;
        public formLogin()
        {
            InitializeComponent();
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            CheckDefaultStaff();
            CheckDefaultUser();
            LoadSavedCredentials();
        }
        private void CheckDefaultStaff()
        {
            using (var context = new MyDbContext())
            {
                if (!context.myStaff.Any())
                {
                    var defaultStaff = new Staff
                    {
                        Id = 1,
                        NameStaff = "Admin",
                        Address = "None",
                        Phone = "None",
                        Position = "Manager"
                    };
                    context.myStaff.Add(defaultStaff);
                    context.SaveChanges();
                }
            }
        }
        private void CheckDefaultUser()
        {
            using (var context = new MyDbContext())
            {
                if (!context.myUser.Any())
                {
                    var defaultUser = new User
                    {
                        UserName = "admin",
                        Password = CryptoLib.Encryptor.MD5Hash("admin"),
                        Permision = "manager",
                        IdStaff = 1
                    };
                    context.myUser.Add(defaultUser);
                    context.SaveChanges();
                }
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            using (var context = new MyDbContext())
            {
                var userToLogin = context.myUser.SingleOrDefault(r => r.UserName == username && r.Password == password);
                if (userToLogin != null)
                {
                    UserIdLogin = int.Parse(userToLogin.ID.ToString());
                    _permision = userToLogin.Permision.ToLower();
                    return true;
                }
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _username = txbUserName.Text;
            _password = CryptoLib.Encryptor.MD5Hash(txbPassword.Text);

            if (IsValidInput())
            {
                if (AuthenticateUser(_username, _password))
                {
                    this.DialogResult = DialogResult.OK;
                    SaveCredentials();
                }
                else
                {
                    ShowLoginError();
                }
            }
            else
            {
                ShowLoginError();
            }
        }

        private bool IsValidInput()
        {
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(txbPassword.Text);
        }

        private void SaveCredentials()
        {
            if (ckbReAcc.Checked)
            {
                Properties.Settings.Default.UserName = _username;
                Properties.Settings.Default.Password = txbPassword.Text;
            }
            else
            {
                Properties.Settings.Default.UserName = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
            }
            Properties.Settings.Default.Save();
        }

        private void LoadSavedCredentials()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                txbUserName.Text = Properties.Settings.Default.UserName;
                txbPassword.Text = Properties.Settings.Default.Password;
            }
        }

        private void ShowLoginError()
        {
            if (MessageBox.Show("Thông tin tài khoản và mật khẩu không chính xác vui lòng đăng nhập lại!",
                                "Cảnh báo",
                                MessageBoxButtons.RetryCancel,
                                MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnShowp_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = !txbPassword.UseSystemPasswordChar;
        }
    }
}