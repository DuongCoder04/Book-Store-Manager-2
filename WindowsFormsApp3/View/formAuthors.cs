using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formAuthors : Form
    {
        public formAuthors()
        {
            InitializeComponent();
            // Ngăn người dùng thay đổi kích cỡ form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Hoặc FixedDialog
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to

        }

        #region Method
        private void Retrieve()
        {
            using (var context = new MyDbContext())
            {
                dataGridViewAuthors.DataSource = context.myAuthors
                    .Select(c => new { c.Id, c.NameAuthors })
                    .ToList();
            }
        }

        private void SetPermissions(string permission)
        {
            bool isManager = permission == "manager";
            btnDelete.Enabled = isManager;
            btnUpdate.Enabled = isManager;
            btnAdd.Enabled = isManager;
            btnReload.Enabled = true;
        }

        private int SaveChanges(Action<MyDbContext> action)
        {
            using (var context = new MyDbContext())
            {
                action(context);
                return context.SaveChanges();
            }
        }

        private int Insert()
        {
            return SaveChanges(context =>
            {
                context.myAuthors.Add(new Authors { NameAuthors = txbNameAuthors.Text });
            });
        }

        private new int Update()
        {
            return SaveChanges(context =>
            {
                int authorId = int.Parse(txbIdAuthors.Text);
                var authorToUpdate = context.myAuthors.SingleOrDefault(r => r.Id == authorId);
                if (authorToUpdate != null)
                {
                    authorToUpdate.NameAuthors = txbNameAuthors.Text;
                }
                else
                {
                    throw new InvalidOperationException("Author not found");
                }
            });
        }

        private int Delete()
        {
            return SaveChanges(context =>
            {
                int authorId = int.Parse(txbIdAuthors.Text);
                var authorToDelete = context.myAuthors.SingleOrDefault(r => r.Id == authorId);
                if (authorToDelete != null)
                {
                    context.myAuthors.Remove(authorToDelete);
                }
                else
                {
                    throw new InvalidOperationException("Author not found");
                }
            });
        }

        private void Search(string key)
        {
            using (var context = new MyDbContext())
            {
                dataGridViewAuthors.DataSource = context.myAuthors
                    .Where(c => c.NameAuthors.Contains(key))
                    .Select(c => new { c.Id, c.NameAuthors })
                    .ToList();
            }
        }
        #endregion

        #region Events
        private void formAuthors_Load(object sender, EventArgs e)
        {
            Retrieve();
            SetPermissions(formMain.__Permision);
            UpdateGridHeaders();
        }

        private void UpdateGridHeaders()
        {
            dataGridViewAuthors.Columns["Id"].HeaderText = "Mã tác giả";
            dataGridViewAuthors.Columns["NameAuthors"].HeaderText = "Tên tác giả";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowMessage(Insert(), "Thêm dữ liệu");
            Retrieve();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search(txbSearch.Text);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowMessage(Update(), "Sửa dữ liệu");
            Retrieve();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ShowMessage(Delete(), "Xóa dữ liệu");
            Retrieve();
        }

        private void ShowMessage(int result, string action)
        {
            MessageBox.Show(result > 0 ? $"{action} thành công" : $"{action} không thành công", "Thông báo");
        }

        private void dataGridViewAuthors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int authorId = int.Parse(dataGridViewAuthors.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                using (var context = new MyDbContext())
                {
                    var selectedAuthor = context.myAuthors.SingleOrDefault(r => r.Id == authorId);
                    if (selectedAuthor != null)
                    {
                        txbIdAuthors.Text = selectedAuthor.Id.ToString();
                        txbNameAuthors.Text = selectedAuthor.NameAuthors;
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
