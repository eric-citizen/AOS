(function (app) {
    var apiURL = "/api/behaviorcategory";
    app.BehaviorCategoryControl = ko.observable({});
    app.BehaviorCategoryControl().SelectedBehaviorCategory = ko.observable({});
    var test = "";

    var configure = function () {
        
        load();
    };

    var load = function () {
        app.ApiHelper.GetAll(apiURL).done(function (data) {
            //need to stringify data before ko.mapping will work
            data = JSON.stringify(data);
            app.BehaviorCategoryControl().Categories = ko.mapping.fromJSON(data);
            ko.applyBindings(app.BehaviorCategoryControl, $('#behavior-category-knockout-scope')[0]);
        });

        $.ajax({
            url: window.App.baseURL + apiURL,
            beforeSend: function (request) {
                request.setRequestHeader("exhibitId", 420);
            },
            cache: false,
            type: 'GET',
            contentType: 'application/json; charaset=urt-8'
        }).done(function(data) {
            test = data;
        });
    };

    var addEnabled = function () {
        return app.ExhibitControl().SelectedExhibit().ExhibitID;
    };

    var createNew = function () {

    };

    var edit = function (behaviorCategory, event) {
        event.preventDefault();
        CZAOSUIDialogs.ShowDialogFromArgs($(event.target));
    };
    
    var updateSelectedBehaviorCategory = function (behaviorCategory, event) {
        //get the currently active item and remove the active class
        $('#behavior-category-knockout-scope .active').toggleClass('active');
        $(event.target.parentElement).toggleClass('active');
        app.BehaviorCategoryControl().SelectedBehaviorCategory(behaviorCategory);
    };

    app.BehaviorCategoryControl().UpdateSelectedBehaviorCategory = updateSelectedBehaviorCategory;
    app.BehaviorCategoryControl().Configure = configure;
    app.BehaviorCategoryControl().CreateNew = createNew;
    app.BehaviorCategoryControl().Edit = edit;
    app.BehaviorCategoryControl().AddEnabled = addEnabled;
})(window.App);