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
        // Khởi tạo đối tượng FileXml
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
            //thiết lập cấu trúc hiển thị cho DataGridView danh sách order
            SetupDisplayDataGridView();
            // Tải danh sách các tầng và điền vào ComboBox `cmbTang`.
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
                    // Sử dụng `this.BeginInvoke` để đưa hành động chọn bàn vào hàng đợi xử lý của luồng giao diện (UI thread).
                    // Hành động này sẽ chỉ được thực thi SAU KHI tất cả các tác vụ hiện tại (bao gồm cả
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

            // Gán sự kiện cmbTang_SelectedIndexChanged trong constructor
            this.cmbTang.SelectedIndexChanged += new System.EventHandler(this.cmbTang_SelectedIndexChanged);

            // Gán sự kiện SelectedIndexChanged cho cmbChietKhau
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

        // Hàm thiết lập cấu trúc cho DataGridView hiển thị danh sách order
        private void SetupDisplayDataGridView()
        {
            if (dgvDSorder.Columns.Count == 0)
            {
                dgvDSorder.AutoGenerateColumns = false;
                dgvDSorder.Columns.Clear();

                // Cột ẩn: Mã Order
                var colMaOrder = new DataGridViewTextBoxColumn()
                {
                    Name = "MaOder",
                    HeaderText = "Mã Order",
                    DataPropertyName = "MaOder",
                    Visible = false
                };
                dgvDSorder.Columns.Add(colMaOrder);

                // Cột ẩn: Mã sản phẩm
                var colMaSP = new DataGridViewTextBoxColumn()
                {
                    Name = "MaSanPham",
                    HeaderText = "Mã SP",
                    DataPropertyName = "MaSanPham",
                    Visible = false
                };
                dgvDSorder.Columns.Add(colMaSP);

                // Cột hiển thị: Tên món
                dgvDSorder.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "TenMon",
                    HeaderText = "Tên món",
                    DataPropertyName = "TenMon",
                    Width = 150
                });

                // Cột hiển thị: Số lượng
                dgvDSorder.Columns.Add(new DataGridViewTextBoxColumn()
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
                dgvDSorder.Columns.Add(colThanhTien);
                dgvDSorder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDSorder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDSorder.MultiSelect = false;
            }
        }


        // Hàm tải danh sách tầng vào ComboBox
        private void LoadTangToComboBox()
        {
            try
            {
                DataTable dtTang = fileXml.HienThi("TANG.xml");
                // Kiểm tra xem DataTable có cột MaTang và TenTang không
                if (dtTang.Columns.Contains("MaTang") && dtTang.Columns.Contains("TenTang"))
                {
                    cmbTang.DataSource = dtTang;
                    cmbTang.DisplayMember = "TenTang";
                    cmbTang.ValueMember = "MaTang";
                }
                else
                {
                    // Xử lý nếu file XML không đúng cấu trúc
                    MessageBox.Show("File TANG.xml không đúng cấu trúc (thiếu cột MaTang hoặc TenTang).", "Lỗi Cấu Trúc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách tầng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tải danh sách bàn vào panel dựa trên tên tầng
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
                                           // Lấy MaTang của bàn và chuyển đổi an toàn
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
            else // "Trống" hoặc các trạng thái khác
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }

        //sự kiện khi thay đổi tầng ở combobox
        private void cmbTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTang.SelectedItem != null)
            {
                //Lấy tên tầng đúng từ ComboBox đang được chọn
                string tenTang = cmbTang.GetItemText(cmbTang.SelectedItem);
                LoadBanVaoPanel(tenTang);

                // Cập nhật lại các biến lưu trữ bàn và tầng được chọn
                this.tenBanDuocChon = string.Empty;
                this.tenTangDuocChon = tenTang;
                lbBanDangChon.Text = "Chưa chọn bàn";
                lbTongTien.Text = "Chưa chọn bàn";
                dgvDSorder.DataSource = null; // Xóa dữ liệu cũ
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
                // Đảm bảo DataGridView đã có cột
                //SetupDisplayDataGridView();

                // Lấy danh sách món chưa thanh toán của bàn
                DataTable dtMon = order.LoadDanhSachMonChuaThanhToanTheoBan(maBanDuocChon);
                if (dtMon == null || dtMon.Rows.Count == 0)
                {
                    dgvDSorder.DataSource = null;
                    lbTongTien.Text = "Chưa có món nào";
                    return;
                }

                // Lấy danh sách sản phẩm để join tên món
                DataTable dtSanPham = fileXml.HienThi("SANPHAM.xml");

                // Chuẩn bị DataTable theo cấu trúc DataGridView
                DataTable dtHienThi = new DataTable();
                dtHienThi.Columns.Add("MaOder", typeof(int));
                dtHienThi.Columns.Add("MaSanPham", typeof(int));
                dtHienThi.Columns.Add("TenMon", typeof(string));
                dtHienThi.Columns.Add("SoLuong", typeof(int));
                dtHienThi.Columns.Add("ThanhTien", typeof(decimal));

                // Ghép dữ liệu từ Order + Sản phẩm
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
                // Gán dữ liệu lên DataGridView
                dgvDSorder.DataSource = dtHienThi;

                CalculateTotal();
                // Ẩn các cột mã
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
                // Khởi tạo form mới và truyền dữ liệu cần thiết
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
            // 2. Tính Tổng tiền trước chiết khấu
            decimal tongTienTruocCK = dgvDSorder.Rows.Cast<DataGridViewRow>()
                                            .Sum(row => Convert.ToDecimal(row.Cells["ThanhTien"].Value));

            // 3. Xử lý Chiết khấu
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

            // 4. Lưu kết quả cuối cùng vào biến cục bộ
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
            // Cập nhật lại giao diện
            ReloadpForm(this.tenBanDuocChon, this.maNV, this.tenTangDuocChon);
        }

    }
}