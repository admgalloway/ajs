var ads = angular.module('ads');

ads.directive('jsTooltip', function () {
    var directive = {};

    directive.restrict = 'C';
    directive.templateUrl = 'content/scripts/app/common/templates/tooltip.html';
    directive.transclude = true;
    directive.scope = {};

    return directive;
})