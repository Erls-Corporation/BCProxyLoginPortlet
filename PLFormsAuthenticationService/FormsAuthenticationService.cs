using System;
using System.Web;
using System.Web.Security;
using mobile.Infrastructure.Security;

namespace PLFormsAuthenticationService
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Clear();
            FormsAuthentication.SignOut();
        }
    }
}
