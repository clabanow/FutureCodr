/// <reference path="bootcampLocationsModule.js" />

bootcampLocationsModule.factory('bootcampLocationsFactory', function($http) {
    var factory = {};
    var url = '/api/bootcampLocation/';

    factory.getAllBootcampLocations = function() {
        return $http.get(url);
    };

    factory.add = function(itemToAdd) {
        return $http.post(url, itemToAdd);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})