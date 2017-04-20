using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngdungDesktop_QuanLyKhachSan
{
   
    public partial class MH_CapNhat_GiaThuePhong : Form
    {
        public MH_CapNhat_GiaThuePhong()
        {
            InitializeComponent();
            KhoiDong();
        }

        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU LuuTru = new XL_LUUTRU();
        protected XL_NGHIEPVU NghiepVu = new XL_NGHIEPVU();
        protected XL_THEHIEN TheHien = new XL_THEHIEN();

        List<CGiaThuePhong> BangGiaThuePhong ;
        #endregion

        #region "Xử lý: Khởi động"
        private void KhoiDong()
        {
            //Đưa màn hình về giữa desktop
            this.StartPosition = FormStartPosition.CenterScreen;
            BangGiaThuePhong = new List<CGiaThuePhong>();

                   

            HienThiBangGiaThuePhong();
        }
        #endregion

        private void HienThiBangGiaThuePhong()
        {
            TheHien.Xuat_Chuoi("", labelThongBao);

            string ChuoiXml = LuuTru.DocDuLieu_GiaThuePhong();
            BangGiaThuePhong = NghiepVu.Chuyen_Xml_thanh_BangGiaThuePhong(ChuoiXml);

            TheHien.Xuat_Chuoi(BangGiaThuePhong[1].LoaiPhong, labelTenLoai1);
            TheHien.Xuat_Chuoi(BangGiaThuePhong[2].LoaiPhong, labelTenLoai2);
            TheHien.Xuat_Chuoi(BangGiaThuePhong[3].LoaiPhong, labelTenLoai3);

            TheHien.Xuat_Chuoi(BangGiaThuePhong[1].GiaThuePhong + "", txtbDonGia_Loai1);
            TheHien.Xuat_Chuoi(BangGiaThuePhong[2].GiaThuePhong + "", txtbDonGia_Loai2);
            TheHien.Xuat_Chuoi(BangGiaThuePhong[3].GiaThuePhong + "", txtbDonGia_Loai3);
         } 
        private void MH_CapNhat_GiaThuePhong_Load(object sender, EventArgs e)
        {
            
        }
        //Chỉ được nhập ký tự số vào khung giá.
        private void txtbDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        //Thực hiện cập nhật giá thuê phòng
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            

            if (TheHien.ChuoiRong(txtbDonGia_Loai1))
            {
                TheHien.Xuat_Chuoi("Vui lòng bổ sung đơn giá thuê " + TheHien.Nhap_Chuoi(labelTenLoai1) + " trước khi cập nhật.",labelThongBao);
                return;
            }
            if (TheHien.ChuoiRong(txtbDonGia_Loai2))
            {
                TheHien.Xuat_Chuoi("Vui lòng bổ sung đơn giá thuê " + TheHien.Nhap_Chuoi(labelTenLoai2) + " trước khi cập nhật.", labelThongBao);
                return;
            }
            if (TheHien.ChuoiRong(txtbDonGia_Loai3))
            {
                TheHien.Xuat_Chuoi("Vui lòng bổ sung đơn giá thuê " + TheHien.Nhap_Chuoi(labelTenLoai3) + " trước khi cập nhật.", labelThongBao);
                return;
            }

            BangGiaThuePhong[1].GiaThuePhong = Int32.Parse(TheHien.Nhap_Chuoi(txtbDonGia_Loai1));
            BangGiaThuePhong[2].GiaThuePhong = Int32.Parse(TheHien.Nhap_Chuoi(txtbDonGia_Loai2));
            BangGiaThuePhong[3].GiaThuePhong = Int32.Parse(TheHien.Nhap_Chuoi(txtbDonGia_Loai3));

            string ChuoiXml = NghiepVu.Chuyen_BangGiaThuePhong_thanh_Xml(BangGiaThuePhong);
            
            LuuTru.GhiDuLIeu_GiaThuePhong(ChuoiXml);

            MessageBox.Show("Cập nhật thành công.");
            HienThiBangGiaThuePhong();


        }
    }
}
