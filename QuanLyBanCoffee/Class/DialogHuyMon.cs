using System.Windows.Forms;

namespace QuanLyBanCoffee.Class
{
    public static class DialogHuyMon
    {
        public static ResultOfDialogHuyMon Show(int soLuongHienCo)
        {
            Form prompt = new Form()
            {
                Width = 260,
                Height = 260,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Hủy món"
            };

            Label lbSoLuong = new Label()
            {
                Left = 20,
                Top = 20,
                AutoSize = true,
                Text = $"Số lượng order: {soLuongHienCo}"
            };

            TextBox txtSoLuong = new TextBox()
            {
                Left = 20,
                Top = 50,
                Width = 200
            };

            Label lbLyDo = new Label()
            {
                Left = 20,
                Top = 85,
                AutoSize = true,
                Text = $"Lý do hủy"
            };

            TextBox txtLyDo = new TextBox()
            {
                Left = 20,
                Top = 110,
                Width = 200
            };

            Button ok = new Button()
            {
                Text = "OK",
                Left = 40,
                Width = 65,
                Top = 160,
                DialogResult = DialogResult.None
            };

            Button cancel = new Button()
            {
                Text = "Hủy",
                Left = 130,
                Width = 65,
                Top = 160,
                DialogResult = DialogResult.Cancel
            };

            ok.Click += (sender, e) =>
            {
                if (!int.TryParse(txtSoLuong.Text, out int soLuongHuy))
                {
                    MessageBox.Show("Số lượng không hợp lệ.");
                    return;
                }

                if (soLuongHuy <= 0)
                {
                    MessageBox.Show("Số lượng hủy phải lớn hơn 0.");
                    return;
                }

                if (soLuongHuy > soLuongHienCo)
                {
                    MessageBox.Show("Không thể hủy nhiều hơn số lượng order.");
                    return;
                }

                ResultOfDialogHuyMon result = new ResultOfDialogHuyMon()
                {
                    SoLuongHuy = soLuongHuy,
                    LyDo = txtLyDo.Text
                };

                prompt.Tag = result;
                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            };

            prompt.Controls.Add(lbSoLuong);
            prompt.Controls.Add(txtSoLuong);
            prompt.Controls.Add(lbLyDo);
            prompt.Controls.Add(txtLyDo);
            prompt.Controls.Add(ok);
            prompt.Controls.Add(cancel);

            return prompt.ShowDialog() == DialogResult.OK
                ? (ResultOfDialogHuyMon)prompt.Tag
                : null;
        }
    }

}
