using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngdungDesktop_QuanLyKhachSan
{
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
}
