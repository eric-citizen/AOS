(function(app) {
    var apiURL = "exhibitbehavior",
        exhibitID = 0;
    app.BehaviorControl = ko.observable({});
    app.BehaviorControl().SelectableBehaviors = ko.observableArray([]);
    app.BehaviorControl().SelectedBehavior = ko.observable({});

    var configure = function (id) {
        exhibitID = id;
        
        app.BehaviorCategoryControl().SelectedCategory.subscribe(function() {
            updateSelectableBehaviors();
        });
        
        load();
    };

    var updateSelectableBehaviors = function () {
        app.BehaviorControl().SelectableBehaviors.removeAll();
        _.each(app.BehaviorControl().Behaviors(), function(item) {
            if (item.BvrCatID() == app.BehaviorCategoryControl().SelectedCategory().BvrCatID()) {
                app.BehaviorControl().SelectableBehaviors.push(item);
            }
        });
    };

    var load = function() {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.BehaviorControl().Behaviors = ko.mapping.fromJSON(data);
            ko.applyBindings(app.BehaviorControl, $('#behavior-knockout-scope')[0]);
            updateSelectableBehaviors();
        });
    };

    app.BehaviorControl().Configure = configure;
})(window.AOS);