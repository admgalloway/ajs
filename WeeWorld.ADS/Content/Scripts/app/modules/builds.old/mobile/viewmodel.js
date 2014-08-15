define(['require', 'Boiler', 'knockout', '../restClient', '../../applications/restClient'], function (require, Boiler, ko, restClient, applicationClient) {

    var ViewModel = function (moduleContext) {
        var self = this;
        
        self.buildClient = new restClient(moduleContext);
        self.applicationClient = new applicationClient(moduleContext);

        self.userId = window.token.User;
        self.allBuilds = ko.observableArray();
        self.apps = ko.observableArray();
        self.index = ko.observable(0);
        self.filter = ko.observable('all');
        self.loadingList = ko.observable(false);

        self.builds = ko.computed(function () {

            if (self.apps() == undefined)
                return [];

            var app = self.apps()[self.index()];

            if (app == undefined)
                return [];

            var builds = self.allBuilds().filter(function (b) {
                return (b.Application == app.Id);
            });

            self.loadingList(false);
            return builds;

        });

        self.currentApp = ko.computed(function () {
            var i = self.index();
            return self.apps()[i];
        });

        self.latestBuiled = ko.computed(function () {

            return self.builds()[0];

        });

        self.Init = function () {
            self.loadingList(true);

            var url = '/users/' + self.userId + '/applications';
            self.buildClient.GetAll(function (builds) {
                self.applicationClient.GetByUser(self.userId, function (apps) {
                    self.allBuilds(builds);
                    self.apps(apps);
                    self.loadingList(false);
                });
            });
        };

        self.Next = function () {
            var index = self.index() + 1;

            if (index > self.apps().length - 1) 
                index = 0;

            self.index(index);
        };

        self.Prev = function () {
            var index = self.index() - 1;

            if (index < 0)
                index = self.apps().length - 1;

            self.index(index);
        };

        self.Filter = function (element, ui) {
            self.filter(type);
        };

        self.ToggleDetails = function(value, event)
        {
            var details = $(event.target).siblings().last();
            $(details).toggleClass('hide');
        }

        ko.bindingHandlers.formattedDate = {
            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var value = valueAccessor(),
                    allBindings = allBindingsAccessor();
                var valueUnwrapped = ko.utils.unwrapObservable(value);

                var pattern = allBindings.format || 'dd/MM/yy';
                var dt = new Date(valueUnwrapped);

                var month = new Array();
                month[0] = "Jan";
                month[1] = "Feb";
                month[2] = "Mar";
                month[3] = "Apr";
                month[4] = "May";
                month[5] = "Jun";
                month[6] = "Jul";
                month[7] = "Aug";
                month[8] = "Sep";
                month[9] = "Oct";
                month[10] = "Nov";
                month[11] = "Dec";

                var output = month[dt.getMonth()] + ' ' + dt.getDate() + ' ' + dt.getHours() + ':' + dt.getMinutes();

                $(element).text(output);
                $(element).val(output);
            }
        }

    };

    return ViewModel;
});
