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

    var createNew = function () {

    };

    var updateSelectedRegion = function (region, event) {
        //get the currently active item and remove the active class
        $('#region-knockout-scope .active').toggleClass('active');
        $(event.target).toggleClass('active');

        app.RegionControl().SelectedRegion(region);
    };

    app.RegionControl().UpdateSelectedRegion = updateSelectedRegion;
    app.RegionControl().Configure = configure;
    app.RegionControl().CreateNew = createNew;
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

    var createNew = function () {

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
        $(event.target).toggleClass('active');
        
        app.ExhibitControl().SelectedExhibit(exhibit);
    };

    app.ExhibitControl().Display = display;
    app.ExhibitControl().UpdateSelectedExhibit = updateSelectedExhibit;
    app.ExhibitControl().Configure = configure;
    app.ExhibitControl().CreateNew = createNew;
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

    var createNew = function () {

    };

    var display = function (animal) {
        if (!app.RegionControl().SelectedRegion().AnimalRegionCode) {
            return false;
        } else {
            return animal.AnimalRegionCode() == app.RegionControl().SelectedRegion().AnimalRegionCode();
        }
    };

    var updateSelectedAnimal = function (animal, event) {
        //get the currently active item and remove the active class
        $('#animal-knockout-scope .active').toggleClass('active');
        $(event.target).toggleClass('active');
        app.AnimalControl().SelectedAnimal(animal);
    };

    app.AnimalControl().Display = display;
    app.AnimalControl().UpdateSelectedAnimal = updateSelectedAnimal;
    app.AnimalControl().Configure = configure;
    app.AnimalControl().CreateNew = createNew;
})(window.App);