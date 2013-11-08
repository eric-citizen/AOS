(function (app) {
    var apiURL = "behaviorcategory";
    

    var configure = function (id, controller, scope) {
        var owner = controller || app;
        var bindingScope = scope || '#behavior-category-knockout-scope';
        owner.BehaviorCategoryControl = ko.observable({});
        owner.BehaviorCategoryControl().SelectedCategory = ko.observable({});
        
        return $.Deferred(function(dfd) {
            load(id, owner, bindingScope, dfd.resolve);
        });

    };

    var load = function (exhibitID, owner, bindingScope, callback) {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //get the categories that aren't masked for professionals
            data = _.filter(data, function(category) {
                return !category.MaskProf;
            });
            data = JSON.stringify(data);
            owner.BehaviorCategoryControl().SelectableCategories = ko.mapping.fromJSON(data);

            if (bindingScope != 'false') {
                ko.applyBindings(app.BehaviorCategoryControl, $(bindingScope)[0]);
            }

            callback();

            //app.ConfigureBehaviorControl(exhibitID, owner, 'false').done(function() {
            //    callback();
            //});
        });

    };

    app.ConfigureBehaviorCategoryControl = configure;
})(window.AOS);