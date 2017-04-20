using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngdungDesktop_QuanLyKhuVuc
{
    public class CThongTinKhachHang
    {
     
        protected string m_HoTen;
        protected string m_CMND;

    
        public string HoTen
        {
            get
            {
                return m_HoTen;
            }

            set
            {
                m_HoTen = value;
            }
        }
        public string CMND
        {
            get
            {
                return m_CMND;
            }

            set
            {
                m_CMND = value;
            }
        }
    }
}
