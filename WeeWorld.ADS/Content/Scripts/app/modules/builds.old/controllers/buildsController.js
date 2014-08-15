var builds = angular.module('ads.builds');

builds.controller('BuildsController', function ($scope, $rootScope, BuildService, ApplicationService) {

    // call init method to setup data/properties
    $scope.Init();

    $scope.obj = null;
    $scope.builds;
    $scope.loaded = false;

    $scope.Init = function () {

        $scope.builds = BuildService.GetAll();
        $scope.build = $scope.builds[0];
        $scope.editting = false;
        $scope.loaded = true;
    };

    $scope.Load = function (obj) {
        $scope.loadingForm = true;
        $scope.errors = [];
        $scope.showAlert = false;
        $scope.loadingForm = false;

        if ($scope.editting && $scope.obj.id == obj.id) {
            $scope.editting = false;
        }
        else {
            $scope.editting = true;
        }
        $scope.obj = obj;
    };

    $scope.Close = function () {
        $scope.loadingForm = true;
        $scope.errors = [];
        $scope.showAlert = false;
        $scope.obj = null;
        $scope.loadingForm = false;
        $scope.editting = false;
    };

    // call init method to setup data/properties
    $scope.Init();

});
