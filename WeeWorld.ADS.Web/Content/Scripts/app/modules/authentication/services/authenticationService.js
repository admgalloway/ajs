var authentication = angular.module('ads.authentication');

authentication.service('AuthenticationService', function ($http, $q) {

    var resourceUrl = 'api/auth/';

    var authenticationService = {};

    authenticationService.Authenticate = function (emailAddress, password) {

        if (emailAddress == null || password == null)
        {
            return $q.reject('email and password are required')
        }

        if (emailAddress.indexOf('@') < 0)
        {
            emailAddress += "@weeworld.com";
        }

        var credentials = {
            emailAddress: emailAddress,
            password: password
        };

        return $http.post(resourceUrl, credentials)
            .then(authenticationService.onSuccess, authenticationService.onError);
    };

    authenticationService.onSuccess = function (response) {

        // just unwrap content from response
        return response.data;
    }

    authenticationService.onError = function (response) {

        // log error, send along error msg
        if (true) {
            console.log(response);
        }

        return $q.reject(response.data);
    }

    return authenticationService;
});