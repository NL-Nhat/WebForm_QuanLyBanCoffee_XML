namespace QuanLyBanCoffee.GUI
{
    partial class UC_DSDH
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.dgvDSDH = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDH)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDSDH
            // 
            this.dgvDSDH.BackgroundColor = System.Drawing.Color.White;
            this.dgvDSDH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSDH.Location = new System.Drawing.Point(47, 85);
            this.dgvDSDH.Name = "dgvDSDH";
            this.dgvDSDH.ReadOnly = true;
            this.dgvDSDH.RowHeadersWidth = 51;
            this.dgvDSDH.RowTemplate.Height = 24;
            this.dgvDSDH.Size = new System.Drawing.Size(1160, 498);
            this.dgvDSDH.TabIndex = 0;
            // 
            // UC_DSDH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.dgvDSDH);
            this.Name = "UC_DSDH";
            this.Size = new System.Drawing.Size(1252, 624);
            this.Load += new System.EventHandler(this.UC_DSDH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSDH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDSDH;
    }
}
