using QuanLyBanCoffee.Class;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class frmOrder : Form
    {
        private FileXml fileXml = new FileXml();
        private Order order = new Order();
        private Ban ban = new Ban();

        private int maBanHienTai;
        private string tenBan;
        private string tenTang;
        private int maNV;
        private DataTable dtMon;
        private decimal tongTienThanhToanCuoiCung = 0;


        // Hàm được gọi khi bấm nút thêm món từ frmBan, nhân các tham số để lưu order
        public frmOrder(int maBan, int maNV, string tenBan, string tenTang) : this()
        {
            this.maBanHienTai = maBan;
            this.maNV = maNV;
            this.tenBan = tenBan;
            this.tenTang = tenTang;
            this.dtMon = new DataTable();
            dtMon = order.LoadDanhSachMonChuaThanhToanTheoBan(this.maBanHienTai);

            // Tính Tổng số lượng món
            int tongSoLuong = dtMon.AsEnumerable()
                                        .Sum(row => row.Field<int>("SoLuong"));
            lbTongSoMon.Text = $"Tổng số món đã chọn: {tongSoLuong}";

            // Thêm cột "TenMon" nếu chưa tồn tại
            if (!dtMon.Columns.Contains("TenMon"))
                dtMon.Columns.Add("TenMon", typeof(string));

            // Thêm cột "ThanhTien" nếu chưa tồn tại
            if (!dtMon.Columns.Contains("ThanhTien"))
                dtMon.Columns.Add("ThanhTien", typeof(decimal));

            // Tính giá trị Thành tiền và điền tên món
            DataTable dtSanPham = new FileXml().HienThi("SANPHAM.xml"); // Lấy danh sách sản phẩm
            foreach (DataRow row in dtMon.Rows)
            {
                int maSP = Convert.ToInt32(row["MaSanPham"]);
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                decimal donGia = Convert.ToDecimal(row["DonGia"]);

                // Tính thành tiền
                row["ThanhTien"] = soLuong * donGia;

                // Lấy tên món
                string tenMon = dtSanPham.AsEnumerable()
                    .Where(sp => Convert.ToInt32(sp["MaSanPham"]) == maSP)
                    .Select(sp => sp["TenSanPham"].ToString())
                    .FirstOrDefault() ?? "Không xác định";
                row["TenMon"] = tenMon;
            }

            this.Text = $"Order - {tenBan} ({tenTang})";
            SetupOrderDataTable();

            dgvDSMon.DataSource = this.dtMon;

        }


        public frmOrder()
        {
            InitializeComponent();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            LoadSanPhamVaoPanel(1);
        }

        private void SetupOrderDataTable()
        {
            if (dgvDSMon.Columns.Count == 0)
            {
                dgvDSMon.AllowUserToAddRows = false;
                dgvDSMon.AutoGenerateColumns = false;
                dgvDSMon.Columns.Clear();

                // Cột ẩn: Mã Order
                var colMaOrder = new DataGridViewTextBoxColumn()
                {
                    Name = "MaOder",
                    HeaderText = "Mã Order",
                    DataPropertyName = "MaOder",
                    //Visible = false
                };
                dgvDSMon.Columns.Add(colMaOrder);

                // Cột ẩn: Mã sản phẩm
                var colMaSP = new DataGridViewTextBoxColumn()
                {
                    Name = "MaSanPham",
                    HeaderText = "Mã SP",
                    DataPropertyName = "MaSanPham",
                    //Visible = false
                };
                dgvDSMon.Columns.Add(colMaSP);

                // Cột hiển thị: Tên món
                dgvDSMon.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "TenMon",
                    HeaderText = "Tên món",
                    DataPropertyName = "TenMon",
                    Width = 150
                });

                // Cột hiển thị: Số lượng
                dgvDSMon.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "SoLuong",
                    HeaderText = "Số lượng",
                    DataPropertyName = "SoLuong",
                    Width = 70,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                // Cột hiển thị: Thành tiền
                var colThanhTien = new DataGridViewTextBoxColumn()
                {
                    Name = "ThanhTien",
                    HeaderText = "Thành tiền",
                    DataPropertyName = "ThanhTien",
                    Width = 100
                };
                colThanhTien.DefaultCellStyle.Format = "N0";
                colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDSMon.Columns.Add(colThanhTien);
            }
        }

        private void LoadSanPhamVaoPanel(int maDanhMuc)
        {
            flpDSMon.Controls.Clear();

            try
            {
                DataTable dtSanPham = fileXml.HienThi("SANPHAM.xml");
                DataView dv = new DataView(dtSanPham);
                dv.RowFilter = $"MaDanhMuc = {maDanhMuc}";
                DataTable dtFiltered = dv.ToTable();

                string appDirectory = Application.StartupPath;
                string imageBaseDir = Path.Combine(appDirectory, "HinhAnh");

                foreach (DataRow row in dtFiltered.Rows)
                {
                    int maSanPham = 0;
                    decimal donGia = 0;

                    if (!int.TryParse(row["MaSanPham"]?.ToString(), out maSanPham)) continue;
                    if (!decimal.TryParse(row["DonGia"]?.ToString(), out donGia)) continue;

                    string tenSanPham = row["TenSanPham"].ToString();
                    string tenFileAnh = row["HinhAnhURL"].ToString();
                    string imagePath = Path.Combine(imageBaseDir, tenFileAnh);

                    pcMon productCard = new pcMon();

                    productCard.MaSanPham = maSanPham;
                    productCard.DonGia = donGia;
                    productCard.TenMonAn = tenSanPham;

                    if (File.Exists(imagePath))
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                            {
                                productCard.AnhMonAn.Image = Image.FromStream(fs);
                            }
                        }
                        catch { /* Bỏ qua lỗi load ảnh */ }
                    }

                    productCard.tenMon.Text = tenSanPham;
                    productCard.giaMon.Text = $"Giá: {donGia.ToString("N0")} VND";
                    productCard.MonAn_Click += ProductCard_MonAnClick;

                    flpDSMon.Controls.Add(productCard);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc dữ liệu sản phẩm: {ex.Message}", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductCard_MonAnClick(object sender, EventArgs e)
        {
            pcMon selectedProduct = sender as pcMon;
            if (selectedProduct == null) return;

            int maSP = selectedProduct.MaSanPham;
            string tenSP = selectedProduct.TenMonAn;
            decimal donGia = selectedProduct.DonGia;

            DataRow foundRow = dtMon.AsEnumerable()
                                        .FirstOrDefault(row => row.Field<int>("MaSanPham") == maSP);

            if (foundRow != null)
            {
                int currentSL = foundRow.Field<int>("SoLuong");
                currentSL++;
                foundRow["SoLuong"] = currentSL;
                foundRow["ThanhTien"] = donGia * currentSL;
            }
            else
            {
                DataRow newRow = dtMon.NewRow();
                newRow["MaSanPham"] = maSP;
                newRow["TenMon"] = tenSP;
                newRow["SoLuong"] = 1;
                newRow["DonGia"] = donGia;
                newRow["ThanhTien"] = donGia;
                dtMon.Rows.Add(newRow);
            }

            CalculateTotal();
        }

        private void CalculateTotal()
        {
            // 1. Tính Tổng số lượng món
            int tongSoLuong = dtMon.AsEnumerable()
                                        .Sum(row => row.Field<int>("SoLuong"));
            lbTongSoMon.Text = $"Tổng số món đã chọn: {tongSoLuong}";

            tongTienThanhToanCuoiCung = dtMon.AsEnumerable().Sum(row => Convert.ToDecimal(row["ThanhTien"]));


        }
        private void btnCaPhe_Click(object sender, EventArgs e)
        {
            LoadSanPhamVaoPanel(1);
        }

        private void btnSinhTo_Click(object sender, EventArgs e)
        {
            LoadSanPhamVaoPanel(2);
        }

        private void btnMatcha_Click(object sender, EventArgs e)
        {
            LoadSanPhamVaoPanel(3);
        }

        private void btnKhac_Click(object sender, EventArgs e)
        {
            LoadSanPhamVaoPanel(4);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (dgvDSMon.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có món nào được chọn để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy mã order từ dòng đầu tiên nếu có
            int maOrder = -1; // giá trị mặc định

            if (dgvDSMon.Rows.Count > 0 && !dgvDSMon.Rows[0].IsNewRow)
            {
                object value = dgvDSMon.Rows[0].Cells["MaOder"].Value;
                if (value != null && value != DBNull.Value && int.TryParse(value.ToString(), out int result))
                    maOrder = result;
            }

            if (maOrder == -1)
            {
                this.dtMon = (DataTable)dgvDSMon.DataSource;
                int MaOrder = order.LayMaOrderTiepTheo();
                int maCTOrder = order.LayMaCTOrderTiepTheo();
                foreach (DataRow row in this.dtMon.Rows)
                {
                    int maSanPham = Convert.ToInt32(row["MaSanPham"]);
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(row["DonGia"]);
                    order.ThemChiTietOrderMoi(maCTOrder, MaOrder, maSanPham, soLuong, donGia);
                    maCTOrder++;
                }
                    CalculateTotal();
                    order.ThemOrderMoi(MaOrder, this.maBanHienTai, this.maNV, this.tongTienThanhToanCuoiCung, DateTime.Now, "Chưa thanh toán");
                    ban.CapNhatTrangThaiBan(this.maBanHienTai, "Có khách");
                    MessageBox.Show("Lưu order thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Load lại dữ liệu mới cho frmBan
                // Lấy form đang mở
                frmBan frmban = Application.OpenForms.OfType<frmBan>().FirstOrDefault();
                if (frmban != null)
                {
                    frmban.ReloadpForm(this.tenBan, this.maNV, this.tenTang); // load lại dữ liệu
                }

                this.Close();
            }
            else
            {
                // Lấy dữ liệu hiện tại từ DataGridView
                DataTable dataTable = (DataTable)dgvDSMon.DataSource;

                DataTable dtMonCu = new DataTable();
                dtMonCu = order.LoadDanhSachMonChuaThanhToanTheoBan(this.maBanHienTai);

                // Duyệt qua từng món hiện tại trong order
                foreach (DataRow rowMoi in dataTable.Rows)
                {
                    int maSP = Convert.ToInt32(rowMoi["MaSanPham"]);
                    int soLuong = Convert.ToInt32(rowMoi["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(rowMoi["DonGia"]);

                    // Tìm xem món này đã có trong chi tiết order cũ chưa
                    DataRow rowCu = dtMonCu.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("MaSanPham") == maSP);

                    if (rowCu != null)
                    {
                        int maSanPham = Convert.ToInt32(rowCu["MaSanPham"]);

                        order.CapNhatChiTietOrder(maOrder, maSanPham, soLuong);
                    }
                    else
                    {
                        // Nếu món mới → thêm vào bảng chi tiết order
                        int maCTOrderMoi = order.LayMaCTOrderTiepTheo();
                        order.ThemChiTietOrderMoi(maCTOrderMoi, maOrder, maSP, soLuong, donGia);
                    }
                }

                CalculateTotal();
                order.CapNhatOrder(maOrder, tongTienThanhToanCuoiCung);

                MessageBox.Show("Cập nhật order thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại dữ liệu cho frmBan
                frmBan frmban = Application.OpenForms.OfType<frmBan>().FirstOrDefault();
                if (frmban != null)
                    frmban.ReloadpForm(this.tenBan, this.maNV, this.tenTang);

                this.Close();
            }
        }
    }
}