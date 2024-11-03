using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using COMExcel = Microsoft.Office.Interop.Excel;
namespace do_an_nhom_8
{
    public partial class GIANGVIEN : Form
    {
        public GIANGVIEN()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-RLTJDOU\\DEVELYSIA;Initial Catalog=quanli_thuctap;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select *from GIANGVIEN";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void GIANGVIEN_Load(object sender, EventArgs e)
        {

            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtMaGV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenGV.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtGioiTinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtDienThoaiGV.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtMaNganh.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into GIANGVIEN values ('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtGioiTinh.Text + "',N'" + txtDienThoaiGV.Text + "',N'" + txtMaNganh.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from GIANGVIEN where MaGV ='" + txtMaGV.Text + "'";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(str))
                {
                    connection.Open();

                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM GIANGVIEN WHERE MaGV = @magv", connection);
                    checkCommand.Parameters.AddWithValue("@magv", txtMaGV.Text);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        SqlCommand command = new SqlCommand("UPDATE GIANGVIEN SET TenGV = @tengv, GioiTinhGV = @gioitinh , DienThoaiGV = @dienthoai, MaNganh = @manganh WHERE MaGV = @magv", connection);
                        command.Parameters.AddWithValue("@magv", txtMaGV.Text);
                        command.Parameters.AddWithValue("@tengv", txtTenGV.Text);
                        command.Parameters.AddWithValue("@gioitinh", txtGioiTinh.Text);
                        command.Parameters.AddWithValue("@dienthoai", txtDienThoaiGV.Text);
                        command.Parameters.AddWithValue("@manganh", txtMaNganh.Text);
                        
                        command.ExecuteNonQuery();
                        connection.Close();
                        loaddata();

                        MessageBox.Show("Sửa thông tin thành công.", "thông báo !");
                    }
                    else
                    {
                        // hiển thị thông báo lỗi
                        MessageBox.Show("Không được sửa mã học phần.", "thông báo !");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "thông báo !");
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "select *from GIANGVIEN where MaGV ='" + txtTimKiem.Text + "'";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR TenGV  LIKE   '%" + txtTimKiem.Text + "%'";
            }

            
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR GioiTinhGV   =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR DienThoaiGV   =   '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR MaNganh    =  '" + txtTimKiem.Text + "'";
            }
            
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

       
            
        
    }
}
