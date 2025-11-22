using QuanLyBanCoffee.Class;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // Thư viện biểu đồ

namespace QuanLyBanCoffee.GUI.Admin
{
    public partial class UC_BaoCao : UserControl
    {
        BaoCao baoCao = new BaoCao();

        public UC_BaoCao()
        {
            InitializeComponent();
        }

        private void UC_BaoCao_Load(object sender, EventArgs e)
        {
            // Mặc định lấy ngày đầu tháng đến hiện tại
            DateTime now = DateTime.Now;
            DateTime dauThang = new DateTime(now.Year, now.Month, 1);

            dtpTuNgay.Value = dauThang;
            dtpDenNgay.Value = now;

            dtpTuNgayHuy.Value = dauThang;
            dtpDenNgayHuy.Value = now;
        }

        // TAB 1: DOANH THU
        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;

            DataTable dt = baoCao.LayDoanhThu(tuNgay, denNgay);

            dgvDoanhThu.DataSource = dt;
            dgvDoanhThu.Columns["TongTien"].DefaultCellStyle.Format = "N0"; 
            dgvDoanhThu.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";

            decimal tongCong = 0;
            if (dt.Rows.Count > 0)
            {
                tongCong = Convert.ToDecimal(dt.Compute("SUM(TongTien)", string.Empty));
            }
            lblTongDoanhThu.Text = $"Tổng doanh thu: {tongCong:N0} VNĐ";

            VeBieuDoDoanhThu(dt);
        }

        private void VeBieuDoDoanhThu(DataTable dt)
        {
            chartDoanhThu.Series.Clear();
            chartDoanhThu.Titles.Clear();

            chartDoanhThu.Titles.Add("Biểu đồ doanh thu theo ngày");

            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column; 
            series.XValueType = ChartValueType.Date;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0"; 

            // Đổ dữ liệu vào Chart
            foreach (DataRow row in dt.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                decimal tien = Convert.ToDecimal(row["TongTien"]);

                // Thêm điểm dữ liệu (X: Ngày, Y: Tiền)
                series.Points.AddXY(ngay.ToString("dd/MM"), tien);
            }

            chartDoanhThu.Series.Add(series);
        }

        // TAB 2: THỐNG KÊ HỦY MÓN
        private void btnXemHuyMon_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgayHuy.Value;
            DateTime denNgay = dtpDenNgayHuy.Value;

            DataTable dt = baoCao.LayDanhSachHuyMon(tuNgay, denNgay);

            dgvHuyMon.DataSource = dt;

            dgvHuyMon.Columns["TenMon"].HeaderText = "Tên Món";
            dgvHuyMon.Columns["LyDo"].HeaderText = "Lý do hủy";
            dgvHuyMon.Columns["ThoiGianHuy"].HeaderText = "Thời gian";
            dgvHuyMon.Columns["TenNhanVien"].HeaderText = "Nhân viên Order";

            dgvHuyMon.Columns["ThoiGianHuy"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHuyMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}