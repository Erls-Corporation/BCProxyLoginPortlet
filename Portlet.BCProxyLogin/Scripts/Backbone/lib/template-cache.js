(function (cache, $) {
    cache.get = function (selector) {
        if (!this.templates) {
            this.templates = {};
        }

        var template = this.templates[selector];
        if (!template) {
            var tmpl = $(selector).text();
            template = _.template(tmpl);
            this.templates[selector] = template;
        }

        return template;
    };
} (window.TemplateCache = window.TemplateCache || {}, jQuery));