using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp.DataService;

namespace WindowsFormsApp.View
{
    public partial class formSalesStatistics : Form
    {
        public formSalesStatistics()
        {
            InitializeComponent();
        }

        #region Method
        void Retrieve()
        {
            // Ngăn người dùng thay đổi kích cỡ form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; // Vô hiệu hóa nút phóng to

            ShowStatisticsBasedOnSelection();
            ShowStatisticsOnChart();
        }

        private void ShowStatisticsBasedOnSelection()
        {
            if (rbtMonth.Checked)
            {
                ShowMonthlyStatistics();
            }
            else if (rbtYear.Checked)
            {
                ShowYearlyStatistics();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại thống kê (theo tháng hoặc năm)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowMonthlyStatistics()
        {
            using (var context = new MyDbContext())
            {
                var monthlyStatistics = from billdetail in context.myBillDetail
                                        group billdetail by new
                                        {
                                            Month = billdetail.Bill.CheckOut.Month,
                                            Year = billdetail.Bill.CheckOut.Year
                                        } into g
                                        select new
                                        {
                                            MonthYear = g.Key.Month + "/" + g.Key.Year,  // Ghép tháng và năm để hiển thị
                                            TotalAmount = g.Sum(x => x.Price * x.Quantity)  // Tính tổng thành tiền
                                        };

                var statisticsList = monthlyStatistics.ToList();

                // Kiểm tra nếu không có dữ liệu
                if (statisticsList.Any())
                {
                    dataGridViewStatistics.DataSource = statisticsList;
                    dataGridViewStatistics.Columns["MonthYear"].HeaderText = "Tháng";
                    dataGridViewStatistics.Columns["TotalAmount"].HeaderText = "Tổng thành tiền";
                    dataGridViewStatistics.Columns["TotalAmount"].DefaultCellStyle.Format = "C0";
                    dataGridViewStatistics.Columns["TotalAmount"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thống kê theo tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowYearlyStatistics()
        {
            using (var context = new MyDbContext())
            {
                var yearlyStatistics = from billdetail in context.myBillDetail
                                       group billdetail by billdetail.Bill.CheckOut.Year into g
                                       select new
                                       {
                                           Year = g.Key,  // Lấy năm để hiển thị
                                           TotalAmount = g.Sum(x => x.Price * x.Quantity)  // Tính tổng thành tiền
                                       };

                var statisticsList = yearlyStatistics.ToList();

                if (statisticsList.Any())
                {
                    dataGridViewStatistics.DataSource = statisticsList;
                    dataGridViewStatistics.Columns["Year"].HeaderText = "Năm";
                    dataGridViewStatistics.Columns["TotalAmount"].HeaderText = "Tổng thành tiền";
                    dataGridViewStatistics.Columns["TotalAmount"].DefaultCellStyle.Format = "C0";
                    dataGridViewStatistics.Columns["TotalAmount"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu thống kê theo năm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowStatisticsOnChart()
        {
            // Xóa các series và dữ liệu cũ khỏi Chart
            chartStatistics.Series.Clear();

            // Kiểm tra nếu không có dữ liệu trong DataGridView
            if (dataGridViewStatistics.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị trên biểu đồ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Tạo một series mới cho biểu đồ
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Doanh thu",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column // Loại biểu đồ cột
            };

            // Thêm series vào chart
            chartStatistics.Series.Add(series);

            // Kiểm tra nếu đang ở chế độ tháng hoặc năm để hiển thị biểu đồ tương ứng
            if (rbtMonth.Checked) // Chế độ tháng
            {
                foreach (DataGridViewRow row in dataGridViewStatistics.Rows)
                {
                    if (row.Cells["MonthYear"].Value != null && row.Cells["TotalAmount"].Value != null)
                    {
                        // Lấy giá trị từ DataGridView
                        string monthYear = row.Cells["MonthYear"].Value.ToString();
                        decimal totalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value);

                        // Thêm điểm dữ liệu vào series
                        series.Points.AddXY(monthYear, totalAmount);
                    }
                }

                // Cấu hình biểu đồ cho chế độ tháng
                chartStatistics.ChartAreas[0].AxisX.Title = "Tháng";
                chartStatistics.ChartAreas[0].AxisY.Title = "Tổng doanh thu";
                chartStatistics.ChartAreas[0].AxisX.Interval = 1;  // Đặt khoảng cách giữa các nhãn trên trục X
            }
            else if (rbtYear.Checked) // Chế độ năm
            {
                foreach (DataGridViewRow row in dataGridViewStatistics.Rows)
                {
                    if (row.Cells["Year"].Value != null && row.Cells["TotalAmount"].Value != null)
                    {
                        // Lấy giá trị từ DataGridView
                        string year = row.Cells["Year"].Value.ToString();
                        decimal totalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value);

                        // Thêm điểm dữ liệu vào series
                        series.Points.AddXY(year, totalAmount);
                    }
                }

                // Cấu hình biểu đồ cho chế độ năm
                chartStatistics.ChartAreas[0].AxisX.Title = "Năm";
                chartStatistics.ChartAreas[0].AxisY.Title = "Tổng doanh thu";
                chartStatistics.ChartAreas[0].AxisX.Interval = 1;  // Đặt khoảng cách giữa các nhãn trên trục X
            }
        }


        #endregion

        #region Events
        private void formSalesStatistics_Load(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void rbtMonth_CheckedChanged(object sender, EventArgs e)
        {
            Retrieve();
        }

        private void rbtYear_CheckedChanged(object sender, EventArgs e)
        {
            Retrieve();
        }
        #endregion
    }
}
