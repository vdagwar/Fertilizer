'use strict';
app.factory('ordersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var ordersServiceFactory = {};

    var _getOrders = function () {
        return $http.get(serviceBase + 'api/orders').then(function (results) {
            return results;
        });
    };
    ordersServiceFactory.getOrders = _getOrders;

    var _getpriority = function (time) {
        return $http.get(serviceBase + 'api/OrderMaster/priority?time=' + time).then(function (results) {
            return results;
        });
    };
    ordersServiceFactory.getpriority = _getpriority;

    return ordersServiceFactory;

}]);