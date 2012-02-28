using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Web;
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
            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            lnbLogback.Click += new EventHandler(lnbLogback_Click);
            lnbRelog.Click += new EventHandler(lnbRelog_Click);
        }

        void lnbRelog_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Remove("userSections");
            HttpContext.Current.Session.Remove("isFaculty");
            this.PortalGlobal.Login(PortalUser.Current.Username, String.Empty);
            RedirectUrl();
            
        }

        void lnbLogback_Click(object sender, EventArgs e)
        {
            String originalUser = HttpContext.Current.Session["ProxyLoginOriginalUser"].ToString();
            HttpContext.Current.Session.Remove("userSections");
            HttpContext.Current.Session.Remove("isFaculty");
            HttpContext.Current.Session.Remove("ProxyLoginOriginalUser");
            this.PortalGlobal.Login(originalUser, String.Empty);
            if (HttpContext.Current.Session["ProxyLoginDontRedirect"] ==null){
                RedirectUrl();
            }else
            {
                HttpContext.Current.Session.Remove("ProxyLoginDontRedirect");
            }
        }

        private void RedirectUrl()
        {
            String url = PortalContext.RootContext.URL;
            JCUtilities.ResolveUrl(url);

            Response.Redirect(url, false);
        }
    }
}