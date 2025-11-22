using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class NhanVien
    {
        FileXml fileXml = new FileXml();
        TaiKhoan taiKhoan = new TaiKhoan();

        public string TimTenNhanVienTheoMa(int maNV)
        {
            try
            {
                string duongDan = "NHANVIEN.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaNhanVien"]) == maNV)
                    {
                        return row["TenNhanVien"].ToString();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy tên nhân viên theo mã: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable LayDanhSachNhanVien()
        {
            try
            {
                DataTable dtNhanVien = fileXml.HienThi("NHANVIEN.xml");
                DataTable dtTaiKhoan = fileXml.HienThi("TAIKHOAN.xml");

                if (dtNhanVien == null || dtNhanVien.Rows.Count == 0) return dtNhanVien;
                if (dtTaiKhoan == null) return dtNhanVien;

                // 2. Sử dụng LINQ để Join 2 bảng lại với nhau
                var query = from nv in dtNhanVien.AsEnumerable()
                            join tk in dtTaiKhoan.AsEnumerable()
                            // Nối bảng qua cột MaTaiKhoan (chuyển về string để so sánh cho an toàn)
                            on nv["MaTaiKhoan"].ToString() equals tk["MaTaiKhoan"].ToString()
                            select new
                            {
                                MaNhanVien = Convert.ToInt32(nv["MaNhanVien"]),
                                TenNhanVien = nv["TenNhanVien"].ToString(),
                                GioiTinh = nv["GioiTinh"].ToString(),
                                NgaySinh = nv["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(nv["NgaySinh"]) : (DateTime?)null,
                                SDT = nv["SDT"].ToString(),
                                DiaChi = nv["DiaChi"].ToString(),
                                MaTaiKhoan = Convert.ToInt32(nv["MaTaiKhoan"]),
                                LoaiTaiKhoan = tk["LoaiTaiKhoan"].ToString(),
                                TrangThai = tk["TrangThai"] != DBNull.Value ? Convert.ToBoolean(tk["TrangThai"]) : false
                            };

                DataTable resultTable = new DataTable();
                resultTable.Columns.Add("MaNhanVien", typeof(int));
                resultTable.Columns.Add("TenNhanVien", typeof(string));
                resultTable.Columns.Add("GioiTinh", typeof(string));
                resultTable.Columns.Add("NgaySinh", typeof(DateTime));
                resultTable.Columns.Add("SDT", typeof(string));
                resultTable.Columns.Add("DiaChi", typeof(string));
                resultTable.Columns.Add("MaTaiKhoan", typeof(int)); 

                resultTable.Columns.Add("LoaiTaiKhoan", typeof(string));
                resultTable.Columns.Add("TrangThai", typeof(bool));

                foreach (var item in query)
                {
                    resultTable.Rows.Add(
                        item.MaNhanVien,
                        item.TenNhanVien,
                        item.GioiTinh,
                        item.NgaySinh.HasValue ? (object)item.NgaySinh.Value : DBNull.Value,
                        item.SDT,
                        item.DiaChi,
                        item.MaTaiKhoan,
                        item.LoaiTaiKhoan,
                        item.TrangThai
                    );
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách nhân viên (kèm tài khoản): {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void ThemNhanVienVaTaiKhoanMoi(string tenNV, string gioiTinh, DateTime ngaySinh, string sdt, string diaChi, string tenDangNhap, string matKhau, string loaiTaiKhoan, bool trangThai)
        {
            try
            {
                int maTaiKhoan = taiKhoan.ThemTaiKhoan(tenDangNhap, matKhau, loaiTaiKhoan, trangThai);

                if (maTaiKhoan == -1)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (maTaiKhoan == -2)
                {
                    MessageBox.Show("Lỗi khi tạo tài khoản mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Thêm nhân viên mới vào XML
                string duongDanNhanVien = "NHANVIEN.xml";
                DataTable dtNhanVien = fileXml.HienThi(duongDanNhanVien);
                
                // Tìm mã tài khoản lớn nhất hiện có
                int nextId = 0;
                foreach (DataRow row in dtNhanVien.Rows)
                {
                    int maNhanVien = Convert.ToInt32(row["MaNhanVien"]);

                    if (maNhanVien > nextId)
                    {
                        nextId = maTaiKhoan;
                    }
                }

                // Tạo mã tài khoản mới
                int maTaiKhoanMoi = nextId + 1;

                DataRow newRow = dtNhanVien.NewRow();
                newRow["MaNhanVien"] = nextId;
                newRow["TenNhanVien"] = tenNV;
                newRow["GioiTinh"] = gioiTinh;
                newRow["NgaySinh"] = ngaySinh.ToString("yyyy-MM-dd"); 
                newRow["SDT"] = sdt;
                newRow["DiaChi"] = diaChi;
                newRow["MaTaiKhoan"] = maTaiKhoan;

                dtNhanVien.Rows.Add(newRow);

                fileXml.Luu(duongDanNhanVien, dtNhanVien);

                MessageBox.Show("Thêm nhân viên và tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void XoaNhanVienTheoMa(int maNV, int maTaiKhoan)
        {
            try
            {
                string duongDanNhanVien = "NHANVIEN.xml";
                DataTable dtNhanVien = fileXml.HienThi(duongDanNhanVien);
                DataRow rowToDelete = null;
                foreach (DataRow row in dtNhanVien.Rows)
                {
                    if (Convert.ToInt32(row["MaNhanVien"]) == maNV)
                    {
                        rowToDelete = row;
                        break;
                    }
                }
                if (rowToDelete != null)
                {
                    dtNhanVien.Rows.Remove(rowToDelete);
                    fileXml.Luu(duongDanNhanVien, dtNhanVien);
                    taiKhoan.XoaTaiKhoan(maTaiKhoan);
                    MessageBox.Show("Xóa nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã đã cho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CapNhatThongTinNhanVien(int maNV, string tenNV, string gioiTinh, DateTime ngaySinh, string sdt, string diaChi, string vaiTro, bool trangThai)
        {
            try
            {
                string duongDanNhanVien = "NHANVIEN.xml";
                DataTable dtNhanVien = fileXml.HienThi(duongDanNhanVien);
                DataRow rowToUpdate = null;
                foreach (DataRow row in dtNhanVien.Rows)
                {
                    if (Convert.ToInt32(row["MaNhanVien"]) == maNV)
                    {
                        rowToUpdate = row;
                        break;
                    }
                }
                if (rowToUpdate != null)
                {
                    rowToUpdate["TenNhanVien"] = tenNV;
                    rowToUpdate["GioiTinh"] = gioiTinh;
                    rowToUpdate["NgaySinh"] = ngaySinh.ToString("yyyy-MM-dd");
                    rowToUpdate["SDT"] = sdt;
                    rowToUpdate["DiaChi"] = diaChi;
                    fileXml.Luu(duongDanNhanVien, dtNhanVien);
                    taiKhoan.CapNhatTaiKhoan(maNV, vaiTro, trangThai);
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã đã cho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
