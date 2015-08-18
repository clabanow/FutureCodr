/// <reference path="bootcampsModule.js" />

bootcampsModule.controller('BootcampsCtrl', function($scope, bootcampsFactory) {
    //this function loads all data to be displayed to view table

    function loadData() {
        bootcampsFactory.getAllBootcamps()
            .success(function(model) {
                $scope.bootcamps = model.Bootcamps;
                $scope.locations = model.Locations;
                $scope.technologies = model.Technologies;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    }

    //load data automatically when page is opened
    loadData();

    //var which shows/hides the AddBootcamp form; default is hidden
    $scope.showAddBootcamp = false;
    $scope.showEditBootcamp = false;

    $scope.hide = function() {
        $scope.showAddBootcamp = false;
        $scope.showEditBootcamp = false;
    };

    //add bootcamp and reload all bootcamps
    $scope.add = function () {
        //add values from drop down lists
        $scope.bootcamp.LocationID = $scope.selectedLocationId;
        $scope.bootcamp.PrimaryTechnologyID = $scope.selectedTechnologyId;
        
        bootcampsFactory.add($scope.bootcamp)
            .success(function() {
                loadData();
                $scope.hide();
                
                //reset all of the $scope.bootcamp properties back to empty
                $scope.bootcamp.Name = "";
                $scope.selectedTechnologyId = null;
                $scope.selectedLocationId = null;
                $scope.bootcamp.Price = null;
                $scope.bootcamp.LengthInWeeks = null;
                $scope.bootcamp.Website = "";
                $scope.bootcamp.LogoLink = "";
                
                //set the form back to pristine
                $scope.form.$setPristine();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    //display edit bootcamp div with info to be edited
    $scope.showEdit = function(id) {
        bootcampsFactory.getBootcampById(id)
            .success(function(bootcamp) {
                $scope.bootcamp = bootcamp;
                $scope.selectedTechnologyId = bootcamp.PrimaryTechnologyID;
                $scope.selectedLocationId = bootcamp.LocationID;
                $scope.id = bootcamp.BootcampID;
                $scope.showEditBootcamp = true;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    //edit bootcamp and reload all bootcamps
    $scope.save = function () {
        //add values from drop down lists
        $scope.bootcamp.LocationID = $scope.selectedLocationId;
        $scope.bootcamp.PrimaryTechnologyID = $scope.selectedTechnologyId;
        $scope.bootcamp.BootcampID = $scope.id;

        bootcampsFactory.edit($scope.bootcamp)
            .success(function () {
                loadData();
                $scope.hide();

                //reset all of the $scope.bootcamp properties back to empty
                $scope.bootcamp.Name = "";
                $scope.selectedTechnologyId = null;
                $scope.selectedLocationId = null;
                $scope.bootcamp.Price = null;
                $scope.bootcamp.LengthInWeeks = null;
                $scope.bootcamp.Website = "";
                $scope.bootcamp.LogoLink = "";

                //set the form back to pristine
                $scope.form.$setPristine();
            })
            .error(function (status) {
                alert("Error! Status: " + status);
            });
    };

    //delete bootcamp and reload all bootcamp data
    $scope.delete = function(id) {
        bootcampsFactory.delete(id)
            .success(function() {
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});