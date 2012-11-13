// Library of objects that could be used across portlets.

///////////////////////////////////////////////////////////////////////////////
// portletBase
///////////////////////////////////////////////////////////////////////////////
(function (pb, $) {
    // Public methods.

    // Known Issue: Refresh or Back Button will lose min/max state.
    // Need to solve a couple of tough problems to resolve this:
    // 1. Making sure portlet can be uniquely identified even if several of same are on a page.
    // 2. Maintain min/max state in URL hash info to track for back/refresh, including specific portlet.
    // However, if ASP.NET and JICS manages min/max, these calls are unnecessary and the problem is resolved.

    // Default view size, only works when on a screen with other portlets.
    pb.minimizeDefaultView = function (callingElement) {
        var jqElem = $(callingElement);
        jqElem.parents('.portlet').hide();
        jqElem.parents('.pColumn').removeAttr('style');
        $('.portlet').show(800);
    };

    // Main view size, only works when on a screen with other portlets.
    pb.maximizeDefaultView = function (callingElement) {
        var jqElem = $(callingElement);
        jqElem.parents('.pColumn').attr('style', 'width: 100%');
        var portlet = jqElem.parents('.portlet');
        $('.portlet').not(portlet).hide(400);
        portlet.show(800);
    };

    // Future change would be to globalize link text.
    // Would need a generic portlet level web service to get the text.
    pb.toggleSizeOfDefaultView = function (callingElement) {
        var jqElem = $(callingElement);
        if (!jqElem.parents('.pColumn').attr('style')) {
            pb.maximizeDefaultView(jqElem);
            $(callingElement).text('Minimize View');
        } else {
            pb.minimizeDefaultView(jqElem);
            $(callingElement).text('Maximize View');
        }
    };

    // Take over entire window, only works on a screen with only one portlet. 
    // However, a default view portlet with other portlet can be maximized with maximizeDefaultView
    // and then take over entire window with this method.
    pb.enableEntireWindowView = function () {
        $('#tdlEntireWindowStyle').attr('href', 'portlets/cus/todolistportlet/Styles/EntireWindow.css');
    };

    // Reset entire window to normal view.
    pb.disableEntireWindowView = function () {
        $('#tdlEntireWindowStyle').attr('href', '');
    };

    // Future change would be to globalize link text.
    // Would need a generic portlet level web service to get the text.
    pb.toggleEntireWindowView = function (callingElement) {
        if ($('#tdlEntireWindowStyle').attr('href') == '') {
            pb.enableEntireWindowView();
            if (callingElement) {
                $(callingElement).text('Return to Normal View');
            }
        } else {
            pb.disableEntireWindowView();
            if (callingElement) {
                $(callingElement).text('Go to Full-Screen View');
            }
        }
    };

    pb.setCurrentViewBasedOnHash = function (portlet, defaultViewName) {
        var hash = location.hash.substring(1);
        if (hash == '') {
            hash = defaultViewName;
        }
        // Set pointer to correct view so widgets can call it.
        portlet.currentView = eval(hash);
        // Fire event of desired view.
        eval(hash + '.init()');
    };
} (window.portletBase = window.portletBase || {}, jQuery));

///////////////////////////////////////////////////////////////////////////////
// ajaxUtil
///////////////////////////////////////////////////////////////////////////////
(function (au, $) {
    au.callWebService = function (webService, data, callback) {
        $.ajax({
            type: 'POST',
            url: webService,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            dataType: 'json',
            success: callback
        });
    };
} (window.ajaxUtil = window.ajaxUtil || {}, jQuery));

///////////////////////////////////////////////////////////////////////////////
// templateUtil
///////////////////////////////////////////////////////////////////////////////
(function (tu, $) {
    tu.render = function (target, filePath, data, callback) {
        $.get(filePath, function (template) {
            var htmlString;
            if (!data) {
                htmlString = template;
            } else {
                htmlString = $.templates(template).render(data);
            }
            target.html(htmlString);
            if (callback && typeof (callback) === "function") {
                callback();
            }
        });
    };
} (window.templateUtil = window.templateUtil || {}, jQuery));