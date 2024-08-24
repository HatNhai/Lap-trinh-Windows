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

namespace Quản_lý_khách_hàng_trong_khách_sạn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=NHAIZU\\SQLEXPRESS;Initial Catalog=QLKHACHHANG;Integrated Security=True";
        SqlConnection conn = null;
        SqlDataAdapter daKhachhang;
        DataTable dtKhachhang;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS STT, * FROM KHACHHANG";
            daKhachhang = new SqlDataAdapter(sql, conn);
            dtKhachhang = new DataTable();
            daKhachhang.Fill(dtKhachhang);
            dgKhachhang.DataSource = dtKhachhang;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoten = txtHoten.Text;
            string gioitinh = "";
            if (rdoNam.Checked)
            {
                gioitinh = "Nam";
            }
            else if (rdoNu.Checked)
            {
                gioitinh = "Nu";
            }
            string loaiphong = cboLoai.Text;
            string sophong = txtSo.Text;

            string sql = "INSERT INTO KHACHHANG VALUES('" + hoten + "', '" + gioitinh + "', '" + loaiphong + "', '" + sophong + "' )";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            dtKhachhang.Clear();
            daKhachhang.Fill(dtKhachhang);

        }
        int vitrichon = -1;
        private void dgKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vitrichon = e.RowIndex;
            if(vitrichon >= 0)
            {
                txtHoten.Text = dtKhachhang.Rows[vitrichon]["HOTEN"].ToString();
                string gioitinh = "";
                if (rdoNam.Checked)
                {
                    gioitinh = dtKhachhang.Rows[vitrichon]["GIOITINH"].ToString();
                } else if (rdoNu.Checked)
                {
                    gioitinh = dtKhachhang.Rows[vitrichon]["GIOITINH"].ToString();
                }
                cboLoai.Text = dtKhachhang.Rows[vitrichon]["LOAIPHONG"].ToString();
                txtSo.Text = dtKhachhang.Rows[vitrichon]["SOPHONGTHUE"].ToString();
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(vitrichon >= 0)
            {
                string hoten = dtKhachhang.Rows[vitrichon]["HOTEN"].ToString();
                string sql = "DELETE FROM KHACHHANG WHERE HOTEN = '" + hoten + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dtKhachhang.Clear();
                daKhachhang.Fill(dtKhachhang);
            }
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (vitrichon >= 0)
            {
                string hoten = dtKhachhang.Rows[vitrichon]["HOTEN"].ToString();
                string gioitinh = "";
                if (rdoNam.Checked)
                {
                    gioitinh = "Nam";
                }
                else if (rdoNu.Checked)
                {
                    gioitinh = "Nu";
                }
                string loaiphong = cboLoai.Text;
                string sophong = txtSo.Text;

                string sql = "UPDATE KHACHHANG SET GIOITINH = '"+gioitinh+"', LOAIPHONG = '"+loaiphong+"', SOPHONGTHUE = '"+sophong+"' WHERE HOTEN = '"+hoten+"' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                dtKhachhang.Clear();
                daKhachhang.Fill(dtKhachhang);
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTimkiem.Text;
            string sql = "SELECT * FROM KHACHHANG WHERE HOTEN LIKE '%" + tukhoa + "%'";
            daKhachhang = new SqlDataAdapter(sql, conn);
            dtKhachhang = new DataTable();
            daKhachhang.Fill(dtKhachhang);
            dgKhachhang.DataSource = dtKhachhang;
        }
    }
}
