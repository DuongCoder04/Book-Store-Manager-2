namespace WindowsFormsApp.View
{
    partial class formNewBill
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
            this.txbIdBill = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtCheckIn = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.label4 = new System.Windows.Forms.Label();
            this.txbTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnViewBill = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.dataGridViewBills = new System.Windows.Forms.DataGridView();
            this.dataGridViewBillDetails = new System.Windows.Forms.DataGridView();
            this.cbUpdateDateSell = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddBillDetail = new System.Windows.Forms.Button();
            this.btnRemoveBillDetail = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.dtDateFind = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnFind = new System.Windows.Forms.Button();
            this.cbIdCustomer = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.dtCheckOut = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.nmQuantity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dtCheckIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBillDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // txbIdBill
            // 
            this.txbIdBill.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbIdBill.Location = new System.Drawing.Point(130, 20);
            this.txbIdBill.Name = "txbIdBill";
            this.txbIdBill.ReadOnly = true;
            this.txbIdBill.Size = new System.Drawing.Size(113, 23);
            this.txbIdBill.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label5.Location = new System.Drawing.Point(12, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 19);
            this.label5.TabIndex = 15;
            this.label5.Text = "Mã hóa đơn:";
            // 
            // dtCheckIn
            // 
            // 
            // 
            // 
            this.dtCheckIn.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtCheckIn.ButtonDropDown.Visible = true;
            this.dtCheckIn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCheckIn.ForeColor = System.Drawing.Color.Black;
            this.dtCheckIn.Location = new System.Drawing.Point(365, 48);
            // 
            // 
            // 
            this.dtCheckIn.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            this.dtCheckIn.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtCheckIn.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtCheckIn.MonthCalendar.DisplayMonth = new System.DateTime(2024, 6, 1, 0, 0, 0, 0);
            this.dtCheckIn.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtCheckIn.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtCheckIn.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtCheckIn.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtCheckIn.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtCheckIn.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtCheckIn.MonthCalendar.TodayButtonVisible = true;
            this.dtCheckIn.Name = "dtCheckIn";
            this.dtCheckIn.Size = new System.Drawing.Size(104, 23);
            this.dtCheckIn.TabIndex = 14;
            this.dtCheckIn.Value = new System.DateTime(2024, 6, 4, 14, 32, 59, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label4.Location = new System.Drawing.Point(251, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ngày lập:";
            // 
            // txbTotal
            // 
            this.txbTotal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTotal.Location = new System.Drawing.Point(129, 79);
            this.txbTotal.Name = "txbTotal";
            this.txbTotal.Size = new System.Drawing.Size(114, 23);
            this.txbTotal.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Tổng tiền:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label2.Location = new System.Drawing.Point(251, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = "Thanh toán:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "Khách hàng:";
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInsert.Location = new System.Drawing.Point(398, 109);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 32);
            this.btnInsert.TabIndex = 21;
            this.btnInsert.Text = "Thêm HD";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnUpdate.Location = new System.Drawing.Point(398, 147);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Sửa HD";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnViewBill
            // 
            this.btnViewBill.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnViewBill.Location = new System.Drawing.Point(398, 222);
            this.btnViewBill.Name = "btnViewBill";
            this.btnViewBill.Size = new System.Drawing.Size(75, 32);
            this.btnViewBill.TabIndex = 21;
            this.btnViewBill.Text = "Xem HD";
            this.btnViewBill.UseVisualStyleBackColor = true;
            this.btnViewBill.Click += new System.EventHandler(this.btnViewBill_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnExit.Location = new System.Drawing.Point(398, 297);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 31);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Location = new System.Drawing.Point(12, 109);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.Size = new System.Drawing.Size(378, 449);
            this.dataGridViewBooks.TabIndex = 22;
            // 
            // dataGridViewBills
            // 
            this.dataGridViewBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBills.Location = new System.Drawing.Point(480, 54);
            this.dataGridViewBills.Name = "dataGridViewBills";
            this.dataGridViewBills.Size = new System.Drawing.Size(418, 261);
            this.dataGridViewBills.TabIndex = 22;
            this.dataGridViewBills.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBills_CellClick);
            // 
            // dataGridViewBillDetails
            // 
            this.dataGridViewBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBillDetails.Location = new System.Drawing.Point(480, 319);
            this.dataGridViewBillDetails.Name = "dataGridViewBillDetails";
            this.dataGridViewBillDetails.Size = new System.Drawing.Size(418, 239);
            this.dataGridViewBillDetails.TabIndex = 22;
            // 
            // cbUpdateDateSell
            // 
            this.cbUpdateDateSell.AutoSize = true;
            this.cbUpdateDateSell.Location = new System.Drawing.Point(481, 28);
            this.cbUpdateDateSell.Name = "cbUpdateDateSell";
            this.cbUpdateDateSell.Size = new System.Drawing.Size(116, 17);
            this.cbUpdateDateSell.TabIndex = 23;
            this.cbUpdateDateSell.Text = "Cập nhật ngày bán";
            this.cbUpdateDateSell.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(130)))), ((int)(((byte)(137)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(197)))));
            this.label6.Location = new System.Drawing.Point(603, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tìm kiếm:";
            // 
            // btnAddBillDetail
            // 
            this.btnAddBillDetail.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnAddBillDetail.Location = new System.Drawing.Point(396, 417);
            this.btnAddBillDetail.Name = "btnAddBillDetail";
            this.btnAddBillDetail.Size = new System.Drawing.Size(77, 43);
            this.btnAddBillDetail.TabIndex = 24;
            this.btnAddBillDetail.Text = ">";
            this.btnAddBillDetail.UseVisualStyleBackColor = true;
            this.btnAddBillDetail.Click += new System.EventHandler(this.btnAddBillDetail_Click);
            // 
            // btnRemoveBillDetail
            // 
            this.btnRemoveBillDetail.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnRemoveBillDetail.Location = new System.Drawing.Point(396, 466);
            this.btnRemoveBillDetail.Name = "btnRemoveBillDetail";
            this.btnRemoveBillDetail.Size = new System.Drawing.Size(77, 43);
            this.btnRemoveBillDetail.TabIndex = 24;
            this.btnRemoveBillDetail.Text = "<";
            this.btnRemoveBillDetail.UseVisualStyleBackColor = true;
            this.btnRemoveBillDetail.Click += new System.EventHandler(this.btnRemoveBillDetail_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnRemoveAll.Location = new System.Drawing.Point(396, 515);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(77, 43);
            this.btnRemoveAll.TabIndex = 24;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // dtDateFind
            // 
            // 
            // 
            // 
            this.dtDateFind.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtDateFind.ButtonDropDown.Visible = true;
            this.dtDateFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDateFind.ForeColor = System.Drawing.Color.Black;
            this.dtDateFind.Location = new System.Drawing.Point(701, 24);
            // 
            // 
            // 
            this.dtDateFind.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            this.dtDateFind.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtDateFind.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtDateFind.MonthCalendar.DisplayMonth = new System.DateTime(2024, 6, 1, 0, 0, 0, 0);
            this.dtDateFind.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtDateFind.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtDateFind.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtDateFind.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtDateFind.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtDateFind.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtDateFind.MonthCalendar.TodayButtonVisible = true;
            this.dtDateFind.Name = "dtDateFind";
            this.dtDateFind.Size = new System.Drawing.Size(119, 23);
            this.dtDateFind.TabIndex = 14;
            this.dtDateFind.Value = new System.DateTime(2024, 6, 4, 14, 32, 59, 0);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(824, 22);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 25);
            this.btnFind.TabIndex = 21;
            this.btnFind.Text = "Tìm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // cbIdCustomer
            // 
            this.cbIdCustomer.FormattingEnabled = true;
            this.cbIdCustomer.Location = new System.Drawing.Point(130, 52);
            this.cbIdCustomer.Name = "cbIdCustomer";
            this.cbIdCustomer.Size = new System.Drawing.Size(113, 21);
            this.cbIdCustomer.TabIndex = 25;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDelete.Location = new System.Drawing.Point(398, 185);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 31);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Xóa HD";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDeleteBill_Click);
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnReload.Location = new System.Drawing.Point(398, 260);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 31);
            this.btnReload.TabIndex = 21;
            this.btnReload.Text = "Làm mới";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // dtCheckOut
            // 
            // 
            // 
            // 
            this.dtCheckOut.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtCheckOut.ButtonDropDown.Visible = true;
            this.dtCheckOut.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtCheckOut.ForeColor = System.Drawing.Color.Black;
            this.dtCheckOut.Location = new System.Drawing.Point(365, 77);
            // 
            // 
            // 
            this.dtCheckOut.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            this.dtCheckOut.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtCheckOut.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtCheckOut.MonthCalendar.DisplayMonth = new System.DateTime(2024, 6, 1, 0, 0, 0, 0);
            this.dtCheckOut.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtCheckOut.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtCheckOut.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtCheckOut.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtCheckOut.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtCheckOut.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtCheckOut.MonthCalendar.TodayButtonVisible = true;
            this.dtCheckOut.Name = "dtCheckOut";
            this.dtCheckOut.Size = new System.Drawing.Size(104, 23);
            this.dtCheckOut.TabIndex = 14;
            this.dtCheckOut.Value = new System.DateTime(2024, 6, 4, 14, 32, 59, 0);
            // 
            // nmQuantity
            // 
            this.nmQuantity.Location = new System.Drawing.Point(398, 391);
            this.nmQuantity.Name = "nmQuantity";
            this.nmQuantity.Size = new System.Drawing.Size(71, 20);
            this.nmQuantity.TabIndex = 26;
            this.nmQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // formNewBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 579);
            this.Controls.Add(this.nmQuantity);
            this.Controls.Add(this.cbIdCustomer);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveBillDetail);
            this.Controls.Add(this.btnAddBillDetail);
            this.Controls.Add(this.cbUpdateDateSell);
            this.Controls.Add(this.dataGridViewBillDetails);
            this.Controls.Add(this.dataGridViewBills);
            this.Controls.Add(this.dataGridViewBooks);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnViewBill);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbIdBill);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtDateFind);
            this.Controls.Add(this.dtCheckOut);
            this.Controls.Add(this.dtCheckIn);
            this.Controls.Add(this.label4);
            this.Name = "formNewBill";
            this.Text = "formNewBill";
            this.Load += new System.EventHandler(this.formNewBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtCheckIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBillDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbIdBill;
        private System.Windows.Forms.Label label5;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtCheckIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnViewBill;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.DataGridView dataGridViewBills;
        private System.Windows.Forms.DataGridView dataGridViewBillDetails;
        private System.Windows.Forms.CheckBox cbUpdateDateSell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddBillDetail;
        private System.Windows.Forms.Button btnRemoveBillDetail;
        private System.Windows.Forms.Button btnRemoveAll;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtDateFind;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ComboBox cbIdCustomer;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReload;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtCheckOut;
        private System.Windows.Forms.NumericUpDown nmQuantity;
    }
}