using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class HeThong
    {
        private FileXml fileXml = new FileXml();

        // 1. Tạo file XML cho tất cả các bảng
        public void TaoXML()
        {
            try
            {
                // Tạo file XML cho từng bảng
                fileXml.TaoXML("TAIKHOAN");
                fileXml.TaoXML("NHANVIEN");
                fileXml.TaoXML("DANHMUC");
                fileXml.TaoXML("SANPHAM");
                fileXml.TaoXML("TANG");
                fileXml.TaoXML("BAN");
                fileXml.TaoXML("ODER");
                fileXml.TaoXML("CHITIETODER");
                fileXml.TaoXML("HUYMON");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo file XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Đồng bộ dữ liệu từ file XML về SQL Server cho một bảng
        private void CapNhapTungBang(string tenBang)
        {
            try
            {
                string duongDan = $"{tenBang}.xml";
                DataTable table = fileXml.HienThi(duongDan);

                foreach (DataRow row in table.Rows)
                {
                    // Tạo câu lệnh INSERT
                    string sql = $"INSERT INTO {tenBang} (";
                    for (int j = 0; j < table.Columns.Count - 1; j++)
                    {
                        sql += table.Columns[j].ColumnName + ",";
                    }
                    sql += table.Columns[table.Columns.Count - 1].ColumnName + ") VALUES (";

                    for (int j = 0; j < table.Columns.Count - 1; j++)
                    {
                        // Thoát ký tự đặc biệt để tránh lỗi SQL Injection
                        sql += $"N'{row[j].ToString().Replace("'", "''")}',";
                    }
                    sql += $"N'{row[table.Columns.Count - 1].ToString().Replace("'", "''")}'";
                    sql += ")";

                    fileXml.InsertOrUpdateSQL(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đồng bộ bảng {tenBang}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Đồng bộ toàn bộ dữ liệu từ file XML về SQL Server
        public void CapNhapSQL()
        {
            try
            {
                // Xóa toàn bộ dữ liệu trong các bảng
                fileXml.InsertOrUpdateSQL("DELETE FROM HUYMON");
                fileXml.InsertOrUpdateSQL("DELETE FROM CHITIETODER");
                fileXml.InsertOrUpdateSQL("DELETE FROM ODER");
                fileXml.InsertOrUpdateSQL("DELETE FROM BAN");
                fileXml.InsertOrUpdateSQL("DELETE FROM TANG");
                fileXml.InsertOrUpdateSQL("DELETE FROM SANPHAM");
                fileXml.InsertOrUpdateSQL("DELETE FROM DANHMUC");
                fileXml.InsertOrUpdateSQL("DELETE FROM NHANVIEN");
                fileXml.InsertOrUpdateSQL("DELETE FROM TAIKHOAN");

                // Đồng bộ dữ liệu từ file XML về SQL Server
                CapNhapTungBang("TAIKHOAN");
                CapNhapTungBang("NHANVIEN");    
                CapNhapTungBang("DANHMUC");
                CapNhapTungBang("SANPHAM");
                CapNhapTungBang("TANG");
                CapNhapTungBang("BAN");
                CapNhapTungBang("ODER");
                CapNhapTungBang("CHITIETODER");
                CapNhapTungBang("HUYMON");

                MessageBox.Show("Đã đồng bộ tất cả dữ liệu từ XML về SQL Server thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đồng bộ dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}