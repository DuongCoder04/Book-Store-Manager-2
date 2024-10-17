using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formCategory : Form
    {
        public formCategory()
        {
            InitializeComponent();
            // Ngăn người dùng thay đổi kích cỡ form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Hoặc FixedDialog
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to
        }

        private void formCategory_Load(object sender, EventArgs e)
        {
            RetrieveCategories();
            SetPermissions(formMain.__Permision);
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewCategory.Columns["Id"].HeaderText = "Mã loại sách";
            dataGridViewCategory.Columns["NameCategory"].HeaderText = "Tên loại sách";
        }

        private void RetrieveCategories()
        {
            using (var context = new MyDbContext())
            {
                dataGridViewCategory.DataSource = context.myCategory
                    .Select(c => new { c.Id, c.NameCategory })
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

        private int InsertCategory()
        {
            using (var context = new MyDbContext())
            {
                var category = new Category
                {
                    NameCategory = txbNameCategory.Text
                };
                context.myCategory.Add(category);
                return context.SaveChanges();
            }
        }

        private int UpdateCategory()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    int id = int.Parse(txbIdCategory.Text);
                    var categoryToUpdate = context.myCategory.SingleOrDefault(c => c.Id == id);
                    if (categoryToUpdate != null)
                    {
                        categoryToUpdate.NameCategory = txbNameCategory.Text;
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

        private int DeleteCategory()
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    int id = int.Parse(txbIdCategory.Text);
                    var categoryToDelete = context.myCategory.SingleOrDefault(c => c.Id == id);
                    if (categoryToDelete != null)
                    {
                        context.myCategory.Remove(categoryToDelete);
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

        private void SearchCategories(string key)
        {
            using (var context = new MyDbContext())
            {
                var result = context.myCategory
                    .Where(c => c.NameCategory.Contains(key))
                    .Select(c => new { c.Id, c.NameCategory })
                    .ToList();
                dataGridViewCategory.DataSource = result;
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowMessage(InsertCategory() > 0, "Thêm dữ liệu");
            ReloadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShowMessage(UpdateCategory() > 0, "Sửa dữ liệu");
            ReloadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ShowMessage(DeleteCategory() > 0, "Xóa dữ liệu");
            ReloadData();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            RetrieveCategories();
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

        private void dataGridViewCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = (int)dataGridViewCategory.Rows[e.RowIndex].Cells["Id"].Value;
                using (var context = new MyDbContext())
                {
                    var category = context.myCategory.SingleOrDefault(c => c.Id == id);
                    if (category != null)
                    {
                        txbIdCategory.Text = category.Id.ToString();
                        txbNameCategory.Text = category.NameCategory;
                    }
                }
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txbSearch.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                SearchCategories(searchText);
            }
            else
            {
                RetrieveCategories();
            }
        }
    }
}
