using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngdungDesktop_QuanLyKhuVuc
{
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
            for (int i = 0; i <= 3; i++)
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
            for (int iLoaiPhong = 0; iLoaiPhong <= 3; iLoaiPhong++)
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

        public int TinhDoanhThu_Nam_TheoLoai(int Loai)
        {
            int Tong = 0;
            switch(Loai)
            {
                case 1:
                    for(int i=1;i<=12;i++)
                    {
                        Tong += DS_DoanhThu_CacThang[i].DS_DoanhThu_TheoLoaiPhong[1].TongTien;
                    }
                    break;
                case 2:
                    for (int i = 1; i <= 12; i++)
                    {
                        Tong += DS_DoanhThu_CacThang[i].DS_DoanhThu_TheoLoaiPhong[2].TongTien;
                    }
                    break;
                case 3:
                    for (int i = 1; i <= 12; i++)
                    {
                        Tong += DS_DoanhThu_CacThang[i].DS_DoanhThu_TheoLoaiPhong[3].TongTien;
                    }
                    break;
            }
            return Tong;
        }
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
            for (int i = 0; i <= 12; i++)
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
}
