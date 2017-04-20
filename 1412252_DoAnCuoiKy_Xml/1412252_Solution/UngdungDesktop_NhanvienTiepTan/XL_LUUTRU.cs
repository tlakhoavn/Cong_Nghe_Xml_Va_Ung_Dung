using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UngdungDesktop_NhanvienTiepTan.DICHVU_ASMX_;
using System.Windows.Forms;

namespace UngdungDesktop_NhanvienTiepTan
{
    public class XL_LUUTRU
    {

        #region "Biến/Đối tượng toàn cục"
       
        DICHVU_ASMXSoapClient DichVu = new DICHVU_ASMXSoapClient();
        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Đọc - Ghi dữ liệu có cấu trúc"
        public XmlElement DocDuLieu_DangNhap_TiepTan()
        {
            XmlElement DuLieuXml = null;
            try
            {
                string Chuoi_Xml = DichVu.DocDuLieu_DangNhap_TiepTan();
                XmlDocument Tai_Lieu = new XmlDocument();
                Tai_Lieu.LoadXml(Chuoi_Xml);
                DuLieuXml = Tai_Lieu.DocumentElement;                
            }
            catch
            {

            }            
            return DuLieuXml;
        }
        public XmlElement DocDuLieu_ThongTinPhong()
        {
            XmlElement DuLieuXml = null;
            try
            {
                string Chuoi_Xml = DichVu.DocDuLieu_ThongTinPhong();
                XmlDocument Tai_Lieu = new XmlDocument();
                Tai_Lieu.LoadXml(Chuoi_Xml);
                DuLieuXml = Tai_Lieu.DocumentElement;
                
            }
            catch
            {

            }
            return DuLieuXml;
        }

        #endregion

        #region "Xử lý: Đọc - Ghi dữ liệu Media"
        public byte[] DocHinh(string Maso)
        {
            byte[] kq = new byte[] { };
            try
            {
                kq = DichVu.DocHinh(Maso);              

            }
            catch
            {
                kq = new byte[] { };
               
            }           
            return kq;
        }
        #endregion

        #region "Xử lý: Ghi - Trả phòng"
        public void GhiDuLieu_TraPhong(CPhieuThuePhong phieuThue)
        {
            
            XmlDocument TaiLieu = new XmlDocument();          
            XmlElement Root = TaiLieu.CreateElement("ROOT");          
            XmlElement XEPhieuThue = TaiLieu.CreateElement("PHIEU_THUE_PHONG");

            XEPhieuThue.SetAttribute("LoaiCapNhat", "TraPhong");  //Đánh dấu loại cập nhật phiếu thuê: trả phòng
            XEPhieuThue.SetAttribute("ID", "" + phieuThue.ID);            
            XEPhieuThue.SetAttribute("NgayTra", "" + phieuThue.NgayTra);
            XEPhieuThue.SetAttribute("SoTien", "" + phieuThue.TienThuePhong);         

     
            Root.AppendChild(XEPhieuThue);       
            TaiLieu.AppendChild(Root);

            CapNhatPhieuThuePhong(TaiLieu.DocumentElement.OuterXml);    
            

            
        }

       
        #endregion

        #region "Xử lý: Ghi - Thuê phòng"
        public void GhiDuLieu_ThuePhong(CPhieuThuePhong phieuThue)
        {

            XmlDocument TaiLieu = new XmlDocument();
            XmlElement Root = TaiLieu.CreateElement("ROOT");
            XmlElement XEPhieuThue = TaiLieu.CreateElement("PHIEU_THUE_PHONG");

            XEPhieuThue.SetAttribute("LoaiCapNhat", "ThuePhong");  //Đánh dấu loại cập nhật phiếu thuê: thuê phòng
            XEPhieuThue.SetAttribute("ID_Phong", "" + phieuThue.MaPhong);
            XEPhieuThue.SetAttribute("NgayBatDau", "" + phieuThue.NgayBatDau);
            XEPhieuThue.SetAttribute("NgayDuKienTra", "" + phieuThue.NgayDuKienTra);

            string DSTenKhachHang = "";
            string DSCMNDKhachhang = "";
            for(int i=0;i<phieuThue.DSKhachHang.Count;i++)
            {
                DSTenKhachHang += phieuThue.DSKhachHang[i].HoTen + "|";
                DSCMNDKhachhang += phieuThue.DSKhachHang[i].CMND + "|";
            }
            XEPhieuThue.SetAttribute("DS_TenKhachHang", DSTenKhachHang);
            XEPhieuThue.SetAttribute("DS_CMND", DSCMNDKhachhang);

            Root.AppendChild(XEPhieuThue);
            TaiLieu.AppendChild(Root);

            //Thực hiện một lệnh cập nhật phiếu
            CapNhatPhieuThuePhong(TaiLieu.DocumentElement.OuterXml);

        }
        #endregion

        private void CapNhatPhieuThuePhong(string strTaiLieuXML)
        {
            DichVu.GhiDuLieu_CapNhatPhieuThuePhong(strTaiLieuXML);
        }

    }
}
