(function (app) {
    var apiURL = "/api/exhibitbehavior";
    app.BehaviorControl = ko.observable({});
    app.BehaviorControl().SelectedBehavior = ko.observable({});

    var configure = function () {
        if (app.ExhibitControl && app.BehaviorCategoryControl) {
            app.ExhibitControl().SelectedExhibit.subscribe(function () {
                app.BehaviorControl().SelectedBehavior({});
                $('#behavior-knockout-scope .active').toggleClass('active');
            });
            app.BehaviorCategoryControl().SelectedBehaviorCategory.subscribe(function() {
                app.BehaviorControl().SelectedBehavior({});
                $('#behavior-knockout-scope .active').toggleClass('active');
            });
        }
        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.BehaviorControl().Behaviors = ko.mapping.fromJSON(data);
            ko.applyBindings(app.BehaviorControl, $('#behavior-knockout-scope')[0]);
        });
    };

    var addEnabled = function () {
        return (app.ExhibitControl().SelectedExhibit().ExhibitID && app.BehaviorCategoryControl().SelectedBehaviorCategory().BvrCatID);
    };

    var createNew = function () {

    };

    var edit = function (behavior, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };

    var display = function (behavior) {
        if (!app.ExhibitControl().SelectedExhibit().ExhibitID || !app.BehaviorCategoryControl().SelectedBehaviorCategory().BvrCatID) {
            return false;
        } else {
            return (behavior.ExhibitID() == app.ExhibitControl().SelectedExhibit().ExhibitName()
                && behavior.BvrCatID() == app.BehaviorCategoryControl().SelectedBehaviorCategory().BvrCatID());
        }
    };

    var updateSelectedBehavior = function (behavior, event) {
        //get the currently active item and remove the active class
        $('#behavior-knockout-scope .active').toggleClass('active');
        $(event.target.parentElement).toggleClass('active');
        app.BehaviorControl().SelectedBehavior(behavior);
    };

    app.BehaviorControl().Display = display;
    app.BehaviorControl().UpdateSelectedBehavior = updateSelectedBehavior;
    app.BehaviorControl().Configure = configure;
    app.BehaviorControl().CreateNew = createNew;
    app.BehaviorControl().Edit = edit;
    app.BehaviorControl().AddEnabled = addEnabled;
})(window.App);