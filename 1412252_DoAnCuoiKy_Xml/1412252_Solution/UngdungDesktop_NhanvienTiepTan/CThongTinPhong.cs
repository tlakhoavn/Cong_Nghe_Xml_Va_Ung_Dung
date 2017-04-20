using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngdungDesktop_NhanvienTiepTan
{
    public class CThongTinPhong
    {
        protected int m_ID;
        protected string m_Ten;
        protected string m_Tang;
        protected string m_KhuVuc;
        protected string m_LoaiPhong;
        protected string m_TienNghi;
        protected int m_SoKhachToiDa;
        protected int m_DonGia;
        

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
        public string Ten
        {
            get
            {
                return m_Ten;
            }

            set
            {
                m_Ten = value;
            }
        }
        public string Tang
        {
            get
            {
                return m_Tang;
            }

            set
            {
                m_Tang = value;
            }
        }
        public string KhuVuc
        {
            get
            {
                return m_KhuVuc;
            }

            set
            {
                m_KhuVuc = value;
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
        public string TienNghi        {
            get
            {
                return m_TienNghi;
            }

            set
            {
                m_TienNghi = value;
            }
        }
        public int SoKhachToiDa
        {
            get
            {
                return m_SoKhachToiDa;
            }

            set
            {
                m_SoKhachToiDa = value;
            }
        }
        public int DonGia
        {
            get
            {
                return m_DonGia;
            }

            set
            {
                m_DonGia = value;
            }
        }
       
    }
}
