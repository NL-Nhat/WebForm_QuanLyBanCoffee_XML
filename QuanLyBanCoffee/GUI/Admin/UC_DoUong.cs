using QuanLyBanCoffee.Class;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI.Admin
{
    public partial class UC_DoUong : UserControl
    {
        private DoUong doUong = new DoUong();

        private int maDanhMuc;
        private string duongDanFileAnhMoiDuocChon = "";
        // Biến lưu trạng thái hiện tại: "XEM", "THEM", "SUA"
        private string cheDoHienTai = "XEM";

        public UC_DoUong()
        {
            InitializeComponent();
        }

        private void UC_DoUong_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMuc();
            SetupDataGridView(dgvDoUong);
            LoadDuLieuVaoDataGridView(dgvDoUong);

            ThietLapCheDo("XEM");
        }

        private void dgvDoUong_SelectionChanged(object sender, EventArgs e)
        {
            // Nếu đang ở chế độ THÊM thì không cho load dữ liệu từ bảng lên
            if (cheDoHienTai == "THEM") return;

            if (dgvDoUong.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDoUong.SelectedRows[0];
                HienThiChiTietTuRow(selectedRow);
            }
        }

        private void HienThiChiTietTuRow(DataGridViewRow selectedRow)
        {
            duongDanFileAnhMoiDuocChon = "";
            try
            {
                if (selectedRow.Cells["TenSanPham"].Value == null) return;

                string tenSP = selectedRow.Cells["TenSanPham"].Value?.ToString() ?? "";
                string donGia = selectedRow.Cells["DonGia"].Value?.ToString() ?? "";
                string tenDanhMuc = selectedRow.Cells["TenDanhMuc"].Value?.ToString() ?? "";
                string tenFileAnh = selectedRow.Cells["HinhAnhURL"].Value?.ToString() ?? "";

                txtTenSanPham.Text = tenSP;
                txtGia.Text = donGia;
                cmbDanhMuc.Text = tenDanhMuc;

                if (picHinhAnhSanPham.Image != null)
                {
                    picHinhAnhSanPham.Image.Dispose();
                    picHinhAnhSanPham.Image = null;
                }

                string thuMucImages = Path.Combine(Application.StartupPath, "HinhAnh");
                string duongDanAnhDayDu = Path.Combine(thuMucImages, tenFileAnh);

                if (File.Exists(duongDanAnhDayDu))
                {
                    try
                    {
                        // 2. Đọc file vào MemoryStream để tránh khóa file
                        byte[] imageBytes = File.ReadAllBytes(duongDanAnhDayDu);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            picHinhAnhSanPham.Image = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 3. Bắt lỗi cụ thể nếu file không phải là ảnh
                        picHinhAnhSanPham.Image = null;
                        // Nên thông báo lỗi thay vì "nuốt" lỗi
                        Debug.WriteLine($"Lỗi tải ảnh {tenFileAnh}: {ex.Message}");
                    }

                }
                else
                {
                    picHinhAnhSanPham.Image = null;
                }
            }
            catch
            {
                picHinhAnhSanPham.Image = null;
            }
        }


        private void LoadComboBoxDanhMuc()
        {
            DataTable dtDanhMuc = doUong.LayDanhSachDanhMuc();

            if (dtDanhMuc != null)
            {
                cmbDanhMuc.DataSource = dtDanhMuc;

                // Chỉ định cột nào sẽ hiển thị cho người dùng
                cmbDanhMuc.DisplayMember = "TenDanhMuc";

                // Chỉ định cột nào sẽ là giá trị (ID) ẩn bên dưới
                cmbDanhMuc.ValueMember = "MaDanhMuc";
            }
            else
            {
                MessageBox.Show("Không thể tải danh sách danh mục.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDanhMuc.DataSource == null) return;

            if (cmbDanhMuc.SelectedItem is DataRowView drv)
            {
                maDanhMuc = Convert.ToInt32(drv["MaDanhMuc"]);
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            dgv.Columns.Clear();
            //Tắt tự động tạo cột
            dgv.AutoGenerateColumns = false;

            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSanPham",
                HeaderText = "Mã sản phẩm",
                DataPropertyName = "MaSanPham",
                Visible = false 
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSanPham",
                HeaderText = "Tên Đồ Uống",
                DataPropertyName = "TenSanPham",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "DonGia",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDanhMuc",
                HeaderText = "Danh mục",
                DataPropertyName = "TenDanhMuc",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HinhAnhURL",
                HeaderText = "URL hình ảnh",
                DataPropertyName = "HinhAnhURL",
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            // --- CHỈNH HEADER ---
            dgv.EnableHeadersVisualStyles = false; // cho phép tùy chỉnh màu header
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow; // màu nền header
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // màu chữ header
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); // font header
            dgv.ColumnHeadersHeight = 40; // chiều cao header

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }

        private void LoadDuLieuVaoDataGridView(DataGridView dgv)
        {
            try
            {
                DataTable dtDoUong = doUong.LayDanhSachDoUong();

                if (dtDoUong != null)
                {
                    dgv.DataSource = dtDoUong;
                }
                else
                {
                    MessageBox.Show("Không tải được danh sách đồ uống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu lên DataGridView: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            // Tạo một OpenFileDialog mới để người dùng chọn file
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                // Thiết lập bộ lọc file để chỉ hiển thị các file ảnh thông dụng
                openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
                openFile.Title = "Chọn hình ảnh sản phẩm";

                // Hiển thị hộp thoại. Nếu người dùng chọn "OK"
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn file đầy đủ mà người dùng đã chọn
                    string selectedFilePath = openFile.FileName;
                        
                    try
                    {
                        if (picHinhAnhSanPham.Image != null)
                        {
                            picHinhAnhSanPham.Image.Dispose();
                            picHinhAnhSanPham.Image = null;
                        }

                        // 2. Dùng MemoryStream để không khóa file nguồn
                        byte[] imageBytes = File.ReadAllBytes(selectedFilePath);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            picHinhAnhSanPham.Image = Image.FromStream(ms);
                        }

                        this.duongDanFileAnhMoiDuocChon = selectedFilePath;

                    }
                    catch (Exception ex)
                    {
                        // Xử lý nếu file không phải là ảnh hợp lệ
                        MessageBox.Show("Không thể tải hình ảnh. File có thể bị hỏng hoặc không phải là file ảnh: " + ex.Message, "Lỗi Tải Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Dọn dẹp nếu có lỗi
                        if (picHinhAnhSanPham.Image != null)
                        {
                            picHinhAnhSanPham.Image.Dispose();
                        }
                        picHinhAnhSanPham.Image = null;
                        this.duongDanFileAnhMoiDuocChon = ""; // Reset biến
                    }
                }
                // Nếu người dùng nhấn "Cancel", không làm gì cả
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cheDoHienTai == "XEM")
            {
                if (dgvDoUong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món cần sửa!");
                    return;
                }

                // Lần bấm 1: Chuyển sang chế độ SỬA
                ThietLapCheDo("SUA");
                txtTenSanPham.Focus();
            }
            else if (cheDoHienTai == "SUA")
            {
                // Lần bấm 2: Thực hiện LƯU (Update)
                ThucHienLuu();
            }
        }

        private string XuLyLayTenFileHinhAnh(string duongDanAnhNguon)
        {
            if (string.IsNullOrEmpty(duongDanAnhNguon)) return "";

            try
            {
                string tenFileAnh = Path.GetFileName(duongDanAnhNguon);
                string thuMucDich = Path.Combine(Application.StartupPath, "HinhAnh");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(thuMucDich))
                {
                    Directory.CreateDirectory(thuMucDich);
                }

                string duongDanDich = Path.Combine(thuMucDich, tenFileAnh);

                // Chỉ copy nếu file nguồn tồn tại
                if (File.Exists(duongDanAnhNguon))
                {
                    // Overwrite = true để ghi đè nếu file đã tồn tại
                    File.Copy(duongDanAnhNguon, duongDanDich, true);
                    return tenFileAnh; // Trả về tên file để lưu vào DB
                }
                else
                {
                    return ""; // File nguồn không tìm thấy
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu ảnh vào thư mục: {ex.Message}");
                return "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cheDoHienTai == "XEM")
            {
                // Lần bấm 1: Chuyển sang chế độ THÊM
                ThietLapCheDo("THEM");
                txtTenSanPham.Focus(); // Đưa con trỏ chuột vào ô tên
            }
            else if (cheDoHienTai == "THEM")
            {
                // Lần bấm 2: Thực hiện lưu vào xml
                ThucHienLuu();
            }
        }

        private void ThucHienLuu()
        {
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Chưa nhập tên món!");
                txtTenSanPham.Focus();
                return;
            }
            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá tiền không hợp lệ!");
                txtGia.Focus();
                return;
            }

            try
            {
                string tenFileAnh = "";

                // Nếu đang sửa và không chọn ảnh mới -> Lấy ảnh cũ
                if (cheDoHienTai == "SUA" && string.IsNullOrEmpty(duongDanFileAnhMoiDuocChon))
                {
                    tenFileAnh = dgvDoUong.SelectedRows[0].Cells["HinhAnhURL"].Value?.ToString() ?? "";
                }
                else
                {
                    // Nếu đang thêm hoặc đang sửa mà có chọn ảnh mới -> Xử lý ảnh
                    tenFileAnh = XuLyLayTenFileHinhAnh(duongDanFileAnhMoiDuocChon);
                }

                string tenSP = txtTenSanPham.Text;

                if (cheDoHienTai == "THEM")
                {
                    doUong.ThemDoUong(tenSP, gia, this.maDanhMuc, tenFileAnh);
                }
                else if (cheDoHienTai == "SUA")
                {
                    int maSP = Convert.ToInt32(dgvDoUong.SelectedRows[0].Cells["MaSanPham"].Value);

                    doUong.CapNhatDoUong(maSP, tenSP, gia, this.maDanhMuc, tenFileAnh);
                }

                LoadDuLieuVaoDataGridView(dgvDoUong);
                ThietLapCheDo("XEM");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void ThietLapCheDo(string cheDo)
        {
            cheDoHienTai = cheDo;

            // Xử lý trạng thái các ô nhập liệu (Mở khóa khi không phải chế độ XEM)
            bool choPhepNhap = (cheDo != "XEM");
            txtTenSanPham.ReadOnly = !choPhepNhap;
            txtGia.ReadOnly = !choPhepNhap;
            cmbDanhMuc.Enabled = choPhepNhap;
            btnChonAnh.Enabled = choPhepNhap;

            // Xử lý trạng thái các nút
            switch (cheDo)
            {
                case "XEM":
                    // Trạng thái bình thường
                    btnThem.Text = "Thêm";
                    btnSua.Text = "Sửa";

                    btnThem.Enabled = true;
                    btnSua.Enabled = true;

                    // Xóa trắng các ô nhập để chờ chọn dòng mới
                    XoaTrangForm();
                    break;

                case "THEM":
                    // Đang thêm: Nút Thêm biến thành Lưu, Nút Sửa bị khóa
                    btnThem.Text = "Lưu";
                    btnSua.Enabled = false;

                    // Xóa trắng form để nhập mới
                    XoaTrangForm();
                    break;

                case "SUA":
                    // Đang sửa: Nút Sửa biến thành Lưu, Nút Thêm bị khóa
                    btnSua.Text = "Lưu";
                    btnThem.Enabled = false;
                    break;
            }
        }

        // Hàm phụ để xóa trắng các ô nhập
        private void XoaTrangForm()
        {
            txtTenSanPham.Text = "";
            txtGia.Text = "";
            if (picHinhAnhSanPham.Image != null) picHinhAnhSanPham.Image.Dispose();
            picHinhAnhSanPham.Image = null;
            duongDanFileAnhMoiDuocChon = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDoUong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!");
                return;
            }

            int maSP = Convert.ToInt32(dgvDoUong.SelectedRows[0].Cells["MaSanPham"].Value);
            doUong.XoaDoUong(maSP);
            LoadDuLieuVaoDataGridView(dgvDoUong);
        }
    }
}
