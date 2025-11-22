using QuanLyBanCoffee.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI.Admin
{
    public partial class UC_NhanVien : UserControl
    {

        private NhanVien nhanVien = new NhanVien();

        // Biến lưu trạng thái hiện tại: "XEM", "THEM", "SUA"
        private string cheDoHienTai = "XEM";
        private int maNV = -1;
        private int maNV_DangNhap = -1;

        public UC_NhanVien(int maNV_DangNhap) : this()
        {
            this.maNV_DangNhap = maNV_DangNhap;
        }

        public UC_NhanVien()
        {
            InitializeComponent();
        }

        private void UC_NhanVien_Load(object sender, EventArgs e)
        {
            SetupDataGridView(dgvNhanVien);
            LoadDuLieu();
            ThietLapCheDo("XEM");
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            dgv.Columns.Clear();

            dgv.AutoGenerateColumns = false; // Tắt tự động sinh cột để tự chỉnh
            dgv.AllowUserToAddRows = false;  // Không cho user tự thêm dòng trực tiếp
            dgv.ReadOnly = true;            
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 

            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNhanVien",
                DataPropertyName = "MaNhanVien",
                HeaderText = "Mã NV",
                Visible = false
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNhanVien",
                DataPropertyName = "TenNhanVien",
                HeaderText = "Họ và Tên"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GioiTinh",
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới Tính",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgaySinh",
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày Sinh",
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SDT",
                DataPropertyName = "SDT",
                HeaderText = "Số điện thoại"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                DataPropertyName = "DiaChi",
                HeaderText = "Địa Chỉ"
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiTaiKhoan",
                DataPropertyName = "LoaiTaiKhoan",
                HeaderText = "Vai trò"
            });

            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "TrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaTaiKhoan",
                DataPropertyName = "MaTaiKhoan",
                HeaderText = "Mã tài khoản",
                Visible = false
            });

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40;
        }

        private void LoadDuLieu()
        {
            try
            {
                DataTable dt = nhanVien.LayDanhSachNhanVien();

                if (dt != null)
                {
                    dgvNhanVien.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tải được dữ liệu nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (cheDoHienTai == "THEM") return;

            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                HienThiChiTietTuRow(selectedRow);
                if (selectedRow.Cells["MaNhanVien"].Value != null)
                {
                    maNV = Convert.ToInt32(selectedRow.Cells["MaNhanVien"].Value);
                }
            }
        }

        private void HienThiChiTietTuRow(DataGridViewRow selectedRow)
        {
            try
            {
                if (selectedRow == null || selectedRow.Cells["MaNhanVien"].Value == null) return;

                string tenNV = selectedRow.Cells["TenNhanVien"].Value?.ToString() ?? "";
                string gioiTinh = selectedRow.Cells["GioiTinh"].Value?.ToString() ?? "";
                string sdt = selectedRow.Cells["SDT"].Value?.ToString() ?? "";
                string diaChi = selectedRow.Cells["DiaChi"].Value?.ToString() ?? "";
                string chucVu = selectedRow.Cells["LoaiTaiKhoan"].Value?.ToString() ?? "";

                DateTime ngaySinh = DateTime.Now; 
                if (selectedRow.Cells["NgaySinh"].Value != DBNull.Value && selectedRow.Cells["NgaySinh"].Value != null)
                {
                    ngaySinh = Convert.ToDateTime(selectedRow.Cells["NgaySinh"].Value);
                }

                bool trangThai = false;

                if (selectedRow.Cells["TrangThai"].Value != DBNull.Value && selectedRow.Cells["TrangThai"].Value != null)
                {
                    trangThai = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);
                }

                txtHoTen.Text = tenNV;
                txtSDT.Text = sdt;
                txtDiaChi.Text = diaChi;
                dtpNgaySinh.Value = ngaySinh;
                cmbVaiTro.Text = chucVu;
                cbTrangThai.Checked = trangThai;
                cmbGioiTinh.Text = gioiTinh;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }

        private void ThucHienLuu()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Chưa nhập họ tên!");
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Chưa nhập số điện thoại!");
                txtSDT.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Chưa nhập địa chỉ!");
                txtDiaChi.Focus();
                return;
            }

            try
            {
                string hoTen = txtHoTen.Text.Trim();
                string gioiTinh = cmbGioiTinh.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string vaiTro = cmbVaiTro.Text;
                bool trangThai = cbTrangThai.Checked;

                if (cheDoHienTai == "THEM")
                {
                    if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                    {
                        MessageBox.Show("Chưa nhập tên đăng nhập!");
                        txtTenDangNhap.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        MessageBox.Show("Chưa nhập mật khẩu!");
                        txtMatKhau.Focus();
                        return;
                    }

                    string tenDangNhap = txtTenDangNhap.Text.Trim();
                    string matKhau = txtMatKhau.Text.Trim();

                    nhanVien.ThemNhanVienVaTaiKhoanMoi(hoTen, gioiTinh, ngaySinh, sdt, diaChi, tenDangNhap, matKhau, vaiTro, trangThai);

                }
                else if (cheDoHienTai == "SUA")
                {
                    nhanVien.CapNhatThongTinNhanVien(maNV ,hoTen, gioiTinh, ngaySinh, sdt, diaChi, vaiTro, trangThai);
                }

                LoadDuLieu();
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
            txtHoTen.ReadOnly = !choPhepNhap;
            txtDiaChi.ReadOnly = !choPhepNhap;
            txtMatKhau.ReadOnly = !choPhepNhap;
            txtSDT.ReadOnly = !choPhepNhap;
            txtTenDangNhap.ReadOnly = !choPhepNhap;
            cmbGioiTinh.Enabled = choPhepNhap;
            cmbVaiTro.Enabled = choPhepNhap;
            cbTrangThai.Enabled = choPhepNhap;
            dtpNgaySinh.Enabled = choPhepNhap;

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
                    txtTenDangNhap.ReadOnly = true;
                    txtMatKhau.ReadOnly = true;
                    break;
            }
        }

        // Hàm phụ để xóa trắng các ô nhập
        private void XoaTrangForm()
        {
            txtTenDangNhap.Clear();
            txtSDT.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cheDoHienTai == "XEM")
            {
                // Lần bấm 1: Chuyển sang chế độ THÊM
                ThietLapCheDo("THEM");
                txtHoTen.Focus(); // Đưa con trỏ chuột vào ô tên
            }
            else if (cheDoHienTai == "THEM")
            {
                // Lần bấm 2: Thực hiện lưu vào xml
                ThucHienLuu();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cheDoHienTai == "XEM")
            {
                if (dgvNhanVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                    return;
                }

                // Lần bấm 1: Chuyển sang chế độ SỬA
                ThietLapCheDo("SUA");
                txtHoTen.Focus();
            }
            else if (cheDoHienTai == "SUA")
            {
                // Lần bấm 2: Thực hiện LƯU (Update)
                ThucHienLuu();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maNV == maNV_DangNhap)
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!");
                return;
            }
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }
            int maTaiKhoan = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaTaiKhoan"].Value);

            nhanVien.XoaNhanVienTheoMa(maNV, maTaiKhoan);
            LoadDuLieu();
        }
    }
}
