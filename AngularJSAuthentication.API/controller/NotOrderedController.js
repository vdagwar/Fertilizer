'use strict';
app.controller('NotOrderedController', ['$scope', '$http', '$modal', "$filter", function ($scope, $http, $modal, $filter) {
    
    $scope.skcode = "";

    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A'
        });
    });

    $scope.getNotorderddata = function () {

        $scope.notOrdered = {};
        var url = serviceBase + "api/NotOrdered";
        $http.get(url)
        .success(function (results) {

            $scope.notOrdered = results;
            $scope.callmethod();
        })
         .error(function (data) {
             console.log(data);
         })
    };
    $scope.getNotorderddata();


    $scope.Search = function () {
      
        if ($('#dat').val() == null || $('#dat').val() == "") {
            $('input[name=daterangepicker_start]').val("");
            $('input[name=daterangepicker_end]').val("");
        }
        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');

        var start = f.val();
        var end = g.val();

        if ($scope.skcode != "" || (start != null && start != "")) {
            $scope.notOrdered = {};
            var url = serviceBase + "api/NotOrdered/search?start=" + start + "&end=" + end + "&skcode=" + $scope.skcode;
            $http.get(url).success(function (response) {
                $scope.notOrdered = response;
                $scope.callmethod();
            });
        }
        else {
            alert('Please select one parameter');
        }
        }
   
    
    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }
    $scope.callmethod = function () {
        var init;
        return $scope.stores = $scope.notOrdered,

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

            $scope.numPerPageOpt = [10, 25, 50, 100],
            $scope.numPerPage = $scope.numPerPageOpt[0],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })

        ()


    }
    $scope.exportData = function () {
        alasql('SELECT ShopName,Skcode,SalespersonName,status,Comment, CreatedDate INTO XLSX("NotOrdered.xlsx",{headers:true}) FROM ?', [$scope.currentPageStores]);
    };
}]);
