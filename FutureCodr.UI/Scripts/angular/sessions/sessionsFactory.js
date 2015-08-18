/// <reference path="sessionsModule.js" />

sessionsModule.factory('sessionsFactory', function($http) {
    var factory = {};
    var url = '/api/bootcampSession/';

    factory.getAllSessions = function() {
        return $http.get(url);
    };

    factory.getSessionInfo = function(id) {
        return $http.get(url + id);
    };

    factory.add = function(session) {
        return $http.post(url, session);
    };

    factory.delete = function(id) {
        return $http.delete(url + id);
    };

    return factory;
})