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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=DESKTOP-RLTJDOU\\DEVELYSIA;Initial Catalog=quanli_thuctap;Integrated Security=True";

        // Câu truy vấn SQL để kiểm tra thông tin đăng nhập
        string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE Taikhoan = @TaiKhoan AND MatKhau = @MatKhau";

        private bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            
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
        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text;
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string nhapLaiMatKhau = txtNhapLaiMatKhau.Text;



            // Kiểm tra xem các ô nhập liệu có trống không
            if (taiKhoan == "" || matKhauCu == "" || matKhauMoi == "" || nhapLaiMatKhau == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra xem mật khẩu mới và nhập lại mật khẩu có trùng khớp không
            if (matKhauMoi != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại mật khẩu không khớp.");
                return;
            }

            // Kiểm tra xem tài khoản và mật khẩu cũ có khớp với cơ sở dữ liệu không
            if (!KiemTraDangNhap(taiKhoan, matKhauCu))
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu cũ không chính xác.");
                return;
            }

            // Nếu tất cả thông tin đều hợp lệ, cập nhật mật khẩu mới vào cơ sở dữ liệu
           
            // Viết mã SQL hoặc gọi stored procedure để cập nhật mật khẩu mới vào cơ sở dữ liệu
            // Ví dụ (lưu ý: đây chỉ là phần minh họa, bạn cần điều chỉnh để phù hợp với cấu trúc cơ sở dữ liệu của bạn):
            string query = "UPDATE TAIKHOAN SET MatKhau = @MatKhauMoi WHERE Taikhoan = @TaiKhoan";

            // Thực hiện kết nối và thực thi truy vấn cập nhật
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    command.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công!");
                        // Thực hiện các hành động khác sau khi cập nhật mật khẩu thành công
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật mật khẩu thất bại!");
                        // Xử lý khi cập nhật thất bại (nếu cần)
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
