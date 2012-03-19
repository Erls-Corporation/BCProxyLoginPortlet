using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using AjaxControlToolkit;
using Jenzabar.Portal.Framework;
using Jenzabar.Portal.Framework.Facade;
using StructureMap;
using System.Text;
using System.Web.Script.Services;

namespace BCProxyLogin
{
    /// <summary>
    /// Summary description for StudentSearch
    /// </summary>
    [WebService(Namespace = "https://my.bethelcollege.edu/WebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    public class UserSearch : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public List<Student> FindUser(string term)
        {
            Guid portletID = new Guid(this.Session["BCPLID"].ToString());
            IPortletFacade portletFacade = ObjectFactory.GetInstance<IPortletFacade>();
            Portlet portlet = portletFacade.FindByGuid(portletID);
            List<Student> stuList = new List<Student>();

            int userID = 0;
            if (Int32.TryParse(term, out userID))
            {
                IPortalUserFacade puFacade = ObjectFactory.GetInstance<IPortalUserFacade>();
                PortalUser pu = puFacade.FindByHostID(term.PadLeft(11,'0'));
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
                List<PortalGroup> Groups = new List<PortalGroup>();
                if (portlet.AccessCheck("CanProxyStudent"))
                    Groups.Add(PortalGroup.Students);

                if (portlet.AccessCheck("CanProxyStaff"))
                    Groups.Add(PortalGroup.Staff);

                if (portlet.AccessCheck("CanProxyFaculty"))
                    Groups.Add(PortalGroup.Faculty);

                if (portlet.AccessCheck("CanProxyCandidate"))
                    Groups.Add(PortalGroup.FindByStatusCode("CAN"));

                if (portlet.AccessCheck("CanProxyConstituent"))
                    Groups.Add(PortalGroup.FindByStatusCode("ALM"));

                if (Groups.Count == 0)
                    return new List<Student>();

                IPortalUserFacade userFacade = ObjectFactory.GetInstance<IPortalUserFacade>();
                PortalUserSearch puSearch = new PortalUserSearch(false, 0, 500);
                puSearch.MainCritetia = new PortalUserSearchCriteria(Groups.ToArray(), "", term, UserProfileType.All, false);

                foreach (PortalUser user in userFacade.FindBySearch(puSearch))
                {
                    stuList.Add(new Student(user.FirstName, user.LastName, Convert.ToInt32(user.HostID), user.Username));
                }
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
