(function (app) {
    var apiURL = "exhibitlocation",
        exhibitID = 0;
    app.LocationControl = ko.observable({});
    app.LocationControl().SelectedLocation = ko.observable({});

    var configure = function (id) {
        exhibitID = id;
        load();
    };

    var load = function () {
        app.get(apiURL, { exhibitId: exhibitID }).done(function (data) {
            //get the locations that aren't masked for professionals
            data = _.filter(data, function (category) {
                return !category.MaskProf;
            });
            data = JSON.stringify(data);
            app.LocationControl().SelectableLocations = ko.mapping.fromJSON(data);
            ko.applyBindings(app.LocationControl, $('#location-knockout-scope')[0]);
        });
    };

    app.LocationControl().Configure = configure;
})(window.AOS);