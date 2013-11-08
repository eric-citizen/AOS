(function (app) {
    var apiURL = "exhibitlocation";

    var configure = function (id, controller, scope) {
        var bindingScope = scope || '#location-knockout-scope';
        var owner = controller || app;
        owner.LocationControl = ko.observable({});
        owner.LocationControl().SelectedLocation = ko.observable({});

        return $.Deferred(function(dfd) {
            load(id, owner, bindingScope, dfd.resolve);
        });

    };

    var load = function (exhibitID, owner, bindingScope, callback) {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //get the locations that aren't masked for professionals
            data = _.filter(data, function (category) {
                return !category.MaskProf;
            });
            data = JSON.stringify(data);
            owner.LocationControl().SelectableLocations = ko.mapping.fromJSON(data);
            if (bindingScope != 'false') {
                ko.applyBindings(owner.LocationControl, $(bindingScope)[0]);
            }
            callback();
        });
    };

    app.ConfigureLocationControl = configure;
})(window.AOS);