(function(app) {
    var apiURL = "animalobservation";
    app.AnimalControl = ko.observable({});
    app.AnimalControl().AnimalGroups = ko.observableArray([]);
    app.AnimalControl().SelectedAnimals = ko.observableArray([]);

    var configure = function(id) {
        load(id);
    };

    var load = function(id) {
        //get all of the animal observations for the observation
        app.get(apiURL, { observationId: id }).done(function(data) {
            setUpGroups(data);
            $(document).ajaxStop(function() {
                 ko.applyBindings(window.AOS.AnimalControl, $('#animalgroup-knockout-scope')[0])
            });
            //$('#animalgroup-knockout-scope').accordion({ header: "h2" });
        });
    };

    var addAllFromGroup = function(group) {
        var animalIds = _.map(group.Animals, function(animal) {
            return animal.ZooID;
        });
        var idsToAdd = _.difference(animalIds, app.AnimalControl().SelectedAnimals());
        _.each(idsToAdd, function(animalId) {
            app.AnimalControl().SelectedAnimals.push(animalId);
        });
    };

    var removeAllFromGroup = function(group) {
        var animalIds = _.map(group.Animals, function(animal) {
            return animal.ZooID;
        });
        app.AnimalControl().SelectedAnimals.removeAll(animalIds);
    };

    var addRecordToGroup = function(record) {
        //if no groups yet, then create one
        if (app.AnimalControl().AnimalGroups().length == 0) {
            createNewGroup(record);
        } else {
            //check to see if the group already exists
            if (!_.some(app.AnimalControl().AnimalGroups(), function(group) {
                return group.id == record.GroupID;
            })) {
                //if no, create the group and add the record to it
                createNewGroup(record);
            } else {
                //if yes, add the record to the group
                _.find(app.AnimalControl().AnimalGroups(), function(group) {
                    return group.id == record.GroupID;
                }).Animals.push(record);
            }
        }
    };

    //sorts data into groups
    var setUpGroups = function (data) {
        for (var i in data) {
            addRecordToGroup(data[i]);
        }
            //_.each(data, (function(item) {
            //    addRecordToGroup(item);
            //}));
        app.AnimalControl().AnimalGroups(_.sortBy(app.AnimalControl().AnimalGroups(), function(group) {
            return group.id;
        }));
    };

    var createNewGroup = function(record) {
        var group = {
            id: record.GroupID,
            Animals: []
        };
        group.Animals.push(record);

        //create behavior and location controls for group
        app.ConfigureLocationControl(exhibitID, group, 'false').done(function () {
            app.ConfigureBehaviorCategoryControl(exhibitID, group, 'false').done(function () {
                app.ConfigureBehaviorControl(exhibitID, group, 'false').done(function () {
                    app.AnimalControl().AnimalGroups().unshift(group);
                });
            });
        });
        


        //window.AOS.ConfigureLocationControl(exhibitID, _.find(app.AnimalControl().AnimalGroups(), function(item) {
        //    return item.id == record.GroupID;
        //}), '#location-control-' + record.GroupID);
    };

    app.AnimalControl().Configure = configure;
    app.AnimalControl().AddAllFromGroup = addAllFromGroup;
    app.AnimalControl().RemoveAllFromGroup = removeAllFromGroup;
})(window.AOS);