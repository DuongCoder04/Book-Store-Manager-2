using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.DataSet;
using WindowsFormsApp.Models;
using WindowsFormsApp.Report;

namespace WindowsFormsApp.View
{
    public partial class formNewBill : Form
    {
        int idBook;
        string title;
        int price;
        int quantity = 1;
        DataTable billDetailsTable;
        private dsBillDetails billDataSet;
        public formNewBill()
        {
            // Ngăn người dùng thay đổi kích cỡ form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Hoặc FixedDialog
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to
            InitializeComponent();
            CreateDataTable();
        }
        #region DataSet
        private void InitializeBillDetailsTable()
        {
            // Initialize dataset and datatable
            if (billDetailsTable == null)
            {
                billDetailsTable = new DataTable("BillDetails");

                // Add necessary columns to the BillDetails table
                billDetailsTable.Columns.Add("Id", typeof(int));
                billDetailsTable.Columns.Add("Title", typeof(string));
                billDetailsTable.Columns.Add("Price", typeof(int));
                billDetailsTable.Columns.Add("Quantity", typeof(int));
                billDetailsTable.Columns.Add("Total", typeof(int), "Price * Quantity");
            }
            // Set data source for the Bill Details DataGridView
            dataGridViewBillDetails.DataSource = billDetailsTable;
        }
        private void CreateDataTable()
        {
            billDataSet = new dsBillDetails();

            // Đảm bảo bảng "BillTable" đã tồn tại trong DataSet
            if (!billDataSet.Tables.Contains("BillTable"))
            {
                // Tạo DataTable "BillTable" nếu chưa có
                DataTable billTable = new DataTable("BillTable");
                billTable.Columns.Add("IdBill", typeof(string)); // Chuyển thành string
                billTable.Columns.Add("IdBook", typeof(int));
                billTable.Columns.Add("IdCustomer", typeof(int));
                billTable.Columns.Add("IdUser", typeof(int));
                billTable.Columns.Add("IdStaff", typeof(int));
                billTable.Columns.Add("NameCustomer", typeof(string));
                billTable.Columns.Add("NameStaff", typeof(string));
                billTable.Columns.Add("Title", typeof(string));
                billTable.Columns.Add("CheckIn", typeof(DateTime));
                billTable.Columns.Add("CheckOut", typeof(DateTime));
                billTable.Columns.Add("Quantity", typeof(int));
                billTable.Columns.Add("Price", typeof(int));
                // Thêm DataTable vào DataSet
                billDataSet.Tables.Add(billTable);
            }
        }
        
        // Phương thức lấy dữ liệu hóa đơn theo IdBill và đổ vào DataSet đã tạo
        private void GetBillData(string billId)
        {
            using (var context = new MyDbContext())
            {
                var billTable = billDataSet.Tables["BillTable"];

                // Xóa dữ liệu cũ trong DataTable nếu có
                billTable.Clear();

                // Truy vấn cơ sở dữ liệu để lấy hóa đơn theo IdBill (string)
                var billDetails = from billdetail in context.myBillDetail
                                  join customer in context.myCustomer on billdetail.Bill.IdCustomer equals customer.Id
                                  join user in context.myUser on billdetail.Bill.IdUser equals user.ID
                                  join book in context.myBooks on billdetail.IdBook equals book.Id
                                  where billdetail.IdBill == billId
                                  select new
                                  {
                                      billdetail.IdBill,
                                      billdetail.Bill.IdCustomer,
                                      billdetail.Bill.IdUser,
                                      billdetail.Bill.User.IdStaff,
                                      billdetail.Bill.CheckIn,
                                      billdetail.Bill.CheckOut,
                                      billdetail.IdBook,
                                      book.Title,
                                      billdetail.Quantity,
                                      billdetail.Price,
                                      customer.NameCustomer,
                                      user.Staff.NameStaff
                                  };

                // Đổ dữ liệu vào DataTable
                foreach (var item in billDetails)
                {
                    billTable.Rows.Add(
                        item.IdBill,
                        item.IdCustomer,
                        item.IdUser,
                        item.IdStaff,
                        item.CheckIn,
                        item.CheckOut,
                        item.IdBook,
                        item.Title,
                        item.Quantity,
                        item.Price,
                        item.NameCustomer,
                        item.NameStaff
                    );
                }
            }
        }
        #endregion
        #region Report
        // Phương thức hiển thị báo cáo Crystal Report
        private void ShowBillReport(string billId)
        {
            var context = new MyDbContext();
            var billDetails = from billdetail in context.myBillDetail
                              join customer in context.myCustomer on billdetail.Bill.IdCustomer equals customer.Id
                              join user in context.myUser on billdetail.Bill.IdUser equals user.ID
                              join book in context.myBooks on billdetail.IdBook equals book.Id
                              where billdetail.IdBill == billId
                              select new
                              {
                                  billdetail.IdBill,
                                  billdetail.Bill.IdCustomer,
                                  billdetail.Bill.IdUser,
                                  billdetail.Bill.User.IdStaff,
                                  billdetail.Bill.CheckIn,
                                  billdetail.Bill.CheckOut,
                                  billdetail.IdBook,
                                  book.Title,
                                  billdetail.Quantity,
                                  billdetail.Price,
                                  customer.NameCustomer,
                                  user.Staff.NameStaff
                              };
            // Tạo một instance của báo cáo Crystal Report
            crNewReport report = new crNewReport();
            // Đặt DataSet làm nguồn dữ liệu cho báo cáo
            report.SetDataSource(billDetails.ToList());
            // Tạo một form hiển thị báo cáo
            formReportBills f = new formReportBills(); // Giả sử bạn có một form để hiển thị báo cáo
            // Đặt báo cáo Crystal Report vào viewer trong form
            f.crystalReportViewer.ReportSource = report;
            // Hiển thị form báo cáo
            f.Show();
        }
        private void SetHeaderDataGridViewBills()
        {
            dataGridViewBills.Columns["Id"].HeaderText = "Mã hóa đơn";
            dataGridViewBills.Columns["CheckIn"].HeaderText = "Ngày lập";
            dataGridViewBills.Columns["CheckOut"].HeaderText = "Ngày thanh toán";
            dataGridViewBills.Columns["NameStaff"].HeaderText = "Người lập";
        }
        private void SetHeaderDataGridViewBooks()
        {
            dataGridViewBooks.Columns["Id"].HeaderText = "Mã sách";
            dataGridViewBooks.Columns["Title"].HeaderText = "Tên sách";
            dataGridViewBooks.Columns["Price"].HeaderText = "Đơn giá";
            dataGridViewBooks.Columns["Price"].DefaultCellStyle.Format = "C0";
            dataGridViewBooks.Columns["Price"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
        }
        private void SetHeaderDataGridViewBillDetails()
        {
            dataGridViewBillDetails.Columns["IdBook"].HeaderText = "Mã sách";
            dataGridViewBillDetails.Columns["Title"].HeaderText = "Tên sách";
            dataGridViewBillDetails.Columns["Quantity"].HeaderText = "Số lượng";
            dataGridViewBillDetails.Columns["Price"].HeaderText = "Đơn giá";
            dataGridViewBillDetails.Columns["Total"].HeaderText = "Thành tiền";
            dataGridViewBillDetails.Columns["Price"].DefaultCellStyle.Format = "C0";
            dataGridViewBillDetails.Columns["Price"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            dataGridViewBillDetails.Columns["Total"].DefaultCellStyle.Format = "C0";
            dataGridViewBillDetails.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
        }
        #endregion
        #region Methods
        // Load books from the database into dataGridViewBooks
        private void LoadBooks()
        {
            using (var context = new MyDbContext())
            {
                var books = context.myBooks
                    .Select(b => new { b.Id, b.Title, b.Price })
                    .ToList();
                dataGridViewBooks.DataSource = books;
            }
        }

        // Load bills from the database into dataGridViewBills
        private void LoadBills()
        {
            using (var context = new MyDbContext())
            {
                var bills = context.myBill
                    .Select(b => new { b.Id, b.CheckIn, b.CheckOut, b.User.Staff.NameStaff })
                    .ToList();
                dataGridViewBills.DataSource = bills;
            }
        }
        private void LoadCustomers()
        {
            using (var context = new MyDbContext())
            {
                cbIdCustomer.DataSource = context.myCustomer.ToList();
                cbIdCustomer.DisplayMember = "NameCustomer";
                cbIdCustomer.ValueMember = "ID";
            }
        }
        private void LoadBillDetails(string billId)
        {
            using (var context = new MyDbContext())
            {
                var billDetails = context.myBillDetail
                    .Where(bd => bd.IdBill == billId)
                    .Select(bd => new
                    {
                        bd.IdBook,
                        bd.Book.Title,
                        bd.Price,
                        bd.Quantity,
                        Total = bd.Price * bd.Quantity
                    })
                    .ToList();

                // Set the data source for bill details
                dataGridViewBillDetails.DataSource = billDetails;
            }
        }
        void Retrieve()
        {
            try
            {
                LoadBooks();
                LoadBills();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        // Update total price based on Bill Details DataGridView
        private void UpdateTotalLabel()
        {
            decimal total = dataGridViewBillDetails.Rows.OfType<DataGridViewRow>()
                              .Sum(row => int.Parse(row.Cells["Total"].Value.ToString()));

            txbTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        }

        // Set permission for buttons based on user role
        void SetPermission(string permission)
        {
            bool isEnabled = permission == "manager" || permission == "staff";
            btnInsert.Enabled = isEnabled;
            btnAddBillDetail.Enabled = isEnabled;
            btnRemoveBillDetail.Enabled = isEnabled;
            btnRemoveAll.Enabled = isEnabled;
            btnUpdate.Enabled = isEnabled;
            btnViewBill.Enabled = isEnabled;
        }
        // Update existing Bill
        int UpdateBill()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    string billId = txbIdBill.Text;
                    var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
                    if (bill != null)
                    {
                        bill.CheckIn = dtCheckIn.Value.Date;
                        bill.CheckOut = dtCheckOut.Value.Date;
                        bill.IdUser = formMain.__IdUser;
                        bill.IdCustomer = int.Parse(cbIdCustomer.SelectedValue.ToString());
                        return context.SaveChanges();
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return 0;
            }
        }
        // Delete a single BillDetail
        int DeleteBillDetail(string billId, int bookId)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var billDetail = context.myBillDetail.SingleOrDefault(bd => bd.IdBill == billId && bd.IdBook == bookId);
                    if (billDetail != null)
                    {
                        context.myBillDetail.Remove(billDetail);
                        return context.SaveChanges();
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return 0;
            }
        }

        // Delete all BillDetails of a specific Bill
        int DeleteAllBillDetails(string billId)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var billDetails = context.myBillDetail.Where(bd => bd.IdBill == billId).ToList();
                    context.myBillDetail.RemoveRange(billDetails);
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return 0;
            }
        }
        // Search for bills by date
        void Search()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var results = context.myBill
                                         .Where(c => c.CheckOut <= dtDateFind.Value)
                                         .Select(c => new { c.Id, c.CheckOut })
                                         .ToList();
                    dataGridViewBills.DataSource = results;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
        // Common method to display error messages
        void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region Events
        // Load form và khởi tạo dữ liệu
        private void formNewBill_Load(object sender, EventArgs e)
        {
            InitializeBillDetailsTable();
            Retrieve(); // Lấy dữ liệu ban đầu
            SetPermission(formMain.__Permision); // Cài đặt quyền hạn theo vai trò người dùng
            dtCheckIn.Value = DateTime.Now;
            dtCheckOut.Value = DateTime.Now;
            dtDateFind.Value = DateTime.Now;
            SetHeaderDataGridViewBills(); // Đặt tiêu đề cho DataGridView Bills
            SetHeaderDataGridViewBooks(); // Đặt tiêu đề cho DataGridView Books
        }
        // Thêm sách vào chi tiết hóa đơn
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            billDetailsTable.Rows.Add(idBook, title, price, quantity);
            //foreach (DataGridViewRow row in dataGridViewBooks.SelectedRows)
            //{
            //    // Check if the book already exists in the bill details
            //    var existingRow = billDetailsTable.AsEnumerable()
            //        .FirstOrDefault(r => r.Field<int>("Id") == idBook);
            //    if (existingRow != null)
            //    {
            //        // If the book already exists, increase the quantity
            //        existingRow["Quantity"] = int.Parse(existingRow["Quantity"].ToString()) + quantity;
            //    }
            //    else
            //    {
            //        // Add new book to the bill details table
                    
            //    }
            //}
            dataGridViewBillDetails.DataSource = billDetailsTable;
            dataGridViewBillDetails.Refresh();
        }
        // Thêm hóa đơn
        private void btnAddBill_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    // Create a new bill
                    string newBillId = Guid.NewGuid().ToString();
                    var bill = new Bill
                    {
                        Id = newBillId,
                        IdCustomer = int.Parse(cbIdCustomer.SelectedValue.ToString()),
                        CheckIn = dtCheckIn.Value.Date,
                        CheckOut = dtCheckOut.Value.Date,
                        IdUser = formMain.__IdUser // Current user ID
                    };

                    // Add the new bill to the database
                    context.myBill.Add(bill);

                    // Add each book detail from the dataset to the bill
                    foreach (DataRow row in billDetailsTable.Rows)
                    {
                        var billDetail = new BillDetail
                        {
                            IdBill = newBillId,
                            IdBook = int.Parse(row["Id"].ToString()),
                            Quantity = int.Parse(row["Quantity"].ToString()),
                            Price = int.Parse(row["Price"].ToString())
                        };
                        context.myBillDetail.Add(billDetail);
                    }

                    // Save changes to the database
                    context.SaveChanges();
                    MessageBox.Show("Hóa đơn và chi tiết hóa đơn đã được thêm thành công!");

                    // Clear the DataTable after successfully adding the bill
                    billDetailsTable.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Xử lý sự kiện xóa hóa đơn
        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            string billId = txbIdBill.Text;

            if (!string.IsNullOrEmpty(billId))
            {
                if (DeleteBill(billId) > 0)
                {
                    MessageBox.Show("Xóa hóa đơn thành công", "Thông báo");
                    RetrieveBills(); // Cập nhật lại danh sách hóa đơn
                }
                else
                {
                    MessageBox.Show("Xóa hóa đơn không thành công", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("ID hóa đơn không hợp lệ", "Lỗi");
            }
        }
        // Đóng form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Tải lại dữ liệu
        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }
        // Xử lý sự kiện khi chọn một hóa đơn từ DataGridView
        private void dataGridViewBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    using (var context = new MyDbContext())
                    {
                        string billId = dataGridViewBills.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                        var rowToSelect = context.myBill.SingleOrDefault(r => r.Id == billId);
                        if (rowToSelect != null)
                        {
                            txbIdBill.Text = billId;
                            cbIdCustomer.SelectedValue = rowToSelect.Customer.Id;
                            dtCheckIn.Value = rowToSelect.CheckIn;
                            dtCheckOut.Value = rowToSelect.CheckOut;
                        }
                        LoadBillDetails(billId);
                        SetHeaderDataGridViewBillDetails(); // Đặt tiêu đề cho DataGridView Bill Details
                        UpdateTotalLabel(); // Cập nhật tổng tiền
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Xóa một chi tiết hóa đơn
        private void btnRemoveBillDetail_Click(object sender, EventArgs e)
        {
            if (dataGridViewBillDetails.CurrentRow != null)
            {
                string billId = txbIdBill.Text;
                int bookId = int.Parse(dataGridViewBillDetails.CurrentRow.Cells["IdBook"].Value.ToString());

                if (DeleteBillDetail(billId, bookId) > 0)
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn thành công.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn không thành công.", "Thông báo");
                }
                UpdateTotalLabel(); // Cập nhật tổng tiền
                RetrieveBills(); // Tải lại danh sách hóa đơn và chi tiết
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn cần xóa.", "Cảnh báo");
            }
        }
        // Xóa tất cả chi tiết hóa đơn
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            string billId = txbIdBill.Text;

            if (DeleteAllBillDetails(billId) > 0)
            {
                MessageBox.Show("Xóa tất cả chi tiết hóa đơn thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa tất cả chi tiết hóa đơn không thành công.", "Thông báo");
            }
            UpdateTotalLabel(); // Cập nhật tổng tiền
            RetrieveBills(); // Tải lại danh sách hóa đơn
        }
        // Cập nhật hóa đơn
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateBill() > 0)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật hóa đơn không thành công.", "Thông báo");
            }
            RetrieveBills(); // Cập nhật lại danh sách hóa đơn
        }
        // Xem báo cáo hóa đơn
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            string billId = txbIdBill.Text;

            if (!string.IsNullOrEmpty(billId))
            {
                ShowBillReport(billId); // Hiển thị báo cáo hóa đơn
            }
            else
            {
                MessageBox.Show("ID hóa đơn không hợp lệ", "Lỗi");
            }
        }
        // Phương thức xóa hóa đơn và chi tiết hóa đơn
        private int DeleteBill(string billId)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var billToDelete = context.myBill.SingleOrDefault(b => b.Id == billId);
                    if (billToDelete != null)
                    {
                        var billDetails = context.myBillDetail.Where(d => d.IdBill == billId).ToList();
                        context.myBillDetail.RemoveRange(billDetails);
                        context.myBill.Remove(billToDelete);
                    }
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        // Lấy lại danh sách hóa đơn
        private void RetrieveBills()
        {
            using (var context = new MyDbContext())
            {
                var bills = context.myBill
                                   .Select(c => new { c.Id, c.CheckIn, c.CheckOut, c.User.Staff.NameStaff })
                                   .ToList();
                dataGridViewBills.DataSource = bills;
            }
        }
        // Tìm kiếm hóa đơn
        private void btnFind_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion
        private void dataGridViewBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (var context = new MyDbContext())
            {
                int IDB = int.Parse(dataGridViewBooks.CurrentRow.Cells["Id"].Value.ToString());
                var book = context.myBooks.SingleOrDefault(b => b.Id == IDB);
                if (book != null)
                {
                    idBook = IDB;
                    title = book.Title;
                    price = book.Price;
                    quantity = int.Parse(nmQuantity.Value.ToString());
                }
            }
        }

        private void nmQuantity_ValueChanged(object sender, EventArgs e)
        {
            quantity = int.Parse(nmQuantity.Value.ToString());
        }
    }
}
