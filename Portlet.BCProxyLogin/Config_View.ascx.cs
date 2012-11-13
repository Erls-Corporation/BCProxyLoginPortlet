using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Jenzabar.Portal.Framework.InstalledApplications;
using Jenzabar.Portal.Framework.Web.UI;

namespace BCProxyLogin
{
    public partial class Config_View : PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new InstalledApplicationService().IsApplicationAtLeastThisVersion("JICS", "7.5.3") || true)
            {
                ulScheduledReports.Visible = true;
            }
        }
    }
}