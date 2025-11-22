using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class HeThong
    {
        private FileXml fileXml = new FileXml();

        // Tạo file XML cho tất cả các bảng
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

        //  Đồng bộ dữ liệu từ file XML về SQL Server cho một bảng
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

        //  Đồng bộ toàn bộ dữ liệu từ file XML về SQL Server
        public void CapNhapSQL()
        {
            try
            {
                //  Xóa dữ liệu theo ĐÚNG THỨ TỰ (từ con lên cha)
                fileXml.InsertOrUpdateSQL("DELETE FROM HUYMON");
                fileXml.InsertOrUpdateSQL("DELETE FROM CHITIETODER");
                fileXml.InsertOrUpdateSQL("DELETE FROM ODER");
                fileXml.InsertOrUpdateSQL("DELETE FROM BAN");
                fileXml.InsertOrUpdateSQL("DELETE FROM SANPHAM");
                fileXml.InsertOrUpdateSQL("DELETE FROM NHANVIEN");
                fileXml.InsertOrUpdateSQL("DELETE FROM TAIKHOAN");
                fileXml.InsertOrUpdateSQL("DELETE FROM TANG");
                fileXml.InsertOrUpdateSQL("DELETE FROM DANHMUC");

                //  Reset ID tự động tăng (IDENTITY) về 0 (để bản ghi tiếp theo là 1)
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('HUYMON', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('CHITIETODER', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('ODER', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('BAN', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('SANPHAM', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('NHANVIEN', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('TAIKHOAN', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('TANG', RESEED, 0)");
                fileXml.InsertOrUpdateSQL("DBCC CHECKIDENT ('DANHMUC', RESEED, 0)");

                // Đồng bộ dữ liệu bằng SqlBulkCopy
                fileXml.DongBoSQL("TAIKHOAN", "TAIKHOAN.xml");
                fileXml.DongBoSQL("NHANVIEN", "NHANVIEN.xml");
                fileXml.DongBoSQL("DANHMUC", "DANHMUC.xml");
                fileXml.DongBoSQL("SANPHAM", "SANPHAM.xml");
                fileXml.DongBoSQL("TANG", "TANG.xml");
                fileXml.DongBoSQL("BAN", "BAN.xml");
                fileXml.DongBoSQL("ODER", "ODER.xml");
                fileXml.DongBoSQL("CHITIETODER", "CHITIETODER.xml");
                fileXml.DongBoSQL("HUYMON", "HUYMON.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đồng bộ dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public BaoCaoDoanhThuNgay LayBaoCaoDoanhThu(DateTime ngay)
        {
            var baoCao = new BaoCaoDoanhThuNgay();
            try
            {
                DataTable tbOrder = fileXml.HienThi("ODER.xml");
                DataTable tbChiTiet = fileXml.HienThi("CHITIETODER.xml");
                DataTable tbSanPham = fileXml.HienThi("SANPHAM.xml");
                DataTable tbDanhMuc = fileXml.HienThi("DANHMUC.xml");

                DateTime ngayBaoCao = ngay.Date;

                // Lấy danh sách các Order đã thanh toán trong ngày
                var cacOrderThanhToan = tbOrder.AsEnumerable()
                    .Where(r => r["TrangThai"].ToString() == "Đã thanh toán"
                             && r["ThoiGianThanhToan"] != DBNull.Value
                             && ((DateTime)r["ThoiGianThanhToan"]).Date == ngayBaoCao);

                //  Tính Tổng Doanh Thu 
                baoCao.TongDoanhThu = cacOrderThanhToan
                    .Sum(r => Convert.ToDecimal(r["TongTien"]));

                // Lấy ID của các order đã thanh toán để tìm chi tiết món
                var dsMaOrderThanhToan = cacOrderThanhToan
                    .Select(r => r["MaOder"].ToString())
                    .ToHashSet(); 


                // Lọc ChiTietOder dựa trên các MaOder đã thanh toán
                var queryChiTiet = tbChiTiet.AsEnumerable()
                    .Where(ct => dsMaOrderThanhToan.Contains(ct["MaOder"].ToString()));

                var querySanPham = tbSanPham.AsEnumerable();
                var queryDanhMuc = tbDanhMuc.AsEnumerable();

                // Join ChiTiet với SanPham để lấy MaDanhMuc
                var chiTietCoDanhMuc = queryChiTiet.Join(
                    querySanPham,
                    ct => ct["MaSanPham"].ToString(), // Key từ ChiTietOder
                    sp => sp["MaSanPham"].ToString(), // Key từ SanPham
                    (ct, sp) => new
                    {
                        SoLuong = Convert.ToInt32(ct["SoLuong"]),
                        MaDanhMuc = sp["MaDanhMuc"].ToString()
                    }
                );

                // Join kết quả trên với DanhMuc để lấy TenDanhMuc
                var chiTietFinal = chiTietCoDanhMuc.Join(
                    queryDanhMuc,
                    ct_sp => ct_sp.MaDanhMuc, // Key từ kết quả join trước
                    dm => dm["MaDanhMuc"].ToString(), // Key từ DanhMuc
                    (ct_sp, dm) => new
                    {
                        SoLuong = ct_sp.SoLuong,
                        TenDanhMuc = dm["TenDanhMuc"].ToString()
                    }
                ).ToList(); // Chuyển sang List để dùng nhiều lần

                // Tính Tổng Số Món
                baoCao.TongSoMon = chiTietFinal.Sum(d => d.SoLuong);

                // Thống kê số món theo từng danh mục
                baoCao.ThongKeTheoDanhMuc = chiTietFinal
                    .GroupBy(d => d.TenDanhMuc) // Nhóm theo tên danh mục
                    .Select(g => new ThongKeDanhMuc
                    {
                        TenDanhMuc = g.Key,
                        SoLuongMon = g.Sum(item => item.SoLuong) // Tính tổng số lượng trong nhóm
                    })
                    .OrderBy(dm => dm.TenDanhMuc) // Sắp xếp cho đẹp
                    .ToList();

                return baoCao;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy báo cáo doanh thu: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return new BaoCaoDoanhThuNgay();
            }
        }

        public BaoCaoDoanhThuNgay DoanhThuNgayHienTai()
        {
            return LayBaoCaoDoanhThu(DateTime.Today);
        }
    }
}