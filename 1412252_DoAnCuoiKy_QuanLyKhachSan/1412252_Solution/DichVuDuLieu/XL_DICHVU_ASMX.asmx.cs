using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace DichVuDuLieu
{
    /// <summary>
    /// Summary description for XL_DICHVU_ASMX
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class XL_DICHVU_ASMX : System.Web.Services.WebService
    {
        protected static XL_LUUTRU_DICHVU LuuTru = new XL_LUUTRU_DICHVU();   
        [WebMethod]
       
        public string Doc_Du_Lieu()
        {
            string ChuoiKq = "";
            XmlElement DuLieu = LuuTru.DocDuLieu();
            ChuoiKq = DuLieu.OuterXml;
            return ChuoiKq;
        }
    }
}
