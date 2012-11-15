(function (bcpl, $) {
    'use strict';

    // Logs Collection
    
    var logList = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: bcpl.models.Logs

    });

    // Create our global collection of **Logs**
    bcpl.Logs = new logList();
  
} (window.bcProxyLoginPortlet = window.bcProxyLoginPortlet || {}, jQuery));
