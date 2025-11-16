using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    partial class frmOrder
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


        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpDSMon = new System.Windows.Forms.FlowLayoutPanel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnKhac = new System.Windows.Forms.Button();
            this.btnMatcha = new System.Windows.Forms.Button();
            this.btnSinhTo = new System.Windows.Forms.Button();
            this.btnCaPhe = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lbTongSoMon = new System.Windows.Forms.Label();
            this.dgvDSMon = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSMon)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1394, 582);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flpDSMon);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Controls.Add(this.btnKhac);
            this.panel1.Controls.Add(this.btnMatcha);
            this.panel1.Controls.Add(this.btnSinhTo);
            this.panel1.Controls.Add(this.btnCaPhe);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 578);
            this.panel1.TabIndex = 0;
            // 
            // flpDSMon
            // 
            this.flpDSMon.AutoScroll = true;
            this.flpDSMon.BackColor = System.Drawing.Color.Yellow;
            this.flpDSMon.Location = new System.Drawing.Point(3, 119);
            this.flpDSMon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpDSMon.Name = "flpDSMon";
            this.flpDSMon.Size = new System.Drawing.Size(784, 459);
            this.flpDSMon.TabIndex = 2;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(39, 69);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(341, 30);
            this.txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(406, 66);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(105, 34);
            this.btnTimKiem.TabIndex = 0;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // btnKhac
            // 
            this.btnKhac.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhac.Location = new System.Drawing.Point(602, 9);
            this.btnKhac.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKhac.Name = "btnKhac";
            this.btnKhac.Size = new System.Drawing.Size(105, 40);
            this.btnKhac.TabIndex = 0;
            this.btnKhac.Text = "Khác";
            this.btnKhac.UseVisualStyleBackColor = true;
            this.btnKhac.Click += new System.EventHandler(this.btnKhac_Click);
            // 
            // btnMatcha
            // 
            this.btnMatcha.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMatcha.Location = new System.Drawing.Point(427, 9);
            this.btnMatcha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMatcha.Name = "btnMatcha";
            this.btnMatcha.Size = new System.Drawing.Size(105, 40);
            this.btnMatcha.TabIndex = 0;
            this.btnMatcha.Text = "Matcha";
            this.btnMatcha.UseVisualStyleBackColor = true;
            this.btnMatcha.Click += new System.EventHandler(this.btnMatcha_Click);
            // 
            // btnSinhTo
            // 
            this.btnSinhTo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSinhTo.Location = new System.Drawing.Point(251, 9);
            this.btnSinhTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSinhTo.Name = "btnSinhTo";
            this.btnSinhTo.Size = new System.Drawing.Size(105, 40);
            this.btnSinhTo.TabIndex = 0;
            this.btnSinhTo.Text = "Sinh tố";
            this.btnSinhTo.UseVisualStyleBackColor = true;
            this.btnSinhTo.Click += new System.EventHandler(this.btnSinhTo_Click);
            // 
            // btnCaPhe
            // 
            this.btnCaPhe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaPhe.Location = new System.Drawing.Point(81, 9);
            this.btnCaPhe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCaPhe.Name = "btnCaPhe";
            this.btnCaPhe.Size = new System.Drawing.Size(105, 40);
            this.btnCaPhe.TabIndex = 0;
            this.btnCaPhe.Text = "Cà phê";
            this.btnCaPhe.UseVisualStyleBackColor = true;
            this.btnCaPhe.Click += new System.EventHandler(this.btnCaPhe_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dgvDSMon);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(797, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(594, 578);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLuu);
            this.panel3.Controls.Add(this.btnThoat);
            this.panel3.Controls.Add(this.lbTongSoMon);
            this.panel3.Location = new System.Drawing.Point(3, 401);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(588, 175);
            this.panel3.TabIndex = 2;
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(109, 95);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(166, 50);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(348, 95);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(166, 50);
            this.btnThoat.TabIndex = 1;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lbTongSoMon
            // 
            this.lbTongSoMon.AutoSize = true;
            this.lbTongSoMon.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongSoMon.Location = new System.Drawing.Point(34, 28);
            this.lbTongSoMon.Name = "lbTongSoMon";
            this.lbTongSoMon.Size = new System.Drawing.Size(0, 31);
            this.lbTongSoMon.TabIndex = 0;
            // 
            // dgvDSMon
            // 
            this.dgvDSMon.BackgroundColor = System.Drawing.Color.White;
            this.dgvDSMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSMon.Location = new System.Drawing.Point(3, 2);
            this.dgvDSMon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDSMon.Name = "dgvDSMon";
            this.dgvDSMon.RowHeadersWidth = 51;
            this.dgvDSMon.Size = new System.Drawing.Size(588, 395);
            this.dgvDSMon.TabIndex = 1;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 582);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmOrder";
            this.Text = "frmOrder";
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSMon)).EndInit();
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private Button btnKhac;
        private Button btnMatcha;
        private Button btnSinhTo;
        private Button btnCaPhe;
        private FlowLayoutPanel flpDSMon;
        private DataGridView dgvDSMon;
        private Panel panel2;
        private Panel panel3;
        private Button btnLuu;
        private Button btnThoat;
        private Label lbTongSoMon;
    }
}