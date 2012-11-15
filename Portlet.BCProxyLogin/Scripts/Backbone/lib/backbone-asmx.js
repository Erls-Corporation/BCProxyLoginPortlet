// A simple module to replace `Backbone.sync` with *localStorage*-based
// persistence. Models are given GUIDS, and saved into a JSON object. Simple
// as that.

// Generate four random hex digits.
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
};

// Generate a pseudo-GUID by concatenating random hexadecimal.
function guid() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
};

function endsWith(str, suffix) {
    return str.indexOf(suffix, str.length - suffix.length) !== -1;
}

function BackboneServiceBroker() {
    this.init = function(url, service, portletId) {
        this.url = url;
        this.service = service;
        this.portletId = portletId;
    };

    this.ajax = function(action, data, callback) {
        data.portletId = this.portletId;
        jQuery.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: this.url + '/' + this.service + (endsWith(this.service, 'asmx') ? ('/' + action) : ''),
            data: JSON.stringify(data),
            success: function(returnedData) {
                if (callback && typeof(callback) === "function") {
                    callback(returnedData.d);
                }
            }
        });
    };
}

(function(store, $) {
    store.init = function(serviceBroker) {
        this.serviceBroker = serviceBroker;
        return this;
    };

    this.find = function(model) {
        this.serviceBroker.ajax('Read', model, function(data) { model.parse(data); });
    };

    this.create = function(model) {
        this.serviceBroker.ajax('Create', model, function(data) { model.parse(data); });
    };

    this.destroy = function(model) {

    };

}(window.BackboneAsmx = window.BackboneAsmx || { }, jQuery));

// Override `Backbone.sync` to use delegate to the model or collection's
// *localStorage* property, which should be an instance of `Store`.
Backbone.sync = function (method, model, options) {
    console.log(method);
    console.log(model);
    console.log(options);
    return;

    var resp;
    var store = model.store;

    switch (method) {
        case "read": resp = store.read(model) ; break;
        case "create": resp = store.create(model); break;
        case "update": resp = store.update(model); break;
        case "delete": resp = store.destroy(model); break;
    }

    if (resp) {
        options.success(resp);
    } else {
        options.error("Record not found");
    }
};
