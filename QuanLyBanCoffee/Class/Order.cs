using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class Order
    {
        private FileXml fileXml = new FileXml();

        public DataTable LoadDanhSachMonChuaThanhToanTheoBan(int maBan)
        {
            try
            {
                DataTable dtOrder = fileXml.HienThi("ODER.xml");
                DataTable dtChiTietOrder = fileXml.HienThi("CHITIETODER.xml");

                int maOrder = TimMaOrderChuaThanhToanTheoBan(maBan);

                DataTable resultTable = new DataTable("DanhSachMon");
                resultTable.Columns.Add("MaCTOder", typeof(int));
                resultTable.Columns.Add("MaOder", typeof(int));
                resultTable.Columns.Add("MaSanPham", typeof(int));
                resultTable.Columns.Add("SoLuong", typeof(int));
                resultTable.Columns.Add("DonGia", typeof(decimal));
                resultTable.Columns.Add("ChietKhau", typeof(float)); // đổi sang float
                resultTable.Columns.Add("ThoiGianBatDau", typeof(DateTime));

                if (maOrder == 0)
                    return resultTable;

                DataRow[] orderRows = dtOrder.Select($"MaOder = {maOrder}");
                if (orderRows.Length == 0)
                    return resultTable;

                DataRow orderRow = orderRows[0];
                float chietKhau = 0f;
                if (orderRow.Table.Columns.Contains("ChietKhau"))
                    chietKhau = (float)Math.Round(Convert.ToSingle(orderRow["ChietKhau"]), 1); // chỉ 1 số thập phân

                DateTime thoiGianBatDau = Convert.ToDateTime(orderRow["ThoiGianBatDau"]);

                DataRow[] chiTietRows = dtChiTietOrder.Select($"MaOder = {maOrder}");
                foreach (DataRow chiTietRow in chiTietRows)
                {
                    DataRow newRow = resultTable.NewRow();
                    newRow["MaCTOder"] = Convert.ToInt32(chiTietRow["MaCTOder"]);
                    newRow["MaOder"] = Convert.ToInt32(chiTietRow["MaOder"]);
                    newRow["MaSanPham"] = Convert.ToInt32(chiTietRow["MaSanPham"]);
                    newRow["SoLuong"] = Convert.ToInt32(chiTietRow["SoLuong"]);
                    newRow["DonGia"] = Convert.ToDecimal(chiTietRow["DonGia"]);
                    newRow["ChietKhau"] = chietKhau;
                    newRow["ThoiGianBatDau"] = thoiGianBatDau;
                    resultTable.Rows.Add(newRow);
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách món chưa thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public int TimMaOrderChuaThanhToanTheoBan(int maBan)
        {
            try
            {
                DataTable table = fileXml.HienThi("ODER.xml");

                foreach (DataRow row in table.Rows)
                {
                    int maBanTrongFile = Convert.ToInt32(row["MaBan"]);
                    string trangThai = row["TrangThai"].ToString();

                    if (maBanTrongFile == maBan && trangThai.Equals("Chưa thanh toán", StringComparison.OrdinalIgnoreCase))
                        return Convert.ToInt32(row["MaOder"]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm mã Order chưa thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public DataTable TimMaCTOrderTheoMaOrder(int maOrder)
        {
            try
            {
                DataTable table = fileXml.HienThi("CHITIETODER.xml");
                DataTable resultTable = table.Clone();

                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaOder"]) == maOrder)
                        resultTable.ImportRow(row);
                }
                return resultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm chi tiết Order theo mã Order: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public int LayMaOrderTiepTheo()
        {
            try
            {
                DataTable table = fileXml.HienThi("ODER.xml");
                int maxMaOrder = 0;
                foreach (DataRow row in table.Rows)
                {
                    int maOrder = Convert.ToInt32(row["MaOder"]);
                    if (maOrder > maxMaOrder)
                        maxMaOrder = maOrder;
                }
                return maxMaOrder + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy mã Order tiếp theo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        public int LayMaCTOrderTiepTheo()
        {
            try
            {
                DataTable table = fileXml.HienThi("CHITIETODER.xml");
                int maxMaCTOrder = 0;
                foreach (DataRow row in table.Rows)
                {
                    int maCTOrder = Convert.ToInt32(row["MaCTOder"]);
                    if (maCTOrder > maxMaCTOrder)
                        maxMaCTOrder = maCTOrder;
                }
                return maxMaCTOrder + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy mã Chi Tiết Order tiếp theo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        public void ThemOrderMoi(int maOrder, int maBan, int maNhanVien, decimal tongTien, DateTime thoiGianBatDau, string trangThai, float chietKhau = 0.0f)
        {
            try
            {
                DataTable table = fileXml.HienThi("ODER.xml");
                DataRow newRow = table.NewRow();
                newRow["MaOder"] = maOrder;
                newRow["MaBan"] = maBan;
                newRow["MaNhanVien"] = maNhanVien;
                newRow["ThoiGianBatDau"] = thoiGianBatDau;
                newRow["ChietKhau"] = Math.Round(chietKhau, 1); // giữ 1 chữ số sau dấu phẩy
                newRow["TongTien"] = tongTien;
                newRow["TrangThai"] = trangThai;
                table.Rows.Add(newRow);
                fileXml.Luu("ODER.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm Order mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ThemChiTietOrderMoi(int maCTOder, int maOder, int maSanPham, int soLuong, decimal donGia)
        {
            try
            {
                DataTable table = fileXml.HienThi("CHITIETODER.xml");
                DataRow newRow = table.NewRow();
                newRow["MaCTOder"] = maCTOder;
                newRow["MaOder"] = maOder;
                newRow["MaSanPham"] = maSanPham;
                newRow["SoLuong"] = soLuong;
                newRow["DonGia"] = donGia;
                table.Rows.Add(newRow);
                fileXml.Luu("CHITIETODER.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm Chi Tiết Order mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CapNhatOrder(int maOrder, decimal tongTien)
        {
            try
            {
                DataTable table = fileXml.HienThi("ODER.xml");
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaOder"]) == maOrder)
                    {
                        row["TongTien"] = tongTien;
                        break;
                    }
                }
                fileXml.Luu("ODER.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật Order: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CapNhatChiTietOrder(int maOder, int maSanPham, int soLuong)
        {
            try
            {
                DataTable table = fileXml.HienThi("CHITIETODER.xml");
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaOder"]) == maOder && Convert.ToInt32(row["MaSanPham"]) == maSanPham)
                    {
                        row["SoLuong"] = soLuong;
                        break;
                    }
                }
                fileXml.Luu("CHITIETODER.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật Chi Tiết Order: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CapNhatThanhToan(int maOrder, string trangthai, float chietKhau)
        {
            try
            {
                DataTable table = fileXml.HienThi("ODER.xml");
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["MaOder"]) == maOrder)
                    {
                        row["TrangThai"] = trangthai;
                        row["ChietKhau"] = Math.Round(chietKhau, 1); // giữ 1 chữ số sau dấu phẩy
                        row["ThoiGianThanhToan"] = DateTime.Now;
                        break;
                    }
                }
                fileXml.Luu("ODER.xml", table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable LayDonHangDaThanhToan()
        {
            try
            {
                // Load bảng XML
                DataTable tbOrder = fileXml.HienThi("ODER.xml");
                DataTable tbBan = fileXml.HienThi("BAN.xml");
                DataTable tbTang = fileXml.HienThi("TANG.xml");
                DataTable tbCT = fileXml.HienThi("CHITIETODER.xml");

                // Tạo bảng kết quả
                DataTable result = new DataTable("DonHangDaThanhToan");
                result.Columns.Add("MaOrder", typeof(int));
                result.Columns.Add("TenBan", typeof(string));
                result.Columns.Add("TenTang", typeof(string));
                result.Columns.Add("TongTien", typeof(decimal));
                result.Columns.Add("SoLuongMon", typeof(int));
                result.Columns.Add("ThoiGianThanhToan", typeof(DateTime));
                result.Columns.Add("ChietKhau", typeof(float));

                // Ngày hiện tại
                DateTime today = DateTime.Today;

                // Lấy danh sách order đã thanh toán và trong ngày hôm nay
                var rowsOrder = tbOrder.AsEnumerable()
                                       .Where(r => r["TrangThai"].ToString() == "Đã thanh toán"
                                                && r["ThoiGianThanhToan"] != DBNull.Value
                                                && ((DateTime)r["ThoiGianThanhToan"]).Date == today);

                foreach (DataRow o in rowsOrder)
                {
                    int maOrder = Convert.ToInt32(o["MaOder"]);
                    int maBan = Convert.ToInt32(o["MaBan"]);

                    // Lấy thông tin bàn
                    DataRow ban = tbBan.AsEnumerable()
                                       .FirstOrDefault(b => Convert.ToInt32(b["MaBan"]) == maBan);
                    if (ban == null) continue;

                    string tenBan = ban["TenBan"].ToString();
                    int maTang = Convert.ToInt32(ban["MaTang"]);

                    // Lấy tên tầng
                    DataRow tang = tbTang.AsEnumerable()
                                         .FirstOrDefault(t => Convert.ToInt32(t["MaTang"]) == maTang);
                    string tenTang = tang?["TenTang"].ToString() ?? "";

                    // Lấy tổng số lượng món từ CHITIETODER
                    int soLuongMon = tbCT.AsEnumerable()
                                         .Where(ct => Convert.ToInt32(ct["MaOder"]) == maOrder)
                                         .Sum(ct => Convert.ToInt32(ct["SoLuong"]));

                    result.Rows.Add(
                        maOrder,
                        tenBan,
                        tenTang,
                        Convert.ToDecimal(o["TongTien"]),
                        soLuongMon,
                        Convert.ToDateTime(o["ThoiGianThanhToan"]),
                        Convert.ToSingle(o["ChietKhau"])
                    );
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy đơn hàng đã thanh toán: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
