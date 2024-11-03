using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace do_an_nhom_8
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            DoiMatKhau form = new DoiMatKhau();
            form.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblDuLieu.Controls.Add(childForm);
            lblDuLieu.Tag = childForm;
            childForm.BringToFront();
            pictureBox1.Hide();
            childForm.Show();
        }

        private void btnQLSinhVienThucTap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SINHVIEN());
           
        }

        private void btnQLGiangVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GIANGVIEN());
        }

        private void btnHoSoSinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HSSV());
        }

        private void btnHoSoGiangVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HSGV());

        }

        private void btnNganh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NGANH());
        }

        private void btnLop_Click(object sender, EventArgs e)
        {
            OpenChildForm(new LOP());
        }

        private void btnDiem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DIEM());
        }

        private void btnNhom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NHOM());
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có thể thao tác các chức năng thêm, sửa, xóa và tìm kiếm ở mọi chức năng chính của hệ thống ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KHOA());
        }
    }
}
