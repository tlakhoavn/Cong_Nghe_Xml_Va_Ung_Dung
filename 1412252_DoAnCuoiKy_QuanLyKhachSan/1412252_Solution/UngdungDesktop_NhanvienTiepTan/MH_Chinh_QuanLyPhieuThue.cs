using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace UngdungDesktop_NhanvienTiepTan
{
    
    public partial class MH_Chinh_QuanLyPhieuThue : Form
    {
        public MH_Chinh_QuanLyPhieuThue()
        {
            InitializeComponent();
            KhoiDong();
        }
        #region "Biến/Đối tượng toàn cục"
        protected XL_LUUTRU LuuTru = new XL_LUUTRU();
        protected XL_NGHIEPVU NghiepVu = new XL_NGHIEPVU();
        protected XL_THEHIEN TheHien = new XL_THEHIEN();

        protected XmlElement XmlElement_ThongTinPhong;
       
        //Các biến lưu thông tin các panel và tab chưa button bảng các phòng của từng khu vực
        CKhuVuc_TheHien TheHien_KVA;
        CKhuVuc_TheHien TheHien_KVB;
        CKhuVuc_TheHien TheHien_KVC;

        Form ManHinh_DangNhap;              //Form lưu form màn hình đăng nhập, phục vụ chức năng đăng xuất
        static protected int NV_ID = 0;                      //Lưu ID nhân viên
        static protected string NV_MaKhuVuc = "";

        //Kích thước button phòng
        protected int Phong_ChieuRong = 110;
        protected int Phong_ChieuCao = 60;

        static int Chiso_KhuVucDangChon = 1;          //tab khu vực mà người dùng đang chọn hiển thị

        static bool CoDangXuat ; //Bật khi đăng xuất mà không thoát

        static public List<CPhieuThuePhong> DanhSach_PhongDangChoThue;   //Danh sách phiếu thuê các phòng hiện tại đang được cho thuê
        
        //Phiếu thuê của phòng đang được hiển thị - Phòng đang được chọn hiển thị
        CPhieuThuePhong PhieuThue_PhongDangChon;
        CThongTinPhong Phong_DangHienThi;

        //Chưa các text box chưa thông tin khách hàng
        List<TextBox> DanhSach_Textbox_TenKhachHang;
        List<TextBox> DanhSach_Textbox_CMNDKhachHang;
        //Kích thước các textbox
        private int KH_Ten_Width = 209;
        private int KH_Ten_Height = 24;
        private int KH_CMND_Width = 154;
        private int KH_CMND_Height = 24;

        Label label_Trong_A = new Label();
        Label label_Trong_B = new Label();
        Label label_Trong_C = new Label();
        #endregion

        #region "Sự kiện khởi động"       
        private void KhoiDong()
        {
           
            //Cờ đánh dấu sự kiện đăng xuất
            CoDangXuat = false;           
            //Đưa màn hình về giữa desktop
            this.StartPosition = FormStartPosition.CenterScreen;
            //Sét vị trí, kích thước cho 3 pannel đặt khít lên nhau
            TheHien.KhoiTaoViTriPanel(panelBangPhong_KVA, panelBangPhong_KVB, panelBangPhong_KVC);
            

            //Khởi tạo các panel chứa bảng các phòng của các khu vực
            TheHien_KVA = new CKhuVuc_TheHien();
            TheHien_KVB = new CKhuVuc_TheHien();
            TheHien_KVC = new CKhuVuc_TheHien();

            TheHien_KVA.BangKhuVuc = new List<List<CThongTinPhong>>();
            TheHien_KVA.PanelBangPhongKhuVuc = panelBangPhong_KVA;
            TheHien_KVA.BtnKhuVuc = btnKhuVucA;

            TheHien_KVB.BangKhuVuc = new List<List<CThongTinPhong>>();
            TheHien_KVB.PanelBangPhongKhuVuc = panelBangPhong_KVB;
            TheHien_KVB.BtnKhuVuc = btnKhuVucB;

            TheHien_KVC.BangKhuVuc = new List<List<CThongTinPhong>>();
            TheHien_KVC.PanelBangPhongKhuVuc = panelBangPhong_KVC;
            TheHien_KVC.BtnKhuVuc = btnKhuVucC;

            //Khởi tạo danh sách chứa các control textbox chứa thông tin khách hàn
            DanhSach_Textbox_TenKhachHang = new List<TextBox>();
            DanhSach_Textbox_CMNDKhachHang = new List<TextBox>();
           
           
            //Khởi tạo 3 label thông báo số phòng còn trống ở các khu vực           

            label_Trong_A.AutoSize = true;
            label_Trong_A.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label_Trong_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            label_Trong_A.ForeColor = System.Drawing.Color.DarkBlue;
            label_Trong_A.Location = new System.Drawing.Point(4,4);
            label_Trong_A.Name = "label_Trong_A";
            label_Trong_A.Size = new System.Drawing.Size(5, 50);                  
            label_Trong_A.Text = "Cònphòng";


            label_Trong_B.AutoSize = true;
            label_Trong_B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label_Trong_B.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            label_Trong_B.ForeColor = System.Drawing.Color.DarkBlue;
            label_Trong_B.Location = new System.Drawing.Point(4, 4);
            label_Trong_B.Name = "label_Trong_B";
            label_Trong_B.Size = new System.Drawing.Size(5, 50);
            label_Trong_B.Text = "Cònphòng";


            label_Trong_C.AutoSize = true;
            label_Trong_C.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            label_Trong_C.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            label_Trong_C.ForeColor = System.Drawing.Color.DarkBlue;
            label_Trong_C.Location = new System.Drawing.Point(4, 4);
            label_Trong_C.Name = "label_Trong_C";
            label_Trong_C.Size = new System.Drawing.Size(5, 50);
            label_Trong_C.Text = "Cònphòng";


            btnKhuVucA.Controls.Add(label_Trong_A);
            btnKhuVucB.Controls.Add(label_Trong_B);
            btnKhuVucC.Controls.Add(label_Trong_C);

            CapNhat_DuLieuPhong(); //Cập nhật thông tin dữ liệu về các phòng, tình trạng phòng


        }   

        //Hàm thực hiện chức năng cập nhật lại thông tin các phòng, trình trạng các phòng
        private void CapNhat_DuLieuPhong()
        {
            //Tải chuỗi xml element thông tin các phòng từ dịch vụ về
            XmlElement_ThongTinPhong = LuuTru.DocDuLieu_ThongTinPhong();

            //Từ chuỗi xml element trích xuất ra danh sách phiếu các phòng đang được thuê
            DanhSach_PhongDangChoThue = NghiepVu.Doc_DSCacPhongDangThue(XmlElement_ThongTinPhong);

            //Load dữ liệu từ Xml vào bảng ma trận phòng khách sạn
            NghiepVu.KhoiTaoThongTinBangPhong(TheHien_KVA, TheHien_KVB, TheHien_KVC, XmlElement_ThongTinPhong);
            //chuyển thông tin từ xml element sang dạng dữ liệu cơ sở và lưu vào danh sách từng khu vực
            TaiLenThongTinBangPhong(TheHien_KVA, TheHien_KVB, TheHien_KVC);

            switch(Chiso_KhuVucDangChon)
            {
                case 1:
                    TheHien.HienBangPhongKhuVuc(TheHien_KVA, TheHien_KVB, TheHien_KVC); 
                    break;
                case 2:
                    TheHien.HienBangPhongKhuVuc(TheHien_KVB, TheHien_KVA, TheHien_KVC);
                    break;
                    
                case 3:
                    TheHien.HienBangPhongKhuVuc(TheHien_KVC, TheHien_KVB, TheHien_KVA);
                    break;
            }           

            //Ẩn label thông báo lỗi
            TheHien.Xuat_Chuoi("", labelThongBaoLoiNhap);
            //Ẩn button Thuê/Trả phòng
            btnThuePhong.Hide();
            btnTraPhong.Hide();

            //Cập nhật số phòng hiện còn trống hay đã full phòng ở các khu vực.
            CapNhat_TinhTrang_SoPhongConTrong();

        }
        //Hàm quản lý số lượng các phòng còn trống hay đã hết của các khu vực để hiển thị lên nút
        private void CapNhat_TinhTrang_SoPhongConTrong()
        {
            //Khu A
            int SoPhongTrong_A = 0;
            for (int iTang = 0; iTang < TheHien_KVA.BangKhuVuc.Count; iTang++)
            {
                for (int iPhong = 0; iPhong < TheHien_KVA.BangKhuVuc[iTang].Count; iPhong++)
                {
                    int IDPhong = TheHien_KVA.BangKhuVuc[iTang][iPhong].ID;
                    if (NghiepVu.PhongDangTrong(IDPhong, DanhSach_PhongDangChoThue) == true)
                    {
                        SoPhongTrong_A++;
                    }
                }
            }
            if (SoPhongTrong_A > 0)
            {
                TheHien.Xuat_Chuoi("Còn " + SoPhongTrong_A + " phòng", label_Trong_A);
            }
            else
            {
                TheHien.Xuat_Chuoi("Hết phòng" + SoPhongTrong_A, label_Trong_A);
            }
            //Khu B
            int SoPhongTrong_B = 0;
            for (int iTang = 0; iTang < TheHien_KVB.BangKhuVuc.Count; iTang++)
            {
                for (int iPhong = 0; iPhong < TheHien_KVB.BangKhuVuc[iTang].Count; iPhong++)
                {
                    int IDPhong = TheHien_KVB.BangKhuVuc[iTang][iPhong].ID;
                    if (NghiepVu.PhongDangTrong(IDPhong, DanhSach_PhongDangChoThue) == true)
                    {
                        SoPhongTrong_B++;
                    }
                }
            }
            if (SoPhongTrong_B > 0)
            {
                TheHien.Xuat_Chuoi("Còn " + SoPhongTrong_B + " phòng", label_Trong_B);
            }
            else
            {
                TheHien.Xuat_Chuoi("Hết phòng" + SoPhongTrong_B, label_Trong_B);
            }

            //Khu B
            int SoPhongTrong_C = 0;
            for (int iTang = 0; iTang < TheHien_KVC.BangKhuVuc.Count; iTang++)
            {
                for (int iPhong = 0; iPhong < TheHien_KVC.BangKhuVuc[iTang].Count; iPhong++)
                {
                    int IDPhong = TheHien_KVC.BangKhuVuc[iTang][iPhong].ID;
                    if (NghiepVu.PhongDangTrong(IDPhong, DanhSach_PhongDangChoThue) == true)
                    {
                        SoPhongTrong_C++;
                    }
                }
            }
            if (SoPhongTrong_C > 0)
            {
                TheHien.Xuat_Chuoi("Còn " + SoPhongTrong_C + " phòng", label_Trong_C);
            }
            else
            {
                TheHien.Xuat_Chuoi("Hết phòng" + SoPhongTrong_C, label_Trong_C);
            }


        }
        #endregion

        //Truyền vào màn hình đăng nhập, khi cần đăng xuất thì hiện lại màn hình
        public void TruyenDuLieu( Form ManHinhTruoc, int ID_NhanVien, string MaKhuVuc)
        {
            ManHinh_DangNhap = ManHinhTruoc;
            NV_ID = ID_NhanVien;
            NV_MaKhuVuc = MaKhuVuc;       
            
            //Xuất câu chào nhân viên         
            string CauChao = NghiepVu.ChaoNhanVienTiepTan(NV_ID, NV_MaKhuVuc);            
            TheHien.Xuat_Chuoi(CauChao, labelChaoNhanVien);

            Chiso_KhuVucDangChon = NghiepVu.ChiSoMaKhuVuc(NV_MaKhuVuc);
            CapNhat_DuLieuPhong();
        }

        private void MH_Chinh_Load(object sender, EventArgs e)
        {      

            //Tải lên logo Khách sạn
            byte[] DuLieu_NhiPhan = LuuTru.DocHinh("Logo");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnImgLogo);
            //Tải lên biểu tượng logout
            DuLieu_NhiPhan = LuuTru.DocHinh("Logout");
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnDangXuat);

            //Tải button tra cứu Phiếu thuê phòng
            DuLieu_NhiPhan = LuuTru.DocHinh("Find");         
            TheHien.Xuat_Hinh(DuLieu_NhiPhan, btnTxtTraCuPhieuThue);

        }       

        private void MH_Chinh_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(CoDangXuat)
            {

            }
            else { Application.Exit(); }
                    
        }


        //Load thông tin bảng các phòng vào panel nhưng chưa hiện lên
        public void TaiLenThongTinBangPhong(CKhuVuc_TheHien KV_A, CKhuVuc_TheHien KV_B, CKhuVuc_TheHien KV_C)
        {
            TaiLenBangPhong(KV_A);
            TaiLenBangPhong(KV_B);
            TaiLenBangPhong(KV_C);
        }
        //Khởi tạo bảng danh sách các phòng
        private void TaiLenBangPhong(CKhuVuc_TheHien Khu_Vuc)
        {
            int Top = 0;           
            Khu_Vuc.PanelBangPhongKhuVuc.Controls.Clear();
            for (int i = 0; i < Khu_Vuc.BangKhuVuc.Count; i++)
            {
                int Left = 0;
                for (int j = 0; j < Khu_Vuc.BangKhuVuc[i].Count; j++)
                {
                    Button newbtn = new Button();

                    newbtn.Name = string.Format("btnPhong{0}{1}", i, j);
                    newbtn.TabIndex = i;
                    newbtn.Tag = string.Format("{0}", j);
                    newbtn.Text = Khu_Vuc.BangKhuVuc[i][j].Ten;
                    newbtn.Size = new System.Drawing.Size(Phong_ChieuRong, Phong_ChieuCao);
                    newbtn.Padding = new Padding(1, 0, 0, 0);
                    newbtn.Top = Top;
                    newbtn.Left = Left;
                    newbtn.BackColor = Phong_MauNen(Khu_Vuc.BangKhuVuc[i][j]);
                    newbtn.ForeColor = Phong_MauChu(Khu_Vuc.BangKhuVuc[i][j]);
                    newbtn.Click += new EventHandler(btnPhong_click);

                    Label label_Loai = new Label();
                    label_Loai.AutoSize = true;
                    label_Loai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    label_Loai.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                    label_Loai.ForeColor = System.Drawing.Color.YellowGreen;
                    label_Loai.Location = new System.Drawing.Point(5, Phong_ChieuCao - 20);                   
                    label_Loai.Size = new System.Drawing.Size(50, 5);
                    label_Loai.Text = Khu_Vuc.BangKhuVuc[i][j].LoaiPhong;

                    newbtn.Controls.Add(label_Loai);

                    Khu_Vuc.PanelBangPhongKhuVuc.Controls.Add(newbtn);
                    Left += Phong_ChieuRong;
                }
                Top += Phong_ChieuCao;
            }
            Khu_Vuc.PanelBangPhongKhuVuc.Hide();
        }

        //Thiết lập màu nền, màu chữ cho button phòng
        private Color Phong_MauChu(CThongTinPhong cThongTinPhong)
        {
            Color cl = Color.White;
            return cl;
        }
        private Color Phong_MauNen(CThongTinPhong ThongTin_Phong)
        {
            Color cl;
            if (NghiepVu.PhongDangTrong(ThongTin_Phong.ID,DanhSach_PhongDangChoThue) == true)
            {
                cl = Color.DarkSlateBlue;
            }
            else
            {
                cl = Color.Red;
            }

            return cl;
        }
        
        //Hiển thị chức năng trả phòng khi click phòng đang thuê
        private void ChucNangTraPhong(CPhieuThuePhong PhieuThuePhong_)
        {
            txtbNgayDuKienTra.ReadOnly = true;            
            TheHien.HienDieuKhien(btnTraPhong);
            TheHien.AnDieuKhien(btnThuePhong);

            Xuat_PhieuThuePhong(PhieuThuePhong_);            
        }
        //Hiển thị chức năng thuê phòng khi click phòng đang trống
        private void ChucNangThuePhong(CPhieuThuePhong PhieuThuePhong_)
        {
            txtbNgayDuKienTra.ReadOnly = false;
            TheHien.HienDieuKhien(btnThuePhong);
            TheHien.AnDieuKhien(btnTraPhong);

            Xuat_PhieuThuePhong(PhieuThuePhong_);
            TaoTextBox_NhapThongTinKhachHang_TuDong();         //Tạo tự động textbox nhập thông tin                        
        }

        //Tạo textbox nhập thông tin khách hàng khi có khách đặt phòng
        private void TaoTextBox_NhapThongTinKhachHang_TuDong()
        {
            panelKhachThuePhong.Controls.Clear();
            DanhSach_Textbox_TenKhachHang.Clear();
            DanhSach_Textbox_CMNDKhachHang.Clear();

            TextBox txtbKH_Ten, txtbKH_CMND;
            txtbKH_CMND = TheHien.TaoTextBox(new Size(KH_CMND_Width, KH_CMND_Height), KH_Ten_Width + 5, 0);

            txtbKH_Ten = TheHien.TaoTextBox(new Size(KH_Ten_Width, KH_Ten_Height), 0, 0);
            txtbKH_Ten.Tag = "0";
            txtbKH_Ten.Name = string.Format("txtbTenKh_{0}", 0);
            txtbKH_Ten.TabIndex = 50 + 0;

            txtbKH_Ten.TextChanged += new EventHandler(txtbTenKhachHang_TextChanged);
            txtbKH_Ten.Click += new EventHandler(txtbTenKhachHang_Click);

            panelKhachThuePhong.Controls.Add(txtbKH_Ten);
            panelKhachThuePhong.Controls.Add(txtbKH_CMND);

            DanhSach_Textbox_TenKhachHang.Add(txtbKH_Ten);
            DanhSach_Textbox_CMNDKhachHang.Add(txtbKH_CMND);
        }

        //Hiển thị thông tin những phồng đang được click chuột chọn
        private void Xuat_PhieuThuePhong(CPhieuThuePhong PhieuThuePhong_)
        {
            TheHien.Xuat_Chuoi(PhieuThuePhong_.TenPhong, labelTenPhong);
            TheHien.Xuat_Chuoi(PhieuThuePhong_.LoaiPhong, labelLoaiPhong);
            TheHien.Xuat_Chuoi(PhieuThuePhong_.NgayBatDau, labelNgayBatDau);
            TheHien.Xuat_Chuoi(PhieuThuePhong_.NgayDuKienTra, txtbNgayDuKienTra);

            panelKhachThuePhong.Controls.Clear();         
            for(int i=0;i<PhieuThuePhong_.DSKhachHang.Count;i++)
            {
                TextBox txtbKH_Ten, txtbKH_CMND;
                txtbKH_Ten = TheHien.TaoTextBox(new Size(KH_Ten_Width, KH_Ten_Height), 0, i * (KH_Ten_Height + 5));
                txtbKH_CMND = TheHien.TaoTextBox(new Size(KH_CMND_Width, KH_CMND_Height), KH_Ten_Width + 5, i * (KH_CMND_Height + 5));

                TheHien.Xuat_Chuoi(PhieuThuePhong_.DSKhachHang[i].HoTen, txtbKH_Ten);
                TheHien.Xuat_Chuoi(PhieuThuePhong_.DSKhachHang[i].CMND, txtbKH_CMND);       

                panelKhachThuePhong.Controls.Add(txtbKH_Ten);
                panelKhachThuePhong.Controls.Add(txtbKH_CMND);                
            }
                    


        }
       

        
        #region "Các hàm bắt sự kiện"

        //Hàm bắt sự kiện click vào một phòng
        private void btnPhong_click(object sender, EventArgs e)
        {
            List<List<CThongTinPhong>> KhuVucChon;                      

            int ChiSoHang = ((Button)sender).TabIndex;
            int ChiSoCot = Int32.Parse(((Button)sender).Tag.ToString());

            if (1 == Chiso_KhuVucDangChon)
            {
                KhuVucChon = TheHien_KVA.BangKhuVuc;
            }
            else if (2 == Chiso_KhuVucDangChon)
            {
                KhuVucChon = TheHien_KVB.BangKhuVuc;
            }
            else
            {
                KhuVucChon = TheHien_KVC.BangKhuVuc;
            }

            Phong_DangHienThi = KhuVucChon[ChiSoHang][ChiSoCot];
            //Hiện thông tin chi tiết phòng

            if (NghiepVu.PhongDangTrong(Phong_DangHienThi.ID, DanhSach_PhongDangChoThue) == true)
            {
                PhieuThue_PhongDangChon = NghiepVu.TaoMoiPhieuThuePhong(Phong_DangHienThi);
                //Show thông tin tra cứu theo dạng Thuê phòng
                ChucNangThuePhong(PhieuThue_PhongDangChon);
            }
            else
            {
                PhieuThue_PhongDangChon = NghiepVu.LayThongTinPhongDangThue(Phong_DangHienThi, DanhSach_PhongDangChoThue);
                //Show thông tin tra cứu theo dạng trả phòng
                ChucNangTraPhong(PhieuThue_PhongDangChon);
            }
        }

        //Hàm bắt sự kiện textbox TenKH thay đổi/click
        private void txtbTenKhachHang_TextChanged(object sender, EventArgs e)
        {
            int vitri = Int32.Parse(((TextBox)sender).Tag.ToString());

            //QT1: Không nhập thông tin vào các ô bên dưới nếu các ô trên chưa nhập
            if (vitri > 0)
            {
                string Chuoi = TheHien.Nhap_Chuoi(DanhSach_Textbox_TenKhachHang[vitri - 1]);
                if (Chuoi.Length == 0)
                {                   
                    TheHien.Xuat_Chuoi("", (TextBox)sender);                            
                    return;
                }
            }                   
        }       
        private void txtbTenKhachHang_Click(object sender, EventArgs e)
        {
            int vitri = Int32.Parse(((TextBox)sender).Tag.ToString());
            //MessageBox.Show(DanhSach_Textbox_TenKhachHang.Count + " vitri =" + vitri);
            //QT2: Nếu người dùng nhập dữ liệu vào dòng thứ i thì tự động phát sinh dòng thứ i+1

            if(vitri >0 && TheHien.Nhap_Chuoi(DanhSach_Textbox_TenKhachHang[vitri-1]).Length == 0)
            {
                return;
            }

            if (DanhSach_Textbox_TenKhachHang.Count == (vitri + 1))
            {

                TextBox txtbKH_Ten = new TextBox();
                TextBox txtbKH_CMND = new TextBox();
                int Top_ = (KH_Ten_Height + 5) * (vitri + 1);

                txtbKH_Ten = TheHien.TaoTextBox(new Size(KH_Ten_Width, KH_Ten_Height), 0, Top_);
                txtbKH_CMND = TheHien.TaoTextBox(new Size(KH_CMND_Width, KH_CMND_Height), KH_Ten_Width + 5, Top_);

                txtbKH_Ten.Tag = (vitri + 1) + "";
                txtbKH_Ten.Name = string.Format("txtbTenKh_{0}", vitri + 1);
                txtbKH_Ten.TabIndex = 50 + vitri + 1;

                txtbKH_Ten.TextChanged += new EventHandler(txtbTenKhachHang_TextChanged);
                txtbKH_Ten.Click += new EventHandler(txtbTenKhachHang_Click);

                panelKhachThuePhong.Controls.Add(txtbKH_Ten);
                panelKhachThuePhong.Controls.Add(txtbKH_CMND);

                DanhSach_Textbox_TenKhachHang.Add(txtbKH_Ten);
                DanhSach_Textbox_CMNDKhachHang.Add(txtbKH_CMND);

            }
        }
        //Sự kiện thay đổi khung xem các khu vực A,B,C
        private void btnKhuVucA_Click(object sender, EventArgs e)
        {
            TheHien.HienBangPhongKhuVuc(TheHien_KVA, TheHien_KVB, TheHien_KVC); //Hiện KV A ẩn KV B,C
            Chiso_KhuVucDangChon = 1;
            KiemSoat_Quyen_Thue_Tra_Phong(Chiso_KhuVucDangChon);
        }

        private void btnKhuVucB_Click(object sender, EventArgs e)
        {
            TheHien.HienBangPhongKhuVuc(TheHien_KVB, TheHien_KVA, TheHien_KVC); //Hiện KV B ẩn KV A,C
            Chiso_KhuVucDangChon = 2;
            KiemSoat_Quyen_Thue_Tra_Phong(Chiso_KhuVucDangChon);
           
        }       

        private void btnKhuVucC_Click(object sender, EventArgs e)
        {
            TheHien.HienBangPhongKhuVuc(TheHien_KVC, TheHien_KVA, TheHien_KVB); //Hiện KV C ẩn KV A,B
            Chiso_KhuVucDangChon = 3;
            KiemSoat_Quyen_Thue_Tra_Phong(Chiso_KhuVucDangChon);
        }

        //Kiểm soát quyền thực hiện trả/thuê phòng-> Nhân viên chỉ có thể thao tác thuê/trả phòng mà mình đang quản lý
        private void KiemSoat_Quyen_Thue_Tra_Phong(int ChiSo)
        {
            //Chặn quyền thực hiện thuê/trả phòng nếu phòng không thuộc quyền quản lý
            if (ChiSo == NghiepVu.ChiSoMaKhuVuc(NV_MaKhuVuc))
            {
                btnThuePhong.Enabled = true;
                btnTraPhong.Enabled = true;
            }
            else
            {
                btnThuePhong.Enabled = false;
                btnTraPhong.Enabled = false;
            }
        }

        //Sự kiện đăng xuất khỏi chương trình.
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            ManHinh_DangNhap.Show();
            CoDangXuat = true;
            this.Close();
        }

        //Mở màn hình chức năng tra cứu phiếu thuê
        private void btnTxtTraCuPhieuThue_Click(object sender, EventArgs e)
        {
            Form MH_TraCuu = new MH_TraCuu_PhieuThuePhong();
            MH_TraCuu.Show();
        }
        #region "Thao tác thuê/trả phòng"
        private void btnThuePhong_Click(object sender, EventArgs e)
        {
            //Kiểm tra thông tin nhập đúng:
            if(ThongTinNhap_PhieuThueHopLe() == true)
            {
                PhieuThue_PhongDangChon.NgayBatDau = TheHien.Nhap_Chuoi(labelNgayBatDau);
                PhieuThue_PhongDangChon.NgayDuKienTra = TheHien.Nhap_Chuoi(txtbNgayDuKienTra);
                PhieuThue_PhongDangChon.MaPhong = Phong_DangHienThi.ID;
                for(int i=0;i<DanhSach_Textbox_TenKhachHang.Count;i++)
                {
                    string Ten = TheHien.Nhap_Chuoi(DanhSach_Textbox_TenKhachHang[i]);
                    string CMND = TheHien.Nhap_Chuoi(DanhSach_Textbox_CMNDKhachHang[i]);
                    if(Ten.Length != 0)
                    {
                        CThongTinKhachHang KhachHang = new CThongTinKhachHang();
                        KhachHang.HoTen = Ten;
                        KhachHang.CMND = CMND;
                        PhieuThue_PhongDangChon.DSKhachHang.Add(KhachHang);
                    }
                }
                LuuTru.GhiDuLieu_ThuePhong(PhieuThue_PhongDangChon); //Gửi dữ liệu lên host
                CapNhat_DuLieuPhong();
            }
            else
            {
                return;
            }
               
        }

        private bool ThongTinNhap_PhieuThueHopLe()
        {
            //Ngày dự kiến
            string strNgayDuKien = TheHien.Nhap_Chuoi(txtbNgayDuKienTra).ToString();
            if (strNgayDuKien.Length == 0)
            {
                ThongBaoLoiNhapPhieuThue("Chưa nhập vào thông tin ngày dự kiến trả. Vui lòng nhập thông tin trước khi thuê phòng.");
                return false;
            }
            if (NghiepVu.Kiemtra_NgayDuKienHopLe(strNgayDuKien, TheHien.Nhap_Chuoi(labelNgayBatDau).ToString()) == false)
            {
                ThongBaoLoiNhapPhieuThue("Ngày dự kiến vừa nhập không phải là một ngày hợp lệ. Vui lòng nhập lại.");
                return false;
            }
            //Chuẩn hóa chuỗi ngày về dạng xx/xx/xxxx
            if (strNgayDuKien.Length != 10)
            {
                TheHien.Xuat_Chuoi(DateTime.Parse(strNgayDuKien).ToString().Substring(0, 10), txtbNgayDuKienTra);

            }
            //Kiểm tra thông tin Khách hàng: tên + CMND                
            if (DanhSach_Textbox_TenKhachHang.Count > 0)
            {
                //Có nhập ít nhất một tên và CMND
                if (TheHien.Nhap_Chuoi(DanhSach_Textbox_TenKhachHang[0]).Length == 0)
                {
                    ThongBaoLoiNhapPhieuThue("Thông tin khách thuê phòng không được bỏ trống. Vui lòng nhập họ tên và CMND khách hàng.");
                    return false;
                }
                //Mỗi khách hàng phải đầy đủ thông tin bao gồm Họ và tên + CMND
                for (int i = 0; i < DanhSach_Textbox_CMNDKhachHang.Count; i++)
                {
                    string Ten = TheHien.Nhap_Chuoi(DanhSach_Textbox_TenKhachHang[i]);
                    string CMND = TheHien.Nhap_Chuoi(DanhSach_Textbox_CMNDKhachHang[i]);
                    if (Ten.Length == 0 && CMND.Length != 0)
                    {
                        ThongBaoLoiNhapPhieuThue("Chưa nhập tên khách hàng có số CMND " + CMND + ". Vui lòng nhập tên và đặt phòng lại.");
                        return false;
                    }
                    if (Ten.Length != 0 && CMND.Length == 0)
                    {
                        ThongBaoLoiNhapPhieuThue("Chưa nhập số CMND khách hàng " + Ten + ". Vui lòng nhập CMND và đặt phòng lại.");
                        return false;
                    }
                }


            }
            else
            {
                return false;
            }

            return true;
        }

        private void ThongBaoLoiNhapPhieuThue(string Thongbao)
        {
            TheHien.Xuat_Chuoi(Thongbao, labelThongBaoLoiNhap);
        }

        private void btnTraPhong_Click(object sender, EventArgs e)
        {

            int SoNgayThue = NghiepVu.SoNgayThuePhong(TheHien.Nhap_Chuoi(labelNgayBatDau));
            int TienThuePhong = NghiepVu.TinhTienThuePhong(SoNgayThue, Phong_DangHienThi.DonGia);

            //Xuất thông báo thanh toán
            MessageBox.Show("Tên phòng           : " + PhieuThue_PhongDangChon.TenPhong + "\n" +
                            "Ngày bắt đầu        : " + PhieuThue_PhongDangChon.NgayBatDau + "\n" +
                            "Số ngày thuê        : " + SoNgayThue + "\n" +
                            "Tổng tiền thanh toán: " + TienThuePhong + " VNĐ");

            //Thực hiện trả phòng ở đây.
            PhieuThue_PhongDangChon.NgayTra = NghiepVu.Chuoi_NgayThangNam_HienTai();
            PhieuThue_PhongDangChon.TienThuePhong = TienThuePhong;

            LuuTru.GhiDuLieu_TraPhong(PhieuThue_PhongDangChon);
            CapNhat_DuLieuPhong(); //Cập nhật lại bảng phòng                            


        }
        #endregion

        #endregion

      

       
    }
}
