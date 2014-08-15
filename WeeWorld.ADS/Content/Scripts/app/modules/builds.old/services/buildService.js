var applications = angular.module('ads.builds');

applications.factory('BuildService', function ($http) {

    $http.defaults.headers.common.Authorization = 'ttoken';

    var buildService = {};

    buildService.GetAll = function () {

        //return $http.get('api/builds');
        return [
          { id: 1, name: "Administrators" },
          { id: 2, name: "Developers" },
          { id: 3, name: "Testers" },
          { id: 1, name: "Preview Users" }
        ];
    };

    return buildService;
}); 