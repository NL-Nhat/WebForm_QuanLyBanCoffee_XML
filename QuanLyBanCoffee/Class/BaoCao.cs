using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class BaoCao
    {
        FileXml fileXml = new FileXml();

        // 1. Thống kê doanh thu theo khoảng thời gian
        public DataTable LayDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dtResult = new DataTable();
            // Tạo các cột cho bảng kết quả
            dtResult.Columns.Add("Ngay", typeof(DateTime));
            dtResult.Columns.Add("SoDonHang", typeof(int));
            dtResult.Columns.Add("TongTien", typeof(decimal));

            try
            {
                DataTable dtOrder = fileXml.HienThi("ODER.xml");

                if (dtOrder.Rows.Count > 0)
                {
                    // Lọc các hóa đơn "Đã thanh toán" và nằm trong khoảng ngày
                    var query = from row in dtOrder.AsEnumerable()
                                let trangThai = row["TrangThai"].ToString()
                                let ngayTT = row["ThoiGianThanhToan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ThoiGianThanhToan"])
                                where trangThai == "Đã thanh toán"
                                      && ngayTT.Date >= tuNgay.Date
                                      && ngayTT.Date <= denNgay.Date
                                group row by ngayTT.Date into g // Nhóm theo ngày
                                select new
                                {
                                    Ngay = g.Key,
                                    SoDon = g.Count(),
                                    DoanhThu = g.Sum(r => r["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(r["TongTien"]))
                                };

                    // Đổ dữ liệu vào DataTable kết quả
                    foreach (var item in query.OrderBy(x => x.Ngay))
                    {
                        dtResult.Rows.Add(item.Ngay, item.SoDon, item.DoanhThu);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê doanh thu: " + ex.Message);
            }

            return dtResult;
        }

        // 2. Thống kê các món đã bị hủy
        public DataTable LayDanhSachHuyMon(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("TenMon", typeof(string));
            dtResult.Columns.Add("LyDo", typeof(string));
            dtResult.Columns.Add("ThoiGianHuy", typeof(DateTime));
            dtResult.Columns.Add("TenNhanVien", typeof(string)); // Người phụ trách đơn đó

            try
            {
                DataTable dtHuy = fileXml.HienThi("HUYMON.xml");
                DataTable dtSP = fileXml.HienThi("SANPHAM.xml");
                DataTable dtOrder = fileXml.HienThi("ODER.xml");
                DataTable dtNV = fileXml.HienThi("NHANVIEN.xml");

                // Join 4 bảng: HuyMon -> SanPham (lấy tên món) -> Order -> NhanVien (lấy tên NV)
                var query = from h in dtHuy.AsEnumerable()
                            join sp in dtSP.AsEnumerable() on h["MaSanPham"].ToString() equals sp["MaSanPham"].ToString()
                            join o in dtOrder.AsEnumerable() on h["MaOder"].ToString() equals o["MaOder"].ToString()
                            join nv in dtNV.AsEnumerable() on o["MaNhanVien"].ToString() equals nv["MaNhanVien"].ToString()
                            let thoiGian = h["ThoiGianHuy"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(h["ThoiGianHuy"])
                            where thoiGian.Date >= tuNgay.Date && thoiGian.Date <= denNgay.Date
                            select new
                            {
                                TenMon = sp["TenSanPham"].ToString(),
                                LyDo = h["LyDo"].ToString(),
                                ThoiGian = thoiGian,
                                NguoiOrder = nv["TenNhanVien"].ToString()
                            };

                foreach (var item in query.OrderByDescending(x => x.ThoiGian))
                {
                    dtResult.Rows.Add(item.TenMon, item.LyDo, item.ThoiGian, item.NguoiOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê hủy món: " + ex.Message);
            }

            return dtResult;
        }
    }
}