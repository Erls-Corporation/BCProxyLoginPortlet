using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Runtime.Serialization;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;
using StructureMap;

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

            int userID;
            if (Int32.TryParse(term, out userID))
            {
                var puFacade = ObjectFactory.GetInstance<IPortalUserFacade>();
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

                var userFacade = ObjectFactory.GetInstance<IPortalUserFacade>();

                stuList.AddRange(userFacade.PartialFind(term, 100, "", groups.Select(x => x.DN).ToArray()).Select(user => new Student(user.FirstName, user.LastName, Convert.ToInt32(user.HostID), user.Username)));
            }
            return stuList;

        }
        
        [DataContract]
        public class Student
        {
            [DataMember]
            public String firstName { get; set; }

            [DataMember]
            public String lastName { get; set; }

            [DataMember]
            public Int32 hostId { get; set; }

            [DataMember]
            public String text { get; set; }

            [DataMember]
            public String userName { get; set; }

            public Student() { }

            public Student(String firstName, String lastName, Int32 hostId, String userName)
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.hostId = hostId;
                this.text = lastName + ", " + firstName + " - " + hostId;
                this.userName = userName;
            }

        }
        
    }
}
