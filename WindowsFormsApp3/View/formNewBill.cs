using System;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.DataSet;
using WindowsFormsApp.Models;
using WindowsFormsApp.Report;

namespace WindowsFormsApp.View
{
    public partial class formNewBill : Form
    {
        int idBillDetail;
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
                billDetailsTable.Columns.Add("IdBook", typeof(int));
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
                SetHeaderDataGridViewBills();
                SetHeaderDataGridViewBooks();
                SetHeaderDataGridViewBillDetails();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        // Update total price based on Bill Details DataGridView
        private void UpdateTotalLabel()
        {
            int total = dataGridViewBillDetails.Rows.OfType<DataGridViewRow>()
                              .Sum(row => int.Parse(row.Cells["Total"].Value.ToString()));

            txbTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        }

        // Set permission for buttons based on user role
        void SetPermission(string permission)
        {
            if (permission == "manager")
            {
                btnInsert.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnViewBill.Enabled = true;
                btnReload.Enabled = true;
                btnAddBillDetail.Enabled = true;
                btnRemoveBillDetail.Enabled = true;
                btnRemoveAll.Enabled = true;
                btnFind.Enabled = true;
            }
            else
            {
                btnInsert.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnViewBill.Enabled = true;
                btnReload.Enabled = true;
                btnAddBillDetail.Enabled = true;
                btnRemoveBillDetail.Enabled = true;
                btnRemoveAll.Enabled = true;
                btnFind.Enabled = true;
            }
            
        }
        // Update existing Bill
        int UpdateBill()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    string billId = txbIdBill.Text;

                    // Tìm hóa đơn theo Id
                    var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
                    if (bill != null)
                    {
                        // Cập nhật thông tin hóa đơn
                        bill.CheckIn = dtCheckIn.Value.Date;
                        bill.CheckOut = dtCheckOut.Value.Date;
                        bill.IdUser = formMain.__IdUser;
                        bill.IdCustomer = int.Parse(cbIdCustomer.SelectedValue.ToString());

                        // Xóa chi tiết hóa đơn cũ
                        var existingDetails = context.myBillDetail.Where(bd => bd.IdBill == billId).ToList();
                        context.myBillDetail.RemoveRange(existingDetails);

                        // Thêm chi tiết hóa đơn mới từ DataTable billDetailsTable
                        foreach (DataRow row in billDetailsTable.Rows)
                        {
                            int idBook = Convert.ToInt32(row["IdBook"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);
                            int price = Convert.ToInt32(row["Price"]);

                            var billDetail = new BillDetail
                            {
                                IdBill = billId,
                                IdBook = idBook,
                                Quantity = quantity,
                                Price = price
                            };

                            context.myBillDetail.Add(billDetail);
                        }

                        // Lưu thay đổi vào cơ sở dữ liệu
                        return context.SaveChanges();
                    }

                    return 0; // Trả về 0 nếu không tìm thấy hóa đơn
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return 0;
            }
        }

        // Delete a single BillDetail
        private void RemoveSelectedBookFromBillDetail(int idBook, int quantityToRemove)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridViewBillDetails không
            if (dataGridViewBillDetails.CurrentRow != null)
            {
                // Tìm hàng tương ứng trong DataTable bằng IdBook
                var existingRow = billDetailsTable.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("IdBook") == idBook);

                if (existingRow != null)
                {
                    // Lấy số lượng hiện tại của sách trong bảng
                    int currentQuantity = existingRow.Field<int>("Quantity");

                    // Kiểm tra số lượng cần xóa
                    if (quantityToRemove >= currentQuantity)
                    {
                        // Nếu số lượng cần xóa >= số lượng hiện tại, xóa dòng đó khỏi DataTable
                        billDetailsTable.Rows.Remove(existingRow);
                    }
                    else
                    {
                        // Ngược lại, trừ số lượng cần xóa từ số lượng hiện tại
                        existingRow["Quantity"] = currentQuantity - quantityToRemove;
                        existingRow["Total"] = Convert.ToInt32(existingRow["Price"]) * (currentQuantity - quantityToRemove);
                    }

                    // Cập nhật lại DataGridView
                    dataGridViewBillDetails.DataSource = billDetailsTable;
                    dataGridViewBillDetails.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sách trong chi tiết hóa đơn để xóa.", "Thông báo");
            }
        }
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
                                         .Select(c => new { c.Id, c.CheckIn, c.CheckOut, c.User.Staff.NameStaff })
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
            SetHeaderDataGridViewBillDetails(); // Đặt tiêu đề cho DataGridView Bill Details
        }
        // Thêm sách vào chi tiết hóa đơn
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (idBook > 0)
            {
                var existingRow = billDetailsTable.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("IdBook") == idBook);
                if (existingRow != null)
                {
                    // If the book already exists, increase the quantity
                    existingRow["Quantity"] = int.Parse(existingRow["Quantity"].ToString()) + quantity;
                }
                else
                {
                    // Add new book to the bill details table
                    billDetailsTable.Rows.Add(idBook, title, price, quantity);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để thêm", "Thông báo");
            }
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
                            IdBook = int.Parse(row["IdBook"].ToString()),
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
            // Kiểm tra xem người dùng có chọn một hàng hợp lệ không
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Lấy ID của hóa đơn được chọn từ DataGridViewBills
                    string selectedBillId = dataGridViewBills.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    // Xóa dữ liệu cũ trong DataTable billDetailsTable để chuẩn bị cho dữ liệu mới
                    billDetailsTable.Clear();
                    // Lấy chi tiết hóa đơn từ cơ sở dữ liệu
                    using (var context = new MyDbContext())
                    {
                        var bills = context.myBill.SingleOrDefault(b => b.Id == selectedBillId);
                        if (bills != null)
                        {
                            txbIdBill.Text = bills.Id;
                            dtCheckIn.Value = bills.CheckIn.Date;
                            dtCheckOut.Value = bills.CheckOut.Date;
                            cbIdCustomer.SelectedValue = bills.IdCustomer;
                        }
                        var billDetails = context.myBillDetail
                            .Where(bd => bd.IdBill == selectedBillId)
                            .Select(bd => new
                            {
                                bd.IdBook,
                                bd.Book.Title,
                                bd.Quantity,
                                bd.Price,
                                Total = bd.Quantity * bd.Price
                            })
                            .ToList();

                        // Thêm từng chi tiết vào DataTable billDetailsTable
                        foreach (var detail in billDetails)
                        {
                            DataRow row = billDetailsTable.NewRow();
                            row["IdBook"] = detail.IdBook;
                            row["Title"] = detail.Title;
                            row["Quantity"] = detail.Quantity;
                            row["Price"] = detail.Price;
                            row["Total"] = detail.Total;

                            billDetailsTable.Rows.Add(row);
                        }
                    }

                    // Gán DataTable làm nguồn dữ liệu cho DataGridViewBillDetails
                    dataGridViewBillDetails.DataSource = billDetailsTable;
                    dataGridViewBillDetails.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Xóa một chi tiết hóa đơn
        private void btnRemoveBillDetail_Click(object sender, EventArgs e)
        {
            RemoveSelectedBookFromBillDetail(idBillDetail, quantity);
        }
        // Xóa tất cả chi tiết hóa đơn
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            billDetailsTable.Clear();
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
        private void dataGridViewBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idBillDetail = int.Parse(dataGridViewBillDetails.CurrentRow.Cells["IdBook"].Value.ToString());
        }
    }
}
