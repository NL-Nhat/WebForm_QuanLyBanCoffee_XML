namespace QuanLyBanCoffee.GUI
{
    partial class UC_DoiMatKhau
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.txtMKCu = new System.Windows.Forms.TextBox();
            this.txtMKMoi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtXNMKMoi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(458, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐỔI MẬT KHẨU";
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.Yellow;
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMatKhau.Location = new System.Drawing.Point(426, 473);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(339, 46);
            this.btnDoiMatKhau.TabIndex = 1;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // txtMKCu
            // 
            this.txtMKCu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMKCu.Location = new System.Drawing.Point(426, 190);
            this.txtMKCu.Name = "txtMKCu";
            this.txtMKCu.PasswordChar = '*';
            this.txtMKCu.Size = new System.Drawing.Size(339, 30);
            this.txtMKCu.TabIndex = 2;
            // 
            // txtMKMoi
            // 
            this.txtMKMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMKMoi.Location = new System.Drawing.Point(426, 275);
            this.txtMKMoi.Name = "txtMKMoi";
            this.txtMKMoi.PasswordChar = '*';
            this.txtMKMoi.Size = new System.Drawing.Size(339, 30);
            this.txtMKMoi.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(422, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mật khẩu mới:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(422, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Xác nhận mật khẩu mới:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(422, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mật khẩu cũ:";
            // 
            // txtXNMKMoi
            // 
            this.txtXNMKMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXNMKMoi.Location = new System.Drawing.Point(426, 359);
            this.txtXNMKMoi.Name = "txtXNMKMoi";
            this.txtXNMKMoi.PasswordChar = '*';
            this.txtXNMKMoi.Size = new System.Drawing.Size(339, 30);
            this.txtXNMKMoi.TabIndex = 6;
            // 
            // UC_DoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.txtXNMKMoi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMKMoi);
            this.Controls.Add(this.txtMKCu);
            this.Controls.Add(this.btnDoiMatKhau);
            this.Controls.Add(this.label1);
            this.Name = "UC_DoiMatKhau";
            this.Size = new System.Drawing.Size(1252, 624);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDoiMatKhau;
        private System.Windows.Forms.TextBox txtMKCu;
        private System.Windows.Forms.TextBox txtMKMoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtXNMKMoi;
    }
}
