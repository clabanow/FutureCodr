/// <reference path="technologyModule.js" />

technologyApp.controller('TechnologyCtrl', function($scope, technologyFactory) {
    function loadData() {
        technologyFactory.getAllMovies()
            .success(function(technologies) {
                $scope.technologies = technologies;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    }

    //set the add technology display form to false (i.e. hidden)
    $scope.showAddTechnology = false;
    
    //get all the data when ctrl is called
    loadData();

    $scope.toggleAddTechnology = function() {
        $scope.showAddTechnology = !$scope.showAddTechnology;
    };

    //add technology and reload data in table
    $scope.add = function() {
        technologyFactory.post($scope.technology)
            .success(function() {
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //delete technology and reload data in table
    $scope.delete = function(id) {
        technologyFactory.delete(id)
            .success(function() {
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    //form validation
    //$scope.master = {};

    //$scope.update = function (user) {
    //    $scope.master = angular.copy(user);
    //};

    //$scope.reset = function () {
    //    $scope.user = angular.copy($scope.master);
    //};

    //$scope.isUnchanged = function (user) {
    //    return angular.equals(user, $scope.master);
    //};

    //$scope.reset();
});