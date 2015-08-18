/// <reference path="sessionsModule.js" />

sessionsModule.controller('SessionsListCtrl', function($scope, sessionsFactory) {

    function loadData() {
        sessionsFactory.getAllSessions()
            .success(function(model) {
                $scope.sessions = model.Sessions;
                $scope.bootcamps = model.Bootcamps;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };

    //load all sessions when page loads
    loadData();

    //confirm delete and add blurbs hidden by default
    $scope.addSession = false;
    $scope.confirmDelete = false;

    //show confirm delete blurb
    $scope.showConfirmDelete = function (id) {
        $scope.id = id;
        $scope.confirmDelete = true;
        $scope.addSession = false;
    };
    
    //show add session form
    $scope.showAddSession = function() {
        $scope.addSession = true;
    };

    //hide all other forms;
    $scope.hide = function() {
        $scope.confirmDelete = false;
        $scope.addSession = false;
    };

    //delete a session
    $scope.delete = function(id) {
        sessionsFactory.delete(id)
            .success(function() {
                loadData();
                $scope.hide();
            })
            .error(function() {
                alert("Error! Status: " + status);
            });
    };
});
