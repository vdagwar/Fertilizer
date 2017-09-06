'use strict';
app.factory('WarehouseSubCategoryService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    console.log("insubcat service");
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var WarehouseSubCategoryServiceFactory = {};

    var _getwhsubcategorys = function () {

        return $http.get(serviceBase + 'api/WarehouseSubCategory').then(function (results) {
            return results;
        });
    };

    WarehouseSubCategoryServiceFactory.getwhsubcategorys = _getwhsubcategorys;



    var _putwhsubcategorys = function () {

        return $http.put(serviceBase + 'api/WarehouseSubCategory').then(function (results) {
            return results;
        });
    };

    WarehouseSubCategoryServiceFactory.putwhsubcategorys = _putwhsubcategorys;




    var _deletewhsubcategorys = function (data) {
        console.log("Delete Calling");
        console.log(data.SubCategoryId);


        return $http.delete(serviceBase + 'api/WarehouseSubCategory/?id=' + data.WhSubCategoryId).then(function (results) {
            return results;
        });
    };

    WarehouseSubCategoryServiceFactory.deletewhsubcategorys = _deletewhsubcategorys;
    WarehouseSubCategoryServiceFactory.getwhsubcategorys = _getwhsubcategorys;





    return WarehouseSubCategoryServiceFactory;

}]);