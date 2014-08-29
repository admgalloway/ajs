var ads = angular.module('ads');

ads.factory('authInterceptor', function ($rootScope, $q, SessionService) {
    return {
        request: function (config) {
            config.headers = config.headers || {};

            var token = SessionService.getToken();

            if (token == null)
            {
                // redirect to login
                return config;
            }

            config.headers.Authorization = token.Code;
            return config;
        },
        response: function (response) {
            if (response.status === 401) {
                // redirect to login
            }
            return response || $q.when(response);
        }
    };
});

ads.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
});