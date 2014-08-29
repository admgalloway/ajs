var groups = angular.module('ads.groups');

/* page/route controller for the group index page */
groups.controller('GroupsController', function ($scope, $rootScope, GroupService, UserService, ApplicationService) {

    $scope.currentGroup = null;
    $scope.groups = [];
    $scope.users = [];
    $scope.applications = [];

    $scope.fetchingGroups = false;
    $scope.savingGroup = false;
    $scope.alertState = 'none'; /* success, error, warning, info */
    $scope.errors = [];

    $scope.Init = function () {
        $scope.GetGroups();
        $scope.GetUsers();
        $scope.GetApplications();
    };
    
    $scope.GetGroups = function () {
        $scope.fetchingGroups = true;
        GroupService.GetAll().then(function (groups) {
            $scope.groups = groups;
            $scope.fetchingGroups = false;
        });
    };

    $scope.GetUsers = function () {
        UserService.GetAll().then(function (users) {
            $scope.users = users;
        });
    };

    $scope.GetApplications = function () {
        ApplicationService.GetAll().then(function (applications) {
            $scope.applications = applications;
        });
    };

    $scope.Create = function () {

        var group = $scope.currentGroup;

        GroupService.Create(group).then(function (response) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.currentGroup = response;
            $scope.savingGroup= false;
            $scope.GetGroups();
        }, function (response) {
            // error
            $scope.alertState = 'error';
            $scope.errors = response;
            $scope.savingGroup = false;

        });
    };

    $scope.Update = function () {

        var group = $scope.currentGroup;
        $scope.savingGroup = true;

        GroupService.Update(group).then(function (groupResponse) {
            GroupService.UpdateUsers(group.Id, group.Users)
        }).then(function (userResponse) {
            GroupService.UpdateApplications(group.Id, group.Applications)
        }).then(function (applicationResponse) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.savingGroup = false;
            $scope.GetGroups();
        }, function (data) {
            // error
            $scope.alertState = 'error';
            $scope.errors = data;
            $scope.savingGroup = false;
        });
    };

    $scope.Delete = function (id) {

        GroupService.Delete(id).then($scope.GetGroups)
        
        // close form if the deleted group was loaded
        if ($scope.currentGroup != null && $scope.currentGroup.Id == id)
        {
            $scope.CloseForm();
        }

    };

    $scope.OpenForm = function (group) {

        $scope.HideAlert();
        $scope.currentGroup = group || {
            Id: 0,
            Name: null,
            AlertStatus: 0 /* none: opt-in */
        };
    };

    $scope.CloseForm = function () {
        $scope.currentGroup = null;
    };

    $scope.ToggleUser = function (userId) {

        $scope.ToggleCollection($scope.currentGroup.Users, userId);
    }

    $scope.ToggleApplication = function (applicationId) {

        $scope.ToggleCollection($scope.currentGroup.Applications, applicationId);
    };


    $scope.ToggleCollection = function (collection, id) {

        if (collection == null || id == null) {
            // collection or id is null, nothing we can do
            return;
        }
        else if (collection.length == 0) {
            // collection is empty, add the id
            collection.push(id);
        }
        else {
            // add / remove from collection depending on its existence
            var index = collection.indexOf(id);

            (index < 0) ?
                collection.push(id) :
                collection.splice(index, 1);
        }

        console.log(collection);
    };

    $scope.HideAlert = function () {
        $scope.alertState = 'none';
        $scope.alertMsg = null;
        $scope.errors = [];
    };

    $scope.isEditting = function () {
        return $scope.currentGroup != null;
    };

    $scope.Init();

});

