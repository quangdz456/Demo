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
    public partial class DIEM : Form
    {
        public DIEM()
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
            command.CommandText = "select *from BANGDIEM";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into BANGDIEM values ('" + txtMaSV.Text + "',N'" + txtDiem.Text + "','" + txtDotTT.Text + "',N'" + txtThoiGianTT.Text + "',N'" + txtGhiChu.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void DIEM_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtMaSV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtDiem.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtDotTT.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtThoiGianTT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtGhiChu.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from BANGDIEM where MaSV ='" + txtMaSV.Text + "'";
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

                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM BANGDIEM WHERE MaSV = @masv", connection);
                    checkCommand.Parameters.AddWithValue("@masv", txtMaSV.Text);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        SqlCommand command = new SqlCommand("UPDATE BANGDIEM SET Diem = @diem, DotTT = @dottt , ThoiGianTT = @thoigiantt,GhiChu = @ghichu WHERE MaSV = @masv", connection);
                        command.Parameters.AddWithValue("@masv", txtMaSV.Text);
                        command.Parameters.AddWithValue("@diem", txtDiem.Text);
                        command.Parameters.AddWithValue("@dottt", txtDotTT.Text);
                        command.Parameters.AddWithValue("@thoigiantt", txtThoiGianTT.Text);
                        command.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);

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
            command.CommandText = "select *from BANGDIEM where MaSV ='" + txtTimKiem.Text + "'";
            

            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR Diem   =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR DotTT  LIKE   '%" + txtTimKiem.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR ThoiGianTT  LIKE   '%" + txtTimKiem.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR GhiChu  LIKE   '%" + txtTimKiem.Text + "%'";
            }

            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
