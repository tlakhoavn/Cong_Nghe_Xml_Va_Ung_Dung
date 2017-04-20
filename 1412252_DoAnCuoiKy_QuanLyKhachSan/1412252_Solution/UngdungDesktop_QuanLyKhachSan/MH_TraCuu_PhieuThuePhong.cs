using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngdungDesktop_QuanLyKhachSan
{
    public partial class MH_TraCuu_PhieuThuePhong : Form
    {
        public MH_TraCuu_PhieuThuePhong()
        {
            InitializeComponent();
            KhoiDong();
        }

        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU LuuTru = new XL_LUUTRU();
        protected XL_NGHIEPVU NghiepVu = new XL_NGHIEPVU();
        protected XL_THEHIEN TheHien = new XL_THEHIEN();

        protected List<CPhieuThuePhong> DanhSach_KetQuaTimKiem = new List<CPhieuThuePhong>();

        //Kích thước của các textbox hiển thị thông tin bên phải
        protected int KH_CMND_Width = 100;
        protected int KH_CMND_Height = 30;
        protected int KH_Ten_Width = 100;
        protected int KH_Ten_Height = 30;

        //Các chỉ số cài đặt cho button hiển thị kết quả tra cứu trên panel
        protected Color Mau_PhongDaThanhToan = Color.Coral;
        protected Color Mau_PhongDangThue = Color.OrangeRed;

        protected string gl_ChuoiTruyVan = "";
        protected int Nut_Rong = 160; //Độ rộng button hiển thị trong panel kết quả
        protected int Nut_Cao = 50;  //Độ cáo button
        #endregion

        #region "Xử lý: Sự kiện khởi động
        private void KhoiDong()
        {
            TheHien.Xuat_Chuoi("", labelThongBaoTimKiem);
            this.StartPosition = FormStartPosition.CenterScreen;
            //Tải lên logo Khách sạn
            byte[] DuLieu_NhiPhan = LuuTru.DocHinh("Logo");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnImgLogo);
        }
        
        #endregion

        private void MH_TraCuu_PhieuThuePhong_Load(object sender, EventArgs e)
        {

        }     


        private void TraCuuPhieuThue_Theo_HoTen(string chuoiTruyVan)
        {
            DanhSach_KetQuaTimKiem = NghiepVu.TraCuuPhieuThue_Theo_HoTen(chuoiTruyVan);
        }
        private void TraCuuPhieuThue_Theo_NgayThangNam(string chuoiTruyVan)
        {
            DanhSach_KetQuaTimKiem = NghiepVu.TraCuuPhieuThue_Theo_NgayThangNam(chuoiTruyVan);
        }

        private void txtbHoTenKhachHang_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                gl_ChuoiTruyVan = TheHien.Nhap_Chuoi(txtbHoTenKhachHang);
                if(gl_ChuoiTruyVan.Length == 0)
                {
                    TheHien.XoaControl_Con(panelKetQuaTimKiem);
                    return;
                }
                else
                {
                    TraCuuPhieuThue_Theo_HoTen(gl_ChuoiTruyVan);
                    HienThi_KetQuaTraCuu_LenManHinh();
                }
            }
        }

        private void txtbNgayDatPhong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gl_ChuoiTruyVan = TheHien.Nhap_Chuoi(txtbNgayDatPhong);
                if (gl_ChuoiTruyVan.Length == 0)
                {
                    TheHien.XoaControl_Con(panelKetQuaTimKiem);
                    return;
                }
                else
                {
                    TraCuuPhieuThue_Theo_NgayThangNam(gl_ChuoiTruyVan);
                    HienThi_KetQuaTraCuu_LenManHinh();
                }
            }
        }

        //Sự kiện khi click vào một kết quả trong danh sách kết quả hiển thị
        private void btnPhieuThue_Click(object sender, EventArgs e)
        {
            int vitri = Int32.Parse(((Button)sender).Tag.ToString());            
            HienThongTin_PhieuThuePhong(DanhSach_KetQuaTimKiem[vitri]);
        }

        private void HienThongTin_PhieuThuePhong(CPhieuThuePhong Phieu_)
        {
            TheHien.Xuat_Chuoi(Phieu_.TenPhong, labelTenPhong);
            TheHien.Xuat_Chuoi(Phieu_.LoaiPhong, labelLoaiPhong);
            TheHien.Xuat_Chuoi(Phieu_.NgayBatDau, labelNgayBatDau);
            TheHien.Xuat_Chuoi(Phieu_.NgayDuKienTra, labelNgayDuKienTra);
            TheHien.Xuat_Chuoi(Phieu_.NgayTra, labelNgayTra);
            TheHien.Xuat_Chuoi(Phieu_.TienThuePhong+"", labelTienThuePhong);

            //Hiển thị danh sách khách hàng
            TaoTextBox_ThongTinKhachHang(Phieu_);
        }

        private void TaoTextBox_ThongTinKhachHang(CPhieuThuePhong PhieuThue)
        {
            panelKhachThuePhong.Controls.Clear();
            TextBox txtbKH_Ten, txtbKH_CMND;          

            KH_Ten_Width = (int)(0.6 * panelKhachThuePhong.Width);
            KH_CMND_Width = panelKhachThuePhong.Width - KH_Ten_Width - 5;

            int vitri = 0;
            foreach (CThongTinKhachHang ThongTin in PhieuThue.DSKhachHang)
            {
                txtbKH_CMND = TheHien.TaoTextBox(new Size(KH_CMND_Width, KH_CMND_Height), KH_Ten_Width + 5, vitri* (KH_Ten_Height +5));
                txtbKH_Ten = TheHien.TaoTextBox(new Size(KH_Ten_Width, KH_Ten_Height), 0, vitri * (KH_Ten_Height + 5));

                txtbKH_Ten.Tag = "0";
                txtbKH_Ten.Name = string.Format("txtbTenKh_{0}", 0);
                txtbKH_Ten.TabIndex = 50 + 0;

                txtbKH_Ten.Text = ThongTin.HoTen;
                txtbKH_CMND.Text = ThongTin.CMND;
                txtbKH_Ten.ReadOnly = true;
                txtbKH_CMND.ReadOnly = true;
                txtbKH_Ten.Enabled = false;
                txtbKH_CMND.Enabled = false;

                panelKhachThuePhong.Controls.Add(txtbKH_Ten);
                panelKhachThuePhong.Controls.Add(txtbKH_CMND);

                vitri++;
            }           
        }

        //Hiển thị kết quả tra cứu vào panel
        private void HienThi_KetQuaTraCuu_LenManHinh()
        {
            if(DanhSach_KetQuaTimKiem.Count ==0)
            {
                TheHien.Xuat_Chuoi("Không tìm thấy kết quả nào.", labelThongBaoTimKiem);
                TheHien.XoaControl_Con(panelKetQuaTimKiem);
            }
            else
            {
                TheHien.Xuat_Chuoi("", labelThongBaoTimKiem);
                ThemDanhSachPhieuVaoPanel();
            }
        }

        private void ThemDanhSachPhieuVaoPanel()
        {
            int Rong = panelKetQuaTimKiem.Width;
            int Trai = 0;
            int Tren = 0;          

            TheHien.XoaControl_Con(panelKetQuaTimKiem);
            for (int i=0;i<DanhSach_KetQuaTimKiem.Count;i++)
            {
                Button PhieuThue = new Button();
                PhieuThue.Name = string.Format("PhieuSo{0}",i);
                PhieuThue.Tag = string.Format("{0}", i);               

                string ChuoiGoiY = "";              
                foreach (CThongTinKhachHang KhachHang in DanhSach_KetQuaTimKiem[i].DSKhachHang)
                {
                    if(KhachHang.HoTen.ToUpper().Contains(gl_ChuoiTruyVan.ToUpper()) )
                    {
                        ChuoiGoiY += KhachHang.HoTen + ", ";
                    }
                }
                if (ChuoiGoiY.Length == 0) ChuoiGoiY = DanhSach_KetQuaTimKiem[i].DSKhachHang[0].HoTen;

                PhieuThue.Text = ChuoiGoiY; //Thay đổi sau

                PhieuThue.Size = new Size(Nut_Rong, Nut_Cao);
                PhieuThue.Location = new Point(Trai, Tren);

                if(DanhSach_KetQuaTimKiem[i].NgayTra.Trim() == "")
                {
                    PhieuThue.BackColor = Mau_PhongDangThue;
                }
                else
                {
                    PhieuThue.BackColor = Mau_PhongDaThanhToan;
                }

                if ((Rong - Trai)* 0.6  < Nut_Rong)
                {
                    Tren += Nut_Cao;
                    Trai = 0;
                }
                else
                {
                    Trai += Nut_Rong;
                }

                PhieuThue.Click += new EventHandler(btnPhieuThue_Click);                

                panelKetQuaTimKiem.Controls.Add(PhieuThue);
            }
        }
       
    }
}
