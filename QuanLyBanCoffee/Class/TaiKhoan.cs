using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCoffee.Class
{
    class TaiKhoan
    {
        FileXml fileXml = new FileXml();

        public int ThemTaiKhoan(string tenDangNhap, string matKhau, string loaiTaiKhoan, bool trangThai)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable dt = fileXml.HienThi(duongDan);
                // Kiểm tra tenDangNhap đã tồn tại chưa
                var existingAccount = dt.AsEnumerable()
                    .FirstOrDefault(r => r.Field<string>("TenDangNhap").Equals(tenDangNhap, StringComparison.OrdinalIgnoreCase));
                if (existingAccount != null)
                {
                    return -1;
                }
                // Tìm mã tài khoản lớn nhất hiện có
                int maxMaTaiKhoan = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int maTaiKhoan = Convert.ToInt32(row["MaTaiKhoan"]);
                    if (maTaiKhoan > maxMaTaiKhoan)
                    {
                        maxMaTaiKhoan = maTaiKhoan;
                    }
                }
                // Tạo mã tài khoản mới
                int maTaiKhoanMoi = maxMaTaiKhoan + 1;
                // Thêm dòng mới vào DataTable
                DataRow newRow = dt.NewRow();
                newRow["MaTaiKhoan"] = maTaiKhoanMoi;
                newRow["TenDangNhap"] = tenDangNhap;
                newRow["MatKhau"] = matKhau;
                newRow["LoaiTaiKhoan"] = loaiTaiKhoan;
                newRow["TrangThai"] = trangThai;
                dt.Rows.Add(newRow);
                // Lưu lại vào file XML
                fileXml.Luu(duongDan, dt);
                return maTaiKhoanMoi;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi thêm tài khoản: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -2;
            }
        }

        public void XoaTaiKhoan(int maTaiKhoan)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable dt = fileXml.HienThi(duongDan);
                DataRow rowToDelete = dt.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("MaTaiKhoan") == maTaiKhoan);
                if (rowToDelete != null)
                {
                    dt.Rows.Remove(rowToDelete);
                    fileXml.Luu(duongDan, dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void DoiMatKhau(int maTaiKhoan, string matKhauMoi)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable dt = fileXml.HienThi(duongDan);
                DataRow rowToUpdate = dt.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("MaTaiKhoan") == maTaiKhoan);
                if (rowToUpdate != null)
                {
                    rowToUpdate["MatKhau"] = matKhauMoi;
                    fileXml.Luu(duongDan, dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void CapNhatTaiKhoan(int maTaiKhoan, string loaiTaiKhoan, bool trangThai)
        {
            try
            {
                string duongDan = "TAIKHOAN.xml";
                DataTable dt = fileXml.HienThi(duongDan);
                DataRow rowToUpdate = dt.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("MaTaiKhoan") == maTaiKhoan);
                if (rowToUpdate != null)
                {
                    rowToUpdate["LoaiTaiKhoan"] = loaiTaiKhoan;
                    rowToUpdate["TrangThai"] = trangThai;
                    fileXml.Luu(duongDan, dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public string TimMatKhauTheoMaNhanVien(int maNhanVien)
        {
            try
            {
                string duongDanNhanVien = "NHANVIEN.xml";
                DataTable dtNhanVien = fileXml.HienThi(duongDanNhanVien);
                DataRow nhanVienRow = dtNhanVien.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("MaNhanVien") == maNhanVien);
                if (nhanVienRow != null)
                {
                    int maTaiKhoan = Convert.ToInt32(nhanVienRow["MaTaiKhoan"]);
                    string duongDanTaiKhoan = "TAIKHOAN.xml";
                    DataTable dtTaiKhoan = fileXml.HienThi(duongDanTaiKhoan);
                    DataRow taiKhoanRow = dtTaiKhoan.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("MaTaiKhoan") == maTaiKhoan);
                    if (taiKhoanRow != null)
                    {
                        return taiKhoanRow.Field<string>("MatKhau");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi tìm mật khẩu theo mã nhân viên: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
