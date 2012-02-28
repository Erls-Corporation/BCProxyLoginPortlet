<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="Default_View.ascx.cs"
    Inherits="BCProxyLogin.Default_View" TargetSchema="" %>
<%@ Register TagPrefix="common" Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" %>
<style type="text/css">
	.ui-autocomplete {
		max-height: 250px;
		overflow-y: auto;
		/* prevent horizontal scrollbar */
		overflow-x: hidden;
		/* add padding to account for vertical scrollbar */
		padding-right: 20px;
		max-width: 
	}
</style>
<script type="text/javascript">
    jQuery(function ($) {
        if ($.ui.version.match(/^1\.8/) != undefined) {
            $('#<%= tbUserName.ClientID %>').autocomplete({
                minLength: 2,
                delay: 1000,
                source: function (request, response) {
                    $.ajax({
                        url: '<%= this.ResolveUrl("~/Portlets/CUS/ICS/BCProxyLogin/UserSearch.asmx/FindUser") %>',
                        success: function (data) {
                            $('#<%= hdnSearching.ClientID %>').hide();
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
                    $('#<%= tbUserName.ClientID %>').val(ui.item.userName);
                    return false;
                },
                search: function (event, ui) {
                    if (event.target.value.match(/^\d+\s+$/) != undefined)
                        return false;
                    $('#<%= hdnSearching.ClientID %>').show();
                }
            })
                    .data("autocomplete")._renderItem = function (ul, item) {
                        return $("<li></li>")
                            .data("item.autocomplete", item)
                            .append("<a>" + item.text + " - " + item.userName + "</a>")
                            .appendTo(ul);
                    };
        }
    });
</script>
<div class="pSection">
    <table>
        <tr>
            <td><asp:Label ID="lblUserName" runat="server"></asp:Label></td>
            <td>
                <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
                <asp:Label ID="hdnSearching" runat="server" Text="Searching..." CssClass="hidden" />
            </td>
        </tr>
        <asp:Panel ID="pnlPassword" runat="server" Visible="false">
            <tr>
                <td><asp:Label ID="lblPassword" runat="server"></asp:Label></td>
                <td><asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
        </asp:Panel>
        <tr>
            <td><asp:Label ID="lblReason" runat="server"></asp:Label></td>
            <td><asp:TextBox ID="tbReason" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center"><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></td>
        </tr>
    </table>
</div>
