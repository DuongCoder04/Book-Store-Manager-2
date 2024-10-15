using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formCustomer : Form
    {
        public formCustomer()
        {
            InitializeComponent();
        }

        private void formCustomer_Load(object sender, EventArgs e)
        {
            Retrieve();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewCustomer.Columns["Id"].HeaderText = "Mã khách hàng";
            dataGridViewCustomer.Columns["NameCustomer"].HeaderText = "Tên khách hàng";
            dataGridViewCustomer.Columns["Address"].HeaderText = "Địa chỉ";
            dataGridViewCustomer.Columns["Phone"].HeaderText = "Số điện thoại";
        }

        private void Retrieve()
        {
            using (var context = new MyDbContext())
            {
                dataGridViewCustomer.DataSource = context.myCustomer
                    .Select(c => new { c.Id, c.NameCustomer, c.Address, c.Phone })
                    .ToList();
            }
        }

        private void SetPermission(string permission)
        {
            bool isManager = permission == "manager";
            btnDelete.Enabled = isManager;
            btnUpdate.Enabled = isManager;
            btnAdd.Enabled = isManager;
            btnReload.Enabled = true;
        }

        private int InsertCustomer()
        {
            using (var context = new MyDbContext())
            {
                var customer = new Customer
                {
                    NameCustomer = txbNameCustomer.Text,
                    Address = txbAddress.Text,
                    Phone = txbPhone.Text,
                };
                context.myCustomer.Add(customer);
                return context.SaveChanges();
            }
        }

        private int UpdateCustomer()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    int id = int.Parse(txbIdCustomer.Text);
                    var customer = context.myCustomer.SingleOrDefault(r => r.Id == id);
                    if (customer != null)
                    {
                        customer.NameCustomer = txbNameCustomer.Text;
                        customer.Address = txbAddress.Text;
                        customer.Phone = txbPhone.Text;
                        return context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
                return 0;
            }
        }

        private int DeleteCustomer()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    int id = int.Parse(txbIdCustomer.Text);
                    var customer = context.myCustomer.SingleOrDefault(r => r.Id == id);
                    if (customer != null)
                    {
                        context.myCustomer.Remove(customer);
                        return context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
                return 0;
            }
        }

        private void SearchCustomers(string key)
        {
            using (var context = new MyDbContext())
            {
                var result = context.myCustomer
                    .Where(c => c.NameCustomer.Contains(key) || c.Address.Contains(key) || c.Phone.Contains(key))
                    .Select(c => new { c.Id, c.NameCustomer, c.Address, c.Phone })
                    .ToList();
                dataGridViewCustomer.DataSource = result;
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowMessage(InsertCustomer() > 0, "Thêm dữ liệu");
            ReloadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowMessage(UpdateCustomer() > 0, "Sửa dữ liệu");
            ReloadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ShowMessage(DeleteCustomer() > 0, "Xóa dữ liệu");
            ReloadData();
        }

        private void ReloadData()
        {
            Retrieve();
        }

        private void ShowMessage(bool success, string action)
        {
            string message = success ? $"{action} thành công" : $"{action} không thành công";
            MessageBox.Show(message, "Thông báo");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridViewCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dataGridViewCustomer.Rows[e.RowIndex].Cells["Id"].Value;
                using (var context = new MyDbContext())
                {
                    var customer = context.myCustomer.SingleOrDefault(r => r.Id == id);
                    if (customer != null)
                    {
                        txbIdCustomer.Text = customer.Id.ToString();
                        txbNameCustomer.Text = customer.NameCustomer;
                        txbAddress.Text = customer.Address;
                        txbPhone.Text = customer.Phone;
                    }
                }
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txbSearch.Text;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                SearchCustomers(searchText);
            }
            else
            {
                Retrieve();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }
    }
}
