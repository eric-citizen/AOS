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
            //sort the returned data into groups
            _.each(data, (function(item) {
                addRecordToGroup(item);
            }));

            ko.applyBindings(app.AnimalControl, $('#animalgroup-knockout-scope')[0]);
            //$('#animalgroup-knockout-scope').accordion({ header: "h2" });
        });
    };

    var addAllFromGroup = function (group) {
        var animalIds = _.map(group.Animals, function(animal) {
            return animal.ZooID;
        });
        var idsToAdd = _.difference(animalIds, app.AnimalControl().SelectedAnimals());
        _.each(idsToAdd, function(animalId) {
            app.AnimalControl().SelectedAnimals.push(animalId);
        });
    };

    var removeAllFromGroup = function (group) {
        var animalIds = _.map(group.Animals, function (animal) {
            return animal.ZooID;
        });
        app.AnimalControl().SelectedAnimals.removeAll(animalIds);
    };

    var addRecordToGroup = function (record) {
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

    var createNewGroup = function(record) {
        var group = {
            id: record.GroupID,
            Animals: []
        };
        group.Animals.push(record);
        app.AnimalControl().AnimalGroups().unshift(group);
    };

    app.AnimalControl().Configure = configure;
    app.AnimalControl().AddAllFromGroup = addAllFromGroup;
    app.AnimalControl().RemoveAllFromGroup = removeAllFromGroup;
})(window.AOS);