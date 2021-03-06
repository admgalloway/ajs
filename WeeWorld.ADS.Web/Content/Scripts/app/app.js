var ads = angular.module('ads', ['ngRoute', 'ngAnimate', 'ads.authentication', 'ads.users', 'ads.applications', 'ads.groups', 'ads.builds']);

// configure our routes
ads.config(function ($routeProvider) {

    var baseUrl = 'content/scripts/app/modules/';
    $routeProvider
        .when('/users', {
            templateUrl: baseUrl + 'users/views/index.html',
            controller: 'UsersController'
        })
        .when('/groups', {
            templateUrl: baseUrl + 'groups/views/index.html',
            controller: 'GroupsController'
        })
        .when('/applications', {
            templateUrl: baseUrl + 'applications/views/index.html',
            controller: 'ApplicationsController'
        })
        .when('/builds', {
            templateUrl: baseUrl + 'builds/views/index.html',
            controller: 'BuildsController'
        })
        //.when('/login', {
        //    templateUrl: baseUrl + 'authentication/views/login.html',
        //    controller: 'LoginController'
        //})
        .otherwise({ redirectTo: '/users' });
});

ads.controller('appController', function ($scope, SessionService) {

    $scope.loggedIn = false;

    $scope.Logout = function () {
        $scope.$emit('logout');
    };

    /* event listeners */
    $scope.$on('login', function (event, token) {

        SessionService.setToken(token);
        $scope.loggedIn = true;
    });

    $scope.$on('logout', function (event) {

        SessionService.destroyToken();
        $scope.loggedIn = false;
    });


});