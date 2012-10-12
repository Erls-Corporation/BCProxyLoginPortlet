using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using BCProxyLogin.Helpers;
using Jenzabar.Common.Configuration;
using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework.Facade;
using mobile.Infrastructure.Attributes;
using mobile.Infrastructure.Security;
using mobile.Models;
using mobile.Services;
using BCProxyLogin.Entities;

namespace Go.BCProxyLogin.Controllers
{
    [GoView(true)]
    public class BCProxyLoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IPortletTemplateFacade _portletTemplateFacade;
        private readonly IPortalUserFacade _portalUserFacade;
        private readonly bool _requirePassword = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_ENABLE_PW");
        private readonly bool _logIpAddress = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_IP");
        private readonly bool _logFailures = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_LOG_FAILURES");

        public BCProxyLoginController(IPortalUserFacade portalUserFacade, ILoginService loginService, IFormsAuthenticationService formsAuthenticationService, IPortletTemplateFacade portletTemplateFacade)
        {
            _loginService = loginService;
            _formsAuthenticationService = formsAuthenticationService;
            _portletTemplateFacade = portletTemplateFacade;
            _portalUserFacade = portalUserFacade;
        }

        public ActionResult Index(User user)
        {
            if (user.Username == "Guest")
                return View("Error", new Models.Error {Message = "User Not Loged In" });

            return !_portletTemplateFacade.FindByName("[CUS] BCProxyLogin").AccessCheck("CANACCESS",_portalUserFacade.FindByUsername(user.Username)) 
                ? View("Error", new Models.Error {Message = "You are not authorized for this function."}) 
                : View("Index", new Models.Main {RequiresPassword = _requirePassword});
        }

        public ActionResult CheckCredentials(User currentUser, string username, string password, string reason)
        {

            var returnContent = Globalizer.GetGlobalizedString("MSG_LOGINPORTLET_INVALID");
            if (currentUser.Username == "Guest")
                return Content("User Not Loged In");

            var currentPortalUser = _portalUserFacade.FindByUsername(currentUser.Username);

            if (Request.IsAjaxRequest())
            {
                try
                {
                    if (_loginService.IsLoginValid(currentUser.Username, password) || ! _requirePassword)
                    {
                        if (System.Web.HttpContext.Current.Session["ProxyLoginOriginalUser"] != null)
                            return Content(Globalizer.GetGlobalizedString("CUS_BC_PL_ALREADY_PROXIED"));

                        var user = _portalUserFacade.FindByUsername(username);
                        if (user != null)
                        {
                            var roleCheck = new RoleChecker().RoleCheck(user, _portletTemplateFacade.FindByName("[CUS] BCProxyLogin"), currentPortalUser);
                            if (roleCheck.Success)
                            {
                                LogAction(reason, user.ID, currentPortalUser.ID);
                                
                                System.Web.HttpContext.Current.Session.Clear();
                                System.Web.HttpContext.Current.Session["file_access"] = new StringDictionary();// UploadFile doesn't check to see if there is a valid StringDictionary here, and does a cast.  This causes a unhandled exception that bubbles up to a YSOD
                                
                                _formsAuthenticationService.SignIn(username, false);
                                
                                System.Web.HttpContext.Current.Session["ProxyLoginOriginalUser"] = currentUser;
                                returnContent = "OK";
                            }else
                            {
                                if (_logFailures)
                                    LogAction(roleCheck.Reason, user.ID, currentPortalUser.ID);
                                returnContent = roleCheck.Reason;
                            }
                        }else
                        {
                            returnContent = Globalizer.GetGlobalizedString("CUS_BC_PL_USER_NOT_FOUND");
                        }
                    }
                }
                catch (Exception ex)
                {
                    returnContent = ex.GetBaseException().Message;
                }
            }

            return Content(returnContent);
        }

        internal void LogAction(String reason, Guid userId, Guid currentUserId)
        {
           
                var logger = new BCLoggerMapperService();
                var portletTemplate = _portletTemplateFacade.FindByName("[CUS] BCProxyLogin");
                if (_logIpAddress)
                    reason = reason + " (" + ControllerContext.RequestContext.HttpContext.Request.UserHostAddress + ")";

                if (!logger.AddLog(portletTemplate.ID.AsGuid, currentUserId, userId, reason, DateTime.Now))
                {
                    throw new Exception("Can't log action");
                }
            
        }


    }
}