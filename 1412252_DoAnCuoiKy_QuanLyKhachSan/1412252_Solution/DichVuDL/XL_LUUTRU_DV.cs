using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Web.Hosting;
using System.Data;
using System.Data.OleDb;


#region "Định nghĩa một số kiểu ENUM"
public enum DULIEUDOC
{
    TiepTan_DangNhap,
    QLKhuVuc_DangNhap,
    QLKhachSan_DangNhap,
    ThongTinPhong,
    PhieuThuePhong,
    ThongKeDoanhThu,
    GiaThuePhong,
    TraCuuPhieuThue
}

public enum DULIEUGHI
{
    CapNhat_ThuePhong,
    CapNhat_TraPhong,
    CapNhat_GiaThuePhong
}

public enum CONG_NGHE_LUU_TRU
{
    CSDL,
    XML
}

public enum LOAI_CSDL
{
    Access, SQLServer, SQLser_MDF, MySql, Sqlite
}

public enum LOAI_TRA_CUU_PHIEU_THEU
{
    HoTen,
    NgayThangNam
}
#endregion

namespace DichVuDL
{
    #region "Định nghĩa các lớp đối tượng "
    //Lớp lưu doanh thu của một loại phòng
    public class CDoanhThu
    {
        protected int m_ID_LoaiPhong;
        protected string m_Ten_LoaiPhong;
        protected int m_ID_KhuVuc;
        protected int m_TongTien;



        public int ID_LoaiPhong
        {
            get
            {
                return m_ID_LoaiPhong;
            }

            set
            {
                m_ID_LoaiPhong = value;
            }
        }
        public string Ten_LoaiPhong
        {
            get
            {
                return m_Ten_LoaiPhong;
            }

            set
            {
                m_Ten_LoaiPhong = value;
            }
        }
        public int ID_KhuVuc
        {
            get
            {
                return m_ID_KhuVuc;
            }

            set
            {
                m_ID_KhuVuc = value;
            }
        }
        public int TongTien
        {
            get
            {
                return m_TongTien;
            }

            set
            {
                m_TongTien = value;
            }
        }
        public CDoanhThu()
        {
            ID_LoaiPhong = 0;
            Ten_LoaiPhong = "";
            ID_KhuVuc = 0;
            TongTien = 0;
        }
       
    }
    public class CDoanhThu_Thang
    {
        protected int m_Thang;
        protected List<CDoanhThu> m_DS_DoanhThu_TheoLoaiPhong;
        protected int m_TongDoanhThu_Thang;

        public CDoanhThu_Thang()
        {
            m_Thang = 0;
            TongDoanhThu_Thang = 0;

            DS_DoanhThu_TheoLoaiPhong = new List<CDoanhThu>();
            for(int i=0;i<=3;i++)
            {
                DS_DoanhThu_TheoLoaiPhong.Add(new CDoanhThu());
            }
            
        }
        public CDoanhThu_Thang(int Thang_)
        {
            DS_DoanhThu_TheoLoaiPhong = new List<CDoanhThu>();
            m_Thang = Thang_;
            TongDoanhThu_Thang = 0;

            for (int i = 0; i <= 3; i++)
            {
                DS_DoanhThu_TheoLoaiPhong.Add(new CDoanhThu());
            }

        }
        public int TinhTongDoanhThu_Thang()
        {
            int Tong = 0;
            for(int iLoaiPhong = 0; iLoaiPhong <= 3;iLoaiPhong ++)
            {
                Tong += DS_DoanhThu_TheoLoaiPhong[iLoaiPhong].TongTien;
            }
            TongDoanhThu_Thang = Tong;
            return Tong;
        }
        public List<CDoanhThu> DS_DoanhThu_TheoLoaiPhong
        {
            get
            {
                return m_DS_DoanhThu_TheoLoaiPhong;
            }

            set
            {
                m_DS_DoanhThu_TheoLoaiPhong = value;
            }
        }
        public int Thang
        {
            get
            {
                return m_Thang;
            }

            set
            {
                m_Thang = value;
            }
        }
        public int TongDoanhThu_Thang
        {
            get
            {
                return m_TongDoanhThu_Thang;
            }

            set
            {
                m_TongDoanhThu_Thang = value;
            }
        }
    }
    public class CDoanhThu_Nam
    {
        protected int m_Nam;
        protected List<CDoanhThu_Thang> dS_DoanhThu_CacThang;
        protected int tongDoanhThu_Nam;

        public int TinhTongDoanhThu_Nam()
        {
            int Tong = 0;
            for (int iThang = 1; iThang <= 12; iThang++)
            {
                Tong += DS_DoanhThu_CacThang[iThang].TinhTongDoanhThu_Thang();
            }
            TongDoanhThu_Nam = Tong;
            return Tong;
        }

        public CDoanhThu_Nam()
        {
            dS_DoanhThu_CacThang = new List<CDoanhThu_Thang>();
            m_Nam = 0;
            TongDoanhThu_Nam = 0;
            for (int i=0;i<=12;i++)
            {
                DS_DoanhThu_CacThang.Add(new CDoanhThu_Thang(i));
            }
        }
        public CDoanhThu_Nam(int Nam_)
        {
            dS_DoanhThu_CacThang = new List<CDoanhThu_Thang>();
            m_Nam = Nam_;
            TongDoanhThu_Nam = 0;
            for (int i = 0; i <= 12; i++)
            {
                DS_DoanhThu_CacThang.Add(new CDoanhThu_Thang(i));
            }
        }

        public List<CDoanhThu_Thang> DS_DoanhThu_CacThang
        {
            get
            {
                return dS_DoanhThu_CacThang;
            }

            set
            {
                dS_DoanhThu_CacThang = value;
            }
        }
        public int Nam
        {
            get
            {
                return m_Nam;
            }

            set
            {
                m_Nam = value;
            }
        }
        public int TongDoanhThu_Nam
        {
            get
            {
                return tongDoanhThu_Nam;
            }

            set
            {
                tongDoanhThu_Nam = value;
            }
        }
    }

    //Lớp lưu thông tin đơn giá thuê loại phòng
    public class CGiaThuePhong
    {
        protected int m_ID;
        protected string m_LoaiPhong;
        protected int m_GiaThuePhong;

        public int ID
        {
            get
            {
                return m_ID;
            }

            set
            {
                m_ID = value;
            }
        }

        public string LoaiPhong
        {
            get
            {
                return m_LoaiPhong;
            }

            set
            {
                m_LoaiPhong = value;
            }
        }

        public int GiaThuePhong
        {
            get
            {
                return m_GiaThuePhong;
            }

            set
            {
                m_GiaThuePhong = value;
            }
        }

        public CGiaThuePhong()
        {
            ID = 0;
            LoaiPhong = "";
            GiaThuePhong = 0;
        }

    }
    #endregion

    public class XL_LUUTRU_DV
    {
        #region "Biến/Đội tượng toàn cục"
        protected static DirectoryInfo ThuMucProject = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
        protected static DirectoryInfo ThuMucSolution = ThuMucProject.Parent;

        protected static string ThuMucDuLieu = ThuMucSolution.FullName + @"\Du_Lieu";
        protected static string ThuMucCSDL = ThuMucDuLieu + @"\CSDL";
        protected static string ThuMucMedia = ThuMucDuLieu + @"\Media";
        protected static string ThuMucHinh = ThuMucMedia + @"\Hinh";

        protected static string TenDuLieuLuuTru = "DuLieu_QuanLyKhachSan2";

        protected static CONG_NGHE_LUU_TRU CongNghe = CONG_NGHE_LUU_TRU.CSDL;
        protected LOAI_CSDL LoaiCSDL = LOAI_CSDL.Access;

        protected string ChuoiKetNoiAccess;
        protected string DuongDanTapTinAccess = "";
        #endregion

        #region "Xử lý: Khởi động"
        public XL_LUUTRU_DV()
        {
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL && LoaiCSDL == LOAI_CSDL.Access)
            {
                DuongDanTapTinAccess = ThuMucCSDL + @"\" + TenDuLieuLuuTru + ".mdb";
                ChuoiKetNoiAccess = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DuongDanTapTinAccess;
            }
        }
        #endregion       

        #region "Xử lý: Tra cứu phiếu thuê"
        public string TraCuuPhieuThuePhong(string chuoiXmlTruyVan)
        {
            string KQ = "";
            XmlDocument TaiLieu_Nhap = new XmlDocument();
            XmlDocument TaiLieu_Xuat = new XmlDocument();
           
            List<XmlElement> DanhSachKQ = new List<XmlElement>();

            TaiLieu_Nhap.LoadXml(chuoiXmlTruyVan);
            XmlElement Goc = TaiLieu_Nhap.DocumentElement;
            string Loai = "";
            string ChuoiTruyVan = "";

            foreach (XmlElement DoiTuong in Goc.ChildNodes)
            {
                Loai = DoiTuong.GetAttribute("Loai");
                ChuoiTruyVan = DoiTuong.GetAttribute("ChuoiTruyVan");
            }
           

            if(Loai == "HoTen")
            {
                DanhSachKQ = XuLy_TraCuuPhieuThuePhong(LOAI_TRA_CUU_PHIEU_THEU.HoTen, ChuoiTruyVan);
            }
            else if(Loai == "NgayThangNam")
            {
                DanhSachKQ = XuLy_TraCuuPhieuThuePhong(LOAI_TRA_CUU_PHIEU_THEU.NgayThangNam, ChuoiTruyVan);
            }

            XmlElement Root = TaiLieu_Xuat.CreateElement("ROOT");
            for (int i = 0; i < DanhSachKQ.Count; i++)
            {
                TaiLieu_Xuat.LoadXml(DanhSachKQ[i].OuterXml);
                Root.AppendChild(TaiLieu_Xuat.DocumentElement);
            }             

           
            return Root.OuterXml;
        }

        private List<XmlElement> XuLy_TraCuuPhieuThuePhong(LOAI_TRA_CUU_PHIEU_THEU LoaiTraCuu, string chuoiTruyVan)
        {
            XmlDocument TaiLieu = new XmlDocument();
            XmlElement KQ = TaiLieu.CreateElement("PHIEU_THUE_PHONG");

            List<XmlElement> DanhSach_KQ = new List<XmlElement>();

            XmlElement Goc = DocDuLieuTuCSDL(DULIEUDOC.TraCuuPhieuThue);
            switch(LoaiTraCuu)
            {
                case LOAI_TRA_CUU_PHIEU_THEU.HoTen:
                    foreach(XmlElement DoiTuong in Goc.ChildNodes)
                    {
                        if(DoiTuong.GetAttribute("DS_TenKhachHang").ToUpper().Contains(chuoiTruyVan.ToUpper()))
                            {
                            DanhSach_KQ.Add(DoiTuong);                           
                        }
                    }
                    break;
                case LOAI_TRA_CUU_PHIEU_THEU.NgayThangNam:
                    foreach (XmlElement DoiTuong in Goc.ChildNodes)
                    {
                        string Ngay = DoiTuong.GetAttribute("NgayBatDau");                    

                        string Ngay_ = Ngay.Substring(8,2) + "/" + Ngay.Substring(5, 2) + "/" + Ngay.Substring(0, 4);

                        try
                        {
                            TimeSpan SoNgay = DateTime.Parse(chuoiTruyVan) - DateTime.Parse(Ngay_);

                            if (SoNgay.TotalDays == 0)
                            {
                                DanhSach_KQ.Add(DoiTuong);
                            }
                        }
                        catch(Exception e)
                        {
                            return DanhSach_KQ;
                        }
                        
                    }

                    break;
            }

            return DanhSach_KQ;
        }
        #endregion

        #region "Xử lý: Đọc dữ liệu - Phiếu thuê phòng"
        public XmlElement DocDuLieu_PhieuThuePhong()
        {
            XmlElement Kq = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
                Kq = DocDuLieuTuCSDL(DULIEUDOC.PhieuThuePhong);
            return Kq;
        }
        #endregion
        #region "Xử lý: Đọc dữ liệu - Đăng nhập"
        public XmlElement DocDuLieu_DangNhap_TiepTan()
        {
            XmlElement Kq = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
                Kq = DocDuLieuTuCSDL(DULIEUDOC.TiepTan_DangNhap);
            return Kq;
        }
        
        public XmlElement DocDuLieu_DangNhap_QLKhuVuc()
        {
            XmlElement Kq = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
                Kq = DocDuLieuTuCSDL(DULIEUDOC.QLKhuVuc_DangNhap);
            return Kq;
        }

        public XmlElement DocDuLieu_DangNhap_QLKhachSan()
        {
            XmlElement Kq = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
                Kq = DocDuLieuTuCSDL(DULIEUDOC.QLKhachSan_DangNhap);
            return Kq;
        }
        //---------------------------------------------------------
        #endregion
        #region "Xử lý: Đọc dữ liệu - Thông tin phòng"
        public XmlElement DocDuLieu_ThongTinPhong()
        {
            XmlElement Kq = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
                Kq = DocDuLieuTuCSDL(DULIEUDOC.ThongTinPhong);
            return Kq;
        }
        #endregion

        #region "Xử lý: Thống kê doanh thu"
        public XmlElement DocDuLieu_ThongKeDoanhThu()
        {
            XmlElement Goc_DoanhThu_ToanBo = null;
            XmlElement KQ = null;
            List<CDoanhThu_Nam> DanhSach_DoanhThu_CacNam ;           
            

            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
            {
                Goc_DoanhThu_ToanBo = DocDuLieuTuCSDL(DULIEUDOC.ThongKeDoanhThu);

                //Thống kê doanh thu theo năm, tháng và lưu vào Danh Sach
                DanhSach_DoanhThu_CacNam = ThongKe_DoanhThuCacNam(Goc_DoanhThu_ToanBo);
                //Thực hiện chuyển dữ liệu về dạng XmlElement;
                KQ = Chuyen_DanhSachThongKe_Thanh_Xml(DanhSach_DoanhThu_CacNam);

                return KQ;
            }
            return Goc_DoanhThu_ToanBo;
        }
        //Thực hiện chuyển dữ liệu về dạng XmlElement;
        private XmlElement Chuyen_DanhSachThongKe_Thanh_Xml(List<CDoanhThu_Nam> danhSach_DoanhThu_CacNam)
        {
            XmlDocument TaiLieu = new XmlDocument();
            XmlElement Root = TaiLieu.CreateElement("ROOT");
            for(int inam=0; inam < danhSach_DoanhThu_CacNam.Count; inam++)
            {
                XmlElement Nam_ = TaiLieu.CreateElement("NAM");
                Nam_.SetAttribute("Nam", "" + danhSach_DoanhThu_CacNam[inam].Nam);
                Nam_.SetAttribute("TongDoanhThu", "" + danhSach_DoanhThu_CacNam[inam].TinhTongDoanhThu_Nam());
                for(int ithang = 1;ithang <=12;ithang ++ )
                {
                    XmlElement Thang_ = TaiLieu.CreateElement("THANG");
                    Thang_.SetAttribute("Thang", "" + ithang);
                    Thang_.SetAttribute("TongDoanhThu", "" + danhSach_DoanhThu_CacNam[inam].DS_DoanhThu_CacThang[ithang].TongDoanhThu_Thang);
                    for(int iLoaiPhong = 1;iLoaiPhong<=3;iLoaiPhong++)
                    {
                        XmlElement LoaiPhong = TaiLieu.CreateElement("LOAI_PHONG");
                        LoaiPhong.SetAttribute("ID", "" + iLoaiPhong);
                        LoaiPhong.SetAttribute("DoanhThu", "" + danhSach_DoanhThu_CacNam[inam].DS_DoanhThu_CacThang[ithang].DS_DoanhThu_TheoLoaiPhong[iLoaiPhong].TongTien);

                        Thang_.AppendChild(LoaiPhong);
                    }

                    Nam_.AppendChild(Thang_);
                }
                Root.AppendChild(Nam_);
            }
                       
            TaiLieu.AppendChild(Root);

            return Root;
        }

        //Hàm chức năng đọc file xml dữ liệu thống kê và tổng hợp doanh thu theo từng năm, các tháng trong năm, các loại phòng mỗi tháng
        private List<CDoanhThu_Nam> ThongKe_DoanhThuCacNam(XmlElement goc_DoanhThu_ToanBo)
        {
            List<CDoanhThu_Nam> DanhSach_DoanhThu_CacNam = new List<CDoanhThu_Nam>();
            List<int> DanhSach_CacNam = new List<int>(); //Danh sách những năm có dữ liệu trong CSDL;

            string TenNode = goc_DoanhThu_ToanBo.ChildNodes[0].Name;
            foreach (XmlElement DoiTuong in goc_DoanhThu_ToanBo.SelectNodes(TenNode))
            {
                int val_Nam = Int32.Parse(DoiTuong.GetAttribute("Nam"));
                int val_Thang = Int32.Parse(DoiTuong.GetAttribute("Thang"));
                int val_ID_LoaiPhong = Int32.Parse(DoiTuong.GetAttribute("ID_LoaiPhong"));
                int val_SoTien = Int32.Parse(DoiTuong.GetAttribute("SoTien"));

                int ViTri = ViTri_Nam_Trong_DanhSach(val_Nam, DanhSach_CacNam);
                if (ViTri >= 0) //Năm đã được lưu trước đó
                {

                }
                else //Năm chưa được tạo
                {
                    DanhSach_CacNam.Add(val_Nam);
                    DanhSach_DoanhThu_CacNam.Add(new CDoanhThu_Nam(val_Nam)); //Thêm một năm mới vào ds các năm
                    ViTri = DanhSach_CacNam.Count - 1;
                }

                DanhSach_DoanhThu_CacNam[ViTri].DS_DoanhThu_CacThang[val_Thang].DS_DoanhThu_TheoLoaiPhong[val_ID_LoaiPhong].TongTien += val_SoTien;

            }
            return DanhSach_DoanhThu_CacNam;
        }

        //Trả về chỉ số vị trí của một năm trong danh sách các năm đang lưu
        private int ViTri_Nam_Trong_DanhSach(int val_Nam, List<int> danhSach_CacNam)
        {
            int vitri = -1;
            for(int i=0;i<danhSach_CacNam.Count;i++)
            {
                if (danhSach_CacNam[i] == val_Nam) return i;
            }
            return vitri;
        }


        #endregion

        #region "Xử lý: Đọc dữ liệu - Đơn giá thuê"
        public XmlElement DocDuLieu_GiaThuePhong()
        {
            XmlElement Goc = null;
            if (CongNghe == CONG_NGHE_LUU_TRU.CSDL)
            {
                Goc = DocDuLieuTuCSDL(DULIEUDOC.GiaThuePhong);               
            }
            return Goc;
        }
        #endregion
        #region "Xử lý: Ghi dữ liệu - Đơn giá thêu"
        public void CapNhat_GiaThuePhong(string chuoiXml)
        {
            XmlDocument TaiLieu = new XmlDocument();
            TaiLieu.LoadXml(chuoiXml);
            XmlElement Goc = TaiLieu.DocumentElement;

            if(LoaiCSDL == LOAI_CSDL.Access)
            {
                foreach(XmlElement DoiTuong in Goc.ChildNodes)
                {
                    CGiaThuePhong GiaThue = new CGiaThuePhong();

                    GiaThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID"));
                    GiaThue.LoaiPhong = DoiTuong.GetAttribute("Ten");
                    GiaThue.GiaThuePhong = Int32.Parse(DoiTuong.GetAttribute("DonGiaThue"));

                    GhiDuLieuVaoCSDL(DULIEUGHI.CapNhat_GiaThuePhong, GiaThue);
                }
            }
        }
        #endregion

        #region "Xử lý: Ghi dữ liệu cập nhật phiếu thuê phòng"
        public void CapNhatPhieuThuePhong(string chuoiXML)
        {

            string Loai_Cap_Nhat = "";

            XmlDocument vdTai_Lieu = new XmlDocument();
            vdTai_Lieu.LoadXml(chuoiXML);

            XmlElement vdRoot = vdTai_Lieu.DocumentElement;
            foreach (XmlElement DoiTuong in vdRoot.SelectNodes(vdRoot.ChildNodes[0].Name))
            {
                Loai_Cap_Nhat = DoiTuong.GetAttribute("LoaiCapNhat").Trim();
            }     

            switch(Loai_Cap_Nhat)
            {
                case "TraPhong":
                    CapNhat_TraPhong(chuoiXML);
                    break;
                case "ThuePhong":
                    CapNhat_ThuePhong(chuoiXML);
                    break;
            }
         }

        private void CapNhat_ThuePhong(string chuoiXML)
        {
            CPhieuThuePhong PhieuThue = new CPhieuThuePhong();

            XmlDocument Tai_Lieu = new XmlDocument();
            Tai_Lieu.LoadXml(chuoiXML);

            XmlElement Root = Tai_Lieu.DocumentElement;
            foreach (XmlElement DoiTuong in Root.SelectNodes(Root.ChildNodes[0].Name))
            {
                PhieuThue.NgayBatDau = DoiTuong.GetAttribute("NgayBatDau").Trim();
                PhieuThue.NgayDuKienTra = DoiTuong.GetAttribute("NgayDuKienTra").Trim();
                PhieuThue.MaPhong = Int32.Parse(DoiTuong.GetAttribute("ID_Phong").Trim());
                PhieuThue.DanhSachTenKhachHang = DoiTuong.GetAttribute("DS_TenKhachHang").Trim();
                PhieuThue.DanhSachCMNDKhachHang = DoiTuong.GetAttribute("DS_CMND").Trim();

            }
            GhiDuLieuVaoCSDL(DULIEUGHI.CapNhat_ThuePhong, PhieuThue);
        }

        private void CapNhat_TraPhong(string chuoiXML)
        {
            CPhieuThuePhong PhieuThue = new CPhieuThuePhong();

            XmlDocument Tai_Lieu = new XmlDocument();
            Tai_Lieu.LoadXml(chuoiXML);

            XmlElement Root = Tai_Lieu.DocumentElement;
            foreach (XmlElement DoiTuong in Root.SelectNodes(Root.ChildNodes[0].Name))
            {
                PhieuThue.ID = Int32.Parse(DoiTuong.GetAttribute("ID").Trim());
                PhieuThue.NgayTra = DoiTuong.GetAttribute("NgayTra").Trim();
                PhieuThue.TienThuePhong = Int32.Parse(DoiTuong.GetAttribute("SoTien").Trim());                
            }           

            GhiDuLieuVaoCSDL(DULIEUGHI.CapNhat_TraPhong, PhieuThue);
        }
        #endregion               

        #region "Đọc dữ liệu từ csdl"
        protected XmlElement DocDuLieuTuCSDL( DULIEUDOC DLDoc)
        {
            XmlElement DuLieuXml = null;

            DataSet DuLieuADO = new DataSet();
            DuLieuADO.DataSetName = TenDuLieuLuuTru;
            
            switch (DLDoc)
            {
                case DULIEUDOC.TraCuuPhieuThue:
                    DocBang("PHIEU_THUE_PHONG.ID AS ID, PHIEU_THUE_PHONG.NgayBatDau AS NgayBatDau, PHIEU_THUE_PHONG.NgayDuKienTra AS NgayDuKienTra, PHIEU_THUE_PHONG.NgayTra AS NgayTra, PHIEU_THUE_PHONG.SoTien AS SoTien, PHONG.ID AS ID_Phong, PHONG.Ten AS TenPhong, LOAI_PHONG.ID AS ID_Loai, LOAI_PHONG.Ten AS LoaiPhong, PHIEU_THUE_PHONG.DS_TenKhachHang AS DS_TenKhachHang, PHIEU_THUE_PHONG.DS_CMND AS DS_CMND",
                        "LOAI_PHONG INNER JOIN (PHONG INNER JOIN PHIEU_THUE_PHONG ON PHONG.ID = PHIEU_THUE_PHONG.ID_Phong) ON LOAI_PHONG.ID = PHONG.ID_LoaiPhong ORDER BY PHIEU_THUE_PHONG.NgayBatDau DESC",
                        "",
                        DuLieuADO);
                    break;
                case DULIEUDOC.ThongKeDoanhThu:
                    DocBang("Year(PHIEU_THUE_PHONG.NgayTra) AS Nam, Month(PHIEU_THUE_PHONG.NgayTra) AS Thang, LOAI_PHONG.ID AS ID_LoaiPhong, PHIEU_THUE_PHONG.SoTien AS SoTien",
                        "LOAI_PHONG INNER JOIN (PHONG INNER JOIN PHIEU_THUE_PHONG ON PHONG.ID = PHIEU_THUE_PHONG.ID_Phong) ON LOAI_PHONG.ID = PHONG.ID_LoaiPhong",
                        "SoTien > 0 and PHONG.ID_KhuVuc = 1 ORDER BY Year(PHIEU_THUE_PHONG.NgayTra) DESC , Month(PHIEU_THUE_PHONG.NgayTra) DESC , LOAI_PHONG.ID",
                        DuLieuADO);
                    break;
                case DULIEUDOC.GiaThuePhong:
                    DocBang("ID, Ten, DonGiaThue",
                        "LOAI_PHONG ORDER BY ID",
                        "",
                        DuLieuADO
                        );
                    break;
                case DULIEUDOC.TiepTan_DangNhap:
                    DocBang("TIEPTAN_DANGNHAP.ID AS ID, TIEPTAN_DANGNHAP.TenDangNhap AS TenDangNhap, TIEPTAN_DANGNHAP.MatKhau AS MatKhau, TIEPTAN_DANGNHAP.ID_NhanVien AS ID_NhanVien, KHU_VUC.MaSo AS MaKhuVuc",
                        "TIEPTAN_DANGNHAP INNER JOIN (KHU_VUC INNER JOIN NHAN_VIEN ON KHU_VUC.ID = NHAN_VIEN.ID_KhuVuc) ON TIEPTAN_DANGNHAP.ID_NhanVien = NHAN_VIEN.ID",
                        "",
                        DuLieuADO);
                    break;
                case DULIEUDOC.QLKhuVuc_DangNhap:
                    DocBang("*",
                        "QLKHUVUC_DANGNHAP",
                        "",
                        DuLieuADO);
                    break;
                case DULIEUDOC.QLKhachSan_DangNhap:
                    DocBang("*",
                        "QLKHACHSAN_DANGNHAP",
                        "",
                        DuLieuADO);
                    break;
                case DULIEUDOC.ThongTinPhong:
                    //Đọc vào thông tin các tầng, phục vụ tạo bảng phòng
                    DocBang("TANG.ID AS ID, TANG.Ten AS Tang, KHU_VUC.Ten AS KhuVuc, KHU_VUC.MaSo as MaSoKhuVuc",
                        "KHU_VUC INNER JOIN TANG ON KHU_VUC.ID = TANG.ID_KHUVUC",
                        "",
                        DuLieuADO); 
                    //Đọc vào thông tin các phòng                
                    DocBang("PHONG.ID AS ID, PHONG.Ten AS Ten, TANG.ID AS ID_Tang, TANG.Ten AS Tang, KHU_VUC.Ten AS KhuVuc, LOAI_PHONG.Ten AS LoaiPhong, LOAI_PHONG.TienNghi AS TienNghi, LOAI_PHONG.SoKhachToiDa AS SoKhachToiDa, LOAI_PHONG.DonGiaThue AS DonGiaThue",
                        "(LOAI_PHONG INNER JOIN (KHU_VUC INNER JOIN PHONG ON KHU_VUC.ID = PHONG.ID_KhuVuc) ON LOAI_PHONG.ID = PHONG.ID_LoaiPhong) INNER JOIN TANG ON PHONG.ID_Tang = TANG.ID ORDER BY PHONG.Ten",
                        "",
                        DuLieuADO);
                    //Đọc vào thông tin phiếu thuê chưa trả phòng
                    DocBang("*",
                        "PHIEU_THUE_PHONG",
                        "((PHIEU_THUE_PHONG.NgayTra) Is Null)",
                        DuLieuADO);
                    break;
                case DULIEUDOC.PhieuThuePhong:
                    DocBang(" *",
                        "PHIEU_THUE_PHONG",
                        "",
                        DuLieuADO);
                    break;
            }       

            string ChuoiXml = DuLieuADO.GetXml();
            XmlDocument TaiLieu = new XmlDocument();
            TaiLieu.LoadXml(ChuoiXml);
            DuLieuXml = TaiLieu.DocumentElement;
            return DuLieuXml;
        }

        protected DataTable DocBang(string DanhSachCot,string TenBang, string DieuKien , DataSet DuLieuADO = null)
        {
            DataTable BangDuLieu = new DataTable();
            if (LoaiCSDL == LOAI_CSDL.Access)
            {
                BangDuLieu = DocBangAccess(DanhSachCot,TenBang, DieuKien, DuLieuADO);
            }
            return BangDuLieu;
        }
        #endregion

        #region "Ghi dữ liệu vào csdl"
        protected void GhiDuLieuVaoCSDL(DULIEUGHI DLGhi, Object DuLieu)
        {
            string BangCapNhat, GiaTriCapNhat, DieuKienCapNhat;
            string BangThem, DSThuocTinh, DSGiaTri;
            switch(DLGhi)
            {
                case DULIEUGHI.CapNhat_GiaThuePhong:
                    BangCapNhat = "LOAI_PHONG";
                    GiaTriCapNhat = "LOAI_PHONG.DonGiaThue = " + ((CGiaThuePhong)DuLieu).GiaThuePhong;
                    DieuKienCapNhat = "LOAI_PHONG.ID = " + ((CGiaThuePhong)DuLieu).ID;

                    CapNhatBangAccess(BangCapNhat, GiaTriCapNhat, DieuKienCapNhat);
                    break;

                case DULIEUGHI.CapNhat_TraPhong:
                    BangCapNhat = "PHIEU_THUE_PHONG";
                    string NgayTra = ((CPhieuThuePhong)DuLieu).NgayTra;
                    //Convert lại ngày theo dạy mm/dd/yyyy theo csdl access
                    string NgayTraConvert =  "#" + NgayTra.Substring(3, 2) + "/" + NgayTra.Substring(0, 2) + "/" + NgayTra.Substring(6, 4) + "#";

                    GiaTriCapNhat = "PHIEU_THUE_PHONG.NgayTra = " + NgayTraConvert + ", PHIEU_THUE_PHONG.SoTien = " + ((CPhieuThuePhong)DuLieu).TienThuePhong;
                    DieuKienCapNhat = "PHIEU_THUE_PHONG.[ID] =" + ((CPhieuThuePhong)DuLieu).ID;

                    CapNhatBangAccess(BangCapNhat, GiaTriCapNhat, DieuKienCapNhat);                    
                    break;

                case DULIEUGHI.CapNhat_ThuePhong:
                    BangThem = "PHIEU_THUE_PHONG";

                    string NgayBatDau = ((CPhieuThuePhong)DuLieu).NgayBatDau;
                    string NgayBatDauConvert = "#" + NgayBatDau.Substring(3, 2) + "/" + NgayBatDau.Substring(0, 2) + "/" + NgayBatDau.Substring(6, 4) + "#";

                    string NgayDuKienTra = ((CPhieuThuePhong)DuLieu).NgayDuKienTra;
                    string NgayDuKienTraConvert = "#" + NgayDuKienTra.Substring(3, 2) + "/" + NgayDuKienTra.Substring(0, 2) + "/" + NgayDuKienTra.Substring(6, 4) + "#";

                    DSThuocTinh = "NgayBatDau,NgayDuKienTra,ID_Phong,DS_TenKhachHang,DS_CMND";
                    DSGiaTri = NgayBatDauConvert+", "+ NgayDuKienTraConvert+", " + ((CPhieuThuePhong)DuLieu).MaPhong
                        + ", '" + ((CPhieuThuePhong)DuLieu).DanhSachTenKhachHang + "' , '" + ((CPhieuThuePhong)DuLieu).DanhSachCMNDKhachHang + "' ";

                    ThemMoiBangAccess(BangThem, DSThuocTinh, DSGiaTri);
                    break;
            }
        }
        #endregion

        #region "Xử lý CSDL Access"
        protected DataTable DocBangAccess(string DanhSachCot,string TenBang, string DieuKien , DataSet DuLieuADO = null)
        {
            DataTable BangDuLieu = new DataTable(TenBang);
            
            string ChuoiLenh = "SELECT " + DanhSachCot + " FROM " + TenBang;
                        
            if (DieuKien.Trim() != "")
            {
                ChuoiLenh += " WHERE " + DieuKien;
            }

            try
            {
                    OleDbDataAdapter BoThichUng = new OleDbDataAdapter(ChuoiLenh, ChuoiKetNoiAccess);
                    BoThichUng.FillSchema(BangDuLieu, SchemaType.Source);
                    BoThichUng.Fill(BangDuLieu);
                    if (DuLieuADO != null)
                    {
                        DuLieuADO.Tables.Add(BangDuLieu);
                    }

                    BangDuLieu.Columns[0].ReadOnly = false;
                    foreach (DataColumn Cot in BangDuLieu.Columns)
                    {
                        Cot.ColumnMapping = MappingType.Attribute;
                    }
                }
                catch (Exception e)
                {
                    BangDuLieu.ExtendedProperties["ChuoiLoi"] = e.Message;          
                }
            
            return BangDuLieu;
        }

        protected void CapNhatBangAccess(string TenBang, string GiaTriCapNhat, string DieuKien)
        {
            string ChuoiLenh = "UPDATE " + TenBang + " SET " + GiaTriCapNhat;
            if (DieuKien.Trim() != "")
            {
                ChuoiLenh += " WHERE " + DieuKien;
            }

            //string ChuoiLenhtest = "UPDATE PHIEU_THUE_PHONG SET PHIEU_THUE_PHONG.NgayTra = #12/9/2016#, PHIEU_THUE_PHONG.SoTien = 600000 WHERE(((PHIEU_THUE_PHONG.ID) = 1))";

            try
            {
                //OleDbDataAdapter BoThichUng = new OleDbDataAdapter(ChuoiLenh, ChuoiKetNoiAccess);
                OleDbConnection con = new OleDbConnection(ChuoiKetNoiAccess);
                OleDbCommand update = new OleDbCommand(ChuoiLenh, con);
                con.Open();
                update.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {

            }
        }
        protected void ThemMoiBangAccess(string TenBang, string DanhSach_ThuocTinh, string DanhSach_GiaTri)
        {
            string ChuoiLenh = "INSERT INTO " + TenBang + " (" + DanhSach_ThuocTinh + ") "
            + " VALUES (" + DanhSach_GiaTri + ") "; 
            //string ChuoiLenhtest = "INSERT INTO PHIEU_THUE_PHONG (NgayBatDau, NgayDuKienTra, ID_Phong,DS_TenKhachHang,DS_CMND) VALUES(#12/04/2016#,#12/9/2016#,9,'AnhKhoa|','123123|')";
            try
            {
                //OleDbDataAdapter BoThichUng = new OleDbDataAdapter(ChuoiLenh, ChuoiKetNoiAccess);
                OleDbConnection con = new OleDbConnection(ChuoiKetNoiAccess);
                OleDbCommand insert = new OleDbCommand(ChuoiLenh, con);
                con.Open();
                insert.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {

            }
        }


        #endregion

        #region "Xử lý: Đọc - ghi dữ liệu phi cấu trúc(media)"
        public byte[] DocHinh(string Ma_so)
        {
            byte[] Kq = new byte[] { };
            string duongdanphu = ThuMucHinh + @"\KhachSan_1.png";
            string DuongDanHinh = ThuMucHinh + @"\" + Ma_so + ".png";
            try
            {
                Kq = File.ReadAllBytes(DuongDanHinh);
                //Kq = File.ReadAllBytes(duongdanphu);
            }
            catch
            {
                Kq = new byte[] { };

            }

            //BinaryReader binReader = new BinaryReader(File.Open(DuongDanHinh, FileMode.Open, FileAccess.Read));
            //binReader.BaseStream.Position = 0;
            //byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            //binReader.Close();
            //return binFile;

            return Kq;
        }

        public string GhiHinh(string Ma_so, byte[] Nhi_phan_Hinh)
        {
            string Kq = "";
            // Sinh viên tự thực hiện
            return Kq;
        }

       

        #endregion
    }
}