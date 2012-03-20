using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCNHibernate;
using System.Data;
using System.Web.UI.WebControls;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Common.Configuration;
using StructureMap;
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
            BCLoggerMapperService logger = new BCLoggerMapperService();
            IPortalUserFacade userFacade = ObjectFactory.GetInstance<IPortalUserFacade>();
            List<String> dataList = new List<string>();

            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("SourceUser");
            DataColumn dc2 = new DataColumn("TargetUser");
            DataColumn dc3 = new DataColumn("Action");
            DataColumn dc4 = new DataColumn("DateTime");

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);


            foreach (BCLogger log in logger.GetLogs(this.ParentPortlet.Portlet.PortletTemplate.ID.AsGuid).OrderByDescending(x => x.Time))
            {
                DataRow dr = dt.NewRow();
                dr["SourceUser"] = userFacade.FindByGuid(log.SourceUser).Username;
                dr["TargetUser"] = userFacade.FindByGuid(log.TargetUser).Username;
                dr["Action"] = log.Action.Replace("'", "\\'").ToString();
                dr["DateTime"] = log.Time.ToShortDateString() + ' ' + log.Time.ToShortTimeString();

                dt.Rows.Add(dr);
            }
            rptLogList.DataSource = dt;
            rptLogList.DataBind();
        }

        protected void rptLogList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if(_allowRelogin)
            {
                RepeaterItem item = e.Item;
                if ((item.ItemType == ListItemType.Item) ||
                    (item.ItemType == ListItemType.AlternatingItem))
                {
                    DataRowView drv = (DataRowView)item.DataItem;
                    ImageButton ibtn = new ImageButton();
                    ibtn.ImageUrl = "~/UI/Common/Images/PortletImages/Icons/16/arrow_undo.png";
                    ibtn.CommandArgument = drv["TargetUser"].ToString() + "|" + drv["Action"].ToString();
                    ibtn.Click += new System.Web.UI.ImageClickEventHandler(imgbtnRelogin_Click);
                    ((PlaceHolder)item.FindControl("plchldRelogin")).Controls.Add(ibtn);
                }
            }
        }

        protected void imgbtnRelogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ImageButton ibtn = (ImageButton)sender;

            Session["reloginCommand", PortletSessionScope.PortletShortcut] = ibtn.CommandArgument;

            this.ParentPortlet.NextScreen("Default");
        }
    }
}