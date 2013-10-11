(function (app) {
    var apiURL = "/api/exhibitlocation";
    app.LocationControl = ko.observable({});

    var configure = function () {
        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.LocationControl().Locations = ko.mapping.fromJSON(data);
            ko.applyBindings(app.LocationControl, $('#location-knockout-scope')[0]);
        });
    };
    
    var createNew = function () {

    };

    var edit = function (location, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };

    var display = function (location) {
        //check to make sure SelectedExhibit exists, then check to make sure it has an ExhibitID
        if (app.ExhibitControl().SelectedExhibit && app.ExhibitControl().SelectedExhibit() && app.ExhibitControl().SelectedExhibit().ExhibitID) {
            return location.ExhibitID() == app.ExhibitControl().SelectedExhibit().ExhibitID();
        } else {
            return false;
        }
    };

    app.LocationControl().Display = display;
    app.LocationControl().Configure = configure;
    app.LocationControl().CreateNew = createNew;
    app.LocationControl().Edit = edit;
})(window.App);