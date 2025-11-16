using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCoffee.Class
{
    class Ban
    {
        private FileXml fileXml = new FileXml();

        public int TimMaTangTheoTen(string tenTang)
        {
            try
            {
                string duongDan = "TANG.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (row["TenTang"].ToString() == tenTang)
                    {
                        return Convert.ToInt32(row["MaTang"]);
                    }
                }
                return -1; // Không tìm thấy tầng
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi tìm mã tầng theo tên: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
        }

        public int TimMaBanTheoTen(string tenBan, string tenTang)
        {
            try
            {
                int maTang = TimMaTangTheoTen(tenTang);
                string duongDan = "BAN.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (row["TenBan"].ToString() == tenBan && Convert.ToInt32(row["MaTang"]) == maTang)
                    {
                        return Convert.ToInt32(row["MaBan"]);
                    }
                }
                return -1; // Không tìm thấy bàn
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi tìm mã bàn theo tên và mã tầng: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
        }

        public void CapNhatTrangThaiBan(int maBan, string trangThai)
        {
            try
            {
                string duongDan = "BAN.xml";
                DataTable table = fileXml.HienThi(duongDan);
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaBan"]) == maBan)
                    {
                        row["TrangThai"] = trangThai;
                        break;
                    }
                }
                fileXml.Luu(duongDan, table);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
