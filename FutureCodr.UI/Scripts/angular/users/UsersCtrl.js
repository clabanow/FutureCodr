/// <reference path="usersModule.js" />

usersApp.controller('UsersCtrl', function($scope, usersFactory) {
    function loadData() {
        usersFactory.getAllUsers()
            .success(function(users) {
                $scope.users = users;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
    
    //show all users on page load
    loadData();
    
    //hide Are you sure you want to delete? on page load
    $scope.areYouSure = false;

    //shows "Are you sure?" prompt and passes email and id
    $scope.toggleAreYouSure = function(email, id) {
        $scope.areYouSure = !$scope.areYouSure;
        $scope.email = email;
        $scope.id = id;
    };

    $scope.hideAreYouSure = function() {
        $scope.areYouSure = false;
    };

    $scope.delete = function(id) {
        usersFactory.delete(id)
            .success(function() {
                loadData();
                $scope.areYouSure = false;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});