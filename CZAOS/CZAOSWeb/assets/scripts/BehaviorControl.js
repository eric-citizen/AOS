(function(app) {
    var apiURL = "exhibitbehavior";
    

    var configure = function (id, controller, scope) {
        var bindingScope = scope || '#behavior-knockout-scope';
        var owner = controller || app;
        owner.BehaviorControl = ko.observable({});
        owner.BehaviorControl().SelectableBehaviors = ko.observableArray([]);
        owner.BehaviorControl().SelectedBehavior = ko.observable({});
        
        owner.BehaviorCategoryControl().SelectedCategory.subscribe(function() {
            updateSelectableBehaviors(owner);
        });

        return $.Deferred(function(dfd) {
            load(id, owner, bindingScope, dfd.resolve);
        });
    };

    var updateSelectableBehaviors = function (owner) {
        owner.BehaviorControl().SelectableBehaviors.removeAll();
        _.each(owner.BehaviorControl().Behaviors(), function(item) {
            if (item.BvrCatID() == owner.BehaviorCategoryControl().SelectedCategory().BvrCatID()) {
                owner.BehaviorControl().SelectableBehaviors.push(item);
            }
        });
    };

    var load = function(exhibitID, owner, bindingScope, callback) {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            owner.BehaviorControl().Behaviors = ko.mapping.fromJSON(data);

            if (bindingScope != 'false') {
                ko.applyBindings(owner.BehaviorControl, $(bindingScope)[0]);
                updateSelectableBehaviors(owner);
            }

            callback();
        });
    };

    app.ConfigureBehaviorControl = configure;
})(window.AOS);