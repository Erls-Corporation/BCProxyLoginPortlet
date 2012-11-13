using System;
using System.Collections.Generic;
using System.Web.Services;
using Jenzabar.Common.Configuration;
using Jenzabar.Common.Globalization;
using Jenzabar.Common;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin.Services
{
    /// <summary>
    /// Summary description for Globalizer
    /// </summary>
    [WebService(Namespace = "BCProxyLogin.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Globalizer : System.Web.Services.WebService
    {

        private readonly IGlobalizer _globalizer = ObjectFactoryWrapper.GetInstance<IGlobalizer>();

        [WebMethod(EnableSession = true)]
        public IDictionary<String, String> GetGlobalizedText(String viewName)
        {
            var portlet = ObjectFactoryWrapper.GetInstance<IPortletTemplateFacade>().FindByName("[CUS] BCProxyLogin");
            var dict = new Dictionary<String, String>();
            if (!portlet.AccessCheck("CANACCESS"))
                return dict;

            switch(viewName)
            {
                case "Default":
                    GetDefaultViewGlobalizations(dict);
                    break;
            }

            return dict;
        }

        [WebMethod(EnableSession = true)]
        public bool IsAdmin()
        {
            return PortalUser.Current.IsSiteAdmin;
        }

        private void GetDefaultViewGlobalizations(Dictionary<string, string> dict)
        {
            dict.Add("labelUserName", _globalizer.GetString("CUS_BC_PL_USERNAME_LABEL_TEXT"));
            dict.Add("labelPassword", _globalizer.GetString("CUS_BC_PL_PASSWORD_LABEL_TEXT"));
            dict.Add("labelReason", _globalizer.GetString("CUS_BC_PL_REASON_LABEL_TEXT"));

            dict.Add("EnablePassword", ConfigSettings.GetConfigValue("C_PortletSettings", "CUS_BC_PL_ENABLE_PW"));
        }

    }
}
