using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Runtime.Serialization;
using Jenzabar.Common;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin
{
    /// <summary>
    /// Summary description for StudentSearch
    /// </summary>
    [WebService(Namespace = "https://my.bethelcollege.edu/WebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class UserSearch : WebService
    {
        [WebMethod(EnableSession = true)]
        public List<Student> FindUser(string term)
        {
            var portlet = PortletTemplate.FindByName("[CUS] BCProxyLogin");
            var stuList = new List<Student>();

            int userId;
            if (Int32.TryParse(term, out userId))
            {
                var puFacade = ObjectFactoryWrapper.GetInstance<IPortalUserFacade>();
                var pu = puFacade.FindByHostID(term.PadLeft(11,'0'));
                if ((portlet.AccessCheck("CanProxyStudent") && pu.IsMemberOf(PortalGroup.Students)) ||
                    (portlet.AccessCheck("CanProxyStaff") && pu.IsMemberOf(PortalGroup.Staff)) ||
                    (portlet.AccessCheck("CanProxyFaculty") && pu.IsMemberOf(PortalGroup.Faculty)) ||
                    (portlet.AccessCheck("CanProxyCandidate") && pu.IsMemberOf(PortalGroup.FindByStatusCode("CAN"))) ||
                    (portlet.AccessCheck("CanProxyConstituent") && pu.IsMemberOf(PortalGroup.FindByStatusCode("ALM"))))
                {
                    stuList.Add(new Student(pu.FirstName, pu.LastName, Convert.ToInt32(pu.HostID), pu.Username));
                }
            }
            else
            {
                var groups = new List<PortalGroup>();
                if (portlet.AccessCheck("CanProxyStudent"))
                    groups.Add(PortalGroup.Students);

                if (portlet.AccessCheck("CanProxyStaff"))
                    groups.Add(PortalGroup.Staff);

                if (portlet.AccessCheck("CanProxyFaculty"))
                    groups.Add(PortalGroup.Faculty);

                if (portlet.AccessCheck("CanProxyCandidate"))
                    groups.Add(PortalGroup.FindByStatusCode("CAN"));

                if (portlet.AccessCheck("CanProxyConstituent"))
                    groups.Add(PortalGroup.FindByStatusCode("ALM"));

                if (groups.Count == 0)
                    return new List<Student>();

                var userFacade = ObjectFactoryWrapper.GetInstance<IPortalUserFacade>();

                stuList.AddRange(userFacade.PartialFind(term, 100, "", groups.Select(x => x.DN).ToArray()).Select(user => new Student(user.FirstName, user.LastName, Convert.ToInt32(user.HostID), user.Username)));
            }
            return stuList;

        }
        
        [DataContract]
        public class Student
        {
            [DataMember]
            public String FirstName { get; set; }

            [DataMember]
            public String LastName { get; set; }

            [DataMember]
            public Int32 HostId { get; set; }

            [DataMember]
            public String Text { get; set; }

            [DataMember]
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
