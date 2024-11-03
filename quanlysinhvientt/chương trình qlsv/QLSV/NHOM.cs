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
    public partial class NHOM : Form
    {
        public NHOM()
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
            command.CommandText = "select *from NHOM";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void NHOM_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into NHOM values ('" + txtMaNhom.Text + "',N'" + txtTenDT.Text + "',N'" + txtMaGV.Text + "',N'" + txtThoiGianTT.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtMaNhom.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenDT.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtMaGV.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtThoiGianTT.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from NHOM where MaNhom ='" + txtMaNhom.Text + "'";
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

                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM NHOM WHERE MaNhom = @manhom", connection);
                    checkCommand.Parameters.AddWithValue("@manhom", txtMaNhom.Text);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        SqlCommand command = new SqlCommand("UPDATE NHOM SET TenDT = @tendt, MaGV = @magv , ThoiGianTT = @thoigiantt WHERE MaNhom = @manhom", connection);
                        command.Parameters.AddWithValue("@manhom", txtMaNhom.Text);
                        command.Parameters.AddWithValue("@tendt", txtTenDT.Text);
                        command.Parameters.AddWithValue("@magv", txtMaGV.Text);
                        command.Parameters.AddWithValue("@thoigiantt", txtThoiGianTT.Text);
                        

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
            command.CommandText = "select *from NHOM where MaNhom ='" + txtTimKiem.Text + "'";


            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR MaGV  =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR TenDT  LIKE   '%" + txtTimKiem.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR ThoiGianTT  LIKE   '%" + txtTimKiem.Text + "%'";
            }
            

            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
