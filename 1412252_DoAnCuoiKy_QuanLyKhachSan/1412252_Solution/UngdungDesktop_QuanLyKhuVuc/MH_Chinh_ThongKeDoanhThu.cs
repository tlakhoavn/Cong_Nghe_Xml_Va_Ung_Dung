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
    public partial class MH_Chinh_ThongKeDoanhThu : Form
    {
        enum LOAI_THONG_KE
        {
            Thang,
            Nam
        }

        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU LuuTru = new XL_LUUTRU();
        protected XL_NGHIEPVU NghiepVu = new XL_NGHIEPVU();
        protected XL_THEHIEN TheHien = new XL_THEHIEN();

        static int Chiso_KhuVucDangChon = 1;          //tab khu vực mà người dùng đang chọn hiển thị

        static bool CoDangXuat; //Bật khi đăng xuất mà không thoát

        protected List<CDoanhThu_Nam> DanhSach_DoanhThu_CacNam;   //Lưu danh sách danh thu năm/tháng/loại phòng theo các năm


        Form ManHinh_DangNhap;              //Form lưu form màn hình đăng nhập, phục vụ chức năng đăng xuất
        int NV_ID = 0;                      //Lưu ID nhân viên
        string NV_MaKhuVuc = "";

        //Vị trí các năm, các tháng của biểu đồ thống kê doanh thu năm/tháng đang hiệu
        int Vitri_bieudo_Thang_BienTrai = 0;         //Vị trí biên tối đa có thể xem
        int Vitri_bieudo_Thang_BienPhai = 0;
        int Vitri_bieudo_Nam_BienTrai = 0;
        int Vitri_bieudo_Nam_BienPhai = 0;

        static int vitri_bieudo_Nam_DangXem = 0;           //Vị trí hiện tại của biểu đồ năm đang xem
        static int vitri_bieudo_Thang_DangXem = 0;         //Vị trí hiện tại của biểu đồ tháng đang xem
        #endregion
        public MH_Chinh_ThongKeDoanhThu()
        {
            InitializeComponent();
            KhoiDong();
        }

        #region "Sự kiện khởi động"
        //Khởi tạo bảng các phòng, các tầng của khách sạn
        private void KhoiDong()
        {

            //Cờ đánh dấu sự kiện đăng xuất
            CoDangXuat = false;
            //Đưa màn hình về giữa desktop
            this.StartPosition = FormStartPosition.CenterScreen;

            //Tải lên biểu tượng logout
            byte[] DuLieu_NhiPhan = LuuTru.DocHinh("Logo");

            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnImgLogo);
            DuLieu_NhiPhan = LuuTru.DocHinh("Logout");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnDangXuat);

            DuLieu_NhiPhan = LuuTru.DocHinh("Backward");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btn_Nam_QuaTrai);
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btn_Thang_QuaTrai);

            DuLieu_NhiPhan = LuuTru.DocHinh("Forward");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btn_Nam_QuaPhai);
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btn_Thang_QuaPhai);

            DuLieu_NhiPhan = LuuTru.DocHinh("Find");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnTraCuuPhieuThue);

            DuLieu_NhiPhan = LuuTru.DocHinh("Stats");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnThongKe_DoanhThu);


            string ChuoiXml = LuuTru.DocDuLieu_ThongKeDoanhThu();
            XmlDocument Tai_Lieu = new XmlDocument();
            try {
                Tai_Lieu.LoadXml(ChuoiXml);
                //Tai_Lieu.Save("ThongKeDoanhThu_DuLieu.xml");
            }
            catch(Exception e)
            {
                MessageBox.Show("Lỗi: " + e.ToString());
            }

            DanhSach_DoanhThu_CacNam = NghiepVu.KhoiTaoDanhSach_DanhThu_CacNam(ChuoiXml); //Chuyển từ chuỗi xml sáng dữ liệu cơ sở

            KhoiTaoViTriBienChoCacBieuDo(DanhSach_DoanhThu_CacNam);

            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Nam);
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Thang);


        }

        private void VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE Loai)
        {
            AnHien_NutMuiTen_ChuyenBieuDo();
            switch(Loai)
            {
                case LOAI_THONG_KE.Nam:
                    VeBieu_ThongKeTongDoanhThu_Theo_Nam();
                    break;
                case LOAI_THONG_KE.Thang:
                    VeBieu_ThongKeTongDoanhThu_Theo_Thang();
                    break;
            }
        }

        //Ẩn hiện các nút mũi tên chuyển đổi biểu đồ thống kê
        private void AnHien_NutMuiTen_ChuyenBieuDo()
        {
            //Mũi tên - Năm
            if(vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienPhai)
            {
                TheHien.TuChoiSuDung(btn_Nam_QuaPhai);
            }
            else
            {
                TheHien.ChoPhepSuDung(btn_Nam_QuaPhai);

            }
            if (vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienTrai)
            {
                TheHien.TuChoiSuDung(btn_Nam_QuaTrai);
            }
            else
            {
                TheHien.ChoPhepSuDung(btn_Nam_QuaTrai);
            }

            //Mũi tên - Tháng
            if(vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienPhai && vitri_bieudo_Thang_DangXem == DateTime.Now.Month)
            {
                TheHien.TuChoiSuDung(btn_Thang_QuaPhai);
            }

            if(vitri_bieudo_Thang_DangXem == Vitri_bieudo_Thang_BienPhai && vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienPhai)
            {
                TheHien.TuChoiSuDung(btn_Thang_QuaPhai);
            }
            else
            {
                TheHien.ChoPhepSuDung(btn_Thang_QuaPhai);
            }

            if (vitri_bieudo_Thang_DangXem == Vitri_bieudo_Thang_BienTrai && vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienTrai)
            {
                TheHien.TuChoiSuDung(btn_Thang_QuaTrai);
            }
            else
            {
                TheHien.ChoPhepSuDung(btn_Thang_QuaTrai);
            }


        }

        private void VeBieu_ThongKeTongDoanhThu_Theo_Thang()
        {
            int DoanhThu_Loai1 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].DS_DoanhThu_CacThang[vitri_bieudo_Thang_DangXem].DS_DoanhThu_TheoLoaiPhong[1].TongTien;
            int DoanhThu_Loai2 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].DS_DoanhThu_CacThang[vitri_bieudo_Thang_DangXem].DS_DoanhThu_TheoLoaiPhong[2].TongTien;
            int DoanhThu_Loai3 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].DS_DoanhThu_CacThang[vitri_bieudo_Thang_DangXem].DS_DoanhThu_TheoLoaiPhong[3].TongTien;
            int TongDoanhThu_Thang = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].DS_DoanhThu_CacThang[vitri_bieudo_Thang_DangXem].TongDoanhThu_Thang;
            
            if(DoanhThu_Loai1 == 0 && DoanhThu_Loai2 == 0 && DoanhThu_Loai3 == 0) //Cả 3 số liệu đều bằng  0
            {
                DoanhThu_Loai1 = DoanhThu_Loai2 = DoanhThu_Loai3 = 1;
            }
            chartThang.Series[0].Points[0].LegendText = "Loại 1";
            chartThang.Series[0].Points[0].SetValueY(DoanhThu_Loai1);
            chartThang.Series[0].Points[1].LegendText = "Loại 2";
            chartThang.Series[0].Points[1].SetValueY(DoanhThu_Loai2);
            chartThang.Series[0].Points[2].LegendText = "Loại 3";
            chartThang.Series[0].Points[2].SetValueY(DoanhThu_Loai3);

            TheHien.Xuat_Chuoi(SoTien_ChuoiSo(TongDoanhThu_Thang), labelTongDoanhThu_Thang);
            TheHien.Xuat_Chuoi("Tháng " + DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].DS_DoanhThu_CacThang[vitri_bieudo_Thang_DangXem].Thang, label_TK_Thang);

        }

        private void VeBieu_ThongKeTongDoanhThu_Theo_Nam()
        {
            int DoanhThu_Loai1 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].TinhDoanhThu_Nam_TheoLoai(1);
            int DoanhThu_Loai2 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].TinhDoanhThu_Nam_TheoLoai(2);
            int DoanhThu_Loai3 = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].TinhDoanhThu_Nam_TheoLoai(3);
            int TongDoanhThu_Nam = DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].TongDoanhThu_Nam;


            if (DoanhThu_Loai1 == 0 && DoanhThu_Loai2 == 0 && DoanhThu_Loai3 == 0) //Cả 3 số liệu đều bằng  0
            {
                DoanhThu_Loai1 = DoanhThu_Loai2 = DoanhThu_Loai3 = 1;
            }

            chartNam.Series[0].Points[0].LegendText = "Loại 1";
            chartNam.Series[0].Points[0].SetValueY(DoanhThu_Loai1);
            chartNam.Series[0].Points[1].LegendText = "Loại 2";
            chartNam.Series[0].Points[1].SetValueY(DoanhThu_Loai2);
            chartNam.Series[0].Points[2].LegendText = "Loại 3";
            chartNam.Series[0].Points[2].SetValueY(DoanhThu_Loai3);

            TheHien.Xuat_Chuoi(SoTien_ChuoiSo(TongDoanhThu_Nam), label_TongDoanhThu_Nam);
            TheHien.Xuat_Chuoi(DanhSach_DoanhThu_CacNam[vitri_bieudo_Nam_DangXem].Nam + "", label_TK_Nam);
        }
        //Chuyển số là một số tiên về dạng chuỗi, có dấu .phân cách ngàn ví dụ 2000000 -> 2.000.000
        private string SoTien_ChuoiSo(int giatri)
        {
            string Kq = "";

            int SoTien = giatri;
            int Le;
            while(SoTien >= 1000)
            {
                Le = SoTien % 1000;
                if (Le == 0)
                {
                    Kq += ".000";
                }
                else if (Le < 10)
                {
                    Kq += ".00" + Le;
                }
                else if (Le < 100)
                {
                    Kq += ".0" + Le;
                }
                else Kq += "." + Le;
                
                SoTien /= 1000;
            }

            Kq = SoTien + Kq + " VNĐ";

            return Kq;
        }

        private void KhoiTaoViTriBienChoCacBieuDo(List<CDoanhThu_Nam> danhSach_DoanhThu_CacNam)
        {
            Vitri_bieudo_Nam_BienPhai = 0;
            Vitri_bieudo_Nam_BienTrai = danhSach_DoanhThu_CacNam.Count - 1;

            Vitri_bieudo_Thang_BienPhai = 12;
            Vitri_bieudo_Thang_BienTrai = 1;

            vitri_bieudo_Thang_DangXem = DateTime.Now.Month;
            vitri_bieudo_Nam_DangXem = 0;

        }
        #endregion

        //Truyền vào màn hình đăng nhập, khi cần đăng xuất thì hiện lại màn hình
        public void TruyenDuLieu(Form ManHinhTruoc, int ID_NhanVien, string MaKhuVuc)
        {
            ManHinh_DangNhap = ManHinhTruoc;
            NV_ID = ID_NhanVien;
            NV_MaKhuVuc = MaKhuVuc;

            //Xuất câu chào nhân viên         
            string CauChao = NghiepVu.ChaoQuanLyKhuVuc(NV_ID, NV_MaKhuVuc);
            TheHien.Xuat_Chuoi(CauChao, labelChaoNhanVien);
        }

        private void MH_Chinh_ThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            btnImgLogo.FlatStyle = FlatStyle.Flat;
            btnImgLogo.FlatAppearance.BorderSize = 0;

            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.FlatAppearance.BorderSize = 0;

            btnThongKe_DoanhThu.FlatStyle = FlatStyle.Flat;
            btnThongKe_DoanhThu.FlatAppearance.BorderSize = 0;

            btnTraCuuPhieuThue.FlatStyle = FlatStyle.Flat;
            btnTraCuuPhieuThue.FlatAppearance.BorderSize = 0;

         
        }

        private void MH_Chinh_ThongKeDoanhThu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CoDangXuat)
            {

            }
            else { Application.Exit(); }

        }

        #region "Xử lý: Bắt sự kiện"
        private void btn_Nam_QuaTrai_Click(object sender, EventArgs e)
        {
            vitri_bieudo_Nam_DangXem++;
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Nam);
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Thang);
        }

        private void btn_Nam_QuaPhai_Click(object sender, EventArgs e)
        {
            vitri_bieudo_Nam_DangXem--;
            if(vitri_bieudo_Nam_DangXem == Vitri_bieudo_Nam_BienPhai && vitri_bieudo_Thang_DangXem > DateTime.Now.Month)
            {
                vitri_bieudo_Thang_DangXem = DateTime.Now.Month;
            }

            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Nam);
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Thang);
        }

        private void btn_Thang_QuaTrai_Click(object sender, EventArgs e)
        {
            if (vitri_bieudo_Thang_DangXem > 1) vitri_bieudo_Thang_DangXem--;
            else
            {
                vitri_bieudo_Thang_DangXem = 12;
                vitri_bieudo_Nam_DangXem++;
            } 

            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Nam);
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Thang);

        }

        private void btn_Thang_QuaPhai_Click(object sender, EventArgs e)
        {
            if (vitri_bieudo_Thang_DangXem < 12) vitri_bieudo_Thang_DangXem++;
            else
            {
                vitri_bieudo_Thang_DangXem = 1;
                vitri_bieudo_Nam_DangXem--;
            }

            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Nam);
            VeBieuDo_ThongKeDoanhThu(LOAI_THONG_KE.Thang);
        }
        #endregion

        private void chartThang_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {

        }

        private void btnTraCuuPhieuThue_Click(object sender, EventArgs e)
        {
            Form MH_TraCuu = new MH_TraCuu_PhieuThuePhong();
            MH_TraCuu.Show();
        }
    }
}
