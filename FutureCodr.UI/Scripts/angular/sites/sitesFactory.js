/// <reference path="sitesModule.js" />

sitesModule.factory('sitesFactory', function($http) {
    var factory = {};
    var url = '/api/sites/';

    factory.getAllSites = function() {
        return $http.get(url);
    };

    factory.add = function(site) {
        return $http.post(url, site);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})