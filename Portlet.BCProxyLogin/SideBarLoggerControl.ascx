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
	.ui-autocomplete {
		max-height: 250px;
		overflow-y: auto;
		/* prevent horizontal scrollbar */
		overflow-x: hidden;
		/* add padding to account for vertical scrollbar */
		padding-right: 20px;
	    z-index: 1000 !important;
	}
</style>
<script type="text/javascript">
    jQuery(function ($) {
        if ($.ui.version.match(/^1\.8/) != undefined && $('#<%= tbUserName.ClientID %>').length > 0) {
            $('#<%= tbUserName.ClientID %>').autocomplete({
                minLength: 2,
                delay: 100,
                source: function (request, response) {
                    $.ajax({
                        url: '<%= ResolveUrl("~/Portlets/CUS/ICS/BCProxyLogin/UserSearch.asmx/FindUser") %>',
                        success: function (data) {
                            response(data.d);
                        },
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        data: Sys.Serialization.JavaScriptSerializer.serialize({ term: request.term })
                    });
                },
                focus: function (event, ui) {
                    $('#<%= tbUserName.ClientID %>').val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    $('#<%= tbUserName.ClientID %>').val(ui.item.UserName);
                    return false;
                }
            })
                    .data("autocomplete")._renderItem = function (ul, item) {
                        return $("<li></li>")
                            .data("item.autocomplete", item)
                            .append("<a>" + item.Text + " - " + item.UserName + "</a>")
                            .appendTo(ul);
                    };
        }
    });
</script>
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
