using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using BCProxyLogin.Entities;
using BCProxyLogin.Helpers;
using Jenzabar.Common;
using Jenzabar.Common.Configuration;
using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;
using Jenzabar.Portal.Framework.Security;
using Jenzabar.Portal.Framework.Web.UI;

namespace BCProxyLogin.Services
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    [WebService(Namespace = "BCProxyLogin.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Login : WebService
    {
        private readonly bool _requirePassword = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_ENABLE_PW");
        private readonly bool _logIpAddress = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_IP");
        private readonly bool _logFailures = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_FAILURES");
        private readonly IGlobalizer _globalizer = ObjectFactoryWrapper.GetInstance<IGlobalizer>();

        [WebMethod(EnableSession = true)]
        public Ret AttemptLogin(string username, string password, string reason)
        {
            if (String.Empty == reason.Trim())
                return new Ret{ Success = false, Message = _globalizer.GetString("CUS_BC_PL_REQUIRED_REASON")};
            
            if (_requirePassword && !ValidPassword(password))
            {
                if (_logFailures)
                {
                    var user = getPortalUserByUserName(username);
                    LogAction(_globalizer.GetString("CUS_BC_PL_INVALID_PW"), user.ID);
                }
                return new Ret{ Success = false, Message = _globalizer.GetString("CUS_BC_PL_INVALID_PW")};
            }
            
            if (HttpContext.Current.Session["ProxyLoginOriginalUser"] != null)
                return new Ret{ Success = false, Message = _globalizer.GetString("CUS_BC_PL_ALREADY_PROXIED")};
            try
            {
                return new LoginRet {Success = true, Url = PerformLogin(username, reason)};
            }
            catch(RoleCheckFailedException ex)
            {
                return new Ret {Success = false, Message = ex.Message};
            }
            catch(UserNotFoundException ex)
            {
                return new Ret {Success = false, Message = _globalizer.GetString("CUS_BC_PL_USER_NOT_FOUND")};
            }
            catch (Exception ex)
            {
                return new Ret {Success = false, Message = ex.ToString()};
            }
        }

        private bool ValidPassword(string password)
        {
            return new PortalUserControlBase().PortalGlobal.IsLoginValid(PortalUser.Current.Username, password) == Jenzabar.Portal.Framework.Web.LoginResult.Valid;
        }

	    private string PerformLogin(string username, string reason)
        {

            var user = getPortalUserByUserName(username);
            
            if (user != null)
            {
                var portlet = ObjectFactoryWrapper.GetInstance<IPortletTemplateFacade>().FindByName("[CUS] BCProxyLogin");
                var roleCheck = new RoleChecker().RoleCheck(user, portlet);
                if (roleCheck.Success)
                {

                    if (LogAction(reason, user.ID))
                    {
                        var currentUser = PortalUser.Current.Username;
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session["file_access"] = new StringDictionary();// UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
                        new PortalUserControlBase().PortalGlobal.AssignUserCredentials(new UserCredentials(user.Username, String.Empty, true));
                        HttpContext.Current.Session["ProxyLoginOriginalUser"] = currentUser;
                        return BCProxyLogin.GetReturnUrl();
                    }
                }
                else
                {
                    if (_logFailures)
                        LogAction(roleCheck.Reason, user.ID);
                    throw new RoleCheckFailedException(roleCheck.Reason);
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
	        
            return string.Empty;
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
                catch{}
            }

            return user;
        }

        internal bool LogAction(String reason, Guid userId)
        {
            try
            {
                var logger = new BCLoggerMapperService();

                if (_logIpAddress)
                    reason = reason + " (" + HttpContext.Current.Request.UserHostAddress + ")";
                var portlet = ObjectFactoryWrapper.GetInstance<IPortletTemplateFacade>().FindByName("[CUS] BCProxyLogin");

                return logger.AddLog(portlet.ID.AsGuid, PortalUser.Current.ID.AsGuid, userId, reason, DateTime.Now);
            }
            catch (Exception ex)
            {
                if (PortalUser.Current.IsSiteAdmin)
                    throw new Exception(_globalizer.GetString("CUS_BC_PL_ERROR_ADMIN") + ex);
                
                throw new Exception(_globalizer.GetString("CUS_BC_PL_ERROR_USER"));
            }
        }

        [Serializable]
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException() { }
            public UserNotFoundException(string message) : base(message) { }
            public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
            protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class RoleCheckFailedException : Exception
        {
            public RoleCheckFailedException() { }
            public RoleCheckFailedException(string message) : base(message) { }
            public RoleCheckFailedException(string message, Exception inner) : base(message, inner) { }
            protected RoleCheckFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
      
    }
}
