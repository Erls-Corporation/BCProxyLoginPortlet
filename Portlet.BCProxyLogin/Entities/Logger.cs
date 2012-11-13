using System;
using System.Collections.Generic;
using System.Linq;
using Jenzabar.Portal.Framework.NHibernateFWK;
using Jenzabar.Portal.Framework.NHibernateFWK.Base;
using Jenzabar.Common.ApplicationBlocks.ExceptionManagement;

namespace BCProxyLogin.Entities
{
    public class Logger : JICSBase
    {
        public virtual Guid LogNumber { get; set; }
        public virtual Guid PortletId { get; set; }
        public virtual Guid SourceUser { get; set; }
        public virtual Guid TargetUser { get; set; }
        public virtual String Action { get; set; }
        public virtual DateTime Time { get; set; }
    }


    public class BCLoggerMapperService : JICSBaseFacade<Logger>
    {
        public IList<Logger> GetLogs(Guid portletId)
        {
            
            return GetQuery().Where(x => x.PortletId == portletId).OrderBy(x => x.Time).ToList();
        }

        public Logger GetLog(Guid logId)
        {
            return GetList(x => x.ID == logId)[0];
        }

        public IList<Guid> GetPortlets()
        {
            return (from x in GetQuery() select x.PortletId).Distinct().ToList();
        }

        public IList<Guid> GetSourceUsers()
        {
            return (from x in GetQuery() select x.SourceUser).Distinct().ToList();
        }

        public IList<Guid> GetTargetUsers()
        {
            return (from x in GetQuery() select x.TargetUser).Distinct().ToList();
        }

        public IList<String> GetActions()
        {
            return (from x in GetQuery() select x.Action).Distinct().ToList();
        }

        public bool AddLog(Guid portletId, Guid sourceUser, Guid targetUser, String action, DateTime time)
        {

            var log = new Logger
                {PortletId = portletId, SourceUser = sourceUser, TargetUser = targetUser, Action = action, Time = time};

            try
            {
                Save(log);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw;
            }
        }
    }
}