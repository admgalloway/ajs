var authentication = angular.module('ads.authentication');

authentication.service('AuthenticationService', function ($http, $q) {

    $http.defaults.headers.common.Authorization = 'ttoken';

    var resourceUrl = 'api/auth/';

    var authenticationService = {};

    authenticationService.Authenticate = function (emailAddress, password) {

        if (emailAddress == null || password == null)
        {
            return $q.reject('dont take the piss. enter your credentials...')
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