<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Logs_View.ascx.cs" Inherits="BCProxyLogin.Logs_View" %>
<link href="<%= this.ResolveUrl("~/ClientConfig/css/jqueryDataTable.css") %>" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">

    var data = <%= this.GetData() %>;
    jQuery(function($) {
        if ($.fn.dataTableExt == undefined) {
            $.getScript('<%= this.ResolveUrl("~/ClientConfig/js/jquery.dataTables.min.js") %>', function() {
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
                    { "sWidth": "100px", "aTargets": [0, 1, 3] }
                ]
            });
            oTable.fnAddData(data);
        }
    });
</script>
<br />
<em>Note: Only getting last 500 logs.</em>
<br />
<br />
<table id="logList" cellpadding="0" cellspacing="0" border="0" class="display">
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
    </tbody>
</table>
