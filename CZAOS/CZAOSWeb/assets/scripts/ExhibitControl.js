(function (app) {
    var apiURL = "/api/exhibit";
    app.ExhibitControl = ko.observable({});
    app.ExhibitControl().SelectedExhibit = ko.observable({});
    app.ExhibitControl().SelectableExhibits = ko.observableArray([]);

    var configure = function () {
        if (app.RegionControl) {
            app.RegionControl().SelectedRegion.subscribe(function () {
                if (app.ExhibitControl().Exhibits) {
                    app.ExhibitControl().SelectedExhibit({});
                    $('#exhibit-knockout-scope .active').toggleClass('active');

                    updateSelectableExhibits();

                    //create list of exhibits to populate select liste
                    app.ExhibitControl().SelectableExhibits(ko.utils.arrayFilter(app.ExhibitControl().Exhibits(), function (exhibit) {
                        return exhibit.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
                    }));
                }
            });

        }

        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.ExhibitControl().Exhibits = ko.mapping.fromJSON(data);
            ko.applyBindings(app.ExhibitControl, $('#exhibit-knockout-scope')[0]);
            updateSelectableExhibits();
        });
    };

    var updateSelectableExhibits = function () {
        if (app.RegionControl().SelectedRegion().AnimalRegionCode && app.ExhibitControl().Exhibits) {
            //create list of exhibits to populate select list
            app.ExhibitControl().SelectableExhibits(ko.utils.arrayFilter(app.ExhibitControl().Exhibits(), function (exhibit) {
                return exhibit.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
            }));
        }
    };

    var addEnabled = function () {
        return app.RegionControl().SelectedRegion().AnimalRegionCode;
    };

    var createNew = function () {

    };

    var edit = function (exhibit, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };

    var display = function (exhibit) {
        if (!app.RegionControl().SelectedRegion().AnimalRegionCode) {
            return false;
        } else {
            return exhibit.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
        }
    };

    var updateSelectedExhibit = function (exhibit, event) {
        //get the currently active item and remove the active class
        $('#exhibit-knockout-scope .active').toggleClass('active');
        $(event.target.parentElement).toggleClass('active');

        app.ExhibitControl().SelectedExhibit(exhibit);
    };

    app.ExhibitControl().Display = display;
    app.ExhibitControl().UpdateSelectedExhibit = updateSelectedExhibit;
    app.ExhibitControl().Configure = configure;
    app.ExhibitControl().CreateNew = createNew;
    app.ExhibitControl().Edit = edit;
    app.ExhibitControl().AddEnabled = addEnabled;
})(window.App);