(function (app) {
    var apiURL = "/api/animal";
    app.AnimalControl = ko.observable({});
    app.AnimalControl().SelectedAnimal = ko.observable({});
    
    var configure = function () {
        if (app.ExhibitControl) {
            app.ExhibitControl().SelectedExhibit.subscribe(function() {
                app.AnimalControl().SelectedAnimal({});
                $('#animal-knockout-scope .active').toggleClass('active');
            });
        }
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

    var addEnabled = function() {
        return app.ExhibitControl().SelectedExhibit().ExhibitID;
    };

    var createNew = function () {

    };
    
    var edit = function (animal, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };

    var display = function (animal) {
        if (!app.ExhibitControl().SelectedExhibit().ExhibitName) {
            return false;
        } else {
            return animal.CommonName() == app.ExhibitControl().SelectedExhibit().ExhibitName();
        }
    };

    var updateSelectedAnimal = function (animal, event) {
        //get the currently active item and remove the active class
        $('#animal-knockout-scope .active').toggleClass('active');
        $(event.target.parentElement).toggleClass('active');
        app.AnimalControl().SelectedAnimal(animal);
    };
    
    app.AnimalControl().Display = display;
    app.AnimalControl().UpdateSelectedAnimal = updateSelectedAnimal;
    app.AnimalControl().Configure = configure;
    app.AnimalControl().CreateNew = createNew;
    app.AnimalControl().Edit = edit;
    app.AnimalControl().AddEnabled = addEnabled;
})(window.App);