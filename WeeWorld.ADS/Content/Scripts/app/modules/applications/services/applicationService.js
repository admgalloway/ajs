var applications = angular.module('ads.applications');

applications.service('ApplicationService', function ($http, $q) {

    $http.defaults.headers.common.Authorization = 'ttoken';

    var resourceUrl = 'api/applications/';

    var applicationService = {};

    applicationService.GetAll = function () {

        return $http.get(resourceUrl)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.Get = function (id) {

        return $http.get(resourceUrl + id)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.Create = function (application) {

        return $http.post(resourceUrl, application)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.Update = function (application) {
        
        return $http.put(resourceUrl + application.Id, application)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.Delete = function (id) {

        return $http.delete(resourceUrl + id)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.UpdateGroups = function (applicationId, groups) {

        return $http.put(resourceUrl + applicationId + '/groups', groups)
            .then(applicationService.onSuccess, applicationService.onError);
    };

    applicationService.onSuccess = function (response) {

        // just unwrap content from response
        return response.data;
    }

    applicationService.onError = function (response) {

        // log error
        if (true) {
            console.log(response);
        }

        return $q.reject(response.data);
    }

    return applicationService;
}); 