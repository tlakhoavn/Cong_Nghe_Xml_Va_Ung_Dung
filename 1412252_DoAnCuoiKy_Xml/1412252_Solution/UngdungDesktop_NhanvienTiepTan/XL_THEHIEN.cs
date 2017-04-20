using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace UngdungDesktop_NhanvienTiepTan
{
    public class XL_THEHIEN
    {
        #region "Biến/đối tượng toàn cục"      

        protected int ButtonKhuVuc_Rong = 180;
        protected int ButtonKhuVuc_Cao_Hien = 50;
        protected int ButtonKhuVuc_Cao_An = 40;
        protected int   ButtonKhuVuc_Top = 145;

        protected int Panel_Tren = 225;
        protected int Panel_Trai = 50;
        protected int Panel_Rong = 660;
        protected int Panel_Cao = 300;  
        
           
        #endregion

        #region "Xử lý: Khởi động"
        #endregion

        #region "Xử lý: Hiển thị bảng thông tin các phòng"
        

        //Hiện và ẩn khu vực khi người dùng lick chọn tab
        public void HienBangPhongKhuVuc(CKhuVuc_TheHien KhuVucHien, CKhuVuc_TheHien KhuVucAn1, CKhuVuc_TheHien KhuVucAn2 )
        {

            KhuVucHien.PanelBangPhongKhuVuc.Visible = true;
            KhuVucAn1.PanelBangPhongKhuVuc.Visible = false;
            KhuVucAn2.PanelBangPhongKhuVuc.Visible = false;
           

            KhuVucHien.BtnKhuVuc.ForeColor = Color.Black;
            KhuVucAn1.BtnKhuVuc.ForeColor = Color.Black;
            KhuVucAn2.BtnKhuVuc.ForeColor = Color.Black;

            KhuVucHien.BtnKhuVuc.BackColor = Color.OrangeRed;
            KhuVucAn1.BtnKhuVuc.BackColor = Color.Gray;
            KhuVucAn2.BtnKhuVuc.BackColor = Color.Gray;

            KhuVucHien.BtnKhuVuc.Size = new Size(ButtonKhuVuc_Rong, ButtonKhuVuc_Cao_Hien);
            KhuVucAn1.BtnKhuVuc.Size = new Size(ButtonKhuVuc_Rong, ButtonKhuVuc_Cao_An);
            KhuVucAn2.BtnKhuVuc.Size = new Size(ButtonKhuVuc_Rong, ButtonKhuVuc_Cao_An);


            KhuVucHien.BtnKhuVuc.Top = ButtonKhuVuc_Top;
            KhuVucAn1.BtnKhuVuc.Top = ButtonKhuVuc_Top + 10;
            KhuVucAn2.BtnKhuVuc.Top = ButtonKhuVuc_Top + 10;      


        }
        public void KhoiTaoViTriPanel(Panel pn1, Panel pn2, Panel pn3)
        {
            pn1.Location = new Point(Panel_Trai, Panel_Tren);
            pn2.Location = new Point(Panel_Trai, Panel_Tren);
            pn3.Location = new Point(Panel_Trai, Panel_Tren);

            pn1.Size = new Size(Panel_Rong, Panel_Cao);
            pn2.Size = new Size(Panel_Rong, Panel_Cao);
            pn3.Size = new Size(Panel_Rong, Panel_Cao);
        }
               
       
        #endregion

        #region "Xử lý: Nhập xuất"
        public void Xuat_Chuoi(string Chuoi, Control Ctr)
        {
           Ctr.Text = Chuoi;
        }

        public string Nhap_Chuoi(Control Ctr)
        {
            return Ctr.Text.Trim();
        }
        #endregion

        #region "Xử lý : Nhập - Xuất Hình"
        public void Xuat_Hinh(byte[] ChuoiNhiPhan, Button Hinh)
        {
            if (ChuoiNhiPhan.Length > 100)
            {
                MemoryStream Luong = new MemoryStream(ChuoiNhiPhan);
                Bitmap bmpHinh= (Bitmap)Bitmap.FromStream(Luong);
                Hinh.BackgroundImage = bmpHinh;
                Hinh.BackgroundImageLayout = ImageLayout.Stretch;
                //Hinh = bmpHinh;
            }
        }
        #endregion

        #region "Xử lý chung"

        //Hiển một control
        public void HienDieuKhien(Control ctr)
        {
            ctr.Show();
        }
        //Ẩn một control
        public void AnDieuKhien(Control ctr)
        {
            ctr.Hide(); 
        }

        //Hàm khởi tạo một text box
        public TextBox TaoTextBox(Size Size_, int Left_, int Top_)
        {
            TextBox tb = new TextBox();

            tb.Size = Size_;
            tb.Top = Top_;
            tb.Left = Left_;
            tb.ForeColor = Color.Black;
            tb.BackColor = Color.White;
            tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));

            return tb;
        }
        //Trả về độ dài text của một textbox
        public int DoDai_Chuoi(TextBox txtbNgayDuKienTra)
        {
            return txtbNgayDuKienTra.TextLength;
        }
        //Xóa các control con được add vào panel
        public void XoaControl_Con(Panel panel)
        {
            panel.Controls.Clear();
        }
        #endregion
    }
}
