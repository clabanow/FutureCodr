/// <reference path="sitesModule.js" />

sitesModule.controller('SitesCtrl', function($scope, sitesFactory) {

    function loadData() {
        sitesFactory.getAllSites()
            .success(function(sites) {
                $scope.sites = sites;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //load data when the page is loaded
    loadData();

    //hide add-site and delete prompts by default
    $scope.addSite = false;
    $scope.confirmDelete = false;

    //show the delete confirmation box
    $scope.showConfirmDelete = function (id, name) {
        $scope.id = id;
        $scope.name = name;
        $scope.confirmDelete = true;
        $scope.addSite = false;
    };

    $scope.showAddSite = function() {
        $scope.addSite = true;
        $scope.confirmDelete = false;
    };

    //hide everything but the list/table
    $scope.hide = function() {
        $scope.addSite = false;
        $scope.confirmDelete = false;
    };

    //add a site
    $scope.add = function() {
        sitesFactory.add($scope.site)
            .success(function() {
                $scope.hide();
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //delete a site
    $scope.delete = function(id) {
        sitesFactory.delete(id)
            .success(function() {
                $scope.hide();
                loadData();
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});