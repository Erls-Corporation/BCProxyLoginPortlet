using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;

namespace Go.BCProxyLogin.Controllers
{
    public class BCProxyLoginController : Controller
    {
        private readonly IPortalUserFacade _userFacade;

        public BCProxyLoginController(IPortalUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public ActionResult Index(IPrincipal iPrincipal)
        {
            // This gets the current user
            PortalUser user = _userFacade.FindByUsername(iPrincipal.Identity.Name);

            Models.BCProxyLogin model = new Models.BCProxyLogin();

            return View(model);
        }
    }
}