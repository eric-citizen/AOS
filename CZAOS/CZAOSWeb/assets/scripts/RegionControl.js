(function (app) {
    var apiURL = "/api/animalregion";
    app.RegionControl = ko.observable({});
    app.RegionControl().SelectedRegion = ko.observable({});

    var configure = function () {

        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.RegionControl().Regions = ko.mapping.fromJSON(data);
            ko.applyBindings(app.RegionControl, $('#region-knockout-scope')[0]);
        });
    };

    var createNew = function () {

    };

    var edit = function (region, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };
    var updateSelectedRegion = function (region, event) {
        //get the currently active item and remove the active class
        $('#region-knockout-scope .active').toggleClass('active');
        $(event.target.parentElement).toggleClass('active');

        app.RegionControl().SelectedRegion(region);
    };

    app.RegionControl().UpdateSelectedRegion = updateSelectedRegion;
    app.RegionControl().Configure = configure;
    app.RegionControl().CreateNew = createNew;
    app.RegionControl().Edit = edit;
})(window.App);
