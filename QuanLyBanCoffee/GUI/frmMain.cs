using QuanLyBanCoffee.Class;
using QuanLyBanCoffee.GUI;
using QuanLyBanCoffee.GUI.Admin;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanCoffee
{
    public partial class frmMain : Form
    {
        private HeThong ht = new HeThong();
        private DangNhap dn = new DangNhap();

        public frmMain()
        {
            InitializeComponent();
            this.AcceptButton = btnDangNhap; // Đặt btnDangNhap làm nút mặc định khi nhấn Enter
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            // Kiểm tra các trường có trống không
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lbError.Text = "Vui lòng nhập tên đăng nhập và mật khẩu!";
                lbError.Visible = true;
                return;
            }

            try
            {
                int maTK = dn.kiemTraDangNhap(username, password);

                if (maTK > 0) { 

                    string loaiTK = dn.layLoaiTaiKhoan(maTK);
                    int maNV = dn.layMaNhanVien(maTK);

                    if(loaiTK.Equals("Admin"))
                    {
                        frmMainAdmin frmadmin = new frmMainAdmin(maNV);
                        txtMatKhau.Clear();
                        txtUserName.Clear();
                        this.Hide();
                        frmadmin.ShowDialog();
                        this.Show();
                    }
                    else if(loaiTK.Equals("Nhân viên"))
                    {
                        frmBan frmban = new frmBan(maNV);
                        txtMatKhau.Clear();
                        txtUserName.Clear();
                        this.Hide();
                        frmban.ShowDialog();
                        this.Show();
                    }
                }
                else if (maTK == -2)
                {
                    lbError.Visible = false; // Ẩn lỗi
                    MessageBox.Show("Tài khoản này đã bị khóa. Vui lòng liên hệ quản trị viên.",
                                    "Tài khoản bị khóa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else // (maTK == -1)
                {
                    lbError.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    lbError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lbError.Text = $"Lỗi: {ex.Message}";
                lbError.Visible = true;
            }
        }
        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ht.CapNhapSQL();
            }
            catch (Exception ex)
            {
                // Hiển thị nếu có lỗi nghiêm trọng xảy ra khi đồng bộ
                MessageBox.Show("Lỗi nghiêm trọng khi đồng bộ dữ liệu lúc thoát: " + ex.Message,
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
