<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainView.ascx.cs" Inherits="BCProxyLogin.MainView" %>
<link type="text/css" rel="stylesheet" href="portlets/cus/ics/BCProxyLogin/Styles/ProxyLoginStyles.css" />
<script type="text/template" id="bc-pl-tab-header-template">
    <div class="ArrowTabBar" id="bcPLTabBar"> 
    <ul>
        <li class="tabSelected" data-view="Default">Default</li>
        <li data-view="Configure">Configure</li>
        <li data-view="Logs">Logs</li>
    </ul>
    </div>    
</script>
<script type="text/template" id="bc-pl-error-template">
    <div id="bcPLErrorContainer" style="display: none" class="feedbackError">
        {{data.Message}}
    </div>    
</script>
<script type="text/template" id="bc-pl-default-view-template">
    <div id="bcPLViewContainer">
        <div id="bcPLDefaultView">
            <ul>
                <li>
                    <label for="bcPLUserName">{{= data.labelUserName }}</label>
                    <input type="text" id="bcPLUserName" name="bcPLUserName" />
                </li>
                {{if (data.EnablePassword == 'true') { }}
                <li>
                    <label for="bcPLPassword">{{=data.labelPassword}}</label>
                    <input type="password" id="bcPLPassword" name="bcPLPassword"/>
                </li>
                {{ } }}
                <li>
                    <label for="bcPLReason">{{=data.labelReason}}</label>
                    <input type="text" id="bcPLReason" name="bcPLReason"/>
                </li>
                <li>
                    <input type="button" id="bcPLProxy" name="bcPLProxy" value="Submit"/>
                </li>
            </ul>
        </div>
    </div>
</script>
<script type="text/template" id="bc-pl-configure-view-template">
    Configure placeholder
</script>
<script type="text/template" id="bc-pl-logs-view-template">
    Logs placeholder
</script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/assets/base.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/assets/lodash.min.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/lib/backbone-min.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/lib/backbone-asmx.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/lib/template-cache.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/models/ProxyLogin.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/collections/ProxyLogin.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/views/ProxyLogin.js"></script>
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/views/app.js"></script>
<!-- <script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/backbone/routers/router.js"></script> -->
<script src="/ics/Portlets/CUS/ICS/BCProxyLogin/Scripts/Backbone/app.js"></script>
<script type="text/javascript">
    jQuery(function () {
        // Kick things off by creating the **App**.
        var test = new window.bcProxyLoginPortlet.AppView({ el: $("#<%=bcPLContainer.ClientID %>"),
            portletId: '<%= ParentPortlet.Portlet.ID.AsGuid %>', baseServicePath: '<%=ResolveUrl("~/portlets/cus/ics/BCProxyLogin/Services/") %>'
        });
    });    
</script>
<div id="bcPLContainer" runat="server">
    <div id="bcPLTabBarContainer">
    </div>
    <div id="bcPLViewContainer">
    </div>
</div>
