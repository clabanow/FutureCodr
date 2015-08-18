/// <reference path="locationsModule.js" />

locationsModule.factory('locationsFactory', function($http) {
    var factory = {};
    var url = '/api/locations/';

    factory.getAllLocations = function() {
        return $http.get(url);
    };

    factory.add = function(location) {
        return $http.post(url, location);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})