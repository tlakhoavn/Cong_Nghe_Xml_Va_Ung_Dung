using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;


namespace UngdungDesktop_QuanLyKhachSan
{
    #region "Lớp: Lưu thông tin tài khoản đăng nhập"
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
    #endregion
    public class XL_NGHIEPVU
    {
        #region "Biến/đối tượng toàn cục"

        protected XL_THEHIEN The_hien = new XL_THEHIEN();
        protected XL_LUUTRU Luu_tru = new XL_LUUTRU();

        protected DICHVU_ASMX_.DICHVU_ASMXSoapClient Dich_vu = new DICHVU_ASMX_.DICHVU_ASMXSoapClient();

        protected XmlElement Goc;

        public List<CGiaThuePhong> Chuyen_Xml_thanh_BangGiaThuePhong(string chuoiXml)
        {
            List<CGiaThuePhong> BangGiaThuePhong = new List<CGiaThuePhong>();
            BangGiaThuePhong.Add(new CGiaThuePhong());

             XmlDocument TaiLieu = new XmlDocument();
            TaiLieu.LoadXml(chuoiXml);
            XmlElement Goc = TaiLieu.DocumentElement;

            foreach(XmlElement DoiTuong in TaiLieu.ChildNodes[0].ChildNodes)
            {
                CGiaThuePhong GiaThue = new CGiaThuePhong();               
                GiaThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID"));
                GiaThue.LoaiPhong = DoiTuong.GetAttribute("Ten");
                GiaThue.GiaThuePhong = Int32.Parse(DoiTuong.GetAttribute("DonGiaThue"));
                BangGiaThuePhong.Add(GiaThue);
            }
            return BangGiaThuePhong;
        }

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

        public string Chuyen_BangGiaThuePhong_thanh_Xml(List<CGiaThuePhong> bangGiaThuePhong)
        {
            string KQ = "";
            XmlDocument TaiLieu = new XmlDocument();
            XmlElement Root = TaiLieu.CreateElement("ROOT");

            for(int i=1;i< bangGiaThuePhong.Count;i++)
            {
                XmlElement LoaiPhong = TaiLieu.CreateElement("LOAIPHONG");
                LoaiPhong.SetAttribute("ID", "" + bangGiaThuePhong[i].ID);
                LoaiPhong.SetAttribute("Ten", bangGiaThuePhong[i].LoaiPhong);
                LoaiPhong.SetAttribute("DonGiaThue", bangGiaThuePhong[i].GiaThuePhong + "");

                Root.AppendChild(LoaiPhong);
            }
            TaiLieu.AppendChild(Root);
            TaiLieu.Save("Capnhatgiathue.xml");
            KQ = TaiLieu.OuterXml;
           
            return KQ;
        }

        protected const string Ten_KhuVuc_B = "Khu B";
        protected const string MaSo_KhuVuc_C = "KVC";
        protected const string Ten_KhuVuc_C = "Khu C";

        protected int VitriDoc = 0;




        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Đăng nhập hệ thống"
        //Xuất hiện câu chào nhân viên Tiếp tân ở khu vực đang quản lý dựa theo mã khu vực
        internal string ChaoNhanVienTiepTan(int nV_ID, string nV_MaKhuVuc)
        {
            string Cauchao = "Chào nhân viên Tiếp tân ";// + TenKhuVuc(nV_MaKhuVuc);
            return Cauchao;

        }

        public bool DangNhapThanhCong(string TenDangNhap, string MatKhau, ref int ID_NhanVien, ref string MaKhuVuc)
        {
            bool KetQua = false;
            Goc = Luu_tru.DocDuLieu_DangNhap_QuanLyKhachSan();
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

        #region "Xử lý: Convert dữ liệu thống kê doanh thu từ xml sáng dữ liệu cơ sở lưu DanhSach Danh Thu
        public List<CDoanhThu_Nam> KhoiTaoDanhSach_DanhThu_CacNam(string chuoiXml)
        {
            List<CDoanhThu_Nam> DanhSach_KetQua = new List<CDoanhThu_Nam>();
            XmlDocument TaiLieu = new XmlDocument();
            TaiLieu.LoadXml(chuoiXml);
            XmlElement Goc = TaiLieu.DocumentElement;

            foreach (XmlElement xElNam in Goc.ChildNodes)
            {
                CDoanhThu_Nam Nam_ = new CDoanhThu_Nam();
                Nam_.Nam = Int32.Parse(xElNam.GetAttribute("Nam"));
                Nam_.TongDoanhThu_Nam = Int32.Parse(xElNam.GetAttribute("TongDoanhThu"));

                //Nam_.DS_DoanhThu_CacThang.Add(new CDoanhThu_Thang()); //add tháng 0, các tháng sẽ đánh số từ 1 đến 12;
                foreach (XmlElement xElThang in xElNam.ChildNodes)
                {
                    CDoanhThu_Thang Thang_ = new CDoanhThu_Thang();
                    Thang_.Thang = Int32.Parse(xElThang.GetAttribute("Thang"));
                    Thang_.TongDoanhThu_Thang = Int32.Parse(xElThang.GetAttribute("TongDoanhThu"));


                    //Thang_.DS_DoanhThu_TheoLoaiPhong.Add(new CDoanhThu()); //add Loại phòng 0, các loại phòng sẽ đánh số từ 1,2,3
                    foreach (XmlElement xElLoaiPhong in xElThang.ChildNodes)
                    {
                        CDoanhThu LoaiPhong_ = new CDoanhThu();
                        LoaiPhong_.ID_LoaiPhong = Int32.Parse(xElLoaiPhong.GetAttribute("ID"));
                        LoaiPhong_.TongTien = Int32.Parse(xElLoaiPhong.GetAttribute("DoanhThu"));

                        Thang_.DS_DoanhThu_TheoLoaiPhong[LoaiPhong_.ID_LoaiPhong] = LoaiPhong_;
                    }
                    Nam_.DS_DoanhThu_CacThang[Thang_.Thang] = Thang_;
                    //MessageBox.Show(Nam_.Nam + " " + Thang_.Thang + " " + Thang_.TongDoanhThu_Thang);
                }
                //MessageBox.Show(Nam_.Nam + " " + Nam_.DS_DoanhThu_CacThang[12].Thang + " " + Nam_.DS_DoanhThu_CacThang[12].TongDoanhThu_Thang);
                DanhSach_KetQua.Add(Nam_);
            }


            for (int i = 1; i <= 12; i++)
            {
                int tem = DanhSach_KetQua[0].DS_DoanhThu_CacThang[i].TongDoanhThu_Thang;
                //MessageBox.Show(DanhSach_KetQua[0].Nam + " " + DanhSach_KetQua[0].DS_DoanhThu_CacThang[i].Thang +" " + tem);

            }

            return DanhSach_KetQua;
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

            foreach (XmlElement DoiTuong in Goc.ChildNodes)
            {
                CPhieuThuePhong PhieuThue = new CPhieuThuePhong();
                PhieuThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID"));
                PhieuThue.TenPhong = DoiTuong.GetAttribute("TenPhong");
                PhieuThue.NgayBatDau = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayBatDau"));
                PhieuThue.NgayDuKienTra = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayDuKienTra"));
                PhieuThue.NgayTra = ChuanHoaChuoiNgayThangNam(DoiTuong.GetAttribute("NgayTra"));
                PhieuThue.MaPhong = Int32.Parse(DoiTuong.GetAttribute("ID_Phong"));

                //Chỉ những phiếu thuê đã trả phồng mới có số tiền thuê
                if (PhieuThue.NgayTra.Length != 0)
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
            if (strNgayThangNam.Length >= 10)
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
