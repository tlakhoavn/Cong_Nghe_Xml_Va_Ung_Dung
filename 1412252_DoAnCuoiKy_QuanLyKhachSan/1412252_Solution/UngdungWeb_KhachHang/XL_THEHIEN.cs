using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace UngdungWeb_KhachHang
{
    public class XL_THEHIEN
    {
        #region "Biến/đối tượng toàn cục"
        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Nhập xuất"
        #endregion

        #region "Xử lý: Xuất hình ảnh media"
        public void XuatHinh(byte[] MaNhiPhan, HtmlImage ID_HinhAnh)
        {
            if (MaNhiPhan.Length > 100)
            {
                string Chuoi_64 = Convert.ToBase64String(MaNhiPhan);
                ID_HinhAnh.Src = "data:image;base64," + Chuoi_64;                
            }
        }
        #endregion
    }
}