﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using UngdungWeb_KhachHang.DICHVU_ASMX_;


namespace UngdungWeb_KhachHang
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

    }
}