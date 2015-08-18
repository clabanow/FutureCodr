/// <reference path="bootcampTechnologyModule.js" />

bootcampTechnologyModule.factory('bootcampTechnologyFactory', function($http) {
    var factory = {};
    var url = '/api/bootcampTechnology/';

    factory.getAllBootcampTechnologies = function() {
        return $http.get(url);
    };

    factory.add = function(bootcampTechnology) {
        return $http.post(url, bootcampTechnology);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})