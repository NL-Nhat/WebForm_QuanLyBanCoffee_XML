using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    public class HuyMon
    {
        FileXml fileXml = new FileXml();

        public void ThemHuyMon(int maOrder, int maSanPham, int soLuong, string lyDo)
        {
            try
            {
                DataTable table = fileXml.HienThi("HUYMON.xml");

                int maHuyMax = 0;
                foreach (DataRow row in table.Rows)
                {
                    int maHuy = Convert.ToInt32(row["MaHuy"]);
                    if (maHuy > maHuyMax)
                    {
                        maHuyMax = maHuy;
                    }
                }
                int maHuyMoi = maHuyMax + 1;

                DataRow newRow = table.NewRow();
                newRow["MaHuy"] = maHuyMoi;
                newRow["MaOder"] = maOrder;
                newRow["MaSanPham"] = maSanPham;
                newRow["SoLuong"] = soLuong;
                newRow["LyDo"] = lyDo;
                newRow["ThoiGianHuy"] = DateTime.Now;
                table.Rows.Add(newRow);
                fileXml.Luu("HUYMON.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hủy món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
