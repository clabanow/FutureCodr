/// <reference path="locationsModule.js" />

locationsModule.controller('LocationsCtrl', function($scope, locationsFactory) {
    //loads all the locations into $scope
    function loadData() {
        locationsFactory.getAllLocations()
            .success(function(locations) {
                $scope.locations = locations;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //load data on page load
    loadData();
    //hide form to add location by default
    $scope.addLocation = false;
    //hide delete confirmation by default
    $scope.confirmDelete = false;


    //shows the add location form and hides the delete confirmation
    $scope.showAddLocation = function() {
        $scope.addLocation = true;
        $scope.confirmDelete = false;
    };

    //shows the delete confirmation form and hides the add location form
    $scope.showConfirmDelete = function (id, city, country) {
        //pass id, city and country to delete confirm form for display
        $scope.id = id;
        $scope.city = city;
        $scope.country = country;
        
        $scope.confirmDelete = true;
        $scope.addLocation = false;
    };

    //hides the add location form
    $scope.hideAddLocation = function() {
        $scope.addLocation = false;
    };

    //hides the delete confirmation form
    $scope.hideConfirmDelete = function() {
        $scope.confirmDelete = false;
    };

    //add location to the DB and reload locations
    $scope.add = function() {
        locationsFactory.add($scope.location)
            .success(function() {
                loadData();
                $scope.hideAddLocation();
                $scope.location = {};
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //delete location from the db and reload locations
    $scope.delete = function(id) {
        locationsFactory.delete(id)
            .success(function() {
                loadData();
                $scope.hideConfirmDelete();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});