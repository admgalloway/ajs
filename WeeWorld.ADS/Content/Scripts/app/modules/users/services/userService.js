var users = angular.module('ads.users');

users.service('UserService', function ($http, $q) {

    var resourceUrl = 'api/users/';

    var userService = {};

    userService.GetAll = function () {

        return $http.get(resourceUrl)
            .then(userService.onSuccess, userService.onError);
    };

    userService.Get = function (id) {

        return $http.get(resourceUrl + id)
            .then(userService.onSuccess, userService.onError);
    };

    userService.Create = function (user) {

        return $http.post(resourceUrl, user)
            .then(userService.onSuccess, userService.onError);
    };

    userService.Update = function (user) {
        
        return $http.put(resourceUrl + user.Id, user)
            .then(userService.onSuccess, userService.onError);
    };

    userService.Delete = function (id) {

        return $http.delete(resourceUrl + id)
            .then(userService.onSuccess, userService.onError);
    };

    userService.UpdateGroups = function (userId, groups) {

        return $http.put(resourceUrl + userId + '/groups', groups)
            .then(userService.onSuccess, userService.onError);
    };

    userService.onSuccess = function (response) {

        // just unwrap content from response
        return response.data;
    }

    userService.onError = function (response) {

        // log error
        if (true) {
            console.log(response);
        }

        return $q.reject(response.data);
    }

    return userService;
}); 