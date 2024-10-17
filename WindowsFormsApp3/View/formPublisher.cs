using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formPublisher : Form
    {
        private MyDbContext context;

        public formPublisher()
        {
            InitializeComponent();
            context = new MyDbContext();
            // Ngăn người dùng thay đổi kích cỡ form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Hoặc FixedDialog
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to
        }

        #region Method

        void Retrieve()
        {
            var result = context.myPublisher
                                .Select(c => new { c.Id, c.NamePublisher })
                                .ToList();
            dataGridViewPublisher.DataSource = result;
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
            var publisher = new Publisher { NamePublisher = txbNamePublisher.Text };
            context.myPublisher.Add(publisher);
            return context.SaveChanges();
        }

        new int Update()
        {
            if (int.TryParse(txbIdPublisher.Text, out int id))
            {
                var publisher = context.myPublisher.SingleOrDefault(r => r.Id == id);
                if (publisher != null)
                {
                    publisher.NamePublisher = txbNamePublisher.Text;
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        int Delete()
        {
            if (int.TryParse(txbIdPublisher.Text, out int id))
            {
                var publisher = context.myPublisher.SingleOrDefault(r => r.Id == id);
                if (publisher != null)
                {
                    context.myPublisher.Remove(publisher);
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        void Search(string key)
        {
            var result = context.myPublisher
                                .Where(c => c.NamePublisher.Contains(key))
                                .Select(c => new { c.Id, c.NamePublisher })
                                .ToList();
            dataGridViewPublisher.DataSource = result;
        }

        void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Event

        private void formPublisher_Load(object sender, EventArgs e)
        {
            Retrieve();
            SetPermission(formMain.__Permision);
            dataGridViewPublisher.Columns["Id"].HeaderText = "Mã nhà xuất bản";
            dataGridViewPublisher.Columns["NamePublisher"].HeaderText = "Tên nhà xuất bản";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ShowMessage(Delete() > 0 ? "Xóa dữ liệu thành công" : "Xóa dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void dataGridViewPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (int.TryParse(dataGridViewPublisher.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out int id))
                {
                    var publisher = context.myPublisher.SingleOrDefault(r => r.Id == id);
                    if (publisher != null)
                    {
                        txbIdPublisher.Text = publisher.Id.ToString();
                        txbNamePublisher.Text = publisher.NamePublisher;
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
