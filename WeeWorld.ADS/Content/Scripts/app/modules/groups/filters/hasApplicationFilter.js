var users = angular.module('ads.groups');

users.filter('hasApplication', function () {
    return function (applicationId, group) {

        if (group == null || group.Applications == null) {
            return 'fa-square';
        }

        // check if this userId is in the list of the group's users
        var index = group.Applications.indexOf(applicationId);

        return index < 0 ? 'fa-square' : 'fa-check-square';
    };
});
