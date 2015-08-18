/// <reference path="contactFormsModule.js" />

contactFormsApp.controller('ContactFormsCtrl', function($scope, contactFormsFactory) {

    function loadData() {
        contactFormsFactory.getAllContactForms()
            .success(function(contactForms) {
                $scope.contactForms = contactForms;
            });
    };

    //load data on page load
    loadData();

    //hide Are you sure? on page load
    $scope.areYouSure = false;

    $scope.showAreYouSure = function(id, message) {
        $scope.areYouSure = true;
        $scope.id = id;
        $scope.message = message;
    };

    $scope.hideAreYouSure = function() {
        $scope.areYouSure = false;
    };

    $scope.delete = function (id) {
        contactFormsFactory.delete(id)
            .success(function() {
                loadData();
                $scope.areYouSure = false;
            })
            .error(function(status) {
                alert("Error! Status: " + status);
            });
    };
});