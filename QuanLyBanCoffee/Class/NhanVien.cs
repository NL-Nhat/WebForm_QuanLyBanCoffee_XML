using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return null; // Không tìm thấy nhân viên
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy tên nhân viên theo mã: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
