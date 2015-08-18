/// <reference path="bootcampsModule.js" />

bootcampsModule.factory('bootcampsFactory', function($http) {
    var factory = {};
    var url = '/api/bootcamps/';

    //gets all the bootcamps from db
    factory.getAllBootcamps = function() {
        return $http.get(url);
    };
    
    //gets a bootcamp information
    factory.getBootcampById = function(id) {
        return $http.get(url + id);
    };

    //add a bootcamp
    factory.add = function(bootcamp) {
        return $http.post(url, angular.toJson(bootcamp));
    };

    //edit a bootcamp
    factory.edit = function(bootcamp) {
        return $http.put(url, angular.toJson(bootcamp));
    };
    
    //delete a bootcamp
    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})