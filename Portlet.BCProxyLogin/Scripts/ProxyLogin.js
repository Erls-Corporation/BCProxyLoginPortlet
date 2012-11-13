// jQuery document ready event will kick off the JavaScript for the view.
// If there are more than one jQuery ready functions on the page, all will fire.
jQuery(document).ready(function ($) {
    bcProxyLoginPortlet.init();
});

// Below "objects" utilize namespacing and isolated use of jQuery $.

(function(bcpl, $) {

    bcpl.init = function() {
        // Back button support. Simple implementation, jQuery plugins exist for better support.
        $(window).on('hashchange', function() {
            portletBase.setCurrentViewBasedOnHash(bcpl, 'bcProxyLoginDefaultView');
        });
        bcProxyLoginService.isAdmin(function(data) {
            if (data.d == true)
                $('#bcPLTabBar').show();
        });
        // Load first view based on hash.
        portletBase.setCurrentViewBasedOnHash(bcpl, 'bcProxyLoginDefaultView');
        
        $('#bcPLTabBar ul li').on('click', function() {
            if ($(this).hasClass('tabSelected'))
                return;
            
            bcpl.setCurrentView($(this).data('view'));
        });
    };

    bcpl.setCurrentView = function(viewName) {
        // Causes 'hashchange' event to fire.
        window.location.hash = 'bcProxyLogin' + viewName + 'View';
    };
    
    bcpl.renderTemplate = function(target, templateName, data, callback) {
        templateUtil.render(
            target, 
            "Portlets/CUS/ICS/BCProxyLogin/templates/" + templateName + ".htm", 
            data, 
            callback);
    };

    

    // Helper method for templating system.             
    $.views.helpers({
        getBCPLGlobalizedText: function (key) {
            return eval("bcpl.currentView.globalizedText." + key);
        }
    });

}(window.bcProxyLoginPortlet = window.bcProxyLoginPortlet || { }, jQuery));

///////////////////////////////////////////////////////////////////////////////
// bcProxyLoginDefaultView
///////////////////////////////////////////////////////////////////////////////
(function (bcpldv, $) {

    //var canAdmin;
    
    // Public methods.
    bcpldv.init = function () {
        if (!bcpldv.globalizedText) {
            bcProxyLoginService.getGlobalizedText(
                'Default',
                function (data) {
                    bcpldv.globalizedText = data.d;
                    //canAdmin = data.d.CanAdmin;
                    renderView();
                }
            );
        }
        else {
            renderView();
        }
    };


    // Private methods.
    function renderView() {
        $('#bcPLTabBar ul').children().removeClass('tabSelected');
        $('#bcPLTabBar ul li[data-view="Default"]').addClass('tabSelected');
        
        $(this).addClass('tabSelected');
        bcProxyLoginPortlet.renderTemplate(
            $('#bcPLViewContainer'),
            'DefaultView',
            bcpldv.globalizedText,
            initWidgets
        );
    }

    function processLogin(data) {
        if (data.d.Success) {
            window.location = data.d.Url;
        }else {
            $('#bcPLErrorContainer').show().html(data.d.Message);
            $('#bcPLProxy').attr('disabled', false);
        }
    }

    function initWidgets() {
        if ($.ui.version.match(/^1\.8/) != undefined && !$('#bcPLUserName').is(':data(autocomplete)')) {
            $('#bcPLUserName').autocomplete({
                minLength: 2,
                delay: 100,
                source: function(request, response) {
                    bcProxyLoginService.findUser(request.term, function(data) { response(data.d); });
                },
                focus: function(event, ui) {
                    $('#bcPLUserName').val(ui.item.label);
                    return false;
                },
                select: function(event, ui) {
                    $('#bcPLUserName').val(ui.item.UserName);
                    return false;
                }
            })
                .data("autocomplete")._renderItem = function(ul, item) {
                    return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + item.Text + " - " + item.UserName + "</a>")
                        .appendTo(ul);
                };
        }

        $('#bcPLProxy').on('click', function() {
            $(this).attr('disabled', true);
            bcProxyLoginService.login($('#bcPLUserName').val(), $('#bcPLReason').val(), $('#bcPLPassword').val(), processLogin);
        });

        $('#bcPLReason, #bcPLUserName, #bcPLPassword').on('keypress', function(e) {
            if ((e.keyCode || e.which) == 13) {
                $('#bcPLProxy').click();
                return false;
            }
            return true;
        });

    }
} (window.bcProxyLoginDefaultView = window.bcProxyLoginDefaultView || {}, jQuery));


///////////////////////////////////////////////////////////////////////////////
// bcProxyLoginConfigureView
///////////////////////////////////////////////////////////////////////////////
(function (bcplcv, $) {

    // Public methods.
    bcplcv.init = function () {
        if (!bcplcv.globalizedText) {
            bcProxyLoginService.getGlobalizedText(
                'Configure',
                function (data) {
                    bcplcv.globalizedText = data.d;
                    renderView();
                }
            );
        }
        else {
            renderView();
        }
    };


    // Private methods.
    function renderView() {
        $('#bcPLTabBar ul').children().removeClass('tabSelected');
        $('#bcPLTabBar ul li[data-view="Configure"]').addClass('tabSelected');
        bcProxyLoginPortlet.renderTemplate(
            $('#bcPLViewContainer'),
            'ConfigureView',
            bcplcv.globalizedText,
            initWidgets
        );
    }

    function initWidgets() {
       
    }
} (window.bcProxyLoginConfigureView = window.bcProxyLoginConfigureView || {}, jQuery));

///////////////////////////////////////////////////////////////////////////////
// bcProxyLoginLogsView
///////////////////////////////////////////////////////////////////////////////
(function (bcpllv, $) {

    // Public methods.
    bcpllv.init = function () {
        if (!bcpllv.globalizedText) {
            bcProxyLoginService.getGlobalizedText(
                'Logs',
                function (data) {
                    bcpllv.globalizedText = data.d;
                    renderView();
                }
            );
        }
        else {
            renderView();
        }
    };


    // Private methods.
    function renderView() {
        $('#bcPLTabBar ul').children().removeClass('tabSelected');
        $('#bcPLTabBar ul li[data-view="Logs"]').addClass('tabSelected');
        bcProxyLoginPortlet.renderTemplate(
            $('#bcPLViewContainer'),
            'LogsView',
            bcpllv.globalizedText,
            initWidgets
        );
    }

    function initWidgets() {
       
    }
} (window.bcProxyLoginLogsView = window.bcProxyLoginLogsView || {}, jQuery));


///////////////////////////////////////////////////////////////////////////////
// bcProxyLoginService
///////////////////////////////////////////////////////////////////////////////
(function(bcpls, $) {
    // Public members.
    // Can be overriden in document ready function to use different web service location.
    bcpls.webServiceRoot = 'Portlets/CUS/ICS/BCProxyLogin/services/';

    // Fill vary jsonObject with data to submit. 

    // Public methods.
    bcpls.getGlobalizedText = function(viewName, callback) {
        var dto = {};
        dto.viewName = viewName;
        ajaxUtil.callWebService(bcpls.webServiceRoot + 'Globalizer.asmx/GetGlobalizedText', dto, callback);
    };

    bcpls.findUser = function(username, callback) {
        var dto = {};
        dto.term = username;
        return ajaxUtil.callWebService(bcpls.webServiceRoot + 'UserSearch.asmx/FindUser', dto, callback);
    };

    bcpls.login = function(username, reason, password, callback) {
        var dto = { };
        dto.username = username;
        dto.password = password || "";
        dto.reason = reason;
        return ajaxUtil.callWebService(bcpls.webServiceRoot + 'Login.asmx/AttemptLogin', dto, callback);
    };

    bcpls.isAdmin = function(callback) {
        return ajaxUtil.callWebService(bcpls.webServiceRoot + 'Globalizer.asmx/IsAdmin', undefined, callback);
    };

}(window.bcProxyLoginService = window.bcProxyLoginService || {}, jQuery));
