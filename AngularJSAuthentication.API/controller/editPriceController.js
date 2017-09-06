'use strict';
app.controller('editPriceController', ['$scope', 'itemMasterService', 'editPriceService', 'OrderMasterService', 'OrderDetailsService', 'SubsubCategoryService', 'SubCategoryService', 'CategoryService', 'unitMasterService', 'WarehouseService', "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, itemMasterService, editPriceService, OrderMasterService, OrderDetailsService, SubsubCategoryService, SubCategoryService, CategoryService, unitMasterService, WarehouseService, $filter, $http, ngTableParams, $modal, FileUploader) {
    console.log(" edit Price Controller reached");
    $scope.currentPageStores = {};
    $scope.cities = [];
    OrderMasterService.getcitys().then(function (results) {
        $scope.cities = results.data;
    }, function (error) {
    });

    $scope.category = [];
    CategoryService.getcategorys().then(function (results) {
        console.log("get category here");
        console.log(results)
        $scope.category = results.data;
        console.log("result of category here");
        console.log($scope.category);
    }, function (error) {
    });

    $scope.subcategory = [];
    SubCategoryService.getsubcategorys().then(function (results) {
        console.log("get sub category here");
        $scope.subcategory = results.data;
        console.log("result of sub cat here");
        console.log($scope.subcategory);
    }, function (error) {
    });

    $scope.subsubcategory = [];
    SubsubCategoryService.getsubsubcats().then(function (results) {
        console.log("get sub sub category here");
        $scope.subsubcategory = results.data;
        console.log("result of sub sub cat here");
        console.log($scope.subsubcategory);
    }, function (error) {
    });
   
    //....................................................//
    $scope.show = true;
    $scope.order = false;
    $scope.orders = [];

    $scope.dataforsearch = { Cityid: "", Categoryid: "", SubCategoryId: "", SubsubCategoryid: "" };

    $scope.Search = function (data) {
        console.log("data for search here");
        $scope.dataforsearch.Cityid = data.Cityid;
        $scope.dataforsearch.Categoryid = data.Categoryid;
        $scope.dataforsearch.SubCategoryId = data.SubCategoryId;
        $scope.dataforsearch.SubsubCategoryid = data.SubsubCategoryid;       
        $scope.orders = [];
        itemMasterService.getfiltereditemmaster($scope.dataforsearch).then(function (results) {
            console.log("filter details");          
            console.log(results.data);
            $scope.orders = results.data;
            $scope.callmethod();
        });
    } 

    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.orders,
            $scope.searchKeywords = "",
            $scope.filteredStores = [],
            $scope.row = "",
            $scope.select = function (page) {
                var end, start; console.log("select"); console.log($scope.stores);
                return start = (page - 1) * $scope.numPerPage, end = start + $scope.numPerPage, $scope.currentPageStores = $scope.filteredStores.slice(start, end)
            },
            $scope.onFilterChange = function () {
                console.log("onFilterChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1, $scope.row = ""
            },
            $scope.onNumPerPageChange = function () {
                console.log("onNumPerPageChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },
            $scope.onOrderChange = function () {
                console.log("onOrderChange"); console.log($scope.stores);
                return $scope.select(1), $scope.currentPage = 1
            },
            $scope.search = function () {
                console.log("search");
                console.log($scope.stores);
                console.log($scope.searchKeywords);

                return $scope.filteredStores = $filter("filter")($scope.stores, $scope.searchKeywords), $scope.onFilterChange()
            },
            $scope.order = function (rowName) {
                console.log("order"); console.log($scope.stores);
                return $scope.row !== rowName ? ($scope.row = rowName, $scope.filteredStores = $filter("orderBy")($scope.stores, rowName), $scope.onOrderChange()) : void 0
            },
            $scope.numPerPageOpt = [30, 50, 100, 200],
            $scope.numPerPage = $scope.numPerPageOpt[1],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })
        ()
    }

    $scope.itemMasters = [];
   
    //...........Exel export Method............//
    $scope.currentPageStores = {};
  
    // single margin update
    $scope.marginupdate = function (data) {  
        var url = serviceBase + "api/editPrice/byid";
    
        $http.put(url, data)
        .then(function (response) {
            $scope.itemMasters = response.data;
            alert('price update successfully');
            $("#st" + data.ItemId).prop("disabled", true);
        });
    };

    $scope.Saveediteditem = function (data) {
        $scope.itemMasterData = { };
        $scope.editeddata = data;
        console.log(data);
        editPriceService.Saveediteditem(data).then(function (results) {
            console.log("results");
            console.log(results);
        })       
    };     
}]);