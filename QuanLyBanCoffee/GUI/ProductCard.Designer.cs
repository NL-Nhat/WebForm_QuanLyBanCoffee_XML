namespace QuanLyBanCoffee.GUI
{
    partial class pcMon
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
            this.pcbMon = new System.Windows.Forms.PictureBox();
            this.lbTenMon = new System.Windows.Forms.Label();
            this.lbGiaMon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMon)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbMon
            // 
            this.pcbMon.Location = new System.Drawing.Point(0, 0);
            this.pcbMon.Name = "pcbMon";
            this.pcbMon.Size = new System.Drawing.Size(180, 119);
            this.pcbMon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbMon.TabIndex = 0;
            this.pcbMon.TabStop = false;
            // 
            // lbTenMon
            // 
            this.lbTenMon.AutoSize = true;
            this.lbTenMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenMon.Location = new System.Drawing.Point(3, 126);
            this.lbTenMon.Name = "lbTenMon";
            this.lbTenMon.Size = new System.Drawing.Size(105, 16);
            this.lbTenMon.TabIndex = 1;
            this.lbTenMon.Text = "Tên sản phẩm";
            // 
            // lbGiaMon
            // 
            this.lbGiaMon.AutoSize = true;
            this.lbGiaMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGiaMon.Location = new System.Drawing.Point(3, 148);
            this.lbGiaMon.Name = "lbGiaMon";
            this.lbGiaMon.Size = new System.Drawing.Size(69, 16);
            this.lbGiaMon.TabIndex = 2;
            this.lbGiaMon.Text = "Giá 12.000";
            // 
            // pcMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbGiaMon);
            this.Controls.Add(this.lbTenMon);
            this.Controls.Add(this.pcbMon);
            this.Name = "pcMon";
            this.Size = new System.Drawing.Size(180, 169);
            this.Load += new System.EventHandler(this.pcMon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbMon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbMon;
        private System.Windows.Forms.Label lbTenMon;
        private System.Windows.Forms.Label lbGiaMon;
    }
}
