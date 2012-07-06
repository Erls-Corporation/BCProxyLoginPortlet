using System;
using System.Collections.Specialized;
using System.Web;
using Jenzabar.Portal.Framework;
using Jenzabar.Common;
using Jenzabar.Portal.Framework.Web.UI;


namespace BCProxyLogin
{
    public partial class Relogger : PortalUserControlBase 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["ProxyLoginOriginalUser"] != null)
            {
                pnlProxyRelogger.Visible = true;
                //Trace.IsEnabled = true;
            }
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            Load += Page_Load;
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            lnbLogback.Click += LnbLogbackClick;
            lnbRelog.Click += LnbRelogClick;
        }

        void LnbRelogClick(object sender, EventArgs e)
        {
            var originalUser = HttpContext.Current.Session["ProxyLoginOriginalUser"].ToString();
            var dontRedirect = HttpContext.Current.Session["ProxyLoginDontRedirect"];
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["ProxyLoginDontRedirect"] = dontRedirect;
            HttpContext.Current.Session["ProxyLoginOriginalUser"] = originalUser;
            HttpContext.Current.Session["file_access"] = new StringDictionary();// UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
            PortalGlobal.Login(PortalUser.Current.Username, String.Empty);
            if (dontRedirect == null)
                RedirectUrl();
            
        }

        void LnbLogbackClick(object sender, EventArgs e)
        {
            var originalUser = HttpContext.Current.Session["ProxyLoginOriginalUser"].ToString();
            var dontRedirect = HttpContext.Current.Session["ProxyLoginDontRedirect"];
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session["file_access"] = new StringDictionary();// UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
            PortalGlobal.Login(originalUser, String.Empty);

            if (dontRedirect == null)
                RedirectUrl();
        }

        private void RedirectUrl()
        {
            String url = PortalContext.RootContext.URL;
            JCUtilities.ResolveUrl(url);

            Response.Redirect(url, false);
        }
    }
}