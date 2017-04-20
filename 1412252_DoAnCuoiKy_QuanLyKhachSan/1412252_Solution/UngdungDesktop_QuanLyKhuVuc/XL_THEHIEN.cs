using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace UngdungDesktop_QuanLyKhuVuc
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

        #region "Các hàm xử lý chung"
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

        internal int DoDai_Chuoi(TextBox txtbNgayDuKienTra)
        {
            return txtbNgayDuKienTra.TextLength;
        }

        internal void TuChoiSuDung(Control ctr )
        {
            ctr.Enabled = false;
        }

        internal void ChoPhepSuDung(Control ctr)
        {
            ctr.Enabled = true;
        }
        //Xóa các control con được add vào panel
        public void XoaControl_Con(Panel panel)
        {
            panel.Controls.Clear();
        }

        #endregion
    }
}
