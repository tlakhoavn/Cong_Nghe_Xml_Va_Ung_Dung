using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Web.Hosting;
using System.Data;
using System.Data.OleDb;

public enum CONG_NGHE_LUU_TRU
{
    CSDL,
    XML
}

public enum LOAI_CSDL
{
    Access, SQLServer, SQLser_MDF, MySql, Sqlite
}

public class XL_LUUTRU_DICHVU
{
    #region "Biến/Đội tượng toàn cục"
    protected static DirectoryInfo ThuMucProject = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
    protected static DirectoryInfo ThuMucSolution = ThuMucProject.Parent;

    protected static string ThuMucDuLieu = ThuMucSolution.FullName + @"\Du_Lieu";
    protected static string ThuMucCSDL = ThuMucDuLieu + @"\CSDL";
    protected static string ThuMucMedia = ThuMucDuLieu + @"\Media";
    protected static string ThuMucHinh = ThuMucMedia + @"\Hinh";

    protected static string TenDuLieuLuuTru = "DuLieu_QuanLyKhachSan2";

    protected static CONG_NGHE_LUU_TRU CongNghe = CONG_NGHE_LUU_TRU.CSDL;
    protected LOAI_CSDL LoaiCSDL = LOAI_CSDL.Access;

    protected string ChuoiKetNoiAccess;
    protected string DuongDanTapTinAccess = "";


    #endregion

    #region "Xử lý: Khởi động"
    public XL_LUUTRU_DICHVU()
    {
        if (CongNghe == CONG_NGHE_LUU_TRU.CSDL && LoaiCSDL == LOAI_CSDL.Access)
        {
            DuongDanTapTinAccess = ThuMucCSDL + @"\" + TenDuLieuLuuTru + ".mdb";
            ChuoiKetNoiAccess = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DuongDanTapTinAccess;
        }
    }
    #endregion

    #region "Xử lý: Đọc - ghi dữ liệu có cấu trúc"
    public XmlElement DocDuLieu()
    {
        XmlElement DuLieuXml = null;
        if(CongNghe == CONG_NGHE_LUU_TRU.CSDL)
        {
            DuLieuXml = DocDuLieuTuCSDL();
        }
        return DuLieuXml;
    }
    #endregion

    #region "Xử lý: Đọc - ghi dữ liệu phi cấu trúc(media)"
    #endregion

    #region "Đọc dữ liệu từ csdl"
    protected XmlElement DocDuLieuTuCSDL()
    {
        XmlElement DuLieuXml = null;

        DataSet DuLieuADO = new DataSet();
        DuLieuADO.DataSetName = TenDuLieuLuuTru;
        //DocBang("GIOI_TINH", "", DuLieuADO);
        //DocBang("NGOAI_NGU", "", DuLieuADO);
        //DocBang("LOAI_DICH_VU", "", DuLieuADO);
        //DocBang("CONG_TY", "", DuLieuADO);
        //DocBang("CHI_NHANH", "", DuLieuADO);
        //DocBang("DON_VI", "", DuLieuADO);
        //DocBang("NHAN_VIEN", "", DuLieuADO);
        DocBang("TIEPTAN_DANGNHAP", "", DuLieuADO);

        string ChuoiXml = DuLieuADO.GetXml();
        XmlDocument TaiLieu = new XmlDocument();
        TaiLieu.LoadXml(ChuoiXml);
        DuLieuXml = TaiLieu.DocumentElement;
        return DuLieuXml;
    }

    protected DataTable DocBang(string TenBang, string DieuKien = "", DataSet DuLieuADO = null)
    {
        DataTable BangDuLieu = new DataTable();
        if(LoaiCSDL == LOAI_CSDL.Access)
        {
            BangDuLieu = DocBangAccess(TenBang, DieuKien, DuLieuADO);
        }
        return BangDuLieu;
    }
    #endregion

    #region "Xử lý CSDL Access"
    protected DataTable DocBangAccess(string TenBang, string DieuKien = "", DataSet DuLieuADO = null)
    {
        DataTable BangDuLieu = new DataTable(TenBang);
        string ChuoiLenh = "Select * From" + TenBang;
        if (DieuKien.Trim() != "")
        {
            ChuoiLenh += "Where " + DieuKien;
        }
            try
            {
                OleDbDataAdapter BoThichUng = new OleDbDataAdapter(ChuoiLenh, ChuoiKetNoiAccess);
                BoThichUng.FillSchema(BangDuLieu, SchemaType.Source);
                BoThichUng.Fill(BangDuLieu);
                if(DuLieuADO != null)
                {
                    DuLieuADO.Tables.Add(BangDuLieu);
                }

                BangDuLieu.Columns[0].ReadOnly = false;
                foreach (DataColumn Cot in BangDuLieu.Columns)
                    {
                    Cot.ColumnMapping = MappingType.Attribute;
                }
            }
            catch (Exception e)
            {
                BangDuLieu.ExtendedProperties["ChuoiLoi"] = e.Message;
            }
                
        return BangDuLieu;
    }
    #endregion
}