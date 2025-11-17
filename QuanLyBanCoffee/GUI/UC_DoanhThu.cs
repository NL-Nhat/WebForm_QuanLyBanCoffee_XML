using QuanLyBanCoffee.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class UC_DoanhThu : UserControl
    {

        HeThong heThong = new HeThong();

        public UC_DoanhThu()
        {
            InitializeComponent();
        }

        private void UC_DoanhThu_Load(object sender, EventArgs e)
        {
            // 1. Đặt giá trị cho DateTimePicker là ngày hôm nay
            dtpNgayXem.Value = DateTime.Today;

            // 2. Tự động tải báo cáo cho ngày hôm nay
            LoadBaoCao(DateTime.Today);
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            LoadBaoCao(dtpNgayXem.Value);
        }

        private void LoadBaoCao(DateTime ngay)
        {
            try
            {
                // 1. Gọi hàm logic từ lớp HeThong
                // Chúng ta chỉ lấy phần ngày, không quan tâm đến giờ
                BaoCaoDoanhThuNgay baoCao = heThong.LayBaoCaoDoanhThu(ngay.Date);

                // 2. Cập nhật các Label tổng quan
                // Dùng "N0" để định dạng số (ví dụ: 100,000)
                lblTongDoanhThu.Text = baoCao.TongDoanhThu.ToString("N0") + " VNĐ";
                lblTongSoMon.Text = baoCao.TongSoMon.ToString() + " món";

                // 3. Hiển thị chi tiết danh mục lên DataGridView
                dgvThongKe.DataSource = null; // Xóa dữ liệu cũ trước khi nạp

                // Gán nguồn dữ liệu là List<ThongKeDanhMuc>
                dgvThongKe.DataSource = baoCao.ThongKeTheoDanhMuc;

                // 4. (Quan trọng) Cấu hình lại tiêu đề cột cho đẹp
                if (dgvThongKe.Columns.Count > 0)
                {
                    dgvThongKe.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
                    dgvThongKe.Columns["SoLuongMon"].HeaderText = "Số Lượng Bán";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo doanh thu: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
