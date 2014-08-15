var builds = angular.module('ads.builds');

/* page/route controller for the build index page */
builds.controller('BuildsController', function ($scope, BuildService, ApplicationService) {

    $scope.currentBuild = null;
    $scope.builds = [];
    $scope.applications = [];

    $scope.fetchingBuilds = false;
    $scope.savingBuild = false;
    $scope.alertState = 'none'; /* success, error, warning, info */
    $scope.errors = [];

    $scope.Init = function () {
        $scope.GetBuilds();
        $scope.GetApplications();
    };
    
    $scope.GetBuilds = function () {
        $scope.fetchingBuilds = true;
        BuildService.GetAll().then(function (builds) {
            $scope.builds = builds;
            $scope.fetchingBuilds = false;
        });
    };

    $scope.GetApplications = function () {
        ApplicationService.GetAll().then(function (applications) {
            $scope.applications = applications;
        });
    };

    $scope.Create = function () {

        var build = $scope.currentBuild;

        BuildService.Create(build).then(function (response) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.currentBuild = response;
            $scope.savingBuild = false;
            $scope.GetBuilds();
        }, function (response) {
            // error
            $scope.alertState = 'error';
            $scope.errors = response;
            $scope.savingBuild = false;
        });
    };

    $scope.Update = function () {

        var build = $scope.currentBuild;
        $scope.savingBuild = true;

        BuildService.Update(build).then(function (buildResponse) {
            BuildService.UpdateGroups(build.Id, build.Groups)
        }).then(function (groupResponse) {
            // success
            $scope.errors = [];
            $scope.alertState = 'success';
            $scope.savingBuild = false;
            $scope.GetBuilds();
        }, function (data) {
            // error
            $scope.alertState = 'error';
            $scope.errors = data;
            $scope.savingBuild = false;
        });
    };

    $scope.Delete = function (id) {

        BuildService.Delete(id).then($scope.GetBuilds)

        // close form if the deleted build was loaded
        if ($scope.currentBuild != null && $scope.currentBuild.Id == id) {
            $scope.CloseForm();
        }

    };

    $scope.OpenForm = function (build) {

        $scope.HideAlert();
        $scope.currentBuild = build || {
            Id: 0,
            EmailAddress: null
        };
    };

    $scope.CloseForm = function () {
        $scope.currentBuild = null;
    };

    $scope.ToggleGroup = function (group) {

        // if no groups in the array, then push this one into it
        if ($scope.currentBuild.Groups.length == 0) {
            $scope.currentBuild.Groups.push(group.Id);
            return;
        }

        // check the position of the group in the existing array
        var currentPosition = $scope.currentBuild.Groups.indexOf(group.Id);

        // if group is already in the array then remove it. Otherwise, add it now
        (currentPosition < 0) ?
            $scope.currentBuild.Groups.push(group.Id) :
            $scope.currentBuild.Groups.splice(currentPosition, 1);

        console.log($scope.currentBuild.Groups);
    }

    $scope.HideAlert = function () {
        $scope.alertState = 'none';
        $scope.alertMsg = null;
        $scope.errors = [];
    };

    $scope.isEditting = function () {
        return $scope.currentBuild != null;
    };

    $scope.Init();

});

