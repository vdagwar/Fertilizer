'use strict';
app.controller('demandController', ['$scope', 'demandservice', "$filter", "$http", "ngTableParams", 'FileUploader', '$modal', '$log', function ($scope, demandservice, $filter, $http, FileUploader, ngTableParams, $modal, $log) {

    //.................File Uploader method start..................
    //$scope.daterange = { startDate: null, endDate: null };
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            //locale: {
            format: 'MM/DD/YYYY h:mm A'
            //}
        });
    });

    $scope.itemMasters = [];
    demandservice.GetitemMaster(6).then(function (results) {
        console.log("gett");
        $scope.itemMasters = results.data;
        //$scope.callmethod();
    }, function (error) {
        console.log("exel file is not uploaded...");
    });
    $scope.demandlist = [];
    $scope.demanddetail1 = {};
    $scope.AddDemand = function () {
        console.log("add demand is calling...");
        console.log($scope.demand);
        $scope.demanddetail1 = {};
        $scope.demanddetail1 = { "itemname": $scope.demand.itemname1, "Quantity": $scope.demand.Quantity1, "Description": $scope.demand.Description1 };
        $scope.demandlist.push($scope.demanddetail1);
        console.log("demandlist");
        console.log($scope.demandlist); 
    };
    $scope.savedemand = function () {
        console.log("savedemand is calling....");
        console.log($scope.demandlist);
        demandservice.Postdemand($scope.demandlist).then(function (results) {
            console.log("post demand controller is calling.....");
            //$scope.callmethod();
        }, function (error) {
            console.log("exel file is not uploaded...");
        });        
    }
    $scope.cities = [];
    demandservice.getcitys().then(function (results) {
        $scope.cities = results.data;
    }, function (error) {
    });

    $scope.warehouse = [];
    demandservice.getwarehouse().then(function (results) {
        console.log(results.data);
        console.log("data");
        $scope.warehouse = results.data;
    }, function (error) {
    });
    $scope.InitialData = [];
    $scope.DemandDetails = [];
    $scope.dataforsearch = { Cityid: "", Warehouseid: "", datefrom: "", dateto: "" };
    $scope.Search = function (data) {
        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');
        console.log("moin");
        console.log(f);
        console.log(g);
        //alert(f.val());
        //alert(g.val());
        $scope.dataforsearch.Cityid = data.Cityid;
        $scope.dataforsearch.Warehouseid = data.Warehouseid;
        $scope.dataforsearch.datefrom = f.val();
        $scope.dataforsearch.dateto = g.val();

        console.log("search is calling...");
        console.log(data);
        demandservice.getfiltereddetails($scope.dataforsearch).then(function (results) {
            $scope.DemandDetails = [];
            console.log("get getfiltereddetails controller...");
            console.log(results);
            $scope.allfilterdata = results.data;
            $scope.uniqueList1 = _.uniq(results.data, function (item, key, ItemId) {
                return item.ItemId;
            });
            console.log("uniqueList");
            console.log($scope.uniqueList1);
            var quantity = 0;
            _.map($scope.uniqueList1, function (obj1) {
                quantity = 0;
                _.map($scope.allfilterdata, function (obj) {
                    if (obj.ItemId == obj1.ItemId) {
                        quantity = quantity + obj.qty;
                    }
                });
                //console.log("quantity");
                //console.log(quantity);
                obj1.qty = quantity;
                $scope.DemandDetails.push(obj1);                
            });
            console.log("final list");
            console.log($scope.DemandDetails);
            $scope.callmethod();
        }, function (error) {
            console.log("error");
        });
    }

    //............................File Uploader Method End.....................//
    //............................Exel export Method.....................//

    alasql.fn.myfmt = function (n) {
        return Number(n).toFixed(2);
    }

    $scope.exportData1 = function () {
        alasql('SELECT ItemCode,itemname,MinOrderQty,qty INTO XLSX("Demand.xlsx",{headers:true}) FROM ?', [$scope.stores]);
    };
    $scope.exportData = function () {
        alasql('SELECT * INTO XLSX("Demand.xlsx",{headers:true}) \ FROM HTML("#exportable",{headers:true})');
    };

    $scope.callmethod = function () {

        var init;
        return $scope.stores = $scope.DemandDetails,

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

            $scope.numPerPageOpt = [20, 50, 100, 200],
            $scope.numPerPage = $scope.numPerPageOpt[1],
            $scope.currentPage = 1,
            $scope.currentPageStores = [],
            (init = function () {
                return $scope.search(), $scope.select($scope.currentPage)
            })
        ()
    }

    demandservice.getdemanddetails(1).then(function (results) {
        console.log(results.data);
        console.log("data order details...");
        $scope.InitialData = results.data;
        $scope.OrderData = results.data;
        $scope.uniqueList = _.uniq(results.data, function (item, key, ItemId) {
            return item.ItemId;
        });
        console.log($scope.uniqueList);
        var quantity = 0;
        _.map($scope.uniqueList, function (obj1) {
            quantity = 0;
            _.map($scope.OrderData, function (obj) {
                if (obj.ItemId == obj1.ItemId) {
                    quantity = quantity + obj.qty;
                }
            });
            console.log("quantity");
            console.log(quantity);
            obj1.qty = quantity;

            $scope.DemandDetails.push(obj1);
            console.log($scope.DemandDetails);
            $scope.callmethod();
        });
        //$scope.DemandDetails = results.data;
    }, function (error) {
    });
}]);