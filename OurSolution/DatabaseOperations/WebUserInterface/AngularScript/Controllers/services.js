
angular.module('instancesService', []).
    factory('apiFactory', function ($http) {
        var apiFactory = {};

        apiFactory.getInstances = function (currentPage, pageItems, filterByFields, orderbyField, orderByReverse) {
            var data = {
                "currentPage": ++currentPage,
                "pageItems": pageItems,
                "filterByFields": filterByFields,
                "orderbyField": orderbyField,
                "orderByReverse": orderByReverse
            };
            return $http.post(wcfAddress+'Search', JSON.stringify(data));
        };

        apiFactory.getInstanceById = function (instanceId) {
            return $http.get(wcfAddress + 'Instance/' + instanceId);
        };

        apiFactory.getUsers = function () {
            return $http.get(wcfAddress + 'Users');
        };

        apiFactory.getUserById = function (userId) {
            return $http.get(wcfAddress + 'User/' + userId);
        };

        apiFactory.addUser = function (user) {
            return $http.post(wcfAddress + 'UserAdd', JSON.stringify(user));
        };

        apiFactory.editUser = function (user) {
            return $http.put(wcfAddress + 'UserUpdate', JSON.stringify(user));
        };

        apiFactory.deleteUser = function (user) {
            return $http.delete(wcfAddress + 'UserDelete/' + user.Id);
        };

        apiFactory.getOptions = function () {
            return $http.get(wcfAddress + 'Options');
        };

        apiFactory.editOptions = function (options) {
            return $http.put(wcfAddress + 'OptionsUpdate', JSON.stringify(options));
        };

        return apiFactory;
    });

