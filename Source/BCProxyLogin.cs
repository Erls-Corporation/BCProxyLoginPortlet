using System;
using System.Web;
using BCNHibernate;
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

    /// <summary>
	/// Summary description for CUSProxyLogin.
	/// </summary>
	public class BCProxyLogin : SecuredPortletBase
	{

		protected override PortletViewBase GetCurrentScreen()
		{
			PortletViewBase screen = null;
            //this.Page.Trace.IsEnabled = true;
	
			switch(this.CurrentPortletScreenName)
			{
				case "Default":
					screen = this.LoadPortletView("ICS/BCProxyLogin/Default_View.ascx");
					break;
                case "BCViewLogs":
                    screen = this.LoadPortletView("ICS/BCProxyLogin/Logs_View.ascx");
                    this.State = PortletState.Maximized;
                    break;
				default:
					screen = this.LoadPortletView("ICS/BCProxyLogin/Default_View.ascx");
					break;
			}

			return screen;
		}

        protected override bool PopulateToolbar(Toolbar toolbar)
        {
            
            toolbar.MenuItems.Add("View Proxy Logs", "BCViewLogs");

            return PortalUser.Current.IsSiteAdmin;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Toolbar.ItemCommand += new System.Web.UI.WebControls.CommandEventHandler(Toolbar_ItemCommand);
        }

        void Toolbar_ItemCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            this.NextScreen(e.CommandName);
        }

        public static void RedirectUrl(HttpResponse response)
        {
            String url = PortalContext.RootContext.URL;
            JCUtilities.ResolveUrl(url);

            response.Redirect(url, false);
        }

        internal static Check RoleCheck(PortalUser pu, PortletTemplate portletTemplate)
        {
            if(portletTemplate.AccessCheck("DenyAccess", pu))
                return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_DENIED_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.Staff))
                if (!portletTemplate.AccessCheck("CanProxyStaff"))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_STAFF_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.Faculty))
                if (!portletTemplate.AccessCheck("CanProxyFaculty"))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_FACULTY_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.Students))
                if (!portletTemplate.AccessCheck("CanProxyStudent"))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_STUDENT_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.FindByStatusCode("CAN"))) // Candidate
                if (!portletTemplate.AccessCheck("CanProxyCandidate"))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_CANDIDATE_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.FindByStatusCode("ALM")))
                if (!portletTemplate.AccessCheck("CanProxyConstituent"))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_CONSTITUENT_PERMS").ToString());

            if (pu.IsMemberOf(PortalGroup.Administrators))
                return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_SITE_ADMIN_PERMS").ToString());

            return new Check(true);
        }

        

	}


}
