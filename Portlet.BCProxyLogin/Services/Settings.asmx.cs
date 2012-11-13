using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Jenzabar.Common;
using Jenzabar.Common.Configuration;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin.Services
{
    /// <summary>
    /// Summary description for Settings
    /// </summary>
    [WebService(Namespace = "BCProxyLogin.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Settings : WebService
    {
        //[WebMethod(EnableSession = true)]
        //public Dictionary<String,String> GetViewSettings(string viewname)
        //{

        //}

    }
}
