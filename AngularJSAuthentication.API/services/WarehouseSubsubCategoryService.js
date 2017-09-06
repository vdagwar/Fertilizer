'use strict';
app.factory('WarehouseSubsubCategoryService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var WhSubsubCategoryServiceFactory = {};

    var _getwhsubsubcats = function () {

        return $http.get(serviceBase + 'api/WarehouseSubsubCategory').then(function (results) {
            return results;
        });
    };

    WhSubsubCategoryServiceFactory.getwhsubsubcats = _getwhsubsubcats;



    var _putsubsubcats = function () {

        return $http.put(serviceBase + 'api/WarehouseSubsubCategory').then(function (results) {
            return results;
        });
    };

    WhSubsubCategoryServiceFactory.putsubsubcats = _putsubsubcats;




    var _deletewhsubsubcategorys = function (data) {
        console.log("Delete Calling");
        console.log(data.WhSubsubCategoryid);


        return $http.delete(serviceBase + 'api/WarehouseSubsubCategory/?id=' + data.WhSubsubCategoryid).then(function (results) {
            return results;
        });
    };

    WhSubsubCategoryServiceFactory.deletewhsubsubcategorys = _deletewhsubsubcategorys;
    WhSubsubCategoryServiceFactory.getwhsubsubcats = _getwhsubsubcats;





    return WhSubsubCategoryServiceFactory;

}]);