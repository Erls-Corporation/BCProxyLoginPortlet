<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainView.ascx.cs" Inherits="BCProxyLogin.MainView" %>
<!-- jQuery, jQuery UI and JSON2 already included by JICS. jQuery UI Theme included as of 7.5.2. -->
<!-- jsRender templating engine. -->
<script type="text/javascript" src="portlets/cus/ics/BCProxyLogin/Scripts/jsrender.min.js"></script>
<!-- Reuseable lib for jQuery portlet development. -->
<script type="text/javascript" src="portlets/cus/ics/BCProxyLogin/Scripts/Portlet.js"></script>
<!-- All ToDoList jQuery code, the meat of the portlet. -->
<script type="text/javascript" src="portlets/cus/ics/BCProxyLogin/Scripts/ProxyLogin.js"></script>
<!-- Hook for views to render in. -->
<link type="text/css" rel="stylesheet" href="portlets/cus/ics/BCProxyLogin/Styles/ProxyLoginStyles.css"/>
<div class="ArrowTabBar" id="bcPLTabBar" style="display: none"> 
<ul>
    <li class="tabSelected" data-view="Default">Default</li>
    <li data-view="Configure">Configure</li>
    <li data-view="Logs">Logs</li>
</ul>
</div>
<div id="bcPLErrorContainer" style="display: none" class="feedbackError"></div>
<div id="bcPLViewContainer"></div>