using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bailam2
{
    public partial class Form1: Form
    {
        int chucnang = 0;
        string MaSpXoa = "";
        public Form1()
        {
            InitializeComponent();
        }

        string strcon = @"Data Source=ADMIN-PC\MSSQLSERVER11;Initial Catalog=QuanLiHeThong;Integrated Security=True";
        SqlConnection sqlcon = null;

        private SqlConnection MoketNoi()
        {
            try
            {
                if (sqlcon == null) sqlcon = new SqlConnection(strcon);
                if (sqlcon != null && sqlcon.State == ConnectionState.Closed) sqlcon.Open();
                return sqlcon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
                return null;
            }

        }

        private void DongKetNoi()
        {
            if(sqlcon!=null && sqlcon.State == ConnectionState.Open) sqlcon.Close();
        }

        private void HienThi_DanhSach()
        {
            sqlcon = MoketNoi();
            if (sqlcon != null)
            {
                try
                {
                    //Tao cau lenh try van
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select *from SanPham";

                    //Gan vao ket noi
                    cmd.Connection = sqlcon;

                    //thuc thi try van
                    SqlDataReader rd = cmd.ExecuteReader();
                    lsvHienThi.Items.Clear();
                    while (rd.Read())
                    {
                        //lay du lieu tu bang
                        string maSP = rd.GetString(0);
                        string tenSP = rd.GetString(1);
                        string donGia = rd.GetDecimal(2).ToString();
                        string sLNhap = rd.GetInt32(3).ToString();
                        string dVTinh = rd.GetString(4);
                        string ngaynhap = rd.GetDateTime(5).ToString("MM/dd/yyyy");
                        string ghiChu = rd.GetString(6);

                        //Gan vao lsv
                        ListViewItem lsv = new ListViewItem(maSP);
                        lsv.SubItems.Add(tenSP);
                        lsv.SubItems.Add(donGia);
                        lsv.SubItems.Add(sLNhap);
                        lsv.SubItems.Add(dVTinh);
                        lsv.SubItems.Add(ngaynhap);
                        lsv.SubItems.Add(ghiChu);

                        lsvHienThi.Items.Add(lsv);
                    }
                    rd.Close();
                    DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thong bao loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Loi ket noi!");
        }

        private bool ThemSanPham(List<string> sp)
        {
            sqlcon = MoketNoi();
            if (sqlcon != null)
            {

                    //Tao cau lenh try van
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into SanPham values('" + sp[0] + "',N'" + sp[1] + "'," + sp[2] + "," + sp[3] + ",N'" + sp[4] + "',convert(datetime,'" + sp[5] + "',111),N'" + sp[6] + "')";

                    //Gan vao ket noi
                    cmd.Connection = sqlcon;

                    int kq = cmd.ExecuteNonQuery();
                    if (kq > 0) return true;
                    else return false;
  
            }
            else
            { 
                MessageBox.Show("Loi ket noi!");
                return false;
            }
        }

        private bool XoaSanPham()
        {
            sqlcon = MoketNoi();
            if(sqlcon != null)
            {
                //Tao cau lenh try van
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from SanPham where MaSP = '"+MaSpXoa+"'";

                //Gan vao ket noi
                cmd.Connection = sqlcon;

                int kq = cmd.ExecuteNonQuery();
                if (kq > 0) return true;
                else return false;
            }
            else
            {
                MessageBox.Show("Lỗi kết nối!");
                return false;
            }
        }

        private bool SuaSanPham(List<string> sp)
        {
            sqlcon = MoketNoi();
            if (sqlcon != null)
            {
                //Tao cau lenh try van
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update SanPham set MaSP = '" + sp[0] + "', TenSP = N'" + sp[1] + "',DonGia = '" + sp[2] + "',SLNhap = " + sp[3] + ",DonVTinh = N'" + sp[4] + "',NgayNhap = CONVERT(datetime,'" + sp[5] + "',111),GhiChu = N'" + sp[6] + "' where MaSP = '" + sp[0] + "' ";

                //Gan vao ket noi
                cmd.Connection = sqlcon;

                int kq = cmd.ExecuteNonQuery();
                if (kq > 0) return true;
                else return false;
            }
            else
            {
                MessageBox.Show("Lỗi kết nối!");
                return false;
            }
        }

        private void XoaThongTin()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtGhiChu.Clear();
        }

        private void TimKiemTheoMa(string sp)
        {
            sqlcon = MoketNoi();
            if (sqlcon != null)
            {
                try
                {
                    //Tao cau lenh try van
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select *from SanPham where MaSP = '" + sp + "'";

                    //Gan vao ket noi
                    cmd.Connection = sqlcon;

                    //thuc thi try van
                    SqlDataReader rd = cmd.ExecuteReader();
                    lsvHienThi.Items.Clear();
                    while (rd.Read())
                    {
                        //lay du lieu tu bang
                        string maSP = rd.GetString(0);
                        string tenSP = rd.GetString(1);
                        string donGia = rd.GetDecimal(2).ToString();
                        string sLNhap = rd.GetInt32(3).ToString();
                        string dVTinh = rd.GetString(4);
                        string ngaynhap = rd.GetDateTime(5).ToString("MM/dd/yyyy");
                        string ghiChu = rd.GetString(6);

                        //Gan vao lsv
                        ListViewItem lsv = new ListViewItem(maSP);
                        lsv.SubItems.Add(tenSP);
                        lsv.SubItems.Add(donGia);
                        lsv.SubItems.Add(sLNhap);
                        lsv.SubItems.Add(dVTinh);
                        lsv.SubItems.Add(ngaynhap);
                        lsv.SubItems.Add(ghiChu);

                        lsvHienThi.Items.Add(lsv);
                    }
                    rd.Close();
                    DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thong bao loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Loi ket noi!");
        }
        private void TimKiemTheoTen(string sp)
        {
            sqlcon = MoketNoi();
            if (sqlcon != null)
            {
                try
                {
                    //Tao cau lenh try van
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select *from SanPham where TenSP like '%" + sp + "%'";

                    //Gan vao ket noi
                    cmd.Connection = sqlcon;

                    //thuc thi try van
                    SqlDataReader rd = cmd.ExecuteReader();
                    lsvHienThi.Items.Clear();
                    while (rd.Read())
                    {
                        //lay du lieu tu bang
                        string maSP = rd.GetString(0);
                        string tenSP = rd.GetString(1);
                        string donGia = rd.GetDecimal(2).ToString();
                        string sLNhap = rd.GetInt32(3).ToString();
                        string dVTinh = rd.GetString(4);
                        string ngaynhap = rd.GetDateTime(5).ToString("MM/dd/yyyy");
                        string ghiChu = rd.GetString(6);

                        //Gan vao lsv
                        ListViewItem lsv = new ListViewItem(maSP);
                        lsv.SubItems.Add(tenSP);
                        lsv.SubItems.Add(donGia);
                        lsv.SubItems.Add(sLNhap);
                        lsv.SubItems.Add(dVTinh);
                        lsv.SubItems.Add(ngaynhap);
                        lsv.SubItems.Add(ghiChu);

                        lsvHienThi.Items.Add(lsv);
                    }
                    rd.Close();
                    DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thong bao loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Loi ket noi!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grbThongTinChiTiet.Enabled = false;
            cbDVTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTCTK.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDVTinh.Items.AddRange(new string[] { "Chiếc","Cái","Bát" });
            cbTCTK.Items.AddRange(new string[] {"Tìm kiếm theo mã","Tìm kiếm theo tên" });
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            HienThi_DanhSach();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            HienThi_DanhSach();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc chứ?", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            grbThongTinChiTiet.Enabled = true;
            chucnang = 1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string MaSP = txtMaSP.Text.Trim();
            string TenSp = txtTenSP.Text.Trim();
            string DonGia = txtDonGia.Text.Trim();
            string SLNhap = nmudSLNhap.Value.ToString();
            string DVTinh = cbDVTinh.SelectedItem.ToString();
            string NgayNhap = dtpNgayNhap.Value.ToString("yyyy/MM/dd");
            string GhiChu = txtGhiChu.Text.Trim();

            //gan vao list tring
            List<string> sp = new List<string>();
            sp.Add(MaSP);
            sp.Add(TenSp);
            sp.Add(DonGia);
            sp.Add(SLNhap);
            sp.Add(DVTinh);
            sp.Add(NgayNhap);
            sp.Add(GhiChu);

            if(chucnang==1)
            {
                bool kq = ThemSanPham(sp);
                if(kq==true)
                {
                    MessageBox.Show("Them san pham thanh cong");
                    HienThi_DanhSach();
                    XoaThongTin();
                }
                else
                {
                    MessageBox.Show("Them san pham khong thanh cong");
                    HienThi_DanhSach();
                    XoaThongTin();
                }
            }
            else if(chucnang==2)
            {
                bool kq = SuaSanPham(sp);
                if(kq==true)
                {
                    MessageBox.Show("sua san pham thanh cong");
                    HienThi_DanhSach();
                    XoaThongTin();
                }
                else
                {
                    MessageBox.Show("sua san pham khong thanh cong");
                    HienThi_DanhSach();
                    XoaThongTin();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            grbThongTinChiTiet.Enabled = true;
            chucnang = 2;
        }

        private void lsvHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            if(lsvHienThi.SelectedItems.Count == 0) return;
            ListViewItem lsv = lsvHienThi.SelectedItems[0];

            //Gan gia tri
            txtMaSP.Text = lsv.SubItems[0].Text.Trim();
            txtTenSP.Text = lsv.SubItems[1].Text.Trim();
            txtDonGia.Text = lsv.SubItems[2].Text.Trim().Replace(",",".");
            nmudSLNhap.Value = Decimal.Parse(lsv.SubItems[3].Text.Trim());
            string DVTinh = lsv.SubItems[4].Text.Trim();
            if (DVTinh.Equals("Chiếc")) cbDVTinh.SelectedIndex = 0;
            else if (DVTinh.Equals("Cái")) cbDVTinh.SelectedIndex = 1;
            else if (DVTinh.Equals("Bát")) cbDVTinh.SelectedIndex = 2;
            dtpNgayNhap.Value = DateTime.ParseExact(lsv.SubItems[5].Text,"MM/dd/yyyy",null);
            txtGhiChu.Text = lsv.SubItems[6].Text.Trim();

            MaSpXoa = lsv.SubItems[0].Text.Trim();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn chắc muốn xóa chứ?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool kq = XoaSanPham();
                if(kq)
                {
                    MessageBox.Show("Xoa san pham thanh cong!");
                    grbThongTinChiTiet.Enabled = false;
                    HienThi_DanhSach();
                    XoaSanPham();
                }
                else
                {
                    MessageBox.Show("Xoa san pham khong thanh cong!");
                    grbThongTinChiTiet.Enabled = false;
                    HienThi_DanhSach();
                    XoaSanPham();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaThongTin();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string gt = txtGTTK.Text.Trim();
            if (gt != "")
            {
                if (cbTCTK.SelectedIndex == 0) TimKiemTheoMa(gt);
                else if (cbTCTK.SelectedIndex == 1) TimKiemTheoTen(gt);
            }
            else
            {
                MessageBox.Show("Ban chua nhap gia tri tim kiem!");
                txtGTTK.Focus();
            }
        }
    }
}
