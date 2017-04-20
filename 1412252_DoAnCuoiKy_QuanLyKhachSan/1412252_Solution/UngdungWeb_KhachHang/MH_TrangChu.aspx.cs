using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UngdungWeb_KhachHang
{
    public partial class MH_TrangChu : System.Web.UI.Page
    {
        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU LuuTru = new XL_LUUTRU();
        protected XL_NGHIEPVU NghiepVu = new XL_NGHIEPVU();
        protected XL_THEHIEN TheHien = new XL_THEHIEN();

        #endregion

        #region "Xử lý Biến cố : Khởi động"
        public void XL_KhoiDong()
        {
            byte[] MaNhiPhan_HinhAnh = LuuTru.DocHinh("KhachSan_3");           
            TheHien.XuatHinh(MaNhiPhan_HinhAnh, Img_AnhKhachSan);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            XL_KhoiDong();
        }

        protected void ID_BTN_TRANGCHU_Click(object sender, EventArgs e)
        {
            
        }
    }
}