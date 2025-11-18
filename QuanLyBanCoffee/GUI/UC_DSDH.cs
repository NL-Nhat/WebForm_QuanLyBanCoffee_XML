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
    public partial class UC_DSDH : UserControl
    {

        private Order order = new Order();
        private DataTable dtDSDH = new DataTable();
        private int selectedMaOrder = -1;

        public UC_DSDH()
        {
            InitializeComponent();
        }

        private void UC_DSDH_Load(object sender, EventArgs e)
        {
            dtDSDH = order.LayDonHangDaThanhToan();
            SetupDisplayDataGridView();
            dgvDSDH.DataSource = dtDSDH;

            dgvDSDH.CellClick += dgvDSDH_CellClick;
        }

        private void SetupDisplayDataGridView()
        {
            if (dgvDSDH.Columns.Count == 0)
            {
                dgvDSDH.AutoGenerateColumns = false;
                dgvDSDH.Columns.Clear();

                var colMaOrder = new DataGridViewTextBoxColumn()
                {
                    Name = "MaOrder",
                    HeaderText = "Mã Order",
                    DataPropertyName = "MaOrder",
                    Visible = true
                };
                dgvDSDH.Columns.Add(colMaOrder);

                dgvDSDH.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "TenBan",
                    HeaderText = "Tên Bàn",
                    DataPropertyName = "TenBan",
                });

                dgvDSDH.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "TenTang",
                    HeaderText = "Tên Tầng",
                    DataPropertyName = "TenTang",
                });

                var colTongTien = new DataGridViewTextBoxColumn()
                {
                    Name = "TongTien",
                    HeaderText = "Tổng Tiền",
                    DataPropertyName = "TongTien",
                };
                colTongTien.DefaultCellStyle.Format = "N0";
                colTongTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDSDH.Columns.Add(colTongTien);

                dgvDSDH.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "SoLuong",
                    HeaderText = "Số Món",
                    DataPropertyName = "SoLuongMon",
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                var colThoiGian = new DataGridViewTextBoxColumn()
                {
                    Name = "ThoiGianThanhToan",
                    HeaderText = "Thời Gian Thanh Toán",
                    DataPropertyName = "ThoiGianThanhToan",
                    Width = 180,
                    DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
                };
                colThoiGian.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
                dgvDSDH.Columns.Add(colThoiGian);

                var colChietKhau = new DataGridViewTextBoxColumn()
                {
                    Name = "ChietKhau",
                    HeaderText = "Chiết Khấu",
                    DataPropertyName = "ChietKhau",
                };
                colChietKhau.DefaultCellStyle.Format = "P0"; 
                colChietKhau.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDSDH.Columns.Add(colChietKhau);

                dgvDSDH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDSDH.MultiSelect = false;
                dgvDSDH.EnableHeadersVisualStyles = false;
                dgvDSDH.AllowUserToAddRows = false;
                dgvDSDH.ReadOnly = true;
                dgvDSDH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dgvDSDH.EnableHeadersVisualStyles = false; 
                dgvDSDH.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow; 
                dgvDSDH.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dgvDSDH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold); 
                dgvDSDH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; 
                dgvDSDH.ColumnHeadersHeight = 40; 

                foreach (DataGridViewColumn col in dgvDSDH.Columns)
                {
                    if (col.Name != "ThoiGianThanhToan" && col.Visible)
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void dgvDSDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // bỏ qua header

            var row = dgvDSDH.Rows[e.RowIndex];

            var cellValue = row.Cells["MaOrder"].Value;

            // Nếu null hoặc rỗng thì không làm gì
            if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return;
            }

            // Chuyển sang int an toàn
            if (int.TryParse(cellValue.ToString(), out int maOrder))
            {
                selectedMaOrder = maOrder;
                MessageBox.Show($"Đã chọn Mã Order: {selectedMaOrder}", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Giá trị Mã Order không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
