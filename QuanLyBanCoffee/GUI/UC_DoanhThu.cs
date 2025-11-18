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
                BaoCaoDoanhThuNgay baoCao = heThong.LayBaoCaoDoanhThu(ngay.Date);

                lblTongDoanhThu.Text = baoCao.TongDoanhThu.ToString("N0") + " VNĐ";
                lblTongSoMon.Text = baoCao.TongSoMon.ToString() + " món";

                dgvThongKe.DataSource = null; 

                dgvThongKe.DataSource = baoCao.ThongKeTheoDanhMuc;

                if (dgvThongKe.Columns.Count > 0)
                {
                    dgvThongKe.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
                    dgvThongKe.Columns["SoLuongMon"].HeaderText = "Số Lượng Bán";
                }
                dgvThongKe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvThongKe.AllowUserToAddRows = false;
                dgvThongKe.ReadOnly = true;

                dgvThongKe.EnableHeadersVisualStyles = false;
                dgvThongKe.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
                dgvThongKe.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvThongKe.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvThongKe.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvThongKe.ColumnHeadersHeight = 40;
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
