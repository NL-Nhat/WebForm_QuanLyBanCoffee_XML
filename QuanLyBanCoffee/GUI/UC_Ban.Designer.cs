namespace QuanLyBanCoffee.GUI
{
    partial class UC_Ban
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.cmbChietKhau = new System.Windows.Forms.ComboBox();
            this.flpDSBan = new System.Windows.Forms.FlowLayoutPanel();
            this.lbBanDangChon = new System.Windows.Forms.Label();
            this.lbTongTien = new System.Windows.Forms.Label();
            this.btnThemMon = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.dgvDSorder = new System.Windows.Forms.DataGridView();
            this.cmbTang = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSorder)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(637, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Chiết khấu:";
            // 
            // cmbChietKhau
            // 
            this.cmbChietKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChietKhau.FormattingEnabled = true;
            this.cmbChietKhau.IntegralHeight = false;
            this.cmbChietKhau.Items.AddRange(new object[] {
            "0%",
            "10%",
            "20%",
            "50%",
            "100%"});
            this.cmbChietKhau.Location = new System.Drawing.Point(756, 428);
            this.cmbChietKhau.Name = "cmbChietKhau";
            this.cmbChietKhau.Size = new System.Drawing.Size(166, 28);
            this.cmbChietKhau.TabIndex = 29;
            // 
            // flpDSBan
            // 
            this.flpDSBan.Location = new System.Drawing.Point(42, 96);
            this.flpDSBan.Name = "flpDSBan";
            this.flpDSBan.Size = new System.Drawing.Size(568, 499);
            this.flpDSBan.TabIndex = 28;
            // 
            // lbBanDangChon
            // 
            this.lbBanDangChon.AutoSize = true;
            this.lbBanDangChon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBanDangChon.Location = new System.Drawing.Point(442, 39);
            this.lbBanDangChon.Name = "lbBanDangChon";
            this.lbBanDangChon.Size = new System.Drawing.Size(0, 20);
            this.lbBanDangChon.TabIndex = 27;
            // 
            // lbTongTien
            // 
            this.lbTongTien.AutoSize = true;
            this.lbTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTien.ForeColor = System.Drawing.Color.Red;
            this.lbTongTien.Location = new System.Drawing.Point(636, 488);
            this.lbTongTien.Name = "lbTongTien";
            this.lbTongTien.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbTongTien.Size = new System.Drawing.Size(0, 25);
            this.lbTongTien.TabIndex = 26;
            this.lbTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnThemMon
            // 
            this.btnThemMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemMon.Location = new System.Drawing.Point(641, 544);
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Size = new System.Drawing.Size(166, 51);
            this.btnThemMon.TabIndex = 23;
            this.btnThemMon.Text = "Thêm món";
            this.btnThemMon.UseVisualStyleBackColor = true;
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(839, 544);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(166, 51);
            this.btnHuy.TabIndex = 24;
            this.btnHuy.Text = "Hủy món";
            this.btnHuy.UseVisualStyleBackColor = true;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.Location = new System.Drawing.Point(1032, 544);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(166, 51);
            this.btnThanhToan.TabIndex = 25;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // dgvDSorder
            // 
            this.dgvDSorder.BackgroundColor = System.Drawing.Color.White;
            this.dgvDSorder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSorder.Location = new System.Drawing.Point(641, 29);
            this.dgvDSorder.Name = "dgvDSorder";
            this.dgvDSorder.RowHeadersWidth = 51;
            this.dgvDSorder.RowTemplate.Height = 24;
            this.dgvDSorder.Size = new System.Drawing.Size(570, 378);
            this.dgvDSorder.TabIndex = 22;
            // 
            // cmbTang
            // 
            this.cmbTang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTang.FormattingEnabled = true;
            this.cmbTang.Location = new System.Drawing.Point(42, 29);
            this.cmbTang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbTang.Name = "cmbTang";
            this.cmbTang.Size = new System.Drawing.Size(269, 36);
            this.cmbTang.TabIndex = 21;
            // 
            // UC_Ban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbChietKhau);
            this.Controls.Add(this.flpDSBan);
            this.Controls.Add(this.lbBanDangChon);
            this.Controls.Add(this.lbTongTien);
            this.Controls.Add(this.btnThemMon);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.dgvDSorder);
            this.Controls.Add(this.cmbTang);
            this.Name = "UC_Ban";
            this.Size = new System.Drawing.Size(1252, 624);
            this.Load += new System.EventHandler(this.UC_Ban_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSorder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbChietKhau;
        private System.Windows.Forms.FlowLayoutPanel flpDSBan;
        private System.Windows.Forms.Label lbBanDangChon;
        private System.Windows.Forms.Label lbTongTien;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.DataGridView dgvDSorder;
        private System.Windows.Forms.ComboBox cmbTang;
    }
}
