var builds = angular.module('ads.builds');

builds.filter('SubmissionState', function () {
    return function (submissionState) {

        switch (submissionState) {
        case 0:
            return 'Not Submitted';
        case 1:
            return 'Submitted';
        case 2:
            return 'Accepted';
        case 3:
            return 'Rejected';
        default:
            return 'Unknown';
        }
    };
});
