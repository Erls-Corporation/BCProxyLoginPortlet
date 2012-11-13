using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Jenzabar.Common;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin.Services
{
    /// <summary>
    /// Summary description for StudentSearch
    /// </summary>
    [WebService(Namespace = "BCProxyLogin.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class UserSearch : WebService
    {
        [WebMethod(EnableSession = true)]
        public List<Student> FindUser(string term)
        {
            var portlet = ObjectFactoryWrapper.GetInstance<IPortletTemplateFacade>().FindByName("[CUS] BCProxyLogin");
            var stuList = new List<Student>();

            if (!portlet.AccessCheck("CANACCESS"))
                return stuList;

            var userFacade = ObjectFactoryWrapper.GetInstance<IPortalUserFacade>();

            int userId;
            if (Int32.TryParse(term, out userId))
            {
                var pu = userFacade.FindByHostID(term.PadLeft(11, '0'));
                if (pu != null)
                {
                    stuList.Add(new Student(pu.FirstName, pu.LastName, Convert.ToInt32(pu.HostID), pu.Username));
                }
            }
            else
            {
                var groups = new List<PortalGroup> { PortalGroup.Users };
                
                stuList.AddRange(userFacade.PartialFind(term, 100, "", groups.Select(x => x.DN).ToArray()).Select(user => new Student(user.FirstName, user.LastName, Convert.ToInt32(user.HostID), user.Username)));
            }
            return stuList;

        }

        public class Student
        {
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public Int32 HostId { get; set; }
            public String Text { get; set; }
            public String UserName { get; set; }

            public Student() { }

            public Student(String firstName, String lastName, Int32 hostId, String userName)
            {
                FirstName = firstName;
                LastName = lastName;
                HostId = hostId;
                Text = lastName + ", " + firstName + " - " + hostId;
                UserName = userName;
            }

        }

    }
}
