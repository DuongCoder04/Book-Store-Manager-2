namespace WindowsFormsApp.View
{
    partial class formSalesStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewStatistics = new System.Windows.Forms.DataGridView();
            this.chartStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtYear = new System.Windows.Forms.RadioButton();
            this.rbtMonth = new System.Windows.Forms.RadioButton();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatistics)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewStatistics
            // 
            this.dataGridViewStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStatistics.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewStatistics.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewStatistics.Name = "dataGridViewStatistics";
            this.dataGridViewStatistics.Size = new System.Drawing.Size(240, 575);
            this.dataGridViewStatistics.TabIndex = 0;
            // 
            // chartStatistics
            // 
            chartArea1.Name = "ChartArea1";
            this.chartStatistics.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStatistics.Legends.Add(legend1);
            this.chartStatistics.Location = new System.Drawing.Point(240, 100);
            this.chartStatistics.Name = "chartStatistics";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStatistics.Series.Add(series1);
            this.chartStatistics.Size = new System.Drawing.Size(677, 475);
            this.chartStatistics.TabIndex = 1;
            this.chartStatistics.Text = "cSalesChart";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rbtYear);
            this.panel1.Controls.Add(this.rbtMonth);
            this.panel1.Controls.Add(this.dtEnd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtStart);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(240, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 100);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(195, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 33);
            this.label3.TabIndex = 3;
            this.label3.Text = "Biểu đồ thống kê";
            // 
            // rbtYear
            // 
            this.rbtYear.AutoSize = true;
            this.rbtYear.Checked = true;
            this.rbtYear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rbtYear.ForeColor = System.Drawing.Color.Teal;
            this.rbtYear.Location = new System.Drawing.Point(599, 73);
            this.rbtYear.Name = "rbtYear";
            this.rbtYear.Size = new System.Drawing.Size(52, 20);
            this.rbtYear.TabIndex = 2;
            this.rbtYear.TabStop = true;
            this.rbtYear.Text = "Năm";
            this.rbtYear.UseVisualStyleBackColor = true;
            this.rbtYear.CheckedChanged += new System.EventHandler(this.rbtYear_CheckedChanged);
            // 
            // rbtMonth
            // 
            this.rbtMonth.AutoSize = true;
            this.rbtMonth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rbtMonth.ForeColor = System.Drawing.Color.Teal;
            this.rbtMonth.Location = new System.Drawing.Point(500, 73);
            this.rbtMonth.Name = "rbtMonth";
            this.rbtMonth.Size = new System.Drawing.Size(64, 20);
            this.rbtMonth.TabIndex = 2;
            this.rbtMonth.Text = "Tháng";
            this.rbtMonth.UseVisualStyleBackColor = true;
            this.rbtMonth.CheckedChanged += new System.EventHandler(this.rbtMonth_CheckedChanged);
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(321, 74);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(162, 20);
            this.dtEnd.TabIndex = 1;
            this.dtEnd.ValueChanged += new System.EventHandler(this.dtEnd_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(260, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cuối:";
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(75, 73);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(162, 20);
            this.dtStart.TabIndex = 1;
            this.dtStart.ValueChanged += new System.EventHandler(this.dtStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(14, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đầu:";
            // 
            // formSalesStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 575);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chartStatistics);
            this.Controls.Add(this.dataGridViewStatistics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "formSalesStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formSalesStatistics";
            this.Load += new System.EventHandler(this.formSalesStatistics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatistics)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStatistics;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatistics;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtYear;
        private System.Windows.Forms.RadioButton rbtMonth;
        private System.Windows.Forms.Label label3;
    }
}