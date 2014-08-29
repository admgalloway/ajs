var ads = angular.module('ads');

ads.directive('appHeader', function () {
    return {
        restrict: 'AE',
        replace: 'true',
        templateUrl: 'content/scripts/app/common/templates/appHeader.html'
    };
});