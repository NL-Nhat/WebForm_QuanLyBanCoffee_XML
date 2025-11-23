using QuanLyBanCoffee.Class;
using QuanLyBanCoffee.GUI.Admin;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class frmBan : Form
    {
        private NhanVien nhanVien = new NhanVien();

        private UC_Ban ucBan;

        private int maNV;

        //Hàm load lại form khi được gọi từ frmOrder hoặc bấm thanh toán
        public void ReloadpForm(string tenBan, int maNV, string tenTang)
        {
            this.maNV = maNV;
            string tenNV = nhanVien.TimTenNhanVienTheoMa(maNV);
            lbNV.Text = $"Nhân viên: {tenNV}";
            ucBan.ReloadpForm(tenBan, maNV, tenTang);
        }

        // tạo constructor để nhận maNV từ frmMain
        public frmBan(int maNV) : this()
        {
            this.maNV = maNV;
            string tenNV = nhanVien.TimTenNhanVienTheoMa(maNV);
            lbNV.Text = $"Nhân viên: {tenNV}";
            LoadUC_Ban();
        }

        public frmBan()
        {
            InitializeComponent();

        }

        private void LoadUC_Ban()
        {
            pn_MainContent.Controls.Clear();
            ucBan = new UC_Ban(maNV)
            {
                Dock = DockStyle.Fill
            };
            pn_MainContent.Controls.Add(ucBan);
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuOrder_Click(object sender, EventArgs e)
        {
            if (!pn_MainContent.Controls.Contains(ucBan))
            {
                pn_MainContent.Controls.Clear();
                pn_MainContent.Controls.Add(ucBan);
            }
        }

        private void mnuDSDH_Click(object sender, EventArgs e)
        {
            if (pn_MainContent.Controls.Count > 0 && pn_MainContent.Controls[0] is UC_DSDH)
            {
                return;
            }
            // Xóa hết các control hiện có trong panel
            pn_MainContent.Controls.Clear();

            // Khởi tạo UserControl mới
            UC_DSDH ucDSDH = new UC_DSDH
            {
                Dock = DockStyle.Fill  // cho UC fill toàn bộ panel
            };

            // Thêm vào panel
            pn_MainContent.Controls.Add(ucDSDH);
        }

        private void mnuDoanhThu_Click(object sender, EventArgs e)
        {
            if (pn_MainContent.Controls.Count > 0 && pn_MainContent.Controls[0] is UC_DoanhThu)
            {
                return;
            }
            pn_MainContent.Controls.Clear();

            UC_DoanhThu ucDoanhThu = new UC_DoanhThu
            {
                Dock = DockStyle.Fill  
            };

            pn_MainContent.Controls.Add(ucDoanhThu);
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (pn_MainContent.Controls.Count > 0 && pn_MainContent.Controls[0] is UC_DoiMatKhau)
            {
                return;
            }
            pn_MainContent.Controls.Clear();
            UC_DoiMatKhau ucDoiMatKhau = new UC_DoiMatKhau(maNV)
            {
                Dock = DockStyle.Fill 
            };
            pn_MainContent.Controls.Add(ucDoiMatKhau);
        }
    }
}