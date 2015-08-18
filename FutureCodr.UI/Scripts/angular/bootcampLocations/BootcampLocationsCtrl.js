/// <reference path="bootcampLocationsModule.js" />

bootcampLocationsModule.controller('BootcampLocationsCtrl', function($scope, bootcampLocationsFactory) {

    function loadData() {
        bootcampLocationsFactory.getAllBootcampLocations()
            .success(function(model) {
                $scope.bootcampLocations = model.BootcampLocations;
                $scope.bootcamps = model.Bootcamps;
                $scope.locations = model.Locations;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //load all data into $scope when page loads
    loadData();

    //set add and confirm delete boxes to hidden by default
    $scope.addBootcampLocation = false;
    $scope.confirmDelete = false;

    //show add Bootcamp Location box
    $scope.showAddBootcampLocation = function() {
        $scope.addBootcampLocation = true;
        $scope.confirmDelete = false;
    };

    //show confirm delete box and pass it the ID
    $scope.showConfirmDelete = function(id) {
        $scope.id = id;
        $scope.confirmDelete = true;
        $scope.addBootcampLocation = false;
    };

    //hide everything except the table
    $scope.hide = function() {
        $scope.addBootcampLocation = false;
        $scope.confirmDelete = false;
    };

    //add bootcamp Location
    $scope.add = function () {
        var modelToAdd = {
            BootcampID: $scope.bootcampId,
            LocationID: $scope.locationId
        };
        
        bootcampLocationsFactory.add(modelToAdd)
            .success(function () {
                alert(modelToAdd.BootcampID);
                alert(modelToAdd.LocationID);
                $scope.hide();
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //delete bootcamp location
    $scope.delete = function(id) {
        bootcampLocationsFactory.delete(id)
            .success(function() {
                $scope.hide();
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});