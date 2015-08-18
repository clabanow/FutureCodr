/// <reference path="linksModule.js" />

linksApp.factory('linksFactory', function($http) {
    var factory = {};
    var url = '/api/links/';

    factory.getAllLinks = function() {
        return $http.get(url);
    };

    factory.add = function(link) {
        return $http.post(url, angular.toJson(link));
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
});