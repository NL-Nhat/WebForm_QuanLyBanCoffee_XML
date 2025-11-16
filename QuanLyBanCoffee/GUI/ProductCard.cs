using System;
using System.Windows.Forms;

namespace QuanLyBanCoffee.GUI
{
    public partial class pcMon : UserControl
    {

        // Thêm các thuộc tính để lưu trữ dữ liệu sản phẩm
        public int MaSanPham { get; set; }
        public decimal DonGia { get; set; }
        public string TenMonAn { get; set; }

        // Khai báo sự kiện công khai
        public event EventHandler MonAn_Click;

        public pcMon()
        {
            InitializeComponent();
            this.Margin = new Padding(5);
            // Đảm bảo rằng khi User Control hoặc bất kỳ thành phần nào bên trong được click, 
            // sự kiện MonAn_Click sẽ được kích hoạt.
            this.Click += pcMon_Click;
            pcbMon.Click += pcMon_Click;
            tenMon.Click += pcMon_Click;
            giaMon.Click += pcMon_Click;
        }

        private void pcMon_Load(object sender, EventArgs e)
        {

        }

        public PictureBox AnhMonAn // Đặt tên thuộc tính rõ ràng hơn
        {
            get { return pcbMon; }
        }

        public Label tenMon // Đặt tên thuộc tính rõ ràng hơn
        {
            get { return lbTenMon; }
        }
        public Label giaMon // Đặt tên thuộc tính rõ ràng hơn
        {
            get { return lbGiaMon; }
        }
       

        private void pcMon_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện công khai để frmOrder bắt được
            if (MonAn_Click != null)
            {
                MonAn_Click(this, e);
            }
        }
    }
}
