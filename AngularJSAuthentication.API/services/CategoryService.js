﻿'use strict';
app.factory('CategoryService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    console.log("Entered Services");
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var CategoryServiceFactory = {};

    var _getcategorys = function () {

        return $http.get(serviceBase + 'api/Category').then(function (results) {
            return results;
        });
    };

    CategoryServiceFactory.getcategorys = _getcategorys;

    var _getWarhouseCategory = function (data) {
        console.log("Service");
        console.log("get Filter Warhouse category function in warehouse service");
        console.log("ID");
        console.log(data.Warehouseid);

        return $http.get(serviceBase + 'api/Category', {
            params: {
                recordtype: "warehouse",
                whid: data.Warehouseid
            }
        }).success(function (data, status) {
            console.log(data);
            return data;
        });
    };

    CategoryServiceFactory.getWarhouseCategory = _getWarhouseCategory;

    var _getWarhouseCategorybyid = function (data) {
        console.log("Service");
        console.log("get Filter Warhouse categor by id function in warehouse service");
        console.log("ID");
        console.log(data.Warehouseid);
        console.log("WHID");
        console.log(data.WhCategoryid);

        return $http.get(serviceBase + 'api/Category', {
            params: {
                recordtype: "warehouse",
                whid: data.Warehouseid,
                whcatid: data.WhCategoryid
            }
        }).success(function (data, status) {
            console.log(data);
            return data;
        });
    };

    CategoryServiceFactory.getWarhouseCategorybyid = _getWarhouseCategorybyid;

    var _putcategorys = function () {

        return $http.put(serviceBase + 'api/Category').then(function (results) {
            return results;           

        });
    };

    CategoryServiceFactory.putcategorys = _putcategorys;




    var _deleteCategorys = function (data) {
        console.log("Delete Calling");
        console.log(data.Categoryid);
      
        return $http.delete(serviceBase + 'api/Category/?id=' + data.Categoryid).then(function (results) {
            console.log(results);
            return results;
        });
    };

    CategoryServiceFactory.deleteCategorys = _deleteCategorys;
    CategoryServiceFactory.getcategorys = _getcategorys;



    return CategoryServiceFactory;

}]);