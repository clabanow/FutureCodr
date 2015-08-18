/// <reference path="../../angular.min.js" />

var sessionsModule = angular.module('sessions', ['ngRoute']);

sessionsModule.config(function($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'SessionsListCtrl',
            templateUrl: '/Routes/sessionsList.html'
        })
        .when('/add/:BootcampSessionID', {
            controller: 'SessionsAddCtrl',
            templateUrl: '/Routes/sessionsAdd.html'
        })
        .otherwise({
            redirectTo: '/'
        });
});