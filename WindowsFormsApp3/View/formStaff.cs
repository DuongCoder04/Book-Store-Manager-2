using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formStaff : Form
    {
        private MyDbContext context;

        public formStaff()
        {
            InitializeComponent();
            context = new MyDbContext();
        }

        #region Method

        void Retrieve()
        {
            var result = context.myStaff
                                .Select(c => new { c.Id, c.NameStaff, c.Address, c.Phone, c.Position })
                                .ToList();
            dataGridViewStaff.DataSource = result;
        }

        private void SetPermission(string permission)
        {
            bool isManager = permission == "manager";
            btnDelete.Enabled = isManager;
            btnUpdate.Enabled = isManager;
            btnAdd.Enabled = isManager;
            btnReload.Enabled = true;
        }
        int Insert()
        {
            var staff = new Staff
            {
                NameStaff = txbNameStaff.Text,
                Address = txbAddress.Text,
                Phone = txbPhone.Text,
                Position = txbPosition.Text
            };
            context.myStaff.Add(staff);
            return context.SaveChanges();
        }

        new int Update()
        {
            if (int.TryParse(txbIdStaff.Text, out int id))
            {
                var staff = context.myStaff.SingleOrDefault(r => r.Id == id);
                if (staff != null)
                {
                    staff.NameStaff = txbNameStaff.Text;
                    staff.Address = txbAddress.Text;
                    staff.Phone = txbPhone.Text;
                    staff.Position = txbPosition.Text;
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        int Delete()
        {
            if (int.TryParse(txbIdStaff.Text, out int id))
            {
                var staff = context.myStaff.SingleOrDefault(r => r.Id == id);
                if (staff != null)
                {
                    context.myStaff.Remove(staff);
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        void Search(string key)
        {
            var result = context.myStaff
                                .Where(c => c.NameStaff.Contains(key) || c.Address.Contains(key) ||
                                             c.Phone.Contains(key) || c.Position.Contains(key))
                                .Select(c => new { c.Id, c.NameStaff, c.Address, c.Phone, c.Position })
                                .ToList();
            dataGridViewStaff.DataSource = result;
        }

        void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Event

        private void formStaff_Load(object sender, EventArgs e)
        {
            Retrieve();
            SetPermission(formMain.__Permision);
            SetGridHeaders();
        }

        private void SetGridHeaders()
        {
            dataGridViewStaff.Columns["Id"].HeaderText = "Mã nhân viên";
            dataGridViewStaff.Columns["NameStaff"].HeaderText = "Tên nhân viên";
            dataGridViewStaff.Columns["Address"].HeaderText = "Địa chỉ";
            dataGridViewStaff.Columns["Phone"].HeaderText = "Số điện thoại";
            dataGridViewStaff.Columns["Position"].HeaderText = "Chức vụ";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowMessage(Insert() > 0 ? "Thêm dữ liệu thành công" : "Thêm dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowMessage(Update() > 0 ? "Sửa dữ liệu thành công" : "Sửa dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ShowMessage(Delete() > 0 ? "Xóa dữ liệu thành công" : "Xóa dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (int.TryParse(dataGridViewStaff.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out int id))
                {
                    var staff = context.myStaff.SingleOrDefault(r => r.Id == id);
                    if (staff != null)
                    {
                        txbIdStaff.Text = staff.Id.ToString();
                        txbNameStaff.Text = staff.NameStaff;
                        txbAddress.Text = staff.Address;
                        txbPhone.Text = staff.Phone;
                        txbPosition.Text = staff.Position;
                    }
                }
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            Search(txbSearch.Text);
        }

        #endregion
    }
}
