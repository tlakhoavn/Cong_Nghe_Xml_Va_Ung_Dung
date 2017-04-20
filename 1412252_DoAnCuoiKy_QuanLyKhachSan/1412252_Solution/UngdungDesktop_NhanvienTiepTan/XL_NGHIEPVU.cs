using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;


namespace UngdungDesktop_NhanvienTiepTan
{
    public class TaiKhoanDangNhap
    {
        int ID;
        string TenDangNhap;
        string MatKhau;
        int ID_NhanVien;
        string MaKhuVuc;

        public TaiKhoanDangNhap()
        {
            ID_NhanVien = 0;
            MaKhuVuc = "";
        }

        public bool TrungKhop(TaiKhoanDangNhap TK)
        {
            if (this.TenDangNhap_ == TK.TenDangNhap_ && this.MatKhau_ == TK.MatKhau_) return true;
            return false;
        }

        public int ID_
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }

        public string TenDangNhap_
        {
            get
            {
                return TenDangNhap;
            }

            set
            {
                TenDangNhap = value;
            }
        }

        public string MatKhau_
        {
            get
            {
                return MatKhau;
            }

            set
            {
                MatKhau = value;
            }
        }

        public int ID_NhanVien_
        {
            get
            {
                return ID_NhanVien;
            }

            set
            {
                ID_NhanVien = value;
            }
        }

        public string MaKhuVuc_
        {
            get
            {
                return MaKhuVuc;
            }

            set
            {
                MaKhuVuc = value;
            }
        }
    }

    public class XL_NGHIEPVU
    {
        #region "Biến/đối tượng toàn cục"

        protected XL_THEHIEN The_hien = new XL_THEHIEN();
        protected XL_LUUTRU Luu_tru = new XL_LUUTRU();
        protected DICHVU_ASMX_.DICHVU_ASMXSoapClient Dich_vu = new DICHVU_ASMX_.DICHVU_ASMXSoapClient();

        protected XmlElement Goc;

        //Khai báo các biến đối tượng đọc file xml thông tin phòng
        protected string DT_MaSoKhuVuc = "MaSoKhuVuc";
        protected string DT_ID_Tang = "ID_Tang";

        protected string DT_ID = "ID";
        protected string DT_Ten = "Ten";
        protected string DT_Tang = "Tang";
        protected string DT_KhuVuc = "KhuVuc";
        protected string DT_LoaiPhong = "LoaiPhong";
        protected string DT_TienNghi = "TienNghi";
        protected string DT_SoKhachToiDa = "SoKhachToiDa";
        protected string DT_DonGiaThue = "DonGiaThue";
        protected string DT_TinhTrangPhong = "TinhTrangPhong";

        protected const string MaSo_KhuVuc_A = "KVA";
        protected const string Ten_KhuVuc_A = "Khu A";
        protected const string MaSo_KhuVuc_B = "KVB";
        protected const string Ten_KhuVuc_B = "Khu B";
        protected const string MaSo_KhuVuc_C = "KVC";
        protected const string Ten_KhuVuc_C = "Khu C";

        protected int VitriDoc = 0;




        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Đọc thông tin các phòng/ khu vực"
        public void KhoiTaoThongTinBangPhong(CKhuVuc_TheHien KV_A, CKhuVuc_TheHien KV_B, CKhuVuc_TheHien KV_C, XmlElement Goc)
        {

            int SoTang_KhuVucA = 0;
            int SoTang_KhuVucB = 0;
            int SoTang_KhuVucC = 0;
            string MaSoKhuVuc = "";
            VitriDoc = 0;

            #region "Đếm số tầng mỗi khu vực"
            int ViTriDoc_DoiTuongTang = 0;
            string DoiTuong_Tang = Goc.ChildNodes[ViTriDoc_DoiTuongTang].Name;
            foreach (XmlElement DoiTuong in Goc.SelectNodes(DoiTuong_Tang))
            {
                MaSoKhuVuc = DoiTuong.GetAttribute(DT_MaSoKhuVuc).Trim();
                if (MaSoKhuVuc == MaSo_KhuVuc_A)
                {
                    SoTang_KhuVucA++;
                }
                else if (MaSoKhuVuc == MaSo_KhuVuc_B)
                {
                    SoTang_KhuVucB++;
                }
                else
                {
                    SoTang_KhuVucC++;
                }
                VitriDoc++;
            }

            for (int i = 0; i < SoTang_KhuVucA; i++) { KV_A.BangKhuVuc.Add(new List<CThongTinPhong>()); }
            for (int i = 0; i < SoTang_KhuVucB; i++) { KV_B.BangKhuVuc.Add(new List<CThongTinPhong>()); }
            for (int i = 0; i < SoTang_KhuVucC; i++) { KV_C.BangKhuVuc.Add(new List<CThongTinPhong>()); }
            #endregion

            #region "Lưu các phòng vào bảng"
            int ViTriDoc_DoiTuongPhong = ViTriDoc_DoiTuongTang + VitriDoc;
            string DoiTuong_Phong = Goc.ChildNodes[ViTriDoc_DoiTuongPhong].Name;

            foreach (XmlElement DoiTuong in Goc.SelectNodes(DoiTuong_Phong))
            {
                CThongTinPhong Phong = new CThongTinPhong();
                Phong.ID = Int32.Parse(DoiTuong.GetAttribute(DT_ID).Trim());
                Phong.Ten = DoiTuong.GetAttribute(DT_Ten).Trim();
                Phong.Tang = DoiTuong.GetAttribute(DT_Tang).Trim();
                Phong.KhuVuc = DoiTuong.GetAttribute(DT_KhuVuc).Trim();
                Phong.LoaiPhong = DoiTuong.GetAttribute(DT_LoaiPhong).Trim();
                Phong.TienNghi = DoiTuong.GetAttribute(DT_TienNghi).Trim();
                Phong.SoKhachToiDa = Int32.Parse(DoiTuong.GetAttribute(DT_SoKhachToiDa).Trim());
                Phong.DonGia = Int32.Parse(DoiTuong.GetAttribute(DT_DonGiaThue).Trim());                

                int Tang = ChiSoTang(DoiTuong.GetAttribute(DT_Tang).Trim());

                if (Phong.KhuVuc == Ten_KhuVuc_A)
                {
                    KV_A.BangKhuVuc[Tang - 1].Add(Phong);
                }
                else
                if (Phong.KhuVuc == Ten_KhuVuc_B)
                {
                    KV_B.BangKhuVuc[Tang - 1].Add(Phong);
                }
                else
                {
                    KV_C.BangKhuVuc[Tang - 1].Add(Phong);
                }
            }
        }
       
        private string TenKhuVuc(string nV_MaKhuVuc) //Trả về tên khu vực dựa vào mã khu vực    
        {
            string kq = "";
            switch (nV_MaKhuVuc)
            {
                case MaSo_KhuVuc_A:
                    kq = Ten_KhuVuc_A;
                    break;
                case MaSo_KhuVuc_B:
                    kq = Ten_KhuVuc_B;
                    break;
                case MaSo_KhuVuc_C:
                    kq = Ten_KhuVuc_C;
                    break;
            }
            return kq;
        }

        //Trả về chỉ số mã khu vực A,B,C tương ứng với 1,2,3
        public int ChiSoMaKhuVuc(string nV_MaKhuVuc)
        {
            int kq = 1;
            switch (nV_MaKhuVuc)
            {
                case MaSo_KhuVuc_A:
                    kq = 1;
                    break;
                case MaSo_KhuVuc_B:
                    kq = 2;
                    break;
                case MaSo_KhuVuc_C:
                    kq = 3;
                    break;
            }           
            return kq;
        }

        private int ChiSoTang(string v) //Trả về chỉ số tầng
        {
            int kq = 0;
            switch (v)
            {
                case "Tầng 1":
                    kq = 1;
                    break;
                case "Tầng 2":
                    kq = 2;
                    break;
                case "Tầng 3":
                    kq = 3;
                    break;
                case "Tầng 4":
                    kq = 4;
                    break;
                case "Tầng 5":
                    kq = 5;
                    break;
                case "Tầng 6":
                    kq = 6;
                    break;
                case "Tầng 7":
                    kq = 7;
                    break;
                case "Tầng 8":
                    kq = 8;
                    break;
                case "Tầng 9":
                    kq = 9;
                    break;
                case "Tầng 10":
                    kq = 10;
                    break;
            }
            return kq;
        }

        #endregion

            #region "Đọc từ xml element các phiếu thuê các phòng hiện đang được thuê"
            public List<CPhieuThuePhong> Doc_DSCacPhongDangThue(  XmlElement Goc)  
        {
            List<CPhieuThuePhong> danhSach_PhongDangChoThue = new List<CPhieuThuePhong>();
            string DoiTuong_PhieuThue = "PHIEU_THUE_PHONG";
            string strNgay = "";

            string dsTenKhachHang = "";
            string dsCMND = ""; 
            

            foreach (XmlElement DoiTuong in Goc.SelectNodes(DoiTuong_PhieuThue))
            {
                CPhieuThuePhong PhieuThue = new CPhieuThuePhong();
                PhieuThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID").Trim());
                PhieuThue.MaPhong = Int32.Parse(DoiTuong.GetAttribute("ID_Phong").Trim());

                strNgay = DoiTuong.GetAttribute("NgayBatDau").Trim();
                PhieuThue.NgayBatDau = strNgay.Substring(8, 2) + "/" + strNgay.Substring(5, 2) + '/' + strNgay.Substring(0, 4);
                strNgay = DoiTuong.GetAttribute("NgayDuKienTra").Trim();
                PhieuThue.NgayDuKienTra = strNgay.Substring(8, 2) + "/" + strNgay.Substring(5, 2) + '/' + strNgay.Substring(0, 4);

                dsTenKhachHang = DoiTuong.GetAttribute("DS_TenKhachHang").Trim();
                dsCMND = DoiTuong.GetAttribute("DS_CMND");
                int vitri_ten = dsTenKhachHang.IndexOf("|");
                int vitri_cmnd = dsCMND.IndexOf("|");

                while (vitri_ten > 0)
                {
                    CThongTinKhachHang KhachHang = new CThongTinKhachHang();
                    KhachHang.HoTen = dsTenKhachHang.Substring(0, vitri_ten);
                    KhachHang.CMND = dsCMND.Substring(0, vitri_cmnd);
                    dsTenKhachHang = dsTenKhachHang.Substring(vitri_ten + 1, dsTenKhachHang.Length - vitri_ten - 1);
                    dsCMND = dsCMND.Substring(vitri_cmnd + 1, dsCMND.Length - vitri_cmnd - 1);

                    PhieuThue.DSKhachHang.Add(KhachHang);

                    vitri_ten = dsTenKhachHang.IndexOf("|");
                    vitri_cmnd = dsCMND.IndexOf("|");
                }

                danhSach_PhongDangChoThue.Add(PhieuThue);                       
            }
            return danhSach_PhongDangChoThue;

        }        
            #endregion
        #endregion

        #region "Xử lý: Đăng nhập hệ thống"
        //Xuất hiện câu chào nhân viên Tiếp tân ở khu vực đang quản lý dựa theo mã khu vực
        internal string ChaoNhanVienTiepTan(int nV_ID, string nV_MaKhuVuc)
        {
            string Cauchao = "Chào nhân viên Tiếp tân " + TenKhuVuc(nV_MaKhuVuc);
            return Cauchao;

        }

        public bool DangNhapThanhCong(string TenDangNhap, string MatKhau, ref int ID_NhanVien, ref string MaKhuVuc)
        {
            bool KetQua = false;
            Goc = Luu_tru.DocDuLieu_DangNhap_TiepTan();
            TaiKhoanDangNhap TaiKhoanNhap = new TaiKhoanDangNhap();
            TaiKhoanNhap.TenDangNhap_ = TenDangNhap;
            TaiKhoanNhap.MatKhau_ = MatKhau;
            KetQua = TaiKhoanTonTaiTrongDanhSach(ref TaiKhoanNhap, Goc);

            //Bổ sung giá trị ID nhân viên và mã khu vực trước khi return kết quả;
            ID_NhanVien = TaiKhoanNhap.ID_NhanVien_;
            MaKhuVuc = TaiKhoanNhap.MaKhuVuc_;                 
            return KetQua;
        }               
        private bool TaiKhoanTonTaiTrongDanhSach(ref TaiKhoanDangNhap TaiKhoanNhap, XmlElement goc)
        {
          List<TaiKhoanDangNhap> DanhSachTaiKhoanNhanVienTiepTan = new List<TaiKhoanDangNhap>();
            foreach (XmlElement DoiTuong in goc.ChildNodes)
            {                
                TaiKhoanDangNhap TK = new TaiKhoanDangNhap();
                TK.ID_ = Int32.Parse(DoiTuong.GetAttribute("ID").ToString());
                TK.TenDangNhap_ = DoiTuong.GetAttribute("TenDangNhap").ToString();
                TK.MatKhau_ = DoiTuong.GetAttribute("MatKhau").ToString();
                TK.ID_NhanVien_ = Int32.Parse(DoiTuong.GetAttribute("ID_NhanVien").ToString());
                TK.MaKhuVuc_ = DoiTuong.GetAttribute("MaKhuVuc").ToString();
                DanhSachTaiKhoanNhanVienTiepTan.Add(TK);
            }

            foreach(TaiKhoanDangNhap TaiKhoan in DanhSachTaiKhoanNhanVienTiepTan)
            {
                if( TaiKhoan.TrungKhop(TaiKhoanNhap) == true)
                {
                    //Nếu tài khoản tồn tại thì bổ sung giá tri ID nhân viên và Mã khu vực ứng với tài khoản
                    TaiKhoanNhap.ID_NhanVien_ = TaiKhoan.ID_NhanVien_;
                    TaiKhoanNhap.MaKhuVuc_ = TaiKhoan.MaKhuVuc_;                   
                    return true;
                }
            }
            return false;
        }
              
        #endregion

        #region "Xử lý: Tra cứu phiếu thuê phòng"
             

        //Tạo mới thông tin một phiếu thuê phòng
        internal CPhieuThuePhong TaoMoiPhieuThuePhong(CThongTinPhong Phong)
        {
            CPhieuThuePhong PhieuMoi = new CPhieuThuePhong();

            PhieuMoi.TenPhong = Phong.Ten;
            PhieuMoi.MaPhong = Phong.ID;
            PhieuMoi.LoaiPhong = Phong.LoaiPhong;
            PhieuMoi.NgayBatDau = Chuoi_NgayThangNam_HienTai();     //Lấy theo thời gian hệ thống
            PhieuMoi.NgayDuKienTra = "";
            return PhieuMoi;
        }      

        //Trả về thong tin phiếu thuê phòng
        internal CPhieuThuePhong LayThongTinPhongDangThue(CThongTinPhong Phong, List<CPhieuThuePhong> DSPhieu_PhongDangThue)
        {
            CPhieuThuePhong Phieu = new CPhieuThuePhong();
            foreach (CPhieuThuePhong PhieuThue in DSPhieu_PhongDangThue)
            {
                if (PhieuThue.MaPhong == Phong.ID)
                {
                    PhieuThue.LoaiPhong = Phong.LoaiPhong;
                    PhieuThue.TenPhong = Phong.Ten;                   
                    return PhieuThue;
                }
            }
            return Phieu;
        }




        #endregion

        #region "Xử lý: Trả phòng"
        //Hàm tính tiền thuê phòng dựa vào ngày và số tiền, nếu sau có tính theo giờ hoặc VAT thì bổ sung
        public int TinhTienThuePhong(int soNgayThue, int donGia)
        {
            return soNgayThue * donGia;
        }
        //Tính số ngày thuê phòng kể từ ngày bắt đầu đến hiện tại
        public int SoNgayThuePhong(string NgayBatDau)
        {
            int kq;
            DateTime NgayBD = DateTime.Parse(NgayBatDau);
            DateTime Now = DateTime.Now;
            TimeSpan span = Now - NgayBD;
            kq = (int) span.TotalDays;
            return kq;
        }
        #endregion

        #region "Hàm xử lý chung"
        //Nghiệp vụ kiểm tra một phòng có đang trống hay đang được cho thuê
        public bool PhongDangTrong(int iD, List<CPhieuThuePhong> DS_PhongDangChoThue)
        {
            try
            {
                for (int i = 0; i < DS_PhongDangChoThue.Count; i++)
                {
                    if (DS_PhongDangChoThue[i].MaPhong == iD) return false;
                }
            }
            catch(Exception e)
            {
                
            }
            
            return true;
        }        
        public string Chuoi_NgayThangNam_HienTai()
        {
            string ChuoiNgay = "";
            int iNgay = DateTime.Now.Day;
            if(iNgay<10) ChuoiNgay +="0";
            ChuoiNgay += iNgay+"/";

            int iThang = DateTime.Now.Month;
            if (iThang < 10) ChuoiNgay += "0";
            ChuoiNgay += iThang + "/";

            ChuoiNgay += DateTime.Now.Year.ToString();
            return ChuoiNgay;
        }

        //Kiểm tra ngày dự kiến trả có phải là một ngày hợp lệ: đúng dạng xx/xx/xxxx và là một ngày sau ngày bắt đầu
        public bool Kiemtra_NgayDuKienHopLe(string strNgayDuKien, string strNgayBatDau)
        {
            if (Kiemtra_NgayDung(strNgayBatDau) == false) return false;

            try
            {
                TimeSpan SoNgay = DateTime.Parse(strNgayDuKien) - DateTime.Parse(strNgayBatDau);

                if (SoNgay.TotalDays > 0) return true;
            }
            catch(Exception e)
            {

            }    
           

            return false;
        }
        //Kiểm tra chuỗi dạng xx/xx/xxxx có phải là một ngày hợp lệ 
        private bool Kiemtra_NgayDung(string v)
        {
            DateTime Date;
            try
            {
                Date  = DateTime.Parse(v);
            }
            catch(Exception e)
            {
                return false;                
            }           
           
            return true;
        }
        #endregion

        #region "Tra cứu phiếu thuê phòng"
        public List<CPhieuThuePhong> TraCuuPhieuThue_Theo_NgayThangNam(string chuoiTruyVan)
        {
            string ChuoiXml_Truyvan = TaoChuoi_Xml_TraCuu(chuoiTruyVan, "NgayThangNam");
            string ChuoiXml_KetQua = Dich_vu.TraCuuPhieuThuePhong(ChuoiXml_Truyvan);
            return Chuyen_XML_Thanh_Phieu_Thue_Phong(ChuoiXml_KetQua);
        }      

        public List<CPhieuThuePhong> TraCuuPhieuThue_Theo_HoTen(string chuoiTruyVan)
        {
            List<CPhieuThuePhong> KQ = new List<CPhieuThuePhong>();
            string ChuoiXml_Truyvan = TaoChuoi_Xml_TraCuu(chuoiTruyVan, "HoTen");

            try
            {
                string ChuoiXml_KetQua = Dich_vu.TraCuuPhieuThuePhong(ChuoiXml_Truyvan);
                KQ = Chuyen_XML_Thanh_Phieu_Thue_Phong(ChuoiXml_KetQua);
            }
            catch (Exception e)
            {
                return KQ;
            }
            return KQ;
        }

        private List<CPhieuThuePhong> Chuyen_XML_Thanh_Phieu_Thue_Phong(string chuoiXml)
        {
            //MessageBox.Show(chuoiXml);
            List<CPhieuThuePhong> KQ = new List<CPhieuThuePhong>();
            XmlDocument TaiLieu = new XmlDocument();
            TaiLieu.LoadXml(chuoiXml);
            XmlElement Goc = TaiLieu.DocumentElement;

            foreach(XmlElement DoiTuong in Goc.ChildNodes)
            {
                CPhieuThuePhong PhieuThue = new CPhieuThuePhong();
                PhieuThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID"));
                PhieuThue.TenPhong = DoiTuong.GetAttribute("TenPhong");
                PhieuThue.NgayBatDau = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayBatDau"));
                PhieuThue.NgayDuKienTra = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayDuKienTra"));
                PhieuThue.NgayTra = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayTra"));
                PhieuThue.MaPhong = Int32.Parse(DoiTuong.GetAttribute("ID_Phong"));

                //Chỉ những phiếu thuê đã trả phồng mới có số tiền thuê
                if(PhieuThue.NgayTra.Length != 0)
                {                  
                    PhieuThue.TienThuePhong = Int32.Parse("0" + DoiTuong.GetAttribute("SoTien"));                  
                    
                }
                CThongTinKhachHang ThongTin = new CThongTinKhachHang();

                string DS_TenKhachHang = DoiTuong.GetAttribute("DS_TenKhachHang");
                string[] ChuoiTen = DS_TenKhachHang.Split(new Char[] { '|' });

                string DS_CMND = DoiTuong.GetAttribute("DS_CMND");
                string[] ChuoiCMND = DS_CMND.Split(new Char[] { '|' });


                for (int i = 0; i < ChuoiTen.Count(); i++)
                {
                    if (ChuoiTen[i].Trim() != "")
                    {
                        ThongTin = new CThongTinKhachHang();
                        ThongTin.HoTen = ChuoiTen[i];
                        ThongTin.CMND = ChuoiCMND[i];

                        PhieuThue.DSKhachHang.Add(ThongTin);
                    }
                }


                PhieuThue.LoaiPhong = DoiTuong.GetAttribute("LoaiPhong");
                KQ.Add(PhieuThue);
            }     


            return KQ;
        }

        private string ChuanHoaChuoiNgayThangNam(string strNgayThangNam)
        {
            string KQ = "";
            if(strNgayThangNam.Length >=10)
            {
                KQ = strNgayThangNam.Substring(8, 2) + "/" + strNgayThangNam.Substring(5, 2) + "/" + strNgayThangNam.Substring(0, 4);
            }
            return KQ;
        }

        private string TaoChuoi_Xml_TraCuu(string chuoiTruyVan, string loai)
        {
            string KQ = "";
            XmlDocument TaiLieu = new XmlDocument();
            XmlElement Goc = TaiLieu.CreateElement("ROOT");
            XmlElement Tra_Cuu = TaiLieu.CreateElement("TRA_CUU_PHIEU_THUE");
            Tra_Cuu.SetAttribute("Loai", loai);
            Tra_Cuu.SetAttribute("ChuoiTruyVan", chuoiTruyVan);
            Goc.AppendChild(Tra_Cuu);
            return Goc.OuterXml;

        }
        #endregion
    }
}
