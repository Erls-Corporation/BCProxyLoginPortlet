using System;
using System.Web;
using BCNHibernate;
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (this.IsFirstLoad) 
			{
                lblReason.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_REASON_LABEL_TEXT");
                lblUserName.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_USERNAME_LABEL_TEXT");
                lblPassword.Text = Globalizer.GetGlobalizedString("CUS_BC_PL_PASSWORD_LABEL_TEXT");

                if (_requirePassword)
                    pnlPassword.Visible = true;

                this.Page.Session["BCPLID"] = this.ParentPortlet.Portlet.ID.AsGuid;
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.Empty == tbReason.Text.Trim())
            {
                this.ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_REQUIRED_REASON").ToString());
            }
            else if (_requirePassword && !validPassword())
            {
                this.ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_INVALID_PW").ToString());
                if(_logFailures)
                {
                    var user = getPortalUserByUserName(tbUserName.Text);
                    LogAction(Globalizer.GetGlobalizedString("CUS_BC_PL_INVALID_PW").ToString(), user.ID);
                }
            }
            else
            {
                PerformLogin();
            }
            
        }

        private bool validPassword()
        {
            if (this.PortalGlobal.IsLoginValid(PortalUser.Current.Username, tbPassword.Text) == Jenzabar.Portal.Framework.Web.LoginResult.Valid)
                return true;
            else
                return false;
        }

        private void PerformLogin()
        {
            var username = tbUserName.Text;

            var user = getPortalUserByUserName(username);
            
            if (user != null)
            {
                Check roleCheck = BCProxyLogin.RoleCheck(user, this.ParentPortlet.Portlet.PortletTemplate);
                if (roleCheck.success)
                {

                    if (LogAction(tbReason.Text.ToString(), user.ID))
                    {
                        String currentUser = PortalUser.Current.Username;
                        HttpContext.Current.Session.Clear();
                        
                        this.PortalGlobal.Login(user.Username, String.Empty);
                        HttpContext.Current.Session["ProxyLoginOriginalUser"] = currentUser;
                        BCProxyLogin.RedirectUrl(Response);
                    }
                }
                else
                {
                    this.ParentPortlet.ShowFeedback(FeedbackType.Message, roleCheck.reason);
                    if (_logFailures)
                        LogAction(roleCheck.reason, user.ID);
                }
            }
            else
            {
                this.ParentPortlet.ShowFeedback(FeedbackType.Message, Globalizer.GetGlobalizedString("CUS_BC_PL_USER_NOT_FOUND").ToString());
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
                
                logger.AddLog(this.ParentPortlet.Portlet.PortletTemplate.ID.AsGuid, PortalUser.Current.ID.AsGuid, userId, reason, DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                if (PortalUser.Current.IsSiteAdmin)
                    this.ParentPortlet.ShowFeedback(FeedbackType.Error, Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_ADMIN").ToString() + ex);
                else
                    this.ParentPortlet.ShowFeedback(FeedbackType.Error, Globalizer.GetGlobalizedString("CUS_BC_PL_ERROR_USER").ToString());

                return false;
            }
        }
      }

   
}
