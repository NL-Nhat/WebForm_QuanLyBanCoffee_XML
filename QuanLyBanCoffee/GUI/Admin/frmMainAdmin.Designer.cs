namespace QuanLyBanCoffee.GUI.Admin
{
    partial class frmMainAdmin
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
            this.pn_MainContent = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuDoUong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.lbNV = new System.Windows.Forms.Label();
            this.lbAdmin = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_MainContent
            // 
            this.pn_MainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_MainContent.Location = new System.Drawing.Point(0, 39);
            this.pn_MainContent.Name = "pn_MainContent";
            this.pn_MainContent.Size = new System.Drawing.Size(1252, 624);
            this.pn_MainContent.TabIndex = 14;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoUong,
            this.mnuNhanVien,
            this.mnuBaoCao,
            this.mnuDangXuat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1252, 39);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuDoUong
            // 
            this.mnuDoUong.ForeColor = System.Drawing.Color.White;
            this.mnuDoUong.Name = "mnuDoUong";
            this.mnuDoUong.Size = new System.Drawing.Size(121, 35);
            this.mnuDoUong.Text = "Đồ uống";
            this.mnuDoUong.Click += new System.EventHandler(this.mnuDoUong_Click);
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.ForeColor = System.Drawing.Color.White;
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(137, 35);
            this.mnuNhanVien.Text = "Nhân viên";
            this.mnuNhanVien.Click += new System.EventHandler(this.mnuNhanVien_Click);
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.ForeColor = System.Drawing.Color.White;
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(112, 35);
            this.mnuBaoCao.Text = "Báo cáo";
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
            this.lbNV.TabIndex = 13;
            this.lbNV.Text = "Nhân viên";
            // 
            // lbAdmin
            // 
            this.lbAdmin.AutoSize = true;
            this.lbAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdmin.Location = new System.Drawing.Point(941, 9);
            this.lbAdmin.Name = "lbAdmin";
            this.lbAdmin.Size = new System.Drawing.Size(61, 20);
            this.lbAdmin.TabIndex = 15;
            this.lbAdmin.Text = "Admin";
            // 
            // frmMainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1252, 663);
            this.Controls.Add(this.lbAdmin);
            this.Controls.Add(this.pn_MainContent);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lbNV);
            this.Name = "frmMainAdmin";
            this.Text = "Trang chủ Admin";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pn_MainContent;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.Label lbNV;
        private System.Windows.Forms.ToolStripMenuItem mnuDoUong;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.Label lbAdmin;
    }
}