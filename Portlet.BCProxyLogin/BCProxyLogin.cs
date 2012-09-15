using System;
using System.Web;
using Jenzabar.Common;
using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework.Web.UI; // PortletBase
using Jenzabar.Portal.Framework; // NameValueDataSourceType
using Jenzabar.Portal.Framework.Security.Authorization;
using Jenzabar.Common.Web.UI.Controls;

namespace BCProxyLogin
{
    #region "Global Portlet Operation"
            [PortletOperation("CanProxyStudent",
                       "CUS_BC_PL_CAN_PROXY_STUDENT",
                       "CUS_BC_PL_CAN_PROXY_STUDENT_MSG",
                       PortletOperationScope.Global)]
            [PortletOperation("CanProxyStaff",
                       "CUS_BC_PL_CAN_PROXY_STAFF",
                       "CUS_BC_PL_CAN_PROXY_STAFF_MSG",
                       PortletOperationScope.Global)]
            [PortletOperation("CanProxyFaculty",
                       "CUS_BC_PL_CAN_PROXY_FACULTY",
                       "CUS_BC_PL_CAN_PROXY_FACULTY_MSG",
                       PortletOperationScope.Global)]
            [PortletOperation("CanProxyCandidate",
                       "CUS_BC_PL_CAN_PROXY_CANDIDATE",
                       "CUS_BC_PL_CAN_PROXY_CANDIDATE_MSG",
                       PortletOperationScope.Global)]
            [PortletOperation("CanProxyConstituent",
                        "CUS_BC_PL_CAN_PROXY_CONSTITUENT",
                        "CUS_BC_PL_CAN_PROXY_CONSTITUENT_MSG",
                        PortletOperationScope.Global)]
            [PortletOperation("CanViewSideBarControl",
                        "CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL",
                        "CUS_BC_PL_CAN_VIEW_SIDEBAR_CONTROL_MSG",
                        PortletOperationScope.Global)]
            [PortletOperation("DenyAccess",
                               "CUS_BC_PL_DENY_ROLE",
                               "CUS_BC_PL_DENY_ROLE_MSG",
                               PortletOperationScope.Global)]

    #endregion

    #region "Settings - Optional"

    #endregion

    public class BCProxyLogin : SecuredPortletBase
	{
		protected override PortletViewBase GetCurrentScreen()
		{
			switch(CurrentPortletScreenName)
			{
				case "Default":
					return LoadPortletView("ICS/BCProxyLogin/Default_View.ascx");
			    case "BCViewLogs":
                    State = PortletState.Maximized;
                    return LoadPortletView("ICS/BCProxyLogin/Logs_View.ascx");
				default:
					return LoadPortletView("ICS/BCProxyLogin/Default_View.ascx");
			}
		}

        protected override bool PopulateToolbar(Toolbar toolbar)
        {
            toolbar.MenuItems.Add("View Proxy Logs", "BCViewLogs");
            return PortalUser.Current.IsSiteAdmin;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Toolbar.ItemCommand += ToolbarItemCommand;
        }

        void ToolbarItemCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            NextScreen(e.CommandName);
        }

        public static void RedirectUrl(HttpResponse response)
        {
            var url = PortalContext.RootContext.URL;
            JCUtilities.ResolveUrl(url);

            response.Redirect(url, false);
        }
	}


}
