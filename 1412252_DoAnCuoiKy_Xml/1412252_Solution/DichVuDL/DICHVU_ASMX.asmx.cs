using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace DichVuDL
{
    /// <summary>
    /// Summary description for DICHVU_ASMX
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DICHVU_ASMX : System.Web.Services.WebService
    {

        protected static XL_LUUTRU_DV LuuTru = new XL_LUUTRU_DV();

        #region "Đoc+Ghi - Tra cứu phiếu thuê phòng"
        [WebMethod]
        public string TraCuuPhieuThuePhong(string ChuoiXmlTruyVan)
        {
            string KQ = "";
            KQ = LuuTru.TraCuuPhieuThuePhong(ChuoiXmlTruyVan);
            return KQ;
        }
        #endregion

        #region "Đọc - Dữ liệu phiếu thuê phòng"
        [WebMethod]
        public string DocDuLieu_PhieuThuePhong()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_PhieuThuePhong();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }
        #endregion
        #region "Đọc - Dữ liệu đăng nhập"
        [WebMethod]
        public string DocDuLieu_DangNhap_TiepTan()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_DangNhap_TiepTan();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }

        [WebMethod]
        public string DocDuLieu_DangNhap_QLKhuVuc()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_DangNhap_QLKhuVuc();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }

        [WebMethod]
        public string DocDuLieu_DangNhap_QLKhachSan()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_DangNhap_QLKhachSan();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }
        #endregion
        #region "Đọc - Dữ liệu thông tin phòng, tần, khu vực"
        [WebMethod]
        public string DocDuLieu_ThongTinPhong()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_ThongTinPhong();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }
        #endregion

        #region "Đọc - Thống kê doanh thu theo tháng"
        [WebMethod]
        public string DocDuLieu_ThongKeDoanhThu()
        {
            string ChuoiXml = "";

            XmlElement DuLieu = LuuTru.DocDuLieu_ThongKeDoanhThu();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }
        #endregion
       
        #region "Ghi - Cập nhật phiếu thuê phòng: Thuê/Trả phòng"
        [WebMethod]
        public void GhiDuLieu_CapNhatPhieuThuePhong(string ChuoiPhieuThue_)
        {
            LuuTru.CapNhatPhieuThuePhong(ChuoiPhieuThue_);
        }
        #endregion


        #region "Đọc - Dữ liệu giá thuê phòng"
        [WebMethod]
        public string DocDuLieu_GiaThuePhong()
        {                     
            string ChuoiXml = "";
            XmlElement DuLieu = LuuTru.DocDuLieu_GiaThuePhong();
            ChuoiXml = DuLieu.OuterXml;
            return ChuoiXml;
        }
        #endregion
        #region "Ghi - Dữ liệu giá thuê phòng"
        [WebMethod]
        public void GhiDuLieu_GiaThuePhong(string ChuoiXml)
        {
            LuuTru.CapNhat_GiaThuePhong(ChuoiXml);
        }
        #endregion

        #region "Đọc/ghi dữ liệu phi cấu trúc media"

        [WebMethod]
        public byte[] DocHinh(string Ma_so)
        {
            byte[] Kq;
            Kq = LuuTru.DocHinh(Ma_so);
            return Kq;
        }
        [WebMethod]
        public string GhiHinh(string Ma_so, byte[] Nhi_phan_Hinh)
        {
            string Kq = "";
            // Sinh viên tự thực hiện
            return Kq;
        }
        #endregion
    }
}
