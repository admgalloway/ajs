var builds = angular.module('ads.builds');

builds.service('BuildService', function ($http, $q) {

    $http.defaults.headers.common.Authorization = 'ttoken';

    var resourceUrl = 'api/builds/';

    var buildService = {};

    buildService.GetAll = function () {

        return $http.get(resourceUrl)
            .then(buildService.onSuccess, buildService.onError);
    };

    buildService.Get = function (id) {

        return $http.get(resourceUrl + id)
            .then(buildService.onSuccess, buildService.onError);
    };

    buildService.Create = function (build) {

        return $http.post(resourceUrl, build)
            .then(buildService.onSuccess, buildService.onError);
    };

    buildService.Update = function (build) {
        
        return $http.put(resourceUrl + build.Id, build)
            .then(buildService.onSuccess, buildService.onError);
    };

    buildService.Delete = function (id) {

        return $http.delete(resourceUrl + id)
            .then(buildService.onSuccess, buildService.onError);
    };

    buildService.onSuccess = function (response) {

        // just unwrap content from response
        return response.data;
    }

    buildService.onError = function (response) {

        // log error
        if (true) {
            console.log(response);
        }

        return $q.reject(response.data);
    }

    return buildService;
}); 