using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCoffee.Class
{
    public class BaoCaoDoanhThuNgay
    {
        public decimal TongDoanhThu { get; set; }
        public int TongSoMon { get; set; }
        public List<ThongKeDanhMuc> ThongKeTheoDanhMuc { get; set; }

        public BaoCaoDoanhThuNgay()
        {
            TongDoanhThu = 0;
            TongSoMon = 0;
            ThongKeTheoDanhMuc = new List<ThongKeDanhMuc>();
        }
    }
}
