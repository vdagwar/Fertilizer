﻿'use strict';
app.factory('mappService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    console.log("in  service");
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var MappServiceFactory = {};

    var _getallmapp = function () {
            return $http.get(serviceBase + 'api/WarehouseCategory/?i='+"1").then(function (results) {
            return results;
        });
    };

    MappServiceFactory.getallmapp = _getallmapp;



    return MappServiceFactory;

}]);