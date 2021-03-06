﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UngdungDesktop_QuanLyKhachSan.DICHVU_ASMX_;
using System.Windows.Forms;

namespace UngdungDesktop_QuanLyKhachSan
{
    public class XL_LUUTRU
    {

        #region "Biến/Đối tượng toàn cục"
       
        DICHVU_ASMXSoapClient DichVu = new DICHVU_ASMXSoapClient();
        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Đọc - Ghi dữ liệu có cấu trúc"
        public XmlElement DocDuLieu_DangNhap_QuanLyKhachSan()
        {
            XmlElement DuLieuXml = null;
            try
            {
                string Chuoi_Xml = DichVu.DocDuLieu_DangNhap_QLKhachSan();
                XmlDocument Tai_Lieu = new XmlDocument();
                Tai_Lieu.LoadXml(Chuoi_Xml);
                DuLieuXml = Tai_Lieu.DocumentElement;                
            }
            catch
            {

            }            
            return DuLieuXml;
        }

        public string DocDuLieu_GiaThuePhong()
        {
            string KQ = "";
            KQ = DichVu.DocDuLieu_GiaThuePhong();
            return KQ;
        }

        public string DocDuLieu_ThongKeDoanhThu()
        {
            string kq = "";
            try
            {
                kq = DichVu.DocDuLieu_ThongKeDoanhThu();
            }
            catch (Exception e)
            {

            }
            return kq;

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

        public void GhiDuLIeu_GiaThuePhong(string chuoiXml)
        {
            DichVu.GhiDuLieu_GiaThuePhong(chuoiXml);
        }
        #endregion


    }
}
