'use strict';
app.factory('WarehouseCategoryService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    console.log("Entered Services");
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var WarehouseCategoryServiceFactory = {};

    var _getwhcategorys = function () {

        return $http.get(serviceBase + 'api/WarehouseCategory').then(function (results) {
            return results;
        });
    };

    WarehouseCategoryServiceFactory.getwhcategorys = _getwhcategorys;




    var _putwhcategorys = function () {

        return $http.put(serviceBase + 'api/WarehouseCategory').then(function (results) {
            return results;
        });
    };

    WarehouseCategoryServiceFactory.putwhcategorys = _putwhcategorys;


   

    var _deleteWhCategorys = function (data) {
        return $http.delete(serviceBase + 'api/WarehouseCategory/?id=' + data.WhCategoryid).then(function (results) {
            return results;
        });
    };

    WarehouseCategoryServiceFactory.deleteWhCategorys = _deleteWhCategorys;
    WarehouseCategoryServiceFactory.getwhcategorys = _getwhcategorys;
 




    return WarehouseCategoryServiceFactory;

}]);