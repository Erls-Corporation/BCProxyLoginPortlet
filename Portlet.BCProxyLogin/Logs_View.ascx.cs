using System;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using BCProxyLogin.Entities;
using Jenzabar.Common;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Common.Configuration;
using Jenzabar.Portal.Framework.Facade;

namespace BCProxyLogin
{
    public partial class Logs_View : PortletViewBase
    {

        private readonly bool _allowRelogin = ConfigSettings.GetConfigBoolean("C_PortletSettings", "CUS_BC_PL_ALLOWRELOGIN");

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLogData();
        }
        
        public void GetLogData()
        {
            var logger = new BCLoggerMapperService();
            var userFacade = ObjectFactoryWrapper.GetInstance<IPortalUserFacade>();
            
            var dt = new DataTable();
            var dc1 = new DataColumn("SourceUser");
            var dc2 = new DataColumn("TargetUser");
            var dc3 = new DataColumn("Action");
            var dc4 = new DataColumn("DateTime");

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);


            foreach (var log in logger.GetLogs(ParentPortlet.Portlet.PortletTemplate.ID.AsGuid).OrderByDescending(x => x.Time))
            {
                var dr = dt.NewRow();
                dr["SourceUser"] = userFacade.FindByGuid(log.SourceUser).Username;
                dr["TargetUser"] = userFacade.FindByGuid(log.TargetUser).Username;
                dr["Action"] = log.Action.Replace("'", "\\'");
                dr["DateTime"] = log.Time.ToShortDateString() + ' ' + log.Time.ToShortTimeString();

                dt.Rows.Add(dr);
            }
            rptLogList.DataSource = dt;
            rptLogList.DataBind();
        }

        protected void RptLogListItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!_allowRelogin) return;

            var item = e.Item;
            if ((item.ItemType != ListItemType.Item) && (item.ItemType != ListItemType.AlternatingItem)) return;

            var drv = (DataRowView)item.DataItem;
            var ibtn = new ImageButton
                           {
                               ImageUrl = "~/UI/Common/Images/PortletImages/Icons/16/arrow_undo.png",
                               CommandArgument = drv["TargetUser"] + "|" + drv["Action"]
                           };
            ibtn.Click += ImgbtnReloginClick;
            item.FindControl("plchldRelogin").Controls.Add(ibtn);
        }

        protected void ImgbtnReloginClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var ibtn = (ImageButton)sender;

            Session["reloginCommand", PortletSessionScope.PortletShortcut] = ibtn.CommandArgument;

            ParentPortlet.NextScreen("Default");
        }
    }
}