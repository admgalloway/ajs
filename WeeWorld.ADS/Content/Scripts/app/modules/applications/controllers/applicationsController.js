var applications = angular.module('ads.applications');

/* page/route controller for the application index page */
applications.controller('ApplicationsController', function ($scope, $rootScope, ApplicationService, GroupService) {

    $scope.currentApplication = null;
    $scope.applications = [];
    $scope.groups = [];
    $scope.filterPlatform = "";

    $scope.fetchingApplications = false;
    $scope.savingApplication = false;
    $scope.alertState = 'none'; /* success, error, warning, info */
    $scope.errors = [];

    $scope.Init = function () {
        $scope.GetApplications();
        $scope.GetGroups();
    };
    
    $scope.GetApplications = function () {
        $scope.fetchingApplications = true;
        ApplicationService.GetAll().then(function (applications) {
            $scope.applications = applications;
            $scope.fetchingApplications = false;
        });
    };

    $scope.GetGroups = function () {
        GroupService.GetAll().then(function (groups) {
            $scope.groups = groups;
        });
    };

    $scope.Create = function () {

        var application = $scope.currentApplication;

        ApplicationService.Create(application).then(function (response) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.currentApplication = response;
            $scope.savingApplication = false;
            $scope.GetApplications();
        }, function (response) {
            // error
            $scope.alertState = 'error';
            $scope.errors = response;
            $scope.savingApplication = false;
        });
    };

    $scope.Update = function () {

        var application = $scope.currentApplication;
        $scope.savingApplication = true;

        ApplicationService.Update(application).then(function (applicationResponse) {
            ApplicationService.UpdateGroups(application.Id, application.Groups)
        }).then(function (groupResponse) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.savingApplication = false;
            $scope.GetApplications();
        }, function (data) {
            // error
            $scope.alertState = 'error';
            $scope.errors = data;
            $scope.savingApplication = false;
        });
    };

    $scope.Delete = function (id) {

        ApplicationService.Delete(id).then($scope.GetApplications)

        // close form if the deleted application was loaded
        if ($scope.currentApplication != null && $scope.currentApplication.Id == id) {
            $scope.CloseForm();
        }

    };

    $scope.OpenForm = function (application) {

        $scope.HideAlert();
        $scope.currentApplication = application || {
            Id: 0,
            EmailAddress: null,
            Platform: "iOS"
        };
    };

    $scope.CloseForm = function () {
        $scope.currentApplication = null;
    };

    $scope.ToggleGroup = function (group) {

        // if no groups in the array, then push this one into it
        if ($scope.currentApplication.Groups.length == 0) {
            $scope.currentApplication.Groups.push(group.Id);
            return;
        }

        // check the position of the group in the existing array
        var currentPosition = $scope.currentApplication.Groups.indexOf(group.Id);

        // if group is already in the array then remove it. Otherwise, add it now
        (currentPosition < 0) ?
            $scope.currentApplication.Groups.push(group.Id) :
            $scope.currentApplication.Groups.splice(currentPosition, 1);

        console.log($scope.currentApplication.Groups);
    }

    $scope.HideAlert = function () {
        $scope.alertState = 'none';
        $scope.alertMsg = null;
        $scope.errors = [];
    };

    $scope.isEditting = function () {
        return $scope.currentApplication != null;
    };

    $scope.Init();

});

