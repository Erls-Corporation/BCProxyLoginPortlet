using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework;

namespace BCProxyLogin.Helpers
{
    public class RoleChecker
    {
        public Check RoleCheck(PortalUser pu, PortletTemplate portletTemplate, PortalUser currentUser = null)
        {
            if (currentUser == null)
                currentUser = PortalUser.Current;

            if(portletTemplate.AccessCheck("DenyAccess", pu))
                return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_DENIED_PERMS"));

            if (pu.IsMemberOf(PortalGroup.Staff))
                if (!portletTemplate.AccessCheck("CanProxyStaff", currentUser))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_STAFF_PERMS"));

            if (pu.IsMemberOf(PortalGroup.Faculty))
                if (!portletTemplate.AccessCheck("CanProxyFaculty", currentUser))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_FACULTY_PERMS"));

            if (pu.IsMemberOf(PortalGroup.Students))
                if (!portletTemplate.AccessCheck("CanProxyStudent", currentUser))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_STUDENT_PERMS"));

            if (pu.IsMemberOf(PortalGroup.FindByStatusCode("CAN"))) // Candidate
                if (!portletTemplate.AccessCheck("CanProxyCandidate", currentUser))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_CANDIDATE_PERMS"));

            if (pu.IsMemberOf(PortalGroup.FindByStatusCode("ALM")))
                if (!portletTemplate.AccessCheck("CanProxyConstituent", currentUser))
                    return new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_CONSTITUENT_PERMS"));

            return pu.IsMemberOf(PortalGroup.Administrators) ? new Check(false, Globalizer.GetGlobalizedString("CUS_BC_PL_SITE_ADMIN_PERMS")) : new Check(true);
        }
    }
}