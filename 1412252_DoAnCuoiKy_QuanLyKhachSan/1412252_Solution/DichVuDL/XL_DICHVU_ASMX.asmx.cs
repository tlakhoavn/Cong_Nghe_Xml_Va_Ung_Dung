﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace DichVuDL
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
        protected static XL_LUUTRU_DV LuuTru = new XL_LUUTRU_DV();
        [WebMethod]
        public string DocDuLieu()
        {
            string ChuoiKq = "";
            XmlElement DuLieu = LuuTru.DocDuLieu();
            ChuoiKq = DuLieu.OuterXml;
            return ChuoiKq;
        }
    }
}
