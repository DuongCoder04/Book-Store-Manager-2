using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formBook : Form
    {
        public formBook()
        {
            InitializeComponent();
        }

        #region Method
        private void Retrieve()
        {
            using (var context = new MyDbContext())
            {
                cbAuthors.DataSource = context.myAuthors.ToList();
                cbAuthors.DisplayMember = "NameAuthors";
                cbAuthors.ValueMember = "ID";

                cbCategory.DataSource = context.myCategory.ToList();
                cbCategory.DisplayMember = "NameCategory";
                cbCategory.ValueMember = "ID";

                cbPublisher.DataSource = context.myPublisher.ToList();
                cbPublisher.DisplayMember = "NamePublisher";
                cbPublisher.ValueMember = "ID";

                dataGridViewBook.DataSource = context.myBooks.Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.IdAuthors,
                    c.IdPublisher,
                    c.Year,
                    c.Edition,
                    c.IdCategory,
                    c.Tags,
                    c.Rating,
                    c.Description,
                    c.FileDirection,
                    c.FileName,
                    c.Price
                }).ToList();
            }
        }

        private void SetPermissions(string permission)
        {
            bool isManager = permission == "manager";
            btnInsert.Enabled = isManager;
            btnUpdate.Enabled = isManager;
            btnDelete.Enabled = isManager;
            btnReLoad.Enabled = true;
        }

        private Book CreateBookFromInput()
        {
            return new Book
            {
                Title = txbTitle.Text,
                IdAuthors = int.Parse(cbAuthors.SelectedValue.ToString()),
                IdPublisher = int.Parse(cbPublisher.SelectedValue.ToString()),
                IdCategory = int.Parse(cbCategory.SelectedValue.ToString()),
                Rating = int.TryParse(txbRating.Text, out var rate) ? rate : 0,
                Description = txbDescription.Text,
                Year = int.TryParse(txbYear.Text, out var year) ? year : 0,
                Edition = int.TryParse(txbEdition.Text, out var edition) ? edition : 0,
                Tags = txbTags.Text,
                FileDirection = txbFileDirection.Text,
                FileName = txbFileName.Text,
                Price = int.Parse(txbPrice.Text)
            };
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
            return SaveChanges(context => context.myBooks.Add(CreateBookFromInput()));
        }

        private new int Update()
        {
            return SaveChanges(context =>
            {
                int IDB = int.Parse(txbIdBook.Text);
                var rowToUpdate = context.myBooks.SingleOrDefault(r => r.Id == IDB);
                if (rowToUpdate != null)
                {
                    var updatedBook = CreateBookFromInput();
                    rowToUpdate.Title = updatedBook.Title;
                    rowToUpdate.IdAuthors = updatedBook.IdAuthors;
                    rowToUpdate.IdCategory = updatedBook.IdCategory;
                    rowToUpdate.IdPublisher = updatedBook.IdPublisher;
                    rowToUpdate.Rating = updatedBook.Rating;
                    rowToUpdate.Description = updatedBook.Description;
                    rowToUpdate.Year = updatedBook.Year;
                    rowToUpdate.Edition = updatedBook.Edition;
                    rowToUpdate.Tags = updatedBook.Tags;
                    rowToUpdate.FileDirection = updatedBook.FileDirection;
                    rowToUpdate.FileName = updatedBook.FileName;
                    rowToUpdate.Price = updatedBook.Price;
                }
            });
        }

        private int Delete()
        {
            return SaveChanges(context =>
            {
                var rowToDelete = context.myBooks.SingleOrDefault(r => r.Id == int.Parse(txbIdBook.Text));
                if (rowToDelete != null) context.myBooks.Remove(rowToDelete);
            });
        }

        private void Search(string key)
        {
            using (var context = new MyDbContext())
            {
                var result = context.myBooks
                    .Where(c => c.Title.Contains(key) ||
                                c.Tags.Contains(key) ||
                                c.Description.Contains(key) ||
                                c.Authors.NameAuthors.Contains(key) ||
                                c.Publisher.NamePublisher.Contains(key) ||
                                c.Category.NameCategory.Contains(key) ||
                                c.Year.ToString().Contains(key) ||
                                c.Price.ToString().Contains(key))
                    .Select(c => new
                    {
                        c.Id,
                        c.Title,
                        c.IdAuthors,
                        c.IdPublisher,
                        c.Year,
                        c.Edition,
                        c.IdCategory,
                        c.Tags,
                        c.Rating,
                        c.Description,
                        c.FileDirection,
                        c.FileName,
                        c.Price
                    })
                    .ToList();
                dataGridViewBook.DataSource = result;
            }
        }

        private void UpdateDataGridHeaders()
        {
            dataGridViewBook.Columns["Id"].HeaderText = "Mã sách";
            dataGridViewBook.Columns["Title"].HeaderText = "Tên sách";
            dataGridViewBook.Columns["IdAuthors"].HeaderText = "Mã tác giả";
            dataGridViewBook.Columns["IdPublisher"].HeaderText = "Mã nhà xuất bản";
            dataGridViewBook.Columns["Year"].HeaderText = "Năm xuất bản";
            dataGridViewBook.Columns["Edition"].HeaderText = "Lần tái bản";
            dataGridViewBook.Columns["IdCategory"].HeaderText = "Mã loại sách";
            dataGridViewBook.Columns["Tags"].HeaderText = "Từ khóa";
            dataGridViewBook.Columns["Rating"].HeaderText = "Đánh giá";
            dataGridViewBook.Columns["Description"].HeaderText = "Mô tả";
            dataGridViewBook.Columns["FileDirection"].HeaderText = "Địa chỉ lưu trữ";
            dataGridViewBook.Columns["FileName"].HeaderText = "Tên file";
            dataGridViewBook.Columns["Price"].HeaderText = "Giá thành";
            dataGridViewBook.Columns["Price"].DefaultCellStyle.Format = "C0";
            dataGridViewBook.Columns["Price"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
        }
        #endregion

        #region Events
        private void formBook_Load(object sender, EventArgs e)
        {
            Retrieve();
            SetPermissions(formMain.__Permision);
            UpdateDataGridHeaders();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Insert() > 0 ? "Thêm dữ liệu thành công" : "Thêm dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog { Filter = "Chọn file (*.pdf)|*.pdf|All files (*.*)|*.*" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txbFileDirection.Text = dlg.FileName;
                    txbFileName.Text = dlg.SafeFileName;
                }
            }
        }

        private void dataGridViewBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int IDB = int.Parse(dataGridViewBook.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                using (var context = new MyDbContext())
                {
                    var rowToSelect = context.myBooks.SingleOrDefault(r => r.Id == IDB);
                    if (rowToSelect != null)
                    {
                        txbIdBook.Text = rowToSelect.Id.ToString();
                        txbTitle.Text = rowToSelect.Title;
                        cbAuthors.SelectedValue = rowToSelect.IdAuthors;
                        cbPublisher.SelectedValue = rowToSelect.IdPublisher;
                        cbCategory.SelectedValue = rowToSelect.IdCategory;
                        txbRating.Text = rowToSelect.Rating.ToString();
                        txbDescription.Text = rowToSelect.Description;
                        txbYear.Text = rowToSelect.Year.ToString();
                        txbEdition.Text = rowToSelect.Edition.ToString();
                        txbTags.Text = rowToSelect.Tags;
                        txbFileDirection.Text = rowToSelect.FileDirection;
                        txbFileName.Text = rowToSelect.FileName;
                        txbPrice.Text = rowToSelect.Price.ToString();
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Update() > 0 ? "Sửa dữ liệu thành công" : "Sửa dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Delete() > 0 ? "Xóa dữ liệu thành công" : "Xóa dữ liệu không thành công", "Thông báo");
            Retrieve();
        }

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSearch.Text))
            {
                Retrieve();
            }
            else
            {
                Search(txbSearch.Text);
            }
        }
        #endregion
    }
}
