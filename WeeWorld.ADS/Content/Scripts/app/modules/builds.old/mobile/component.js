define(function(require) {

    // Load the dependencies
    var Boiler = require('Boiler'), 
        ko = require('knockout'),
        template = require('text!./view.html'),
        styling = require('text!./style.css'),
        ViewModel = require('./viewmodel');


	var Component = function(moduleContext) {
		var vm, panel = null;
		this.activate = function(parent, params) {
			if(!panel) {
			    vm = new ViewModel(moduleContext);
			    panel = new Boiler.ViewTemplate(parent, template, null, styling);
			    ko.applyBindings(vm, panel.getDomElement());
            }
			vm.Init();
			panel.show();
		};

		this.deactivate = function () {
		    if (panel) {
		        panel.hide();
		    }
		};
	};

	return Component;

}); 