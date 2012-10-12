<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Logs_View.ascx.cs" Inherits="BCProxyLogin.Logs_View" %>
<%@ Import Namespace="System.Data" %>
<link href="//ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.3/css/jquery.dataTables.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript">

    jQuery(function ($) {
        if ($.fn.dataTableExt == undefined) {
            $.getScript('//ajax.aspnetcdn.com/ajax/jquery.dataTables/1.9.3/jquery.dataTables.min.js', function () {
                LoadTable();
            });
        } else {
            LoadTable();
        }


        function LoadTable() {
            var oTable = $('#logList').dataTable({
                "aaSorting": [[3, 'desc']],
                "aoColumnDefs": [
                    { "sType": "date", "aTargets": [3] },
                    { "sWidth": "100px", "aTargets": [0, 1] },
                    { "sWidth": "110px", "aTargets": [3] }
                ]
            });
        }
    });
</script>
<div class="pSection">
    <br />
    <em>Note: Only getting last 500 logs.</em>
    <br />
    <br />
    <asp:Repeater ID="rptLogList" runat="server" OnItemDataBound="RptLogListItemDataBound">
        <HeaderTemplate>
            <table id="logList" cellpadding="0" cellspacing="0" class="display">
                <thead>
                    <tr>
                        <th>
                            Source User
                        </th>
                        <th>
                            Target User
                        </th>
                        <th>
                            Action
                        </th>
                        <th>
                            Date/Time
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# ((DataRowView)Container.DataItem)["SourceUser"]%>
                </td>
                <td>
                    <%# ((DataRowView)Container.DataItem)["TargetUser"]%>
                    <asp:PlaceHolder ID="plchldRelogin" runat="server"></asp:PlaceHolder>
                </td>
                <td>
                    <%# ((DataRowView)Container.DataItem)["Action"]%>
                </td>
                <td>
                    <%# ((DataRowView)Container.DataItem)["DateTime"]%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </asp:Repeater>
    <br />
</div>
