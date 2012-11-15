(function (bcpl, $) {
    'use strict';

    // The Application
    // ---------------
    _.templateSettings = { interpolate: /\{\{=(.*)\}\}/g, evaluate: /\{\{(.*)\}\}/g, escape: /\{\{-(.*)\}\}/g, variable: "data" };
    // Our overall **AppView** is the top-level piece of UI.
    //var bcProxyLogin = {};
    bcpl.AppView = Backbone.View.extend({


        // Instead of generating a new element, bind to the existing skeleton of
        // the App already present in the HTML.
        //el: '#bcPLViewContainer',

        //Pre-compile all the templates used in the portlet
        defaultTemplate: window.TemplateCache.get('#bc-pl-default-view-template'),

        logsTemplate: window.TemplateCache.get('#bc-pl-logs-view-template'),

        configureTemplate: window.TemplateCache.get('#bc-pl-configure-view-template'),

        tabBarTemplate: window.TemplateCache.get('#bc-pl-tab-header-template'),


        //Create a service broker that will be used for the globalization fetcher
        globalizationBroker: new BackboneServiceBroker(),

        //Setup event bindings
        events: {
            'click #bcPLTabBar ul li': 'switchTabs'
        },

        //Initialize views.  
        initialize: function () {
            var view = this;
            this.globalizationBroker.init(this.options.baseServicePath, "Globalizer.asmx", this.options.portletId);
            this.globalizationBroker.ajax("GetGlobalizedText", {}, function (globalizationValues) {
                view.globalizationData = globalizationValues;
                view.$main = $('#bcPLViewContainer');
                view.$tabBar = $('#bcPLTabBarContainer');
                view.render();
            });
        },

        render: function () {
            this.renderTabBar();
            this.renderMain();
        },

        renderMain: function () {

            switch (this.$tabBar.find('ul li.tabSelected').first().data('view')) {
                case "Logs":
                    this.renderLogs();
                    break;
                case "Configure":
                    this.renderConfigure();
                    break;
                case "Default":
                default:
                    this.renderDefault();
                    break;

            }
        },

        renderDefault: function () {
            this.$main.html(this.defaultTemplate(this.globalizationData )).show();
        },

        renderLogs: function () {
            this.$main.html(this.logsTemplate(this.globalizationData )).show();
        },

        renderConfigure: function () {
            this.$main.html(this.configureTemplate(this.globalizationData )).show();
        },

        renderTabBar: function () {
            this.$tabBar.html(this.tabBarTemplate()).show();
        },

        switchTabs: function (ev) {
            var tab = $(ev.currentTarget).data('view');
            this.$tabBar.find('ul').children().removeClass('tabSelected');
            this.$tabBar.find('ul li[data-view="' + tab + '"]').addClass('tabSelected');
            this.renderMain();
        }
    });
} (window.bcProxyLoginPortlet = window.bcProxyLoginPortlet || {}, jQuery));
