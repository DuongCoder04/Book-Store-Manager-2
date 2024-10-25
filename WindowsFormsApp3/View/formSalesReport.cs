using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;
using WindowsFormsApp.Report;

namespace WindowsFormsApp.View
{
    public partial class formSalesReport : Form
    {
        private MyDbContext context;
        public formSalesReport()
        {
            InitializeComponent();
            context = new MyDbContext();
        }
        #region Method
        void Retrieve()
        {
            var result = context.myBillDetail
                                .Select(c => new {
                                    c.IdBook,
                                    c.Book.Title,
                                    c.IdBill,
                                    c.Bill.CheckOut,
                                    c.Quantity,
                                    c.Price,
                                    Total = c.Quantity * c.Book.Price
                                })
                                .ToList();
            dataGridViewBillDetail.DataSource = result;
            SetGridHeaders();
        }
        void Search()
        {
            var result = context.myBillDetail
                                .Where(c => c.Bill.CheckOut >= dtStart.Value && c.Bill.CheckOut <= dtEnd.Value)
                                .Select(c => new {
                                    c.IdBook,
                                    c.Book.Title,
                                    c.IdBill,
                                    c.Bill.CheckOut,
                                    c.Quantity,
                                    c.Price,
                                    Total = c.Quantity * c.Book.Price
                                })
                                .ToList();
            dataGridViewBillDetail.DataSource = result;
        }
        void Search(string key)
        {
            var result = context.myBillDetail
                                .Where(c => c.Book.Title.Contains(key) 
                                || c.Price.ToString().Contains(key) 
                                || c.Quantity.ToString().Contains(key))
                                .Select(c => new {
                                    c.IdBook,
                                    c.Book.Title,
                                    c.IdBill,
                                    c.Bill.CheckOut,
                                    c.Quantity,
                                    c.Price,
                                    Total = c.Quantity * c.Book.Price
                                })
                                .ToList();
            dataGridViewBillDetail.DataSource = result;
        }
        private void SetGridHeaders()
        {
            dataGridViewBillDetail.Columns["IdBook"].HeaderText = "Mã sách";
            dataGridViewBillDetail.Columns["Title"].HeaderText = "Tên sách";
            dataGridViewBillDetail.Columns["IdBill"].HeaderText = "Mã hóa đơn";
            dataGridViewBillDetail.Columns["CheckOut"].HeaderText = "Ngày thanh toán";
            dataGridViewBillDetail.Columns["Quantity"].HeaderText = "Số lượng";
            dataGridViewBillDetail.Columns["Price"].HeaderText = "Đơn giá";
            dataGridViewBillDetail.Columns["Price"].DefaultCellStyle.Format = "C0";
            dataGridViewBillDetail.Columns["Price"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            dataGridViewBillDetail.Columns["Total"].HeaderText = "Thành tiền";
            dataGridViewBillDetail.Columns["Total"].DefaultCellStyle.Format = "C0";
            dataGridViewBillDetail.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
        }
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
        private void ShowSalesReport(DateTime start, DateTime end)
        {
            using (var context = new MyDbContext())  // Sử dụng using để đảm bảo context được dispose
            {
                var billDetails = from billdetail in context.myBillDetail
                                  join book in context.myBooks on billdetail.IdBook equals book.Id
                                  where DbFunctions.TruncateTime(billdetail.Bill.CheckOut) >= DbFunctions.TruncateTime(start)
                                     && DbFunctions.TruncateTime(billdetail.Bill.CheckOut) <= DbFunctions.TruncateTime(end)
                                  select new
                                  {
                                      NameReport = txbReportName.Text,
                                      billdetail.IdBill,
                                      billdetail.IdBook,
                                      book.Title,
                                      billdetail.Quantity,
                                      billdetail.Price,
                                      Total = billdetail.Price * billdetail.Quantity,
                                      billdetail.Bill.CheckOut,
                                      StartDay = start.Date,
                                      EndDay = end.Date
                                  };

                // Chuyển đổi kết quả truy vấn thành một danh sách
                var billDetailList = billDetails.ToList();
                // Tạo một instance của báo cáo Crystal Report
                crSalesReport report = new crSalesReport();

                // Đặt DataTable làm nguồn dữ liệu cho báo cáo
                report.SetDataSource(billDetailList);

                // Tạo một form hiển thị báo cáo
                formReportBills f = new formReportBills();

                // Đặt báo cáo Crystal Report vào viewer trong form
                f.crystalReportViewer.ReportSource = report;

                // Hiển thị form báo cáo
                f.Show();
            }
        }

        #endregion
        #region Event
        private void formSalesReport_Load(object sender, EventArgs e)
        {
            Retrieve();
        }
        #endregion

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtStart.Value.Date <= dtEnd.Value.Date)
            {
                Search();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày đầu phải nhỏ hơn hoặc bằng ngày cuối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtStart.Focus();
            }
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ngày kết thúc lớn hơn hoặc bằng ngày bắt đầu
            if (dtEnd.Value.Date >= dtStart.Value.Date)
            {
                Search(); // Gọi hàm tìm kiếm nếu điều kiện hợp lệ
            }
            else
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Vui lòng chọn ngày kết thúc lớn hơn hoặc bằng ngày bắt đầu",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Đưa con trỏ focus trở lại cho dtEnd để người dùng sửa
                dtEnd.Focus();
            }
        }


        private void btnViewBill_Click(object sender, EventArgs e)
        {
            string billId = dataGridViewBillDetail.CurrentRow.Cells["IdBill"].Value.ToString();
            ShowBillReport(billId);
        }

        private void dataGridViewBillDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            Search(txbSearch.Text);
        }
        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            ShowSalesReport(dtStart.Value, dtEnd.Value);
        }
    }
}
