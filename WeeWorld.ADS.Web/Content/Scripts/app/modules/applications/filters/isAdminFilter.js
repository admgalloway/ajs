var users = angular.module('ads.users');

users.filter('isAdmin', function () {
    return function (isAdmin) {

        return isAdmin ? 'fa-check-square' : 'fa-square';
    };
});
