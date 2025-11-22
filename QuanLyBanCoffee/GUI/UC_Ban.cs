using QuanLyBanCoffee.Class;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class UC_Ban : UserControl
    {
        private FileXml fileXml = new FileXml();
        private Ban ban = new Ban();
        private Order order = new Order();
        private NhanVien nhanVien = new NhanVien();

        private int maNV;
        private int maBanDuocChon;
        private float tyLeChietKhau = 0.0f;
        private string tenBanDuocChon = string.Empty;
        private string tenTangDuocChon = string.Empty;
        private decimal tongTienThanhToanCuoiCung;
        private int maOrder = 0;

        // Hàm load dữ liệu chung
        public void LoadForm(string tenBan, string tenTang)
        {
            if (cmbChietKhau.Items.Count > 0 && cmbChietKhau.SelectedItem == null)
            {
                cmbChietKhau.SelectedIndex = 0; // Chọn 0% mặc định
            }

            CalculateTotal();
            SetupDisplayDataGridView();
            LoadTangToComboBox();

            this.tenTangDuocChon = tenTang;

            if (cmbTang.Items.Count > 0)
            {
                // Tự động chọn tầng mà người dùng đã tương tác trước đó (được truyền qua biến `tenTangDuocChon`).
                int targetIndex = -1;

                // Nếu có tên tầng được chỉ định, tìm chính xác vị trí (index) của nó trong ComboBox.
                if (!string.IsNullOrEmpty(this.tenTangDuocChon))
                {
                    targetIndex = cmbTang.FindStringExact(this.tenTangDuocChon);
                }

                if (targetIndex != -1)
                {
                    // Chọn đúng tầng đã được chỉ định.
                    cmbTang.SelectedIndex = targetIndex;
                }
                else
                {
                    // Nếu không có tầng nào được chỉ định hoặc không tìm thấy, chọn tầng đầu tiên làm mặc định.
                    cmbTang.SelectedIndex = 0;
                }

                this.tenBanDuocChon = tenBan;

                // Sau khi các bàn đã được tải lên, tự động "click" vào bàn được chỉ định (`tenBanDuocChon`).
                if (!string.IsNullOrEmpty(this.tenBanDuocChon))
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        ActivateTableButton(this.tenBanDuocChon);
                    }));
                }
            }
        }

        //Hàm load lại form khi được gọi từ frmOrder hoặc bấm thanh toán
        public void ReloadpForm(string tenBan, int maNV, string tenTang)
        {
            this.maNV = maNV;

            LoadForm(tenBan, tenTang);
        }

        // tạo constructor để nhận maNV từ frmMain
        public UC_Ban(int maNV) : this()
        {
            this.maNV = maNV;
            string tenNV = nhanVien.TimTenNhanVienTheoMa(maNV);
        }
         
        public UC_Ban()
        {
            InitializeComponent();
            this.cmbTang.SelectedIndexChanged += new System.EventHandler(this.cmbTang_SelectedIndexChanged);
            this.cmbChietKhau.SelectedIndexChanged += new System.EventHandler(this.cmbChietKhau_SelectedIndexChanged);
        }

        private void UC_Ban_Load(object sender, EventArgs e)
        {
            lbTongTien.Text = "Chưa chọn bàn";
            LoadForm(this.tenBanDuocChon, this.tenTangDuocChon);
        }

        // Hàm mô phỏng hành động click vào button bàn dựa trên tên bàn
        private void ActivateTableButton(string tenBan)
        {
            // Tìm button trong FlowLayoutPanel theo Tag (là TenBan)
            System.Windows.Forms.Button btnToClick = flpDSBan.Controls
                                                              .OfType<System.Windows.Forms.Button>()
                                                              .FirstOrDefault(b => b.Tag?.ToString() == tenBan);

            if (btnToClick != null)
            {
                // Mô phỏng hành động click
                btnBan_Click(btnToClick, EventArgs.Empty);
            }
        }

        private void SetupDisplayDataGridView()
        {
            if (dgvDSorder.Columns.Count == 0)
            {
                dgvDSorder.AutoGenerateColumns = false;
                dgvDSorder.Columns.Clear();

                var colMaOrder = new DataGridViewTextBoxColumn()
                {
                    Name = "MaOder",
                    HeaderText = "Mã Order",
                    DataPropertyName = "MaOder",
                    Visible = false
                };
                dgvDSorder.Columns.Add(colMaOrder);

                var colMaSP = new DataGridViewTextBoxColumn()
                {
                    Name = "MaSanPham",
                    HeaderText = "Mã SP",
                    DataPropertyName = "MaSanPham",
                    Visible = false
                };
                dgvDSorder.Columns.Add(colMaSP);

                dgvDSorder.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "TenMon",
                    HeaderText = "Tên món",
                    DataPropertyName = "TenMon",
                });

                dgvDSorder.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "SoLuong",
                    HeaderText = "Số lượng",
                    DataPropertyName = "SoLuong",
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                var colThanhTien = new DataGridViewTextBoxColumn()
                {
                    Name = "ThanhTien",
                    HeaderText = "Thành tiền",
                    DataPropertyName = "ThanhTien",
                };
                colThanhTien.DefaultCellStyle.Format = "N0";
                colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDSorder.Columns.Add(colThanhTien);
                dgvDSorder.AllowUserToAddRows = false;  
                dgvDSorder.ReadOnly = true;            
                dgvDSorder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDSorder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDSorder.MultiSelect = false;

                dgvDSorder.EnableHeadersVisualStyles = false;
                dgvDSorder.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
                dgvDSorder.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvDSorder.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDSorder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDSorder.ColumnHeadersHeight = 40;
            }
        }

        private void LoadTangToComboBox()
        {
            try
            {
                DataTable dtTang = fileXml.HienThi("TANG.xml");

                if (dtTang.Columns.Contains("MaTang") && dtTang.Columns.Contains("TenTang"))
                {
                    cmbTang.DataSource = dtTang;
                    cmbTang.DisplayMember = "TenTang";
                    cmbTang.ValueMember = "MaTang";
                }
                else
                {
                    MessageBox.Show("File TANG.xml không đúng cấu trúc (thiếu cột MaTang hoặc TenTang).", "Lỗi Cấu Trúc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách tầng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBanVaoPanel(string tenTang)
        {
            flpDSBan.Controls.Clear();

            try
            {
                DataTable dtBan = fileXml.HienThi("BAN.xml");

                int maTang = ban.TimMaTangTheoTen(tenTang);

                var banTrenTang = dtBan.AsEnumerable()
                                       .Where(r =>
                                       {
                                           int banMaTang;
                                           return int.TryParse(r["MaTang"]?.ToString(), out banMaTang) && banMaTang == maTang;
                                       });

                foreach (var banRow in banTrenTang)
                {
                    string tenBan = banRow["TenBan"]?.ToString() ?? string.Empty;
                    string trangThai = banRow["TrangThai"]?.ToString() ?? string.Empty;

                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                    btn.Text = tenBan;
                    btn.Size = new Size(110, 70);
                    btn.Tag = tenBan;
                    btn.Font = new Font(btn.Font.FontFamily, 12, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.Margin = new Padding(17, 10, 15, 10);
                    btn.Click += btnBan_Click;

                    CapNhatTrangThaiBan(btn, trangThai);

                    flpDSBan.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTrangThaiBan(System.Windows.Forms.Button btn, string trangThai)
        {
            if (trangThai == "Có khách")
            {
                btn.BackColor = Color.Blue;
                btn.ForeColor = Color.White;
            }
            else 
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }

        private void cmbTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTang.SelectedItem != null)
            {
                string tenTang = cmbTang.GetItemText(cmbTang.SelectedItem);
                LoadBanVaoPanel(tenTang);

                this.tenBanDuocChon = string.Empty;
                this.tenTangDuocChon = tenTang;
                lbBanDangChon.Text = "Chưa chọn bàn";
                lbTongTien.Text = "Chưa chọn bàn";
                dgvDSorder.DataSource = null; 
            }
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            var btnBan = sender as System.Windows.Forms.Button;
            if (btnBan == null) return;

            string tenBan = btnBan.Text;
            string tenTang = cmbTang.GetItemText(cmbTang.SelectedItem) ?? string.Empty;

            this.tenBanDuocChon = tenBan;
            this.tenTangDuocChon = tenTang;
            this.maBanDuocChon = ban.TimMaBanTheoTen(tenBan, tenTang);
            lbBanDangChon.Text = $"{this.tenBanDuocChon} ({this.tenTangDuocChon})";

            try
            {
                DataTable dtMon = order.LoadDanhSachMonChuaThanhToanTheoBan(maBanDuocChon);
                if (dtMon == null || dtMon.Rows.Count == 0)
                {
                    dgvDSorder.DataSource = null;
                    lbTongTien.Text = "Chưa có món nào";
                    return;
                }

                DataTable dtSanPham = fileXml.HienThi("SANPHAM.xml");

                DataTable dtHienThi = new DataTable();
                dtHienThi.Columns.Add("MaOder", typeof(int));
                dtHienThi.Columns.Add("MaSanPham", typeof(int));
                dtHienThi.Columns.Add("TenMon", typeof(string));
                dtHienThi.Columns.Add("SoLuong", typeof(int));
                dtHienThi.Columns.Add("ThanhTien", typeof(decimal));

                foreach (DataRow r in dtMon.Rows)
                {
                    int maSP = Convert.ToInt32(r["MaSanPham"]);
                    int soLuong = Convert.ToInt32(r["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(r["DonGia"]);

                    string tenMon = dtSanPham.AsEnumerable()
                        .Where(sp => Convert.ToInt32(sp["MaSanPham"]) == maSP)
                        .Select(sp => sp["TenSanPham"].ToString())
                        .FirstOrDefault() ?? "Không xác định";

                    decimal thanhTien = soLuong * donGia;
                    dtHienThi.Rows.Add(r["MaOder"], maSP, tenMon, soLuong, thanhTien);
                }
                this.maOrder = Convert.ToInt32(dtMon.Rows[0]["MaOder"]);
          
                dgvDSorder.DataSource = dtHienThi;

                CalculateTotal();
        
                if (dgvDSorder.Columns["MaOder"] != null)
                    dgvDSorder.Columns["MaOder"].Visible = false;
                if (dgvDSorder.Columns["MaSanPham"] != null)
                    dgvDSorder.Columns["MaSanPham"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tenBanDuocChon) || string.IsNullOrEmpty(tenTangDuocChon))
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi thêm món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                frmOrder formOrder = new frmOrder(this.maBanDuocChon, this.maNV, this.tenBanDuocChon, this.tenTangDuocChon);

                formOrder.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form thêm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotal()
        {
            decimal tongTienTruocCK = dgvDSorder.Rows.Cast<DataGridViewRow>()
                                            .Sum(row => Convert.ToDecimal(row.Cells["ThanhTien"].Value));

            float chietKhauPhanTram = 0.0f;
            if (cmbChietKhau.SelectedItem != null)
            {
                string chietKhauStr = cmbChietKhau.SelectedItem.ToString().Replace("%", "");
                if (!float.TryParse(chietKhauStr, out chietKhauPhanTram))
                {
                    chietKhauPhanTram = 0.0f;
                }
            }
            this.tyLeChietKhau = chietKhauPhanTram / 100.0f;
            decimal tienChietKhau = tongTienTruocCK * (decimal)this.tyLeChietKhau;

            this.tongTienThanhToanCuoiCung = tongTienTruocCK - tienChietKhau;
            lbTongTien.Text = $"Tổng tiền thanh toán: {this.tongTienThanhToanCuoiCung.ToString("N0")} VND";
        }

        private void cmbChietKhau_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            order.CapNhatOrder(maOrder, tongTienThanhToanCuoiCung);
            order.CapNhatThanhToan(maOrder, "Đã thanh toán", tyLeChietKhau);
            ban.CapNhatTrangThaiBan(maBanDuocChon, "Trống");
            MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            ReloadpForm(this.tenBanDuocChon, this.maNV, this.tenTangDuocChon);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            HuyMon huymon = new HuyMon();

            if (string.IsNullOrEmpty(tenBanDuocChon) || string.IsNullOrEmpty(tenTangDuocChon))
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi hủy món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvDSorder.DataSource == null || dgvDSorder.Rows.Count == 0)
            {
                MessageBox.Show("Không có món nào để hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.maOrder <= 0 || dgvDSorder.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một món trong danh sách để hủy.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSanPham = Convert.ToInt32(dgvDSorder.SelectedRows[0].Cells["MaSanPham"].Value);
            int soLuong = Convert.ToInt32(dgvDSorder.SelectedRows[0].Cells["SoLuong"].Value);

            var result = DialogHuyMon.Show(soLuong);

            if (result == null)
            {
                return;
            }

            if (dgvDSorder.Rows.Count == 1 && result.SoLuongHuy == soLuong)
            {
                huymon.ThemHuyMon(maOrder, maSanPham, result.SoLuongHuy, result.LyDo);
                ban.CapNhatTrangThaiBan(maBanDuocChon, "Trống");
                order.CapNhatThanhToan(maOrder, "Đã hủy", 0);
                order.CapNhatOrder(maOrder, 0);
                MessageBox.Show("Hủy món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBan_Click(flpDSBan.Controls
                .OfType<Button>()
                .First(b => b.Tag?.ToString() == tenBanDuocChon), EventArgs.Empty);
            }
            else {
                huymon.ThemHuyMon(maOrder, maSanPham, result.SoLuongHuy, result.LyDo);
                order.CapNhatHuyChiTietOrder(maOrder, maSanPham, result.SoLuongHuy);
                btnBan_Click(flpDSBan.Controls
                .OfType<Button>()
                .First(b => b.Tag?.ToString() == tenBanDuocChon), EventArgs.Empty);
                CalculateTotal();
                order.CapNhatOrder(maOrder, tongTienThanhToanCuoiCung);
            }
        }
    }
}