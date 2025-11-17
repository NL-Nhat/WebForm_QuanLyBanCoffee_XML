using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCoffee.Class
{
    class DangNhap
    {

        private FileXml fileXml = new FileXml();

        public int kiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable table = fileXml.HienThi(duongDan);

                DataRow userRow = null;
                foreach (DataRow row in table.Rows)
                {
                    if (row["TenDangNhap"].ToString() == tenDangNhap && row["MatKhau"].ToString() == matKhau)
                    {
                        userRow = row;
                        break;
                    }
                }

                if (userRow == null)
                {
                    return -1; // -1 = Sai tên đăng nhập hoặc mật khẩu
                }

                if (Convert.ToBoolean(userRow["TrangThai"]) == false)
                {
                    return -2; // -2 = Tài khoản đã bị khóa
                }

                return Convert.ToInt32(userRow["MaTaiKhoan"]);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi kiểm tra đăng nhập: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
        }

        public string layLoaiTaiKhoan(int maTaiKhoan)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaTaiKhoan"]) == maTaiKhoan)
                    {
                        return row["LoaiTaiKhoan"].ToString();
                    }
                }
                return null; // Không tìm thấy tài khoản
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy loại tài khoản: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        public int layMaNhanVien(int maTaiKhoan)
        {
            try
            {
                string duongDan = "NHANVIEN.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaTaiKhoan"]) == maTaiKhoan)
                    {
                        return Convert.ToInt32(row["MaNhanVien"]);
                    }
                }
                return -1; // Không tìm thấy nhân viên
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy mã nhân viên: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}
