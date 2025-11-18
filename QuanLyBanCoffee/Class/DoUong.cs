using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    class DoUong
    {

        private FileXml fileXml = new FileXml();

        public DataTable LayDanhSachDoUong()
        {
            try
            {
                DataTable tbSanPham = fileXml.HienThi("SANPHAM.xml");
                DataTable tbDanhMuc = fileXml.HienThi("DANHMUC.xml");

                var query = from sp in tbSanPham.AsEnumerable()
                            join dm in tbDanhMuc.AsEnumerable()
                            on sp["MaDanhMuc"].ToString() equals dm["MaDanhMuc"].ToString()
                            select new
                            {
                                MaSanPham = Convert.ToInt32(sp["MaSanPham"]),
                                TenSanPham = sp["TenSanPham"].ToString(),
                                DonGia = Convert.ToDecimal(sp["DonGia"]),
                                TenDanhMuc = dm["TenDanhMuc"].ToString(),
                                HinhAnhURL = sp["HinhAnhURL"].ToString()
                            };

                DataTable resultTable = new DataTable();
                resultTable.Columns.Add("MaSanPham", typeof(int));
                resultTable.Columns.Add("TenSanPham", typeof(string));
                resultTable.Columns.Add("DonGia", typeof(decimal));
                resultTable.Columns.Add("TenDanhMuc", typeof(string));
                resultTable.Columns.Add("HinhAnhURL", typeof(string));

                foreach (var item in query)
                {
                    resultTable.Rows.Add(item.MaSanPham, item.TenSanPham, item.DonGia, item.TenDanhMuc, item.HinhAnhURL);
                }

                return resultTable;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy danh sách đồ uống: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        public DataTable LayDanhSachDanhMuc()
        {
            try
            {
                // Tên file XML của bạn
                string duongDan = "DANHMUC.xml";

                // Dùng biến fileXml có sẵn trong class HeThong
                DataTable table = fileXml.HienThi(duongDan);

                return table;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi lấy danh sách danh mục: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null; // Trả về null nếu có lỗi
            }
        }

        public void CapNhatDoUong(int maSanPham, string tenSanPham, decimal donGia, int maDanhMuc, string hinhAnhURL)
        {
            try
            {
                string duongDan = "SANPHAM.xml";
                DataTable dtSanPham = fileXml.HienThi(duongDan);

                DataRow rowToUpdate = dtSanPham.AsEnumerable()
                    .FirstOrDefault(row => Convert.ToInt32(row["MaSanPham"]) == maSanPham);

                if (rowToUpdate != null)
                {
                    rowToUpdate["TenSanPham"] = tenSanPham;
                    rowToUpdate["DonGia"] = donGia;
                    rowToUpdate["MaDanhMuc"] = maDanhMuc;
                    rowToUpdate["HinhAnhURL"] = hinhAnhURL;

                    fileXml.Luu(duongDan, dtSanPham);
                    MessageBox.Show("Cập nhật đồ uống thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy sản phẩm với Mã {maSanPham} để cập nhật.", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật đồ uống: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void ThemDoUong(string tenSanPham, decimal donGia, int maDanhMuc, string hinhAnhURL)
        {
            try
            {
                string duongDan = "SANPHAM.xml";
                DataTable dtSanPham = fileXml.HienThi(duongDan);
                int newMaSanPham = dtSanPham.AsEnumerable()
                    .Select(row => Convert.ToInt32(row["MaSanPham"]))
                    .DefaultIfEmpty(0)
                    .Max() + 1;
                DataRow newRow = dtSanPham.NewRow();
                newRow["MaSanPham"] = newMaSanPham;
                newRow["TenSanPham"] = tenSanPham;
                newRow["DonGia"] = donGia;
                newRow["MaDanhMuc"] = maDanhMuc;
                newRow["HinhAnhURL"] = hinhAnhURL;
                dtSanPham.Rows.Add(newRow);
                fileXml.Luu(duongDan, dtSanPham);
                MessageBox.Show("Thêm đồ uống thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đồ uống: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void XoaDoUong(int maSanPham)
        {
            try
            {
                string duongDan = "SANPHAM.xml";
                DataTable dtSanPham = fileXml.HienThi(duongDan);
                DataRow rowToDelete = dtSanPham.AsEnumerable()
                    .FirstOrDefault(row => Convert.ToInt32(row["MaSanPham"]) == maSanPham);
                if (rowToDelete != null)
                {
                    dtSanPham.Rows.Remove(rowToDelete);
                    fileXml.Luu(duongDan, dtSanPham);
                    MessageBox.Show("Xóa đồ uống thành công.", "Thông báo", System.Windows.Forms.MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy sản phẩm với Mã {maSanPham} để xóa.", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa đồ uống: {ex.Message}", "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
