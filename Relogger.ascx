<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Relogger.ascx.cs" Inherits="BCProxyLogin.Relogger" %>
<style type="text/css">
    .ProxyLoginLinks li {
        height: 15px;
    }
.ProxyLoginLinks li a
{
    color: #000000 !important;
    margin-left: 18px !important;
}
</style>
<asp:Panel ID="pnlProxyRelogger" runat="server" Visible="false">
    <div class="sideSection">
        <h2>Proxy Login Tools</h2>
        <ul class="ProxyLoginLinks">
            <li><asp:LinkButton runat="server" ID="lnbRelog" Text="Re-Login As Current User"></asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnbLogback" Text="Log Back Into Original User"></asp:LinkButton></li>
        </ul>
    </div>
</asp:Panel>