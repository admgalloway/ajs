define(['require', 'Boiler', 'knockout', '../restClient', '../../applications/restClient'], function (require, Boiler, ko, restClient, applicationClient) {

    var ViewModel = function (moduleContext) {
        var self = this;
        
        self.buildClient = new restClient(moduleContext);
        self.applicationClient = new applicationClient(moduleContext);

        self.list = ko.observableArray();
        self.applications = ko.observableArray();
        self.selectedAppId = ko.observable();

        self.obj = ko.observable();
        self.errors = ko.observableArray();
        self.loadingList = ko.observable(false);
        self.loadingForm = ko.observable(false);
        self.showAlert = ko.observable(false);

        self.builds = ko.computed(function () {
            var builds = self.list().filter(function (b) {
                return (b.Application == self.selectedAppId());
            });

            return builds;
        });

        self.Init = function () {
            self.loadingList(true);
            self.applicationClient.GetAll(function (apps) {
                self.buildClient.GetAll(function (builds) {
                    self.applications(apps);
                    self.list(builds);
                    self.loadingList(false);
                });
            });
        };

        self.Refresh = function () {
            self.loadingList(true);

            self.buildClient.RefreshFromCI(true, true, function (response) {
                self.Init();
            });
        };

        self.Edit = function (obj) {
            self.loadingForm(true);
            self.errors([]);
            self.showAlert(false);
            self.obj(obj);
            self.loadingForm(false);
        };

        self.Save = function () {
            self.loadingForm(true);
            self.errors([]);
            self.showAlert(false);
            self.buildClient.Update(self.obj, self.success, self.erorr);
        };

        self.success = function (data) {
            self.replace(data);
            self.obj(data);
            self.showAlert(true);
            self.loadingForm(false);
        };

        self.erorr = function (errors) {
            self.errors(errors);
            self.loadingForm(false);
        };

        self.replace = function (obj) {
            for (var i = 0; i < self.list().length; i++) {
                if (self.list()[i].Id == obj.Id) {
                    self.list.splice(i, 1, obj);
                    break;
                }
            }
        };

        ko.bindingHandlers.formattedDate = {
            update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var value = valueAccessor(),
                    allBindings = allBindingsAccessor();
                var valueUnwrapped = ko.utils.unwrapObservable(value);

                if (valueUnwrapped == null)
                {
                    $(element).text('');
                    $(element).val('');
                    return;
                }

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
