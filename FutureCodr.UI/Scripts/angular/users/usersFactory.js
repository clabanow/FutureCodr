/// <reference path="usersModule.js" />

usersApp.factory('usersFactory', function($http) {
    var factory = {};
    var url = '/api/users/';

    factory.getAllUsers = function() {
        return $http.get(url);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})