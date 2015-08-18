/// <reference path="technologyModule.js" />

technologyApp.factory('technologyFactory', function($http) {
    var factory = {};
    var url = '/api/technology/';

    factory.getAllMovies = function() {
        return $http.get(url);
    };

    factory.post = function(technology) {
        return $http.post(url, angular.toJson(technology));
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})