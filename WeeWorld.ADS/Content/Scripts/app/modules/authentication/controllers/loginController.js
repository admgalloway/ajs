var authentication = angular.module('ads.authentication');

authentication.controller('LoginController', function ($scope, AuthenticationService) {

    $scope.credentials = {
        emailAddress: null,
        password: null
    };

    $scope.authenticating = false;
    $scope.error;

    $scope.Init = function () {
    };
    

    $scope.Login = function () {

        console.log('logging in');
        $scope.authenticating = true;

        AuthenticationService.Authenticate($scope.credentials.emailAddress, $scope.credentials.password).then(function (token) {
            // fire event to be caught in main controller, set token, user details etc
            $scope.authenticating = true;
            $scope.error = null;

            $scope.$emit('login', token);

        }, function (error) {
            $scope.authenticating = false;
            $scope.error = error;
        });
    };




});

