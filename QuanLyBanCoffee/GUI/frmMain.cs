using QuanLyBanCoffee.Class;
using QuanLyBanCoffee.GUI;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanCoffee
{
    public partial class frmMain : Form
    {
        private FileXml fileXml;
        private HeThong ht;
        private DangNhap dn;

        public frmMain()
        {
            InitializeComponent();
            fileXml = new FileXml();
            ht = new HeThong();
            dn = new DangNhap();
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
                        //frmAdmin adminForm = new frmAdmin(maTK, maNV);
                        //this.Hide();
                        //adminForm.ShowDialog();
                        //this.Show();
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
                else
                {
                    lbError.Text = "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản bị khóa!";
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
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Lệnh này buộc tất cả các luồng tin nhắn (message loop) phải dừng lại
            // và kết thúc toàn bộ ứng dụng, bất kể còn bao nhiêu form đang tồn tại.
            Application.Exit();
        }
    }
}
