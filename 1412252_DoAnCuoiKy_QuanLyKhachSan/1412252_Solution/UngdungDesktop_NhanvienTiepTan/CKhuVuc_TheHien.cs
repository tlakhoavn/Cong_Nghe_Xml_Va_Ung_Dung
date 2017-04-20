using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngdungDesktop_NhanvienTiepTan
{
    public class CKhuVuc_TheHien
    {
        protected
        Panel m_panelBangPhongKhuVuc;
        Button m_btnKhuVuc;
        List<List<CThongTinPhong>> m_BangKhuVuc;

        public Button BtnKhuVuc
        {
            get
            {
                return m_btnKhuVuc;
            }

            set
            {
                m_btnKhuVuc = value;
            }
        }

        public Panel PanelBangPhongKhuVuc
        {
            get
            {
                return m_panelBangPhongKhuVuc;
            }

            set
            {
                m_panelBangPhongKhuVuc = value;
            }
        }

        public List<List<CThongTinPhong>> BangKhuVuc
        {
            get
            {
                return m_BangKhuVuc;
            }

            set
            {
                m_BangKhuVuc = value;
            }
        }
    }
}
