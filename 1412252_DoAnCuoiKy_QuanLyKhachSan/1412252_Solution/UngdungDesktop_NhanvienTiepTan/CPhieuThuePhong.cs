using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UngdungDesktop_NhanvienTiepTan
{
    public class CPhieuThuePhong
    {
        protected int m_ID;
        protected int m_MaPhong;
        protected string m_TenPhong;
        protected string m_LoaiPhong;
        protected string m_NgayBatDau;
        protected string m_NgayDuKienTra;
        protected string m_NgayTra;
        protected List<CThongTinKhachHang> m_DSKhachhang;
        protected int m_TienThuePhong;

        public CPhieuThuePhong()
        {
            m_ID = 0;
            m_MaPhong = 0;
            m_TenPhong = "";
            m_LoaiPhong = "";
            m_NgayBatDau = "";
            m_NgayDuKienTra = "";
            m_NgayTra = "";
            m_DSKhachhang = new List<CThongTinKhachHang>();
            m_TienThuePhong = 0;

        }

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
        public int MaPhong
        {
            get
            {
                return m_MaPhong;
            }

            set
            {
                m_MaPhong = value;
            }
        }
        public string TenPhong
        {
            get
            {
                return m_TenPhong;
            }

            set
            {
                m_TenPhong = value;
            }
        }
        public string NgayBatDau
        {
            get
            {
                return m_NgayBatDau;
            }

            set
            {
                m_NgayBatDau = value;
            }
        }
        public string NgayDuKienTra
        {
            get
            {
                return m_NgayDuKienTra;
            }

            set
            {
                m_NgayDuKienTra = value;
            }
        }
        public string NgayTra
        {
            get
            {
                return m_NgayTra;
            }

            set
            {
                m_NgayTra = value;
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
      
        public int TienThuePhong
        {
            get
            {
                return m_TienThuePhong;
            }

            set
            {
                m_TienThuePhong = value;
            }
        }

        public List<CThongTinKhachHang> DSKhachHang
        {
            get
            {
                return m_DSKhachhang;
            }

            set
            {
                m_DSKhachhang = value;
            }
        }
    }
}
