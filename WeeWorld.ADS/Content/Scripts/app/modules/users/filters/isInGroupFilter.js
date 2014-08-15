var users = angular.module('ads.users');

users.filter('isInGroup', function () {
    return function (groupId, user) {

        if (user == null || user.Groups == null) {
            return 'fa-square';
        }

        // check if this groupId is in the list of the user's groups
        var groupIndex = user.Groups.indexOf(groupId);

        return groupIndex < 0 ? 'fa-square' : 'fa-check-square';
    };
});
