using System;
using System.Collections.Specialized;
using System.Web;
using BCProxyLogin.Entities;
using Jenzabar.Common.Configuration;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Common;
using Jenzabar.Portal.Framework.Facade;
using Jenzabar.Common.Globalization;

namespace BCProxyLogin
{
	/// <summary>
	///		Summary description for Default_View.
	/// </summary>
	public partial class Default_View : PortletViewBase
	{
        private readonly bool _requirePassword = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_ENABLE_PW");
        private readonly bool _logIpAddress = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_IP");
        private readonly bool _logFailures = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_FAILURES");

		protected void Page_Load(object sender, EventArgs e)
		{
		    if (!IsFirstLoad) return;

		    lblReason.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_REASON_LABEL_TEXT");
		    lblUserName.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_USERNAME_LABEL_TEXT");
		    lblPassword.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_PASSWORD_LABEL_TEXT");

		    if (_requirePassword)
		        pnlPassword.Visible = true;

		    if (Session["reloginCommand"] != null)
		    {
		        var args = Session["reloginCommand"].ToString().Split('|');
		        tbUserName.Text = args[0];
		        tbReason.Text = args[1];
		        Session.Remove("reloginCommand");
		    }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

        protected void BtnLoginClick(object sender, EventArgs e)
        {
            if (String.Empty == tbReason.Text.Trim())
            {
                ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_REQUIRED_REASON"));
            }
            else if (_requirePassword && !ValidPassword())
            {
                ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_INVALID_PW"));
                if(_logFailures)
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

        private bool ValidPassword()
        {
            return PortalGlobal.IsLoginValid(PortalUser.Current.Username, tbPassword.Text) == Jenzabar.Portal.Framework.Web.LoginResult.Valid;
        }

	    private void PerformLogin()
        {
            var username = tbUserName.Text;

            var user = getPortalUserByUserName(username);
            
            if (user != null)
            {
                var roleCheck = BCProxyLogin.RoleCheck(user, ParentPortlet.Portlet.PortletTemplate);
                if (roleCheck.Success)
                {

                    if (LogAction(tbReason.Text, user.ID))
                    {
                        var currentUser = PortalUser.Current.Username;
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session["file_access"] = new StringDictionary();// UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
                        
                        PortalGlobal.Login(user.Username, String.Empty);
                        HttpContext.Current.Session["ProxyLoginOriginalUser"] = currentUser;
                        BCProxyLogin.RedirectUrl(Response);
                    }
                }
                else
                {
                    ParentPortlet.ShowFeedback(FeedbackType.Message, roleCheck.Reason);
                    if (_logFailures)
                        LogAction(roleCheck.Reason, user.ID);
                }
            }
            else
            {
                ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_USER_NOT_FOUND"));
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

        internal bool LogAction(String reason, Guid userId)
        {
            try
            {
                var logger = new BCLoggerMapperService();

                if (_logIpAddress)
                    reason = reason + " (" + Request.UserHostAddress + ")";
                
                logger.AddLog(ParentPortlet.Portlet.PortletTemplate.ID.AsGuid, PortalUser.Current.ID.AsGuid, userId, reason, DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                if (PortalUser.Current.IsSiteAdmin)
                    ParentPortlet.ShowFeedback(FeedbackType.Error, Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_ADMIN") + ex);
                else
                    ParentPortlet.ShowFeedback(FeedbackType.Error, Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_USER"));

                return false;
            }
        }
      }

   
}
