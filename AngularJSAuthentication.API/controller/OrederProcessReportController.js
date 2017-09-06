'use strict'
app.controller('OrederProcessReportController', ['$scope', "$http", "ngTableParams", function ($scope, $http, ngTableParams) {
    
    $scope.dbReport = [];
    $http.get(serviceBase + 'api/OrederProcessReport').then(function (results) {
        $scope.dbReport = results.data;
    });
}]);