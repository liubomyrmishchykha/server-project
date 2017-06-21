
var instanceApp = angular.module('instanceApp', [
  'instancesService',
  'ngRoute',
  'instancesControllers',
  'trNgGrid',
  'angular-growl',
  'validation'
]);

instanceApp.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(8000);
}]);

instanceApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/instances', {
            templateUrl: '/home/instances'
        }).
        when('/instances/:instanceId', {
            templateUrl: '/home/instance'
        }).
        when('/users', {
            templateUrl: '/home/users'
        }).
        when('/users/add', {
            templateUrl: '/home/adduser'
        }).
        when('/users/edit/:userId', {
            templateUrl: '/home/edituser'
        }).
        when('/users/delete/:userId', {
            templateUrl: '/home/deleteuser'
        }).
        when('/options', {
            templateUrl: '/home/options'
        }).
        otherwise({
            redirectTo: '/instances'
        });
}]);

