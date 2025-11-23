using QuanLyBanCoffee.Class;
using System;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class UC_DoiMatKhau : UserControl
    {

        private int maNV;

        public UC_DoiMatKhau(int maNV) : this()
        {
            this.maNV = maNV;
        }

        public UC_DoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauCu = txtMKCu.Text;
            string matKhauMoi = txtMKMoi.Text;
            string nhapLaiMatKhauMoi = txtXNMKMoi.Text;

            if(string.IsNullOrWhiteSpace(matKhauCu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(nhapLaiMatKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (matKhauMoi != nhapLaiMatKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới không khớp nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TaiKhoan taiKhoan = new TaiKhoan();
            string matKhauHienTai = taiKhoan.TimMatKhauTheoMaNhanVien(maNV);
            if(matKhauHienTai != matKhauCu)
            {
                MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(matKhauHienTai == matKhauMoi)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            taiKhoan.DoiMatKhau(maNV, matKhauMoi);
            MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMKCu.Clear();
            txtMKMoi.Clear();
            txtXNMKMoi.Clear();
        }
    }
}
