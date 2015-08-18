/// <reference path="contactFormsModule.js" />

contactFormsApp.factory('contactFormsFactory', function($http) {
    var factory = {};
    var url = '/api/contactForms/';

    factory.getAllContactForms = function() {
        return $http.get(url);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
});