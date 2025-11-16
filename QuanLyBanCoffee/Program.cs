using QuanLyBanCoffee.Class;
using System;
using System.Windows.Forms;

namespace QuanLyBanCoffee
{
    internal static class Program
    {
        public static string CurrentUser { get; set; }
        public static string UserRole { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HeThong ht = new HeThong();
            ht.TaoXML();
            Application.Run(new frmMain());
        }
    }
}
