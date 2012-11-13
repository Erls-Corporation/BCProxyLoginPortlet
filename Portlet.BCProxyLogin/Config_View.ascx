<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Config_View.ascx.cs"
    Inherits="BCProxyLogin.Config_View" %>
<%@ Register TagPrefix="jenzabar" Namespace="Jenzabar.Common.Web.UI.Controls" Assembly="Jenzabar.Common" %>
<style type="text/css">
    .ulPLSetup li:nth-child(1) {
        border-top: none !important;
    }
    .ulPLSetup li:nth-child(odd)
    {
        width: 425px;
        clear: left;
        padding-right: 10px;
        font-weight: bold;
        padding-left: 5px;
    }
    .ulPLSetup li:nth-child(even)
    {
        text-align: center;
        width: 150px;
    }
    .ulPLSetup li
    {
        float: left;
        border-top: 1px solid #E4E4E4;
        padding-top: 4px;
    }
    .ulPLSetup
    {
        float: left;
        clear: both;
        width: 100%;
    }
</style>
<div class="pSection">
    <h4 style="float:left;width:100%"><jenzabar:GlobalizedLiteral runat="server" TextKey="CUS_BC_PL_GENERAL_SETUP_HEADER" /></h4>
    <ul id="ulConfig" class="ulPLSetup">
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_ENABLE_PW">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdEnablePWYes">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_YES">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="Y" id="rdEnablePWYes"
                    name="rdEnablePW" />
            <label for="rdEnablePWNo">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_NO">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="N" id="rdEnablePWNo"
                    name="rdEnablePW" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_LOG_IP">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdLogIPYes">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_YES">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="Y" id="rdLogIPYes"
                    name="rdLogIP" />
            <label for="rdLogIPNo">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_NO">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="N" id="rdLogIPNo"
                    name="rdLogIP" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_LOG_FAILURES">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdLogFailuresYes">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_YES">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="Y" id="rdLogFailuresYes"
                    name="rdLogFailures" />
            <label for="rdLogFailuresNo">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_NO">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="N" id="rdLogFailuresNo"
                    name="rdLogFailures" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_ALLOWRELOGIN">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdAllowReloginYes">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_YES">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="Y" id="rdAllowReloginYes"
                    name="rdAllowRelogin" />
            <label for="rdAllowReloginNo">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_NO">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="N" id="rdAllowReloginNo"
                    name="rdAllowRelogin" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_PERMISSIONS_RANK">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdPermissionRankDeny">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="CUS_BC_PL_DENY">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="D" id="rdPermissionRankDeny"
                    name="rdPermissionRank" />
            <label for="rdPermissionRankAllow">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="CUS_BC_PL_ALLOW">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="A" id="rdPermissionRankAllow"
                    name="rdPermissionRank" />
        </li>
    </ul>
    <h4 style="float:left;width:100%"><jenzabar:GlobalizedLiteral runat="server" TextKey="CUS_BC_PL_USAGE_REPORTS_HEADER" /></h4>
    <ul runat="server" id="ulScheduledReports" visible="False" class="ulPLSetup">
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_ENABLE_REPORTS">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="rdEnableReportsYes">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_YES">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="Y" id="rdEnableReportsYes"
                    name="rdEnableReports" />
            <label for="rdEnableReportsNo">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_NO">
                </jenzabar:GlobalizedLiteral></label><input type="radio" value="N" id="rdEnableReportsNo"
                    name="rdEnableReports" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_REPORT_FREQUENCY">
            </jenzabar:GlobalizedLabel>
        </li>
        <li>
            <label for="chkReportDaily">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="CUS_BC_PL_DAILY">
                </jenzabar:GlobalizedLiteral></label><input type="checkbox" id="chkReportDaily" name="chkReportDaily" />
            <label for="chkReportWeekly">
                <jenzabar:GlobalizedLiteral runat="server" TextKey="TXT_WEEKLY">
                </jenzabar:GlobalizedLiteral></label><input type="checkbox" id="chkReportWeekly"
                    name="chkReportWeekly" />
        </li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_REPORT_ROLES">
            </jenzabar:GlobalizedLabel>
        </li>
        <li></li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_REPORT_EMAIL_SUBJECT">
            </jenzabar:GlobalizedLabel>
        </li>
        <li></li>
        <li>
            <jenzabar:GlobalizedLabel runat="server" TextKey="CUS_BC_PL_REPORT_EMAIL_BODY">
            </jenzabar:GlobalizedLabel>
        </li>
        <li></li>
    </ul>
</div>
