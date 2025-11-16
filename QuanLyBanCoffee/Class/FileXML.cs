using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace QuanLyBanCoffee.Class
{
    class FileXml
    {
        // Chuỗi kết nối tới cơ sở dữ liệu QuanLyBanCaPhe
        private string Conn = @"Data Source=.;Initial Catalog=QuanLyBanCaPhe;Integrated Security=True;";


        // 1. Hiển thị dữ liệu từ file XML
        public DataTable HienThi(string file)
        {
            DataTable dt = new DataTable();
            string FilePath = Application.StartupPath + "\\" + file;
            if (File.Exists(FilePath))
            {
                FileStream fsReadXML = new FileStream(FilePath, FileMode.Open);
                dt.ReadXml(fsReadXML);
                fsReadXML.Close();
            }
            else
            {
                MessageBox.Show("File XML '" + file + "' không tồn tại");
            }

            return dt;
        }

        // 2. Tạo file XML từ bảng trong SQL Server
        public void TaoXML(string bang)
        {
            try
            {
                SqlConnection con = new SqlConnection(Conn);
                con.Open();
                string sql = "Select* from " + bang;
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable("'" + bang + "'");
                ad.Fill(dt);
                dt.WriteXml(Application.StartupPath + "\\" + bang + ".xml", XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo file XML cho bảng {bang}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Thêm dữ liệu vào file XML
        public void Them(string duongDan, string noiDung)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(duongDan);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode currNode;
                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = noiDung;
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
                doc.Save(duongDan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu vào file XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Xóa dữ liệu trong file XML
        public void Xoa(string duongDan, string tenBang, string xoaTheoTruong, string giaTriTruong)
        {
            try
            {
                string fileName = Application.StartupPath + "\\" + duongDan;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode nodeCu = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenBang + "_x0027_" + "[./" + xoaTheoTruong + "/text()='" + giaTriTruong + "']");
                doc.DocumentElement.RemoveChild(nodeCu);
                doc.Save(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu trong file XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. Sửa dữ liệu trong file XML
        public void Sua(string duongDan, string tenBang, string suaTheoTruong, string giaTriTruong, string noiDung)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(duongDan);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                reader.Close();
                XmlNode oldHang;
                XmlElement root = doc.DocumentElement;
                oldHang = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenBang + "_x0027_" + "[./" + suaTheoTruong + "/text()='" + giaTriTruong + "']");
                //  oldHang = root.SelectSingleNode("/NewDataSet/" + tenFile + "[" + suaTheoTruong + "='" + giaTriTruong + "']");
                XmlElement newhang = doc.CreateElement("_x0027_" + tenBang + "_x0027_");
                newhang.InnerXml = noiDung;
                root.ReplaceChild(newhang, oldHang);
                doc.Save(duongDan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa dữ liệu trong file XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Luu(string duongDan, DataTable dt)
        {
            try
            {
                dt.WriteXml(duongDan, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu vào file XML: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 6. Lấy giá trị từ file XML (Đã tối ưu hóa)
        public string LayGiaTri(string duongDan, string truongA, string giaTriA, string truongB)
        {
            string giatriB = "";
            DataTable dt = new DataTable();
            dt = HienThi(duongDan);
            int soDongNhanVien = dt.Rows.Count;
            for (int i = 0; i < soDongNhanVien; i++)
            {
                if (dt.Rows[i][truongA].ToString().Trim().Equals(giaTriA))
                {
                    giatriB = dt.Rows[i][truongB].ToString();
                    return giatriB;
                }
            }
            return giatriB;
        }

        public void DoiMatKhau(string nguoiDung, string matKhau)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(Application.StartupPath + "\\TAIKHOAN.xml");
            XmlNode node1 = doc1.SelectSingleNode("NewDataSet/TaiKhoan[MaNhanVien = '" + nguoiDung + "']");
            if (node1 != null)
            {
                node1.ChildNodes[1].InnerText = matKhau;
                doc1.Save(Application.StartupPath + "\\TaiKhoan.xml");
            }
        }

        // Thực thi câu lệnh SQL (Insert, Update, Delete)
        public void InsertOrUpdateSQL(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thực thi SQL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 9. Đồng bộ dữ liệu từ file XML về SQL Server (Sử dụng SqlBulkCopy)
        public void DongBoSQL(string tenBang, string duongDan)
        {
            try
            {
                DataTable dt = HienThi(duongDan);
                if (dt.Rows.Count == 0) return; // Không có dữ liệu để đồng bộ

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Conn))
                {
                    bulkCopy.DestinationTableName = tenBang;
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đồng bộ XML về SQL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}