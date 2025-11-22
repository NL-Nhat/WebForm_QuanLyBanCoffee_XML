using QuanLyBanCoffee.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI.Admin
{
    public partial class frmMainAdmin : Form
    {
        private NhanVien nhanVien = new NhanVien();
        private UC_DoUong uc_douong;

        private int manv;

        public frmMainAdmin(int maNV) : this()
        {
            this.manv = maNV;
            string tenNV = nhanVien.TimTenNhanVienTheoMa(maNV);
            lbAdmin.Text = $"Admin: {tenNV}";
            LoadUC_DoUong();
        }

        public frmMainAdmin()
        {
            InitializeComponent();
        }

        private void LoadUC_DoUong()
        {
            pn_MainContent.Controls.Clear();
            uc_douong = new UC_DoUong()
            {
                Dock = DockStyle.Fill
            };
            pn_MainContent.Controls.Add(uc_douong);
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuDoUong_Click(object sender, EventArgs e)
        {
            if (!pn_MainContent.Controls.Contains(uc_douong))
            {
                pn_MainContent.Controls.Clear();
                pn_MainContent.Controls.Add(uc_douong);
            }
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            if (pn_MainContent.Controls.Count > 0 && pn_MainContent.Controls[0] is UC_NhanVien)
            {
                return;
            }

            pn_MainContent.Controls.Clear();

            UC_NhanVien uc_nhanvien = new UC_NhanVien(manv)
            {
                Dock = DockStyle.Fill
            };

            pn_MainContent.Controls.Add(uc_nhanvien);
        }

        private void mnuBaoCao_Click(object sender, EventArgs e)
        {
            if (pn_MainContent.Controls.Count > 0 && pn_MainContent.Controls[0] is UC_BaoCao)
            {
                return;
            }

            pn_MainContent.Controls.Clear();

            UC_BaoCao uc_baocao = new UC_BaoCao()
            {
                Dock = DockStyle.Fill
            };

            pn_MainContent.Controls.Add(uc_baocao);
        }
    }
}
