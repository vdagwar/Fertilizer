'use strict';
app.factory('supplierService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var suppliersServiceFactory = {};

    var _getsuppliers = function () {
        return $http.get(serviceBase + 'api/suppliers').then(function (results) {
            return results;
        });
    };
    suppliersServiceFactory.getsuppliers = _getsuppliers;

    var _getsuppliersbyid = function (id) {
        return $http.get(serviceBase + 'api/suppliers?id='+id).then(function (results) {
            return results;
        });
    };
    suppliersServiceFactory.getsuppliersbyid = _getsuppliersbyid;   

    var _putsuppliers = function () {
        return $http.put(serviceBase + 'api/suppliers').then(function (results) {
            return results;
        });
    };
    suppliersServiceFactory.putsuppliers = _putsuppliers;

    var _deletesuppliers = function (data) {
        console.log("Delete Calling");
        console.log(data.SupplierId);
        return $http.delete(serviceBase + 'api/suppliers/?id=' + data.SupplierId).then(function (results) {
            return results;
        });
    };

    suppliersServiceFactory.deletesuppliers = _deletesuppliers;
    suppliersServiceFactory.getsuppliers = _getsuppliers;

    return suppliersServiceFactory;

}]);