'use strict';
    app.factory('ClusterService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

        var serviceBase = ngAuthSettings.apiServiceBaseUri;

        var ClusterServiceFactory = {};

        var _getcluster = function () {
            console.log("in cluster service")
            return $http.get(serviceBase + 'api/cluster/all').then(function (results) {
                return results;
            });
        };
        ClusterServiceFactory.getcluster = _getcluster;

        var _getwarehouse = function () {
            console.log("in warehouse service")
            return $http.get(serviceBase + 'api/Warehouse').then(function (results) {
                return results;
            });
        };
        ClusterServiceFactory.getwarehouse = _getwarehouse;

        var _deletecluster = function (data) {
            console.log("Delete Calling");
            console.log(data.Warehouseid);

            return $http.delete(serviceBase + 'api/cluster/delete?id=' + data.ClusterId).then(function (results) {
                return results;
            });
        };

        ClusterServiceFactory.deletecluster = _deletecluster;

        return ClusterServiceFactory;
    }]);