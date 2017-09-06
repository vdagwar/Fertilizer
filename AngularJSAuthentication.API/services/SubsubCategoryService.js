'use strict';
app.factory('SubsubCategoryService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {


    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var SubsubCategoryServiceFactory = {};

    var _getsubsubcats = function () {

        return $http.get(serviceBase + 'api/SubsubCategory').then(function (results) {
            return results;
        });
    };

    SubsubCategoryServiceFactory.getsubsubcats = _getsubsubcats;



    var _putsubsubcats = function () {

        return $http.put(serviceBase + 'api/SubsubCategory').then(function (results) {
            return results;
        });
    };

    SubsubCategoryServiceFactory.putsubsubcats = _putsubsubcats;




    var _deletesubsubcategorys = function (data) {
        console.log("Delete Calling");
        console.log(data.SubsubCategoryid);


        return $http.delete(serviceBase + 'api/SubsubCategory/?id=' + data.SubsubCategoryid).then(function (results) {
            return results;
        });
    };

    SubsubCategoryServiceFactory.deletesubsubcategorys = _deletesubsubcategorys;
    SubsubCategoryServiceFactory.getsubsubcats = _getsubsubcats;





    return SubsubCategoryServiceFactory;

}]);