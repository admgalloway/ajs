var ads = angular.module('ads');

ads.factory('SessionService', function ($window) {

    $sessionStorage = $window.sessionStorage;

    var Session = {

        setToken: function (token) {

            $sessionStorage.setItem('token', JSON.stringify(token));
        },
        destroyToken: function () {

            $sessionStorage.setItem('token', null);
        },
        getToken: function () {

            // check if token is null. check if token has expired. 
            var token = JSON.parse($sessionStorage.getItem('token'));

            return token;
        }
    };

    return Session;
});