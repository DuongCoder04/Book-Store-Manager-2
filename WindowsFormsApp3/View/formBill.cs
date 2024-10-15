using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.View
{
    public partial class formBill : Form
    {
        public formBill()
        {
            InitializeComponent();
        }

        #region Methods

        ///// <summary>
        ///// Lấy dữ liệu danh mục và hóa đơn từ cơ sở dữ liệu và đổ vào các điều khiển giao diện người dùng tương ứng.
        ///// </summary>
        //void Retrieve()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            // Lấy danh sách danh mục từ cơ sở dữ liệu
        //            dtDate.Value =  DateTime.Now.Date;
        //            var categories = context.myCategory
        //                                    .Select(c => new { c.Id, c.NameCategory })
        //                                    .ToList();
        //            cbCategory.DataSource = categories;
        //            cbCategory.DisplayMember = "NameCategory";
        //            cbCategory.ValueMember = "Id";
        //            cbCategory.SelectedIndex = -1;

        //            // Lấy danh sách hóa đơn từ cơ sở dữ liệu
        //            var bills = context.myBill
        //                               .Select(c => new { c.Id, c.CheckOut })
        //                               .ToList();
        //            dataGridViewBill.DataSource = bills;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void UpdateTotalLabel()
        //{
        //    decimal total = 0;

        //    foreach (DataGridViewRow row in dataGridViewBillDetail.Rows)
        //    {
        //        if (row.Cells["Total"].Value != null)
        //        {
        //            total += Convert.ToDecimal(row.Cells["Total"].Value);
        //        }
        //    }

        //    labelTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        //}

        ///// <summary>
        ///// Tải sách cho một danh mục nhất định và đổ vào combobox sách.
        ///// </summary>
        ///// <param name="categoryId">ID của danh mục.</param>
        //void LoadBooksForCategory(int categoryId)
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            var books = context.myBooks
        //                               .Where(b => b.IdCategory == categoryId)
        //                               .ToList();
        //            cbBookName.DataSource = books;
        //            cbBookName.DisplayMember = "Title";
        //            cbBookName.ValueMember = "Id";
        //            //cbBookName.SelectedIndex = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        ///// <summary>
        ///// Thiết lập quyền dựa trên vai trò người dùng.
        ///// </summary>
        ///// <param name="permission">Vai trò quyền hạn.</param>
        //void SetPermission(string permission)
        //{
        //    switch (permission)
        //    {
        //        case "manager":
        //            btnAddBill.Enabled = true;
        //            btnAddBook.Enabled = true;
        //            btnDelete.Enabled = true;
        //            btnDeleteBook.Enabled = true;
        //            btnUpdate.Enabled = true;
        //            btnUpdateBI.Enabled = true;
        //            break;
        //        case "staff":
        //            btnAddBill.Enabled = true;
        //            btnAddBook.Enabled = true;
        //            btnDelete.Enabled = false;
        //            btnDeleteBook.Enabled = true;
        //            btnUpdate.Enabled = true;
        //            btnUpdateBI.Enabled = true;
        //            break;
        //    }
        //}

        ///// <summary>
        ///// Thêm hóa đơn mới vào cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int InsertBill()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            DateTime date = dtDate.Value;
        //            var bill = new Bill
        //            {
        //                CheckOut = date,
        //            };
        //            context.myBill.Add(bill);
        //            return context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Thêm thông tin hóa đơn mới vào cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int InsertBillDetail()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            var BillDetail = new BillDetail
        //            {
        //                IdBill = int.Parse(txbIdBill.Text),
        //                IdBook = int.Parse(cbBookName.SelectedValue.ToString()),
        //                Count = (int)nmCount.Value
        //            };
        //            context.myBillDetail.Add(BillDetail);
        //            return context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Cập nhật hóa đơn hiện tại trong cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int UpdateBill()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            int billId = int.Parse(txbIdBill.Text);
        //            var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
        //            if (bill != null)
        //            {
        //                bill.CheckOut = dtDate.Value;
        //                return context.SaveChanges();
        //            }
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Cập nhật thông tin hóa đơn hiện tại trong cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int UpdateBillDetail()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            int billId = int.Parse(txbIdBill.Text);
        //            int BillDetailId = int.Parse(txbIdBillDetail.Text);
        //            var BillDetail = context.myBillDetail.SingleOrDefault(bi => bi.Id == BillDetailId && bi.IdBill == billId);
        //            if (BillDetail != null)
        //            {
        //                BillDetail.IdBook = int.Parse(cbBookName.SelectedValue.ToString());
        //                BillDetail.Count = (int)nmCount.Value;
        //                return context.SaveChanges();
        //            }
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Xóa hóa đơn hiện tại khỏi cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int DeleteBill()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            int billId = int.Parse(txbIdBill.Text);
        //            var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
        //            if (bill != null)
        //            {
        //                context.myBill.Remove(bill);
        //                return context.SaveChanges();
        //            }
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Xóa thông tin hóa đơn hiện tại khỏi cơ sở dữ liệu.
        ///// </summary>
        ///// <returns>Số lượng mục trạng thái được ghi vào cơ sở dữ liệu.</returns>
        //int DeleteBillDetail()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            int billId = int.Parse(txbIdBill.Text);
        //            int BillDetailId = int.Parse(txbIdBillDetail.Text);
        //            var BillDetail = context.myBillDetail.SingleOrDefault(bi => bi.Id == BillDetailId && bi.IdBill == billId);
        //            if (BillDetail != null)
        //            {
        //                context.myBillDetail.Remove(BillDetail);
        //                return context.SaveChanges();
        //            }
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// Tìm kiếm hóa đơn dựa trên ngày được chọn.
        ///// </summary>
        //void Search()
        //{
        //    try
        //    {
        //        using (var context = new MyDbContext())
        //        {
        //            var results = context.myBill
        //                                 .Where(c => c.CheckOut <= dtDate.Value)
        //                                 .Select(c => new { c.Id, c.CheckOut})
        //                                 .ToList();
        //            dataGridViewBill.DataSource = results;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion

        #region Events

        //private void formBill_Load(object sender, EventArgs e)
        //{
        //    Retrieve();
        //    SetPermission(formMain.__Permision);

        //    // Đổi tên các cột trong dataGridViewBill sang tiếng Việt
        //    dataGridViewBill.Columns["Id"].HeaderText = "Mã hóa đơn";
        //    dataGridViewBill.Columns["CheckOut"].HeaderText = "Ngày thanh toán";
        //    dataGridViewBill.Columns["Status"].HeaderText = "Trạng thái";

        //    cbCategory.SelectedIndex = -1;
        //}

        //private void btnInsert_Click(object sender, EventArgs e)
        //{
        //    if (InsertBill() > 0)
        //    {
        //        MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Thêm dữ liệu không thành công.", "Thông báo");
        //    }
        //    formBill_Load(sender, e);
        //}

        //private void btnExit_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void dataGridViewBill_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        try
        //        {
        //            using (var context = new MyDbContext())
        //            {
        //                int billId = int.Parse(dataGridViewBill.Rows[e.RowIndex].Cells["Id"].Value.ToString());
        //                var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
        //                if (bill != null)
        //                {
        //                    txbIdBill.Text = bill.Id.ToString();
        //                    dtDate.Value = bill.CheckOut;

        //                    var BillDetails = context.myBillDetail
        //                                           .Where(bi => bi.IdBill == billId)
        //                                           .Select(bi => new
        //                                           {
        //                                               bi.Id,
        //                                               bi.Book.Title,
        //                                               bi.Count,
        //                                               Total = bi.Book != null ? bi.Book.Price * bi.Count : 0
        //                                           })
        //                                           .ToList();
        //                    dataGridViewBillDetail.DataSource = BillDetails;

        //                    // Đổi tên các cột sang tiếng Việt
        //                    dataGridViewBillDetail.Columns["Id"].HeaderText = "STT";
        //                    dataGridViewBillDetail.Columns["Title"].HeaderText = "Tên sách";
        //                    dataGridViewBillDetail.Columns["Count"].HeaderText = "Số lượng";
        //                    dataGridViewBillDetail.Columns["Total"].HeaderText = "Tổng tiền (VND)";

        //                    // Định dạng cột Total thành tiền VND
        //                    dataGridViewBillDetail.Columns["Total"].DefaultCellStyle.Format = "C0";
        //                    dataGridViewBillDetail.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");

        //                    UpdateTotalLabel();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void dataGridViewBillDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        try
        //        {
        //            using (var context = new MyDbContext())
        //            {
        //                int BillDetailId = int.Parse(dataGridViewBillDetail.Rows[e.RowIndex].Cells["Id"].Value.ToString());
        //                var BillDetail = context.myBillDetail.SingleOrDefault(bi => bi.Id == BillDetailId);
        //                if (BillDetail != null)
        //                {
        //                    txbIdBillDetail.Text = BillDetail.Id.ToString();
        //                    txbIdBill.Text = BillDetail.IdBill.ToString();
        //                    cbBookName.SelectedValue = BillDetail.IdBook;
        //                    cbCategory.SelectedValue = BillDetail.Book.IdCategory;
        //                    nmCount.Value = BillDetail.Count;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    if (UpdateBill() > 0)
        //    {
        //        MessageBox.Show("Sửa dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Sửa dữ liệu không thành công.", "Thông báo");
        //    }
        //    formBill_Load(sender, e);
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (DeleteBill() > 0)
        //    {
        //        MessageBox.Show("Xóa dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Xóa dữ liệu không thành công.", "Thông báo");
        //    }
        //    formBill_Load(sender, e);
        //}

        //private void btnReLoad_Click(object sender, EventArgs e)
        //{
        //    formBill_Load(sender, e);
        //}

        //private void btnFind_Click(object sender, EventArgs e)
        //{
        //    Search();
        //}

        //private void btnAddBook_Click(object sender, EventArgs e)
        //{
        //    if (InsertBillDetail() > 0)
        //    {
        //        MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Thêm dữ liệu không thành công.", "Thông báo");
        //    }
        //    UpdateTotalLabel();
        //}

        //private void btnDeleteBook_Click(object sender, EventArgs e)
        //{
        //    if (DeleteBillDetail() > 0)
        //    {
        //        MessageBox.Show("Xóa dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Xóa dữ liệu không thành công.", "Thông báo");
        //    }
        //    UpdateTotalLabel();
        //}

        //private void btnUpdateBI_Click(object sender, EventArgs e)
        //{
        //    if (UpdateBillDetail() > 0)
        //    {
        //        MessageBox.Show("Sửa dữ liệu thành công.", "Thông báo");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Sửa dữ liệu không thành công.", "Thông báo");
        //    }
        //    UpdateTotalLabel();
        //}

        //private void cbCategory_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cbCategory.SelectedValue != null && cbCategory.SelectedValue is int)
        //        {
        //            var idCategory = (int)cbCategory.SelectedValue;
        //            LoadBooksForCategory(idCategory);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion
    }
}

