var users = angular.module('ads.users');

/* page/route controller for the user index page */
users.controller('UsersController', function ($scope, $rootScope, UserService, GroupService)  {

    $scope.currentUser = null;
    $scope.users = [];
    $scope.groups = [];

    $scope.fetchingUsers = false;
    $scope.savingUser = false;
    $scope.alertState = 'none'; /* success, error, warning, info */
    $scope.errors = [];

    $scope.Init = function () {
        $scope.GetUsers();
        $scope.GetGroups();
    };
    
    $scope.GetUsers = function () {
        $scope.fetchingUsers = true;
        UserService.GetAll().then(function (users) {
            $scope.users = users;
            $scope.fetchingUsers = false;
        });
    };

    $scope.GetGroups = function () {
        GroupService.GetAll().then(function (groups) {
            $scope.groups = groups;
        });
    };

    $scope.Create = function () {

        var user = $scope.currentUser;

        UserService.Create(user).then(function (response) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.currentUser = response;
            $scope.savingUser = false;
            $scope.GetUsers();
        }, function (response) {
            // error
            $scope.alertState = 'error';
            $scope.errors = response;
            $scope.savingUser = false;
        });
    };

    $scope.Update = function () {

        var user = $scope.currentUser;
        $scope.savingUser = true;

        UserService.Update(user).then(function (userResponse) {
            UserService.UpdateGroups(user.Id, user.Groups)
        }).then(function (groupResponse) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.savingUser = false;
            $scope.GetUsers();
        }, function (data) {
            // error
            $scope.alertState = 'error';
            $scope.errors = data;
            $scope.savingUser = false;
        });
    };

    $scope.Delete = function (id) {

        UserService.Delete(id).then($scope.GetUsers)

        // close form if the deleted user was loaded
        if ($scope.currentUser != null && $scope.currentUser.Id == id) {
            $scope.CloseForm();
        }

    };

    $scope.OpenForm = function (user) {

        $scope.HideAlert();
        $scope.currentUser = user || {
            Id: 0,
            EmailAddress: null
        };
    };

    $scope.CloseForm = function () {
        $scope.currentUser = null;
    };

    $scope.ToggleGroup = function (group) {

        // if no groups in the array, then push this one into it
        if ($scope.currentUser.Groups.length == 0) {
            $scope.currentUser.Groups.push(group.Id);
            return;
        }

        // check the position of the group in the existing array
        var currentPosition = $scope.currentUser.Groups.indexOf(group.Id);

        // if group is already in the array then remove it. Otherwise, add it now
        (currentPosition < 0) ?
            $scope.currentUser.Groups.push(group.Id) :
            $scope.currentUser.Groups.splice(currentPosition, 1);

        console.log($scope.currentUser.Groups);
    }

    $scope.HideAlert = function () {
        $scope.alertState = 'none';
        $scope.alertMsg = null;
        $scope.errors = [];
    };

    $scope.isEditting = function () {
        return $scope.currentUser != null;
    };

    $scope.Init();

});

