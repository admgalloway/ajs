var builds = angular.module('ads.builds');

builds.filter('applicationName', function () {
    return function (id, applications) {

        if (applications == null || id == null)
        {
            return 'unknown';
        }

        var app = applications.filter(function (element) {
            return element.Id == id;
        });

        return app[0].Name;

    };
});
