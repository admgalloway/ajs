var builds = angular.module('ads.builds');

builds.filter('buildType', function () {
    return function (buildType) {

        switch(buildType) {
        case 0:
            return 'Production';
        case 1:
            return 'Ad-Hoc';
        default:
            return 'Unknown';
        }
    };
});
