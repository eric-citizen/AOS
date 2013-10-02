(function (app) {
    var apiURL = "/api/animalregion";
    app.RegionControl = ko.observable({});
    app.RegionControl().SelectedRegion = ko.observable({});
    
    var configure = function () {
        
        load();
    };

    var load = function() {
        app.ApiHelper.GetAll(apiURL).done(function(data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.RegionControl().Regions = ko.mapping.fromJSON(data);
            ko.applyBindings(app.RegionControl, $('#region-knockout-scope')[0]);
        });
    };

    var createRegion = function() {

    };

    var updateSelectedRegion = function (region) {
        app.RegionControl().SelectedRegion(region);
    };

    app.RegionControl().UpdateSelectedRegion = updateSelectedRegion;
    app.RegionControl().Configure = configure;
})(window.App);

(function (app) {
    var apiURL = "/api/exhibit";
    app.ExhibitControl = ko.observable({});
    app.ExhibitControl().SelectedExhibit = ko.observable({});
    
    var configure = function () {

        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.ExhibitControl().Exhibits = ko.mapping.fromJSON(data);
            ko.applyBindings(app.ExhibitControl, $('#exhibit-knockout-scope')[0]);
        });
    };

    var createExhibit = function () {

    };
    
    var display = function (exhibit) {
        if (!app.RegionControl().SelectedRegion().AnimalRegionCode) {
            return false;
        } else {
            return exhibit.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
        }
    };
    
    var updateSelectedExhibit = function (exhibit) {
        app.ExhibitControl().SelectedExhibit(exhibit);
    };

    app.ExhibitControl().Display = display;
    app.ExhibitControl().UpdateSelectedExhibit = updateSelectedExhibit;
    app.ExhibitControl().Configure = configure;
})(window.App);

(function (app) {
    var apiURL = "/api/animal";
    app.AnimalControl = ko.observable({});
    app.AnimalControl().SelectedAnimal = ko.observable({});
    
    var configure = function () {

        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.AnimalControl().Animals = ko.mapping.fromJSON(data);
            ko.applyBindings(app.AnimalControl, $('#animal-knockout-scope')[0]);
        });
    };

    var createAnimal = function () {

    };

    var display = function (animal) {
        if (!app.RegionControl().SelectedRegion().AnimalRegionCode) {
            return false;
        } else {
            return animal.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
        }
    };

    var updateSelectedAnimal = function(animal) {

    };

    app.AnimalControl().Display = display;
    app.AnimalControl().UpdateSelectedAnimal = updateSelectedAnimal;
    app.AnimalControl().Configure = configure;
})(window.App);