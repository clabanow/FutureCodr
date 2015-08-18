/// <reference path="bootcampTechnologyModule.js" />

bootcampTechnologyModule.controller('BootcampTechnologyCtrl', function($scope, bootcampTechnologyFactory) {

    function loadData() {
        bootcampTechnologyFactory.getAllBootcampTechnologies()
            .success(function(model) {
                $scope.bootcampTechnologies = model.BootcampTechnologies;
                $scope.bootcamps = model.Bootcamps;
                $scope.technologies = model.Technologies;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //load data when page loads
    loadData();

    //set both the add-form and the delete confirmation forms to hidden by default
    $scope.addBootcampTechnology = false;
    $scope.confirmDelete = false;

    //show add-form
    $scope.showAddBootcampTechnology = function() {
        $scope.addBootcampTechnology = true;
        $scope.confirmDelete = false;
    };

    //show confirm delete form
    $scope.showConfirmDelete = function (id, bootcamp, technology) {
        $scope.id = id;
        $scope.bootcamp = bootcamp;
        $scope.technology = technology;
        $scope.addBootcampTechnology = false;
        $scope.confirmDelete = true;
    };

    //hides all extraneous forms
    $scope.hide = function() {
        $scope.addBootcampTechnology = false;
        $scope.confirmDelete = false;
    };

    //add a bootcampTechnology
    $scope.add = function () {
        //add scope vars to model
        var bootcampTechnology = {
            BootcampID: $scope.selectedBootcampId,
            TechnologyID: $scope.selectedTechnologyId
        };
        
        bootcampTechnologyFactory.add(bootcampTechnology)
            .success(function() {
                loadData();
                //$scope.hide();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //delete a bootcampTechnology
    $scope.delete = function(id) {
        bootcampTechnologyFactory.delete(id)
            .success(function() {
                loadData();
                $scope.hide();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});