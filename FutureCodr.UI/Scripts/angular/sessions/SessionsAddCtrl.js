/// <reference path="sessionsModule.js" />

sessionsModule.controller('SessionsAddCtrl', function($scope, $location, $routeParams, sessionsFactory) {

    function loadSessionInfo() {
        sessionsFactory.getSessionInfo($routeParams.BootcampSessionID)
            .success(function (session) {
                $scope.session = session;
                $scope.locations = session.Locations;
                $scope.technologies = session.Technologies;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    //load session info (available technologies and locations) on page load
    loadSessionInfo();
    
    //add a session
    $scope.add = function (id) {
        $scope.session.BootcampID = $routeParams.BootcampSessionID;
        $scope.session.LocationID = $scope.selectedLocationId;
        $scope.session.TechnologyID = $scope.selectedTechnologyId;
        sessionsFactory.add($scope.session)
            .success(function() {
                $location.path('/');
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    $(document).ready(function () {
        $(".datepicker").datepicker();
    });
});