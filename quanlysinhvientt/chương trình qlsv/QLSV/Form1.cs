using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace do_an_nhom_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnDangNhap;
        }
        private bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = "Data Source=DESKTOP-RLTJDOU\\DEVELYSIA;Initial Catalog=quanli_thuctap;Integrated Security=True";

            // Câu truy vấn SQL để kiểm tra thông tin đăng nhập
            string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE Taikhoan = @TaiKhoan AND MatKhau = @MatKhau";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);

                    connection.Open();
                    int count = (int)command.ExecuteScalar(); // Lấy số lượng bản ghi trả về từ truy vấn

                    // Nếu có bản ghi khớp
                    if (count > 0)
                    {
                        return true; // Đăng nhập thành công
                    }
                }
            }

            return false; // Đăng nhập thất bại
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;

            if (KiemTraDangNhap(taiKhoan, matKhau))
            {
                // Thực hiện các hành động khi đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!");
                TrangChu  form2 =new TrangChu();
                form2.Show();
            }
            else
            {
                // Thông báo đăng nhập thất bại
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra kết quả từ hộp thoại xác nhận
            if (result == DialogResult.Yes)
            {
                // Đóng ứng dụng
                Application.Exit();
            }
        }
    }
}
