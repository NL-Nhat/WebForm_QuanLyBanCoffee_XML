using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    partial class frmBan
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoanhThu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.lbNV = new System.Windows.Forms.Label();
            this.pn_MainContent = new System.Windows.Forms.Panel();
            this.mnuDSDH = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOrder,
            this.mnuDoanhThu,
            this.mnuDSDH,
            this.mnuDangXuat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1252, 39);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuOrder
            // 
            this.mnuOrder.ForeColor = System.Drawing.Color.White;
            this.mnuOrder.Name = "mnuOrder";
            this.mnuOrder.Size = new System.Drawing.Size(80, 35);
            this.mnuOrder.Text = "Oder";
            this.mnuOrder.Click += new System.EventHandler(this.mnuOrder_Click);
            // 
            // mnuDoanhThu
            // 
            this.mnuDoanhThu.ForeColor = System.Drawing.Color.White;
            this.mnuDoanhThu.Name = "mnuDoanhThu";
            this.mnuDoanhThu.Size = new System.Drawing.Size(142, 35);
            this.mnuDoanhThu.Text = "Doanh thu";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.ForeColor = System.Drawing.Color.White;
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(139, 35);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // lbNV
            // 
            this.lbNV.AutoSize = true;
            this.lbNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNV.Location = new System.Drawing.Point(1008, 9);
            this.lbNV.Name = "lbNV";
            this.lbNV.Size = new System.Drawing.Size(92, 20);
            this.lbNV.TabIndex = 8;
            this.lbNV.Text = "Nhân viên";
            // 
            // pn_MainContent
            // 
            this.pn_MainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_MainContent.Location = new System.Drawing.Point(0, 39);
            this.pn_MainContent.Name = "pn_MainContent";
            this.pn_MainContent.Size = new System.Drawing.Size(1252, 624);
            this.pn_MainContent.TabIndex = 11;
            // 
            // mnuDSDH
            // 
            this.mnuDSDH.ForeColor = System.Drawing.Color.White;
            this.mnuDSDH.Name = "mnuDSDH";
            this.mnuDSDH.Size = new System.Drawing.Size(247, 35);
            this.mnuDSDH.Text = "Danh sách đơn hàng";
            this.mnuDSDH.Click += new System.EventHandler(this.mnuDSDH_Click);
            // 
            // frmBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1252, 663);
            this.Controls.Add(this.pn_MainContent);
            this.Controls.Add(this.lbNV);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmBan";
            this.Text = "Trang chủ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuOrder;
        private ToolStripMenuItem mnuDoanhThu;
        private ToolStripMenuItem mnuDangXuat;
        private Label lbNV;
        private Panel pn_MainContent;
        private ToolStripMenuItem mnuDSDH;
    }
}