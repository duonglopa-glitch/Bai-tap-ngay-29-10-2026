namespace frmHotelManager
{
    partial class frmHotelManager
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboFilterType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurrentRoomInfo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboServices = new System.Windows.Forms.ComboBox();
            this.nudServiceQty = new System.Windows.Forms.NumericUpDown();
            this.btnOrderService = new System.Windows.Forms.Button();
            this.dgvServicesUsed = new System.Windows.Forms.DataGridView();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnCleanDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServiceQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesUsed)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lọc Loại phòng: ";
            // 
            // cboFilterType
            // 
            this.cboFilterType.FormattingEnabled = true;
            this.cboFilterType.Location = new System.Drawing.Point(137, 12);
            this.cboFilterType.Name = "cboFilterType";
            this.cboFilterType.Size = new System.Drawing.Size(234, 24);
            this.cboFilterType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(601, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trạng Thái: ";
            // 
            // cboFilterStatus
            // 
            this.cboFilterStatus.FormattingEnabled = true;
            this.cboFilterStatus.Location = new System.Drawing.Point(704, 11);
            this.cboFilterStatus.Name = "cboFilterStatus";
            this.cboFilterStatus.Size = new System.Drawing.Size(204, 24);
            this.cboFilterStatus.TabIndex = 3;
            // 
            // dgvRooms
            // 
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Location = new System.Drawing.Point(12, 61);
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.RowHeadersWidth = 51;
            this.dgvRooms.RowTemplate.Height = 24;
            this.dgvRooms.Size = new System.Drawing.Size(567, 600);
            this.dgvRooms.TabIndex = 4;
            this.dgvRooms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRooms_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurrentRoomInfo);
            this.groupBox1.Location = new System.Drawing.Point(585, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 65);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Khu vực nghiệp vụ Phòng & Dịch Vụ";
            // 
            // lblCurrentRoomInfo
            // 
            this.lblCurrentRoomInfo.AutoSize = true;
            this.lblCurrentRoomInfo.Location = new System.Drawing.Point(26, 31);
            this.lblCurrentRoomInfo.Name = "lblCurrentRoomInfo";
            this.lblCurrentRoomInfo.Size = new System.Drawing.Size(115, 16);
            this.lblCurrentRoomInfo.TabIndex = 0;
            this.lblCurrentRoomInfo.Text = "Phòng đang chọn:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPhone);
            this.groupBox2.Controls.Add(this.txtCustomerName);
            this.groupBox2.Controls.Add(this.txtCCCD);
            this.groupBox2.Controls.Add(this.btnCheckIn);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(585, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(585, 105);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin Khách hàng Check-in";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "CCCD:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(290, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Họ tên:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "SDT:";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(375, 57);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(121, 33);
            this.btnCheckIn.TabIndex = 3;
            this.btnCheckIn.Text = "Check-in (Nhận phòng)";
            this.btnCheckIn.UseVisualStyleBackColor = true;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(80, 26);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(202, 22);
            this.txtCCCD.TabIndex = 4;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(345, 29);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(202, 22);
            this.txtCustomerName.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(80, 59);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(202, 22);
            this.txtPhone.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCleanDone);
            this.groupBox3.Controls.Add(this.btnCheckOut);
            this.groupBox3.Controls.Add(this.dgvServicesUsed);
            this.groupBox3.Controls.Add(this.btnOrderService);
            this.groupBox3.Controls.Add(this.nudServiceQty);
            this.groupBox3.Controls.Add(this.cboServices);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(585, 243);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(585, 418);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dịch vụ phòng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Dịch vụ:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(323, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "SL:";
            // 
            // cboServices
            // 
            this.cboServices.FormattingEnabled = true;
            this.cboServices.Location = new System.Drawing.Point(103, 21);
            this.cboServices.Name = "cboServices";
            this.cboServices.Size = new System.Drawing.Size(194, 24);
            this.cboServices.TabIndex = 2;
            // 
            // nudServiceQty
            // 
            this.nudServiceQty.Location = new System.Drawing.Point(365, 23);
            this.nudServiceQty.Name = "nudServiceQty";
            this.nudServiceQty.Size = new System.Drawing.Size(58, 22);
            this.nudServiceQty.TabIndex = 3;
            // 
            // btnOrderService
            // 
            this.btnOrderService.Location = new System.Drawing.Point(476, 16);
            this.btnOrderService.Name = "btnOrderService";
            this.btnOrderService.Size = new System.Drawing.Size(94, 33);
            this.btnOrderService.TabIndex = 4;
            this.btnOrderService.Text = "Đặt Dịch Vụ";
            this.btnOrderService.UseVisualStyleBackColor = true;
            this.btnOrderService.Click += new System.EventHandler(this.btnOrderService_Click);
            // 
            // dgvServicesUsed
            // 
            this.dgvServicesUsed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServicesUsed.Location = new System.Drawing.Point(6, 55);
            this.dgvServicesUsed.Name = "dgvServicesUsed";
            this.dgvServicesUsed.RowHeadersWidth = 51;
            this.dgvServicesUsed.RowTemplate.Height = 24;
            this.dgvServicesUsed.Size = new System.Drawing.Size(573, 316);
            this.dgvServicesUsed.TabIndex = 5;
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(70, 377);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(172, 35);
            this.btnCheckOut.TabIndex = 6;
            this.btnCheckOut.Text = "Check-out & Xuất Hóa Đơn";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // btnCleanDone
            // 
            this.btnCleanDone.Location = new System.Drawing.Point(365, 377);
            this.btnCleanDone.Name = "btnCleanDone";
            this.btnCleanDone.Size = new System.Drawing.Size(172, 35);
            this.btnCleanDone.TabIndex = 7;
            this.btnCleanDone.Text = "Đã Dọn Dẹp Xong";
            this.btnCleanDone.UseVisualStyleBackColor = true;
            this.btnCleanDone.Click += new System.EventHandler(this.btnCleanDone_Click);
            // 
            // frmHotelManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 673);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.cboFilterStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboFilterType);
            this.Controls.Add(this.label1);
            this.Name = "frmHotelManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Phòng & Tiện Ích Khách Sạn";
            this.Load += new System.EventHandler(this.frmHotelManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServiceQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServicesUsed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboFilterType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.DataGridView dgvRooms;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCurrentRoomInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnOrderService;
        private System.Windows.Forms.NumericUpDown nudServiceQty;
        private System.Windows.Forms.ComboBox cboServices;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCleanDone;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.DataGridView dgvServicesUsed;
    }
}

