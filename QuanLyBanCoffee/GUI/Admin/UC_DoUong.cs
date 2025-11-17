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
        //Theo dõi trạng thái ( đang xem hay đang sửa )
        private bool isEditing = false;

        public UC_DoUong()
        {
            InitializeComponent();
        }

        private void UC_DoUong_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMuc();
            SetupDataGridView(dgvDoUong);
            LoadDuLieuVaoDataGridView(dgvDoUong);

            //Ban đầu ở chế độ chỉ xem
            ToggleEditMode(false);
        }

        private void dgvDoUong_SelectionChanged(object sender, EventArgs e)
        {
            // Đảm bảo rằng có hàng đang được chọn và đó không phải là hàng trống
            if (dgvDoUong.SelectedRows.Count > 0)
            {
                // Lấy hàng hiện tại đang được chọn
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
            if (isEditing)
            {
                LuuThayDoi();
                LoadDuLieuVaoDataGridView(dgvDoUong);
            }
            else
            {
                if (dgvDoUong.SelectedRows.Count > 0)
                {
                    ToggleEditMode(true);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm để sửa.");
                }
            }
        }

        private void LuuThayDoi()
        {
            try
            {
                if (dgvDoUong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào được chọn.", "Lỗi");
                    return;
                }

                int maSanPham = Convert.ToInt32(dgvDoUong.SelectedRows[0].Cells["MaSanPham"].Value);
                string tenMoi = txtTenSanPham.Text;

                if (!decimal.TryParse(txtGia.Text, out decimal giaMoi))
                {
                    MessageBox.Show("Giá không hợp lệ.", "Lỗi");
                    return;
                }

                int maDanhMucMoi = Convert.ToInt32(cmbDanhMuc.SelectedValue);

                string tenFileAnhDeLuu;
                if (!string.IsNullOrEmpty(duongDanFileAnhMoiDuocChon))
                {
                    tenFileAnhDeLuu = Path.GetFileName(duongDanFileAnhMoiDuocChon);
                    string thuMucDich = Path.Combine(Application.StartupPath, "HinhAnh");
                    Directory.CreateDirectory(thuMucDich);

                    string duongDanDich = Path.Combine(thuMucDich, tenFileAnhDeLuu);

                    if (File.Exists(duongDanFileAnhMoiDuocChon))
                    {
                        File.Copy(duongDanFileAnhMoiDuocChon, duongDanDich, true);
                    }
                    else
                    {
                        MessageBox.Show("File ảnh không tồn tại.", "Lỗi");
                        return;
                    }
                }
                else
                {
                    tenFileAnhDeLuu = dgvDoUong.SelectedRows[0].Cells["HinhAnhURL"].Value?.ToString() ?? "";
                }

                doUong.CapNhatDoUong(maSanPham, tenMoi, giaMoi, maDanhMucMoi, tenFileAnhDeLuu);

                duongDanFileAnhMoiDuocChon = "";
                ToggleEditMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}");
            }
        }


        // Bật hoặc tắt chế độ chỉnh sửa cho các control
        private void ToggleEditMode(bool editMode)
        {
            txtTenSanPham.ReadOnly = !editMode;
            txtGia.ReadOnly = !editMode;

            cmbDanhMuc.Enabled = editMode;
            btnChonAnh.Enabled = editMode;

            btnSua.Text = editMode ? "Lưu" : "Sửa";

            isEditing = editMode;
        }
    }
}
