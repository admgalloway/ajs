var groups = angular.module('ads.groups');

groups.service('GroupService', function ($http, $q) {

    $http.defaults.headers.common.Authorization = 'ttoken';

    var resourceUrl = 'api/groups/';

    var groupService = {};

    groupService.GetAll = function () {

        return $http.get(resourceUrl)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.Get = function (id) {

        return $http.get(resourceUrl + id)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.Create = function (group) {

        return $http.post(resourceUrl, group)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.Update = function (group) {
        
        return $http.put(resourceUrl + group.Id, group)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.Delete = function (id) {

        return $http.delete(resourceUrl + id)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.UpdateUsers = function (groupId, users) {

        return $http.put(resourceUrl + groupId + '/users', users)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.UpdateApplications = function (groupId, applications) {

        return $http.put(resourceUrl + groupId + '/applications', applications)
            .then(groupService.onSuccess, groupService.onError);
    };

    groupService.onSuccess = function (response) {

        // just unwrap content from response
        return response.data;
    }

    groupService.onError = function (response) {

        // log error
        if (true) {
            console.log(response);
        }

        return $q.reject(response.data);
    }

    return groupService;
}); 