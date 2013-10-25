(function (app) {
    var apiURL = "behaviorcategory",
        exhibitID = 0;
    app.BehaviorCategoryControl = ko.observable({});
    app.BehaviorCategoryControl().SelectedCategory = ko.observable({});

    var configure = function (id) {
        exhibitID = id;

        return $.Deferred(function(dfd) {
            load(dfd.resolve);
        });

    };

    var load = function (callback) {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //get the categories that aren't masked for professionals
            data = _.filter(data, function(category) {
                return !category.MaskProf;
            });
            data = JSON.stringify(data);
            app.BehaviorCategoryControl().SelectableCategories = ko.mapping.fromJSON(data);
            ko.applyBindings(app.BehaviorCategoryControl, $('#behavior-category-knockout-scope')[0]);
            app.BehaviorControl().Configure(exhibitID);
            callback();
        });

    };

    app.BehaviorCategoryControl().Configure = configure;
})(window.AOS);