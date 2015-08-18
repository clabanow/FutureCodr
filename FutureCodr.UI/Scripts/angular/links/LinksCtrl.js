/// <reference path="linksModule.js" />

linksApp.controller('LinksCtrl', function($scope, linksFactory) {

    function loadData() {
        linksFactory.getAllLinks()
            .success(function(model) {
                $scope.links = model.Links;
                $scope.bootcamps = model.Bootcamps;
                $scope.sites = model.Sites;
            })
            .error(function(status) {
                alert("Error! Status " + status);
            });
    };

    //load all links on page load
    loadData();

    //hide add-a-link section and are-you-sure section by default
    $scope.addLink = false;
    $scope.areYouSure = false;

    //show "Are you sure...delete?" and pass along id to $scope.id
    $scope.showAreYouSure = function(id) {
        $scope.id = id;
        $scope.areYouSure = true;
        $scope.addLink = false;
    };
    
    //show form area to add link
    $scope.showAddLink = function() {
        $scope.addLink = true;
        $scope.areYouSure = false;
    };
    
    //hide everything expect the table
    $scope.hide = function() {
        $scope.addLink = false;
        $scope.areYouSure = false;
    };

    $scope.add = function () {
        $scope.link.BootcampID = $scope.selectedBootcamp;
        $scope.link.SiteID = $scope.selectedSiteId;
        linksFactory.add($scope.link)
            .success(function() {
                loadData();
                //$scope.addLink = false;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    $scope.delete = function (id) {
        linksFactory.delete(id)
            .success(function() {
                loadData();
                $scope.areYouSure = false;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});