namespace frmInventoryManager
{
    partial class frmInventoryManager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboVoucherType = new System.Windows.Forms.ComboBox();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchVatTu = new System.Windows.Forms.TextBox();
            this.dgvVatTuCatalog = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.nudSafetyThreshold = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.dgvVoucherDetails = new System.Windows.Forms.DataGridView();
            this.lblTotalVoucherAmount = new System.Windows.Forms.Label();
            this.btnSaveVoucher = new System.Windows.Forms.Button();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.btnFilterLowStock = new System.Windows.Forms.Button();
            this.btnTotalValuation = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVatTuCatalog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSafetyThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboSupplier);
            this.groupBox1.Controls.Add(this.cboVoucherType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1182, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loại phiếu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(628, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nhà cung cấp:";
            // 
            // cboVoucherType
            // 
            this.cboVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVoucherType.FormattingEnabled = true;
            this.cboVoucherType.Location = new System.Drawing.Point(169, 20);
            this.cboVoucherType.Name = "cboVoucherType";
            this.cboVoucherType.Size = new System.Drawing.Size(366, 24);
            this.cboVoucherType.TabIndex = 2;
            this.cboVoucherType.SelectedIndexChanged += new System.EventHandler(this.cboVoucherType_SelectedIndexChanged);
            // 
            // cboSupplier
            // 
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(747, 20);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(366, 24);
            this.cboSupplier.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTotalValuation);
            this.groupBox2.Controls.Add(this.btnFilterLowStock);
            this.groupBox2.Controls.Add(this.nudSafetyThreshold);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dgvVatTuCatalog);
            this.groupBox2.Controls.Add(this.txtSearchVatTu);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 607);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DANH MỤC VẬT TƯ TRA CỨU";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtUnitPrice);
            this.groupBox3.Controls.Add(this.btnSaveVoucher);
            this.groupBox3.Controls.Add(this.lblTotalVoucherAmount);
            this.groupBox3.Controls.Add(this.dgvVoucherDetails);
            this.groupBox3.Controls.Add(this.btnAddItem);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nudQuantity);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(581, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(601, 607);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CHI TIẾT PHIẾU KHO TẠM THỜI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tìm kiếm:";
            // 
            // txtSearchVatTu
            // 
            this.txtSearchVatTu.Location = new System.Drawing.Point(122, 37);
            this.txtSearchVatTu.Name = "txtSearchVatTu";
            this.txtSearchVatTu.Size = new System.Drawing.Size(312, 22);
            this.txtSearchVatTu.TabIndex = 1;
            this.txtSearchVatTu.TextChanged += new System.EventHandler(this.txtSearchVatTu_TextChanged_1);
            // 
            // dgvVatTuCatalog
            // 
            this.dgvVatTuCatalog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVatTuCatalog.Location = new System.Drawing.Point(12, 76);
            this.dgvVatTuCatalog.Name = "dgvVatTuCatalog";
            this.dgvVatTuCatalog.RowHeadersWidth = 51;
            this.dgvVatTuCatalog.RowTemplate.Height = 24;
            this.dgvVatTuCatalog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVatTuCatalog.Size = new System.Drawing.Size(557, 432);
            this.dgvVatTuCatalog.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 528);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ngưỡng lọc:";
            // 
            // nudSafetyThreshold
            // 
            this.nudSafetyThreshold.Location = new System.Drawing.Point(122, 522);
            this.nudSafetyThreshold.Name = "nudSafetyThreshold";
            this.nudSafetyThreshold.Size = new System.Drawing.Size(115, 22);
            this.nudSafetyThreshold.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số lượng:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.DecimalPlaces = 2;
            this.nudQuantity.Location = new System.Drawing.Point(125, 37);
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(89, 22);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(276, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Đơn giá:";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(487, 37);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(102, 26);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "Thêm Vào Phiếu";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // dgvVoucherDetails
            // 
            this.dgvVoucherDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoucherDetails.Location = new System.Drawing.Point(6, 76);
            this.dgvVoucherDetails.Name = "dgvVoucherDetails";
            this.dgvVoucherDetails.RowHeadersWidth = 51;
            this.dgvVoucherDetails.RowTemplate.Height = 24;
            this.dgvVoucherDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVoucherDetails.Size = new System.Drawing.Size(583, 432);
            this.dgvVoucherDetails.TabIndex = 4;
            // 
            // lblTotalVoucherAmount
            // 
            this.lblTotalVoucherAmount.AutoSize = true;
            this.lblTotalVoucherAmount.Location = new System.Drawing.Point(47, 528);
            this.lblTotalVoucherAmount.Name = "lblTotalVoucherAmount";
            this.lblTotalVoucherAmount.Size = new System.Drawing.Size(102, 16);
            this.lblTotalVoucherAmount.TabIndex = 5;
            this.lblTotalVoucherAmount.Text = "Tổng tiền phiếu:";
            // 
            // btnSaveVoucher
            // 
            this.btnSaveVoucher.BackColor = System.Drawing.Color.LightGreen;
            this.btnSaveVoucher.Location = new System.Drawing.Point(50, 560);
            this.btnSaveVoucher.Name = "btnSaveVoucher";
            this.btnSaveVoucher.Size = new System.Drawing.Size(126, 35);
            this.btnSaveVoucher.TabIndex = 6;
            this.btnSaveVoucher.Text = "Lưu Phiếu Kho";
            this.btnSaveVoucher.UseVisualStyleBackColor = false;
            this.btnSaveVoucher.Click += new System.EventHandler(this.btnSaveVoucher_Click);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(347, 37);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(122, 22);
            this.txtUnitPrice.TabIndex = 7;
            // 
            // btnFilterLowStock
            // 
            this.btnFilterLowStock.Location = new System.Drawing.Point(33, 560);
            this.btnFilterLowStock.Name = "btnFilterLowStock";
            this.btnFilterLowStock.Size = new System.Drawing.Size(184, 35);
            this.btnFilterLowStock.TabIndex = 5;
            this.btnFilterLowStock.Text = "Lọc Tồn Kho Thấp";
            this.btnFilterLowStock.UseVisualStyleBackColor = true;
            this.btnFilterLowStock.Click += new System.EventHandler(this.btnFilterLowStock_Click);
            // 
            // btnTotalValuation
            // 
            this.btnTotalValuation.Location = new System.Drawing.Point(347, 560);
            this.btnTotalValuation.Name = "btnTotalValuation";
            this.btnTotalValuation.Size = new System.Drawing.Size(188, 35);
            this.btnTotalValuation.TabIndex = 6;
            this.btnTotalValuation.Text = "Tổng Số Lượng Tồn Kho";
            this.btnTotalValuation.UseVisualStyleBackColor = true;
            this.btnTotalValuation.Click += new System.EventHandler(this.btnTotalValuation_Click);
            // 
            // frmInventoryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 673);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInventoryManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Quy Trình Xuất - Nhập Kho";
            this.Load += new System.EventHandler(this.frmInventoryManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVatTuCatalog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSafetyThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoucherDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.ComboBox cboVoucherType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSearchVatTu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nudSafetyThreshold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvVatTuCatalog;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveVoucher;
        private System.Windows.Forms.Label lblTotalVoucherAmount;
        private System.Windows.Forms.DataGridView dgvVoucherDetails;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Button btnTotalValuation;
        private System.Windows.Forms.Button btnFilterLowStock;
    }
}

