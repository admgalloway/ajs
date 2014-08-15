var users = angular.module('ads.groups');

users.filter('hasUser', function () {
    return function (userId, group) {

        if (group == null || group.Users == null) {
            return 'fa-square';
        }

        // check if this userId is in the list of the group's users
        var userIndex = group.Users.indexOf(userId);

        return userIndex < 0 ? 'fa-square' : 'fa-check-square';
    };
});
