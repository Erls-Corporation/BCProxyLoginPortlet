<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarLoggerControl.ascx.cs"
    Inherits="BCProxyLogin.SideBarLoggerControl" %>
<style type="text/css">
    input#<%=tbUserName.ClientID %> {
        width: 100px;
    }
    input#<%=tbReason.ClientID %> {
        width: 84px;
    }
    input#<%=tbPassword.ClientID %> {
        width: 71px;
    }
    div#<%=pnlProxyLogin.ClientID %> span, input#<%= btnLogin.ClientID %>  {
        margin-left: 10px;
    }
</style>
<asp:Panel ID="pnlProxyLogin" runat="server" Visible="false">
    <div class="sideSection">
        <h2>
            Proxy Login Tools</h2>
        <div runat="server" id="divError" class="feedbackError" visible="false">
        </div>
        <div runat="server" id="divmessage" class="feedbackMessage" visible="false">
        </div>
        <asp:Label ID="lblUserName" runat="server" Text="User:"></asp:Label>
        <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
        <br />
        <asp:Panel ID="pnlPassword" runat="server" Visible="false">
            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
        </asp:Panel>
        <asp:Label ID="lblReason" runat="server" Text="Reason:"></asp:Label>
        <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="BtnLoginClick" />
    </div>
</asp:Panel>
