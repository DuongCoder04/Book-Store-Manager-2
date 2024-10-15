using System;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.DataSet;
using WindowsFormsApp.Models;
using WindowsFormsApp.Report;

namespace WindowsFormsApp.View
{
    public partial class formNewBill : Form
    {
        private dsBillDetails billDataSet;
        public formNewBill()
        {
            InitializeComponent();
            billDataSet = new dsBillDetails();

            // Đảm bảo bảng "BillTable" đã tồn tại trong DataSet
            if (!billDataSet.Tables.Contains("BillTable"))
            {
                // Tạo DataTable "BillTable" nếu chưa có
                DataTable billTable = new DataTable("BillTable");

                billTable.Columns.Add("IdBill", typeof(int));
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
        #region DataSet
        // Phương thức lấy dữ liệu hóa đơn theo IdBill và đổ vào DataSet đã tạo
        private void GetBillData(int billId)
        {
            using (var context = new MyDbContext())
            {
                // Lấy DataTable từ DataSet đã tạo sẵn
                var billTable = billDataSet.Tables["BillTable"];

                // Xóa dữ liệu cũ trong DataTable nếu có
                billTable.Clear();

                // Truy vấn cơ sở dữ liệu để lấy hóa đơn theo IdBill
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
        private void ShowBillReport(int billId)
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
            formReport f = new formReport(); // Giả sử bạn có một form để hiển thị báo cáo

            // Đặt báo cáo Crystal Report vào viewer trong form
            f.crystalReportViewer.ReportSource = report;

            // Hiển thị form báo cáo
            f.Show();
        }
        #endregion
        #region Methods

        void Retrieve()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    cbIdCustomer.DataSource = context.myCustomer.ToList();
                    cbIdCustomer.DisplayMember = "NameCustomer";
                    cbIdCustomer.ValueMember = "ID";
                    var books = context.myBooks
                                       .Select(b => new { b.Id, b.Title, b.Price })
                                       .ToList();
                    dataGridViewBooks.DataSource = books;
                    var bills = context.myBill
                                       .Select(c => new { c.Id, c.CheckIn, c.CheckOut, c.User.Staff.NameStaff })
                                       .ToList();
                    dataGridViewBills.DataSource = bills;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalLabel()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dataGridViewBillDetails.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            txbTotal.Text = total.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        }

        void SetPermission(string permission)
        {
            bool isEnabled = (permission == "manager" || permission == "staff");
            btnInsert.Enabled = isEnabled;
            btnAddBillDetail.Enabled = isEnabled;
            btnRemoveBillDetail.Enabled = isEnabled;
            btnRemoveAll.Enabled = isEnabled;
            btnUpdate.Enabled = isEnabled;
            btnViewBill.Enabled = isEnabled;
            cbUpdateDateSell.Enabled = isEnabled;
        }

        int InsertBill()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var bill = new Bill
                    {
                        IdCustomer = int.Parse(cbIdCustomer.SelectedValue.ToString()),
                        CheckIn = dtCheckIn.Value,
                        CheckOut = dtCheckOut.Value,
                        IdUser = formMain.__IdUser,
                    };
                    context.myBill.Add(bill);
                    context.SaveChanges();
                    return bill.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        int InsertBillDetail(int billId, int bookId, int quantity, int price)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var billDetail = new BillDetail
                    {
                        IdBill = billId,
                        IdBook = bookId,
                        Quantity = quantity,
                        Price = price
                    };
                    context.myBillDetail.Add(billDetail);
                    return context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        int UpdateBill()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    int billId = int.Parse(txbIdBill.Text);
                    var bill = context.myBill.SingleOrDefault(b => b.Id == billId);
                    if (bill != null)
                    {
                        bill.CheckIn = dtCheckIn.Value;
                        bill.CheckOut = dtCheckOut.Value;
                        bill.IdUser = formMain.__IdUser;
                        bill.IdCustomer = int.Parse(cbIdCustomer.SelectedValue.ToString());
                        return context.SaveChanges();
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        int DeleteBillDetail(int billId, int bookId)
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
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        int DeleteAllBillDetails(int billId)
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
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

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
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Events

        private void formNewBill_Load(object sender, EventArgs e)
        {
            Retrieve();
            SetPermission(formMain.__Permision);
            dtCheckIn.Value = DateTime.Now;
            dtCheckOut.Value = DateTime.Now;
            dtDateFind.Value = DateTime.Now;

            dataGridViewBills.Columns["Id"].HeaderText = "Mã hóa đơn";
            dataGridViewBills.Columns["CheckIn"].HeaderText = "Ngày lập";
            dataGridViewBills.Columns["CheckOut"].HeaderText = "Ngày thanh toán";
            dataGridViewBills.Columns["NameStaff"].HeaderText = "Người lập";
        }
        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            // Lấy ID hóa đơn từ textbox hoặc từ grid
            int billId;
            if (int.TryParse(txbIdBill.Text, out billId)) // Giả sử có textbox chứa ID hóa đơn
            {
                if (DeleteBill(billId) > 0)
                {
                    MessageBox.Show("Xóa hóa đơn thành công", "Thông báo");
                    // Cập nhật lại danh sách hóa đơn nếu cần
                    RetrieveBills(); // Gọi phương thức lấy lại danh sách hóa đơn
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
        private void btnInsert_Click(object sender, EventArgs e)
        {
            int newBillId = InsertBill();
            if (newBillId > 0)
            {
                MessageBox.Show("Thêm hóa đơn thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm hóa đơn không thành công.", "Thông báo");
            }
            formNewBill_Load(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            Retrieve();
        }
        private void dataGridViewBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    using (var context = new MyDbContext())
                    {
                        int billId = int.Parse(dataGridViewBills.Rows[e.RowIndex].Cells["Id"].Value.ToString());
                        txbIdBill.Text = billId.ToString();
                        var billDetails = context.myBillDetail
                                                 .Where(bd => bd.IdBill == billId)
                                                 .Select(bd => new
                                                 {
                                                     bd.IdBill,
                                                     bd.IdBook,
                                                     bd.Book.Title,
                                                     bd.Quantity,
                                                     bd.Price,
                                                     Total = bd.Quantity * bd.Book.Price
                                                 })
                                                 .ToList();
                        dataGridViewBillDetails.DataSource = billDetails;
                        dataGridViewBillDetails.Columns["IdBill"].HeaderText = "Mã hóa đơn";
                        dataGridViewBillDetails.Columns["IdBook"].HeaderText = "Mã sách";
                        dataGridViewBillDetails.Columns["Title"].HeaderText = "Tên sách";
                        dataGridViewBillDetails.Columns["Quantity"].HeaderText = "Số lượng";
                        dataGridViewBillDetails.Columns["Price"].HeaderText = "Đơn giá";
                        dataGridViewBillDetails.Columns["Total"].HeaderText = "Thành tiền";
                        UpdateTotalLabel();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddBillDetail_Click(object sender, EventArgs e)
        {
            // Giả sử người dùng sẽ chọn sách từ dataGridViewBooks
            if (dataGridViewBooks.CurrentRow != null)
            {
                int billId = int.Parse(txbIdBill.Text);
                int bookId = int.Parse(dataGridViewBooks.CurrentRow.Cells["Id"].Value.ToString());
                int quantity = int.Parse(nmQuantity.Value.ToString()); // Hoặc có thể cho phép nhập số lượng từ một ô khác
                int price = int.Parse(dataGridViewBooks.CurrentRow.Cells["Price"].Value.ToString());
                int result = InsertBillDetail(billId, bookId, quantity, price);
                if (result > 0)
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thành công.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn không thành công.", "Thông báo");
                }
                UpdateTotalLabel();
                formNewBill_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sách để thêm vào hóa đơn.", "Cảnh báo");
            }
        }

        private void btnRemoveBillDetail_Click(object sender, EventArgs e)
        {
            if (dataGridViewBillDetails.CurrentRow != null)
            {
                int billId = int.Parse(dataGridViewBillDetails.CurrentRow.Cells["IdBill"].Value.ToString());
                int bookId = int.Parse(dataGridViewBillDetails.CurrentRow.Cells["IdBook"].Value.ToString());
                int result = DeleteBillDetail(billId, bookId);
                if (result > 0)
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn thành công.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết hóa đơn không thành công.", "Thông báo");
                }
                UpdateTotalLabel();
                formNewBill_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chi tiết hóa đơn cần xóa.", "Cảnh báo");
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            int billId = int.Parse(txbIdBill.Text);
            int result = DeleteAllBillDetails(billId);
            if (result > 0)
            {
                MessageBox.Show("Xóa tất cả chi tiết hóa đơn thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa tất cả chi tiết hóa đơn không thành công.", "Thông báo");
            }
            UpdateTotalLabel();
            formNewBill_Load(sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = UpdateBill();
            if (result > 0)
            {
                MessageBox.Show("Cập nhật hóa đơn thành công.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật hóa đơn không thành công.", "Thông báo");
            }
            formNewBill_Load(sender, e);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            int billId;
            if (int.TryParse(txbIdBill.Text, out billId)) // Giả sử txbBillId là TextBox chứa IdBill
            {
                // Gọi phương thức GetBillData để lấy dữ liệu hóa đơn
                //GetBillData(billId);

                // Sau đó, sử dụng DataSet này để làm báo cáo Crystal Report
                ShowBillReport(billId); // Gọi phương thức tạo báo cáo
            }
            else
            {
                MessageBox.Show("ID hóa đơn không hợp lệ", "Lỗi");
            }
        }
        // Phương thức xóa hóa đơn và chi tiết hóa đơn
        private int DeleteBill(int billId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                // Tìm hóa đơn cần xóa
                var billToDelete = context.myBill.SingleOrDefault(b => b.Id == billId);
                if (billToDelete != null)
                {
                    // Lấy danh sách chi tiết hóa đơn liên quan
                    var billDetails = context.myBillDetail.Where(d => d.IdBill == billId).ToList();

                    // Xóa tất cả chi tiết hóa đơn
                    context.myBillDetail.RemoveRange(billDetails);

                    // Xóa hóa đơn
                    context.myBill.Remove(billToDelete);
                }
                return context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
        private void RetrieveBills()
        {
            // Phương thức lấy lại danh sách hóa đơn (nếu chưa có)
            using (var context = new MyDbContext())
            {
                var bills = context.myBill
                                   .Select(c => new { c.Id, c.CheckIn, c.CheckOut, c.User.Staff.NameStaff})
                                   .ToList();
                dataGridViewBills.DataSource = bills;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion
    }
}
