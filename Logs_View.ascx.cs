using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCNHibernate;
using Jenzabar.Portal.Framework.Web.UI;
using StructureMap;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin
{
    public partial class Logs_View : PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetData()
        {
            BCLoggerMapperService logger = new BCLoggerMapperService();

            String ret = String.Empty;

            IPortalUserFacade userFacade = ObjectFactory.GetInstance<IPortalUserFacade>();
            List<String> dataList = new List<string>();

            ret = "[";

            foreach (BCLogger log in logger.GetLogs(this.ParentPortlet.Portlet.PortletTemplate.ID.AsGuid).OrderByDescending(x => x.Time))
            {
                StringBuilder row = new StringBuilder();

                row.AppendFormat("[ '{0}', '{1}', '{2}', '{3} ' ]",
                        userFacade.FindByGuid(log.SourceUser).Username,
                        userFacade.FindByGuid(log.TargetUser).Username,
                        log.Action.Replace("'","\\'").ToString(),
                        log.Time.ToShortDateString() + ' ' + log.Time.ToShortTimeString());

                dataList.Add(row.ToString());

                if (dataList.Count >= 500)
                    break;
            }
            ret += string.Join(",", dataList.ToArray());
            ret += "]";

            return ret;
        }
    }
}