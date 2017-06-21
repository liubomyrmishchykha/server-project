var instancesControllers = angular.module('instancesControllers', ['ui.bootstrap']);

instancesControllers.controller('InstancesListCtrl', function ($scope, $http, apiFactory, growl) {
    $scope.myColumns = ['Id', 'InstanceName', 'HostName', 'Added', 'Modified', 'Status', 'AuthentificationMode', 'UserName', 'Version'];
    $scope.instancesTotalCount = 0;
    $scope.instances = null;
    $scope.myOrderBy = 'Id';
    $scope.myOrderByReverse = true;
    $scope.filterByFields = {};

    var pcurrentPage;
    var ppageItems;
    var porderBy;
    var porderByReverse;

    //Datepicker Added.
    $scope.openAddedFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.addedFrom = true;
        $scope.addedTo = false;
    };
    $scope.openAddedTo = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.addedTo = true;
        $scope.addedFrom = false;
    };

    //Datepicker Modified.
    $scope.openModifiedFrom = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.modifiedFrom = true;
        $scope.modifiedTo = false;
    };
    $scope.openModifiedTo = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.modifiedTo = true;
        $scope.modifiedFrom = false;
    };
    $scope.dateOptions = {
        formatYear: 'yyyy',
        startingDay: 1,
        showWeeks: 'false'
    };
    $scope.format = 'yyyy-MM-dd';

    //function start when user change same preference
    $scope.onServerSideItemsRequested = function (currentPage, pageItems, filterBy, filterByFields, orderBy, orderByReverse) {
        pcurrentPage = currentPage;
        ppageItems = pageItems;
        porderBy = orderBy
        porderByReverse = orderByReverse;

        loadInstancesList(currentPage, pageItems, orderBy, orderByReverse);
    }

    $scope.clickFilter = function () {
        loadInstancesList(pcurrentPage, ppageItems, porderBy, porderByReverse);
    }

    //Ajax call for load list instances.
    var loadInstancesList = function (currentPage, pageItems, orderBy, orderByReverse) {
        var filterByFields = $scope.filterByFields;
        apiFactory.getInstances(currentPage, pageItems, $scope.filterByFields, orderBy, orderByReverse)
            .success(function (data) {
                $scope.instances = data.Instances;
                // set the total number of items from the server
                $scope.instancesTotalCount = data.Count;
            })
            .error(function (data) {
                growl.error(data["errorMessage"], { title: 'Error!' });
            });
    }
});

instancesControllers.controller('InstanceDetailCtrl', function ($scope, $routeParams, apiFactory, $location) {
    apiFactory.getInstanceById($routeParams.instanceId)
        .success(function (data) {
            $scope.instance = data;
        })
        .error(function (data) {
            growl.error(data["errorMessage"], { title: 'Error!' });
        });
    $scope.back = function () {
        $location.path('/instances');
    };
});

instancesControllers.controller('UsersListCtrl', function ($scope, $http, apiFactory) {
    apiFactory.getUsers()
        .success(function (data) {
            $scope.users = data;
        })
    .error(function (data) {
        growl.error(data["errorMessage"], { title: 'Error!' });
    });
    $scope.orderProp = "Name";
});

instancesControllers.controller('AddUserCtrl', function ($scope, $http, apiFactory, $location, growl) {
    $scope.submitForm = function () {
        if ($scope.userForm.$valid) {
            apiFactory.addUser($scope.user)
            .success(function (data) {
                growl.success('You create user.', { title: 'Success!' });
                $location.path('/users');
            })
            .error(function (data) {
                growl.error(data["errorMessage"], { title: 'Error! User wasn\'t create' });
            })
        }
    }
    $scope.cancel = function () {
        $location.path('/users');
    };
});

instancesControllers.controller('EditUserCtrl', function ($scope, $routeParams, apiFactory, $location, growl) {
    apiFactory.getUserById($routeParams.userId)
        .success(function (data) {
            $scope.user = data;
            $scope.user.Password = "";
        })
        .error(function (data) {
            growl.error(data["errorMessage"], { title: 'Error!' });
        });
    //submit user
    $scope.submitForm = function () {
        if ($scope.userForm.$valid) {
            apiFactory.editUser($scope.user)
            .success(function (data) {
                growl.success('You update user.', { title: 'Success!' });
                $location.path('/users');
            })
            .error(function (data) {
                growl.error(data["errorMessage"], { title: 'Error! Updating user' });
            });
        }
    }

    $scope.cancel = function () {
        $location.path('/users');
    };
});

instancesControllers.controller('DeleteUserCtrl', function ($scope, $routeParams, apiFactory, $location, growl) {
    apiFactory.getUserById($routeParams.userId).success(function (data) {
        $scope.user = data;
        $scope.user.Password = "";
    });

    $scope.deleteUser = function () {
        apiFactory.deleteUser($scope.user)
            .success(function (data) {
                growl.success('User deleted!', { title: 'Success!' });
                $location.path('/users');
            })
            .error(function (data) {
                growl.error(data["errorMessage"], { title: 'Error! Deleting user' });
            })
    };

    $scope.cancel = function () {
        $location.path('/users');
    };
});

instancesControllers.controller('OptionsCtrl', function ($scope, $routeParams, apiFactory, $location, growl) {
    apiFactory.getOptions()
        .success(function (data) {
            $scope.options = data;
        })
        .error(function (data) {
            growl.error(data["errorMessage"], { title: 'Error!' });
        });
    //submit options
    $scope.submitForm = function () {
        if ($scope.optionsForm.$valid) {
            apiFactory.editOptions($scope.options)
            .success(function (data) {
                growl.success('Option updated!', { title: 'Success!' });
                $location.path('/options');
            })
            .error(function (data) {
                growl.error(data["errorMessage"], { title: 'Error! Updating options' });
            });
        }
    };

    $scope.cancel = function () {
        $location.path('/instances');
    };
});


