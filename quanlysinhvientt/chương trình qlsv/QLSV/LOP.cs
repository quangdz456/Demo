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
    public partial class LOP : Form
    {
        public LOP()
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
            command.CommandText = "select *from LOP";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void LOP_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtMaLop.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenLop.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtMaKhoa.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from LOP where MaLop ='" + txtMaLop.Text + "'";
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

                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM LOP WHERE MaLop = @malop", connection);
                    checkCommand.Parameters.AddWithValue("@malop", txtMaLop.Text);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        SqlCommand command = new SqlCommand("UPDATE LOP SET TenLop = @tenlop, MaKhoa = @makhoa  WHERE MaLop = @malop", connection);
                        command.Parameters.AddWithValue("@malop", txtMaLop.Text);
                        command.Parameters.AddWithValue("@tenlop", txtTenLop.Text);
                        command.Parameters.AddWithValue("@makhoa", txtMaKhoa.Text);
                        

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
            command.CommandText = "select *from LOP where MaLop ='" + txtTimKiem.Text + "'";


            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR MaKhoa   =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR TenLop  LIKE   '%" + txtTimKiem.Text + "%'";
            }
            

            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into LOP values ('" + txtMaLop.Text + "',N'" + txtTenLop.Text + "',N'" + txtMaKhoa.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }
    }
}
