using System;
using System.Collections.Specialized;
using System.Web;
using BCProxyLogin.Entities;
using Jenzabar.Common.Configuration;
using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;
using Jenzabar.Common;
using Jenzabar.Portal.Framework.Web.UI;

namespace BCProxyLogin
{
    public partial class SideBarLoggerControl : PortalUserControlBase
    {
        private IPortletTemplateFacade _portletTemplateFacade;
        private PortletTemplate _portletTemplate;
        private readonly bool _requirePassword = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_ENABLE_PW");
        private readonly bool _logIpAddress = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_IP");
        private readonly bool _logFailures = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_FAILURES");

        protected void Page_Load(object sender, EventArgs e)
        {
            _portletTemplateFacade = ObjectFactoryWrapper.GetInstance<IPortletTemplateFacade>();
            _portletTemplate = _portletTemplateFacade.FindByName("[CUS] BCProxyLogin");
            if (_portletTemplate.AccessCheck("CanAccess") && _portletTemplate.AccessCheck("CanViewSideBarControl") && HttpContext.Current.Session["ProxyLoginOriginalUser"] == null)
            {
                pnlProxyLogin.Visible = true;
                
                if (_requirePassword)
                    pnlPassword.Visible = true;
            }
        }

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if (String.Empty == tbReason.Text.Trim())
            {
                divmessage.InnerHtml = Globalizer.GetGlobalizedString("CUS_BC_PL_REQUIRED_REASON");
                divmessage.Visible = true;
            }
            else if (_requirePassword && !ValidPassword())
            {
                divmessage.InnerHtml = Globalizer.GetGlobalizedString("CUS_BC_PL_INVALID_PW");
                divmessage.Visible = true;
                if (_logFailures)
                {
                    var user = getPortalUserByUserName(tbUserName.Text);
                    LogAction(Globalizer.GetGlobalizedString("CUS_BC_PL_INVALID_PW"), user.ID);
                }
            }
            else
            {
                PerformLogin();
            }

        }

        private void PerformLogin()
        {
            var username = tbUserName.Text;

            var user = getPortalUserByUserName(username);

            if (user != null)
            {
                var roleCheck = BCProxyLogin.RoleCheck(user, _portletTemplate);
                if (roleCheck.Success)
                {

                    if (LogAction(tbReason.Text, user.ID))
                    {
                        var currentUser = PortalUser.Current.Username;
                        HttpContext.Current.Session.Clear();
                        PortalGlobal.Login(user.Username, String.Empty);
                        HttpContext.Current.Session["file_access"] = new StringDictionary(); // UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
                        HttpContext.Current.Session["ProxyLoginOriginalUser"] = currentUser;
                        HttpContext.Current.Session["ProxyLoginDontRedirect"] = true;
                    }
                }
                else
                {
                    divmessage.InnerHtml = roleCheck.Reason;
                    divmessage.Visible = true;
                    if (_logFailures)
                        LogAction(roleCheck.Reason, user.ID);

                }
            }
            else
            {
                divmessage.InnerHtml = Globalizer.GetGlobalizedString("CUS_BC_PL_USER_NOT_FOUND");
                divmessage.Visible = true;
            }
        }

        private PortalUser getPortalUserByUserName(string username)
        {
            var userFacade = ObjectFactoryWrapper.GetInstance<IPortalUserFacade>();

            var user = userFacade.FindByUsername(username);

            if (user == null)
            {
                try
                {
                    user = userFacade.FindByHostID(username.Trim().PadLeft(11, '0'));
                }
                catch
                {

                }
            }

            return user;
        }

        private bool ValidPassword()
        {
            return PortalGlobal.IsLoginValid(PortalUser.Current.Username, tbPassword.Text) == Jenzabar.Portal.Framework.Web.LoginResult.Valid;
        }

        internal bool LogAction(String reason, Guid userId)
        {
            try
            {
                var logger = new BCLoggerMapperService();
                
                if (_logIpAddress)
                    reason = reason + " (" + Request.UserHostAddress + ")";

                return logger.AddLog(_portletTemplate.ID.AsGuid, PortalUser.Current.ID.AsGuid, userId, reason, DateTime.Now);
            }
            catch (Exception ex)
            {
                if (PortalUser.Current.IsSiteAdmin)
                    divError.InnerHtml = Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_ADMIN") + ex;
                else
                    divError.InnerHtml = Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_USER");

                divError.Visible = true;
                return false;
            }
        }
       
    }
}