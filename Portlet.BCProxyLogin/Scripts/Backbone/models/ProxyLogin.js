(function (bcpl, $) {
    'use strict';

    // Model
    // ----------

    bcpl.models = bcpl.models || { };
    
    bcpl.models.Logs = Backbone.Model.extend({
        defaults: {
            SourceUser: '',
            TargetUser: '',
            Action:'',
            DateTime:''
        }
    });

} (window.bcProxyLoginPortlet = window.bcProxyLoginPortlet || {}, jQuery));
