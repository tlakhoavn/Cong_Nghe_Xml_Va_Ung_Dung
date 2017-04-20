using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace UngdungDesktop_QuanLyKhuVuc
{    
    public partial class MH_DangNhap : Form
    {
        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU Luu_tru = new XL_LUUTRU();
        protected XL_NGHIEPVU Nghiep_vu = new XL_NGHIEPVU();
        protected XL_THEHIEN The_hien = new XL_THEHIEN();

        protected string Ten_Dang_Nhap;
        protected string Mat_Khau;

        public int NV_ID;
        public string NV_MaKhuVuc;

        #endregion
        public MH_DangNhap()
        {
            InitializeComponent();
            Khoi_Dong();

        }

        #region "Sự kiện khởi động"
        private void Khoi_Dong()
        {
            //Cho màn hình căn giữa
            this.StartPosition = FormStartPosition.CenterScreen;
            //Tải lên logo Khách sạn
            TaiLogoKhachSan();
        }

        private void TaiLogoKhachSan()
        {
            byte[] NhiPhan_Logo = Luu_tru.DocHinh("logo");
            The_hien.Xuat_Hinh(NhiPhan_Logo, btnImgLogo);
        }

        private void MH_DangNhap_Load(object sender, EventArgs e)
        {
            btnImgLogo.FlatStyle = FlatStyle.Flat;
            btnImgLogo.FlatAppearance.BorderSize = 0;
        }

        #endregion
        #region "Sự kiện yêu cầu đăng nhập"   
        //Cách 1: Click button Gửi 
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ThucHienDangNhap();
        }
        //Cách 2: Enter tại textbox Mật khẩu
        private void txtbMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    ThucHienDangNhap();
                    break;
            }
        }

        //Thực hiện đăng nhập tài khoản
        private void ThucHienDangNhap()
        {
            Ten_Dang_Nhap = The_hien.Nhap_Chuoi(txtbTenDangNhap);
            Mat_Khau = The_hien.Nhap_Chuoi(txtbMatKhau);

            if (Nghiep_vu.DangNhapThanhCong(Ten_Dang_Nhap, Mat_Khau, ref NV_ID, ref NV_MaKhuVuc))
            {
                MH_Chinh_ThongKeDoanhThu MH_ = new MH_Chinh_ThongKeDoanhThu();
                //MH_.TruyenDuLieu(this, NV_ID, NV_MaKhuVuc);
                MH_.Show();
                this.Hide();
            }
            else MessageBox.Show("Đăng nhập thất bại");
        }
        #endregion
       
    }
}
