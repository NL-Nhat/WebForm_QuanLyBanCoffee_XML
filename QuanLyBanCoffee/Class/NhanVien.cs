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
    }
}
