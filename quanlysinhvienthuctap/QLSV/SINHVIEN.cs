using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel= Microsoft.Office.Interop.Excel;

namespace do_an_nhom_8
{
    public partial class SINHVIEN : Form
    {
        public SINHVIEN()
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
            command.CommandText = "select *from SINHVIEN";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void SINHVIEN_Load(object sender, EventArgs e)
        {

            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtMaSV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTenSV.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtNgaySinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtDiaChi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtGioiTinh.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtDienThoaiSV.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            txtMaLop.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            txtMaNhom.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            command = connection.CreateCommand();
            command.CommandText = "insert into SINHVIEN values ('" + txtMaSV.Text + "',N'" + txtTenSV.Text + "','" + txtNgaySinh.Text + "',N'" + txtDiaChi.Text + "',N'" + txtGioiTinh.Text + "',N'" + txtDienThoaiSV.Text + "',N'" + txtMaLop.Text + "',N'" + txtMaNhom.Text + "')";
            command.ExecuteNonQuery();
            loaddata();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from SINHVIEN where MaSV ='" + txtMaSV.Text + "'";
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
                    
                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM SINHVIEN WHERE MaSV = @masv", connection);
                    checkCommand.Parameters.AddWithValue("@masv", txtMaSV.Text);

                    int existingRecords = (int)checkCommand.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        SqlCommand command = new SqlCommand("UPDATE SINHVIEN SET TenSV = @tensv, NgaySinh = @ngaysinh , DiaChi = @diachi, GioiTinh = @gioitinh, DienThoaiSV=@dienthoaisv, MaLop=@malop, MaNhom=@manhom WHERE MaSV = @masv", connection);
                        command.Parameters.AddWithValue("@masv", txtMaSV.Text);
                        command.Parameters.AddWithValue("@tensv", txtTenSV.Text);
                        command.Parameters.AddWithValue("@ngaysinh", txtNgaySinh.Text);
                        command.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                        command.Parameters.AddWithValue("@gioitinh", txtGioiTinh.Text);
                        command.Parameters.AddWithValue("@dienthoaisv", txtDienThoaiSV.Text);
                        command.Parameters.AddWithValue("@malop", txtMaLop.Text);
                        command.Parameters.AddWithValue("@manhom", txtMaNhom.Text);
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
            command.CommandText = "select *from SINHVIEN where MaSV ='" + txtTimKiem.Text + "'";
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR TenSV  LIKE   '%" + txtTimKiem.Text + "%'";
            }

            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR DiaChi   LIKE   '%" + txtTimKiem.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR GioiTinh   =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR DienThoaiSV   =   '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR MaLop    =  '" + txtTimKiem.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtTimKiem.Text))
            {
                command.CommandText += " OR MaNhom   =   '" + txtTimKiem.Text + "'";
            }
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void AdjustColumnWidth(COMExcel.Worksheet worksheet, int columnCount)
        {
            for (int i = 1; i <= columnCount; i++)
            {
                worksheet.Columns[i].AutoFit();
            }
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            COMExcel.Application xlApp;
            COMExcel.Workbook xlWorkBook;
            COMExcel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                xlApp = new COMExcel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (COMExcel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                xlWorkSheet.Cells[1, 1] = "Mã Sinh Viên";
                xlWorkSheet.Cells[1, 2] = "Tên Sinh Viên";
                xlWorkSheet.Cells[1, 3] = "Ngày Sinh";
                xlWorkSheet.Cells[1, 4] = "Địa Chỉ";
                xlWorkSheet.Cells[1, 5] = "Giới Tính"; 
                xlWorkSheet.Cells[1, 6] = "Điện Thoại SV";
                xlWorkSheet.Cells[1, 7] = "Mã Lớp";
                xlWorkSheet.Cells[1, 2] = "Mã Nhóm";


                // Ghi tên cột

                AdjustColumnWidth(xlWorkSheet, dataGridView.Columns.Count);
                xlWorkBook.SaveAs(filePath, COMExcel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                    COMExcel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = "SINHVIEN";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(dataGridView1, saveDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
