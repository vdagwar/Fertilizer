﻿'use strict';
app.controller('RedispatchCtrl', ['$scope', 'OrderMasterService', 'OrderDetailsService', '$http', 'ngAuthSettings', '$filter', "ngTableParams", '$modal','DeliveryService',
    function ($scope, OrderMasterService, OrderDetailsService, $http, ngAuthSettings, $filter, ngTableParams, $modal, DeliveryService) {
        console.log("RedispatchCtrl start loading OrderDetailsService");
        $scope.currentPageStores = {};

        $scope.cities = [];
        OrderMasterService.getcitys().then(function (results) {
            $scope.cities = results.data;
        }, function (error) {
        });

        $scope.warehouse = [];
        OrderMasterService.getwarehouse().then(function (results) {
            $scope.warehouse = results.data;
        }, function (error) {
        });
        $scope.DBoys = [];
        DeliveryService.getdboys().then(function (results) {
            $scope.DBoys = results.data;
        }, function (error) {
        });
        $scope.getdborders = function (mob) {
            //$http.get(serviceBase + 'api/Redispatch?mob='+mob).then(function (results) {
            //    $scope.allOrders = results.data;
            //    $scope.callmethod();
            //});
        }
        //$scope.assignedorders = [];
        $http.get(serviceBase + 'api/Redispatch').then(function (results) {
            $scope.allOrders = results.data;
            $scope.callmethod();
            $scope.ProductsAgregate();
        });
        $scope.allOrders = [];
        $scope.allproducts = [];
        $scope.callmethod = function () {

            var init;
            return $scope.stores = $scope.allOrders,

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

                $scope.numPerPageOpt = [3, 5, 10, 20],
                $scope.numPerPage = $scope.numPerPageOpt[2],
                $scope.currentPage = 1,
                $scope.currentPageStores = [],
                (init = function () {
                    return $scope.search(), $scope.select($scope.currentPage)
                })

            ()


        }

        $scope.ProductsAgregate = function () {
            if ($scope.allOrders.length > 0) {
                $scope.selectedorders = angular.copy($scope.allOrders);
                console.log($scope.allOrders);
                var firstreq = true;
                for (var k = 0; k < $scope.selectedorders.length; k++) {
                    for (var j = 0; j < $scope.selectedorders[k].orderDetails.length; j++) {
                        if (firstreq) {
                            $scope.allproducts.push($scope.selectedorders[k].orderDetails[j]);
                            firstreq = false;
                        } else {
                            var checkprod = true;
                            _.map($scope.allproducts, function (prod) {
                                if ($scope.selectedorders[k].orderDetails[j].itemNumber == prod.itemNumber) {//Number
                                    prod.qty = $scope.selectedorders[k].orderDetails[j].qty + prod.qty;
                                    prod.TotalAmt = $scope.selectedorders[k].orderDetails[j].TotalAmt + prod.TotalAmt;
                                    checkprod = false;
                                }
                            })
                            if (checkprod) {
                                $scope.allproducts.push($scope.selectedorders[k].orderDetails[j]);
                            }
                        }
                    }
                }
                console.log("total products");
                console.log($scope.allproducts);
                $scope.totalAmountofallproducts = 0;
                for (var i = 0; i < $scope.allproducts.length; i++) {
                    $scope.totalAmountofallproducts = $scope.totalAmountofallproducts + $scope.allproducts[i].TotalAmt;
                }

            } else {
                alert("Orders to redispatch");
            }
        }

       
        $scope.Return = function (data) {
            data["Page"] = "Redispatch"
            OrderMasterService.saveReturn(data);
            console.log("Order Detail Dialog called ...");
        };
        $scope.mydata = {};
        $scope.showDetail = function (data) {
            console.log("Edit Dialog called city");
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "myredispatchdetail.html",
                    controller: "myredispatchdetailCtrl", resolve: { order: function () { return data } }
                }), modalInstance.result.then(function (selectedItem) {


                },
                function () {
                    console.log("Cancel Condintion");

                })
        };


    }]);

app.controller("myredispatchdetailCtrl", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order detail modal opened");

        $scope.orderDetails = [];
        if (order) {
            $scope.OrderData = order;
            $scope.orderDetails = $scope.OrderData.orderDetails;
            console.log($scope.OrderData);
            console.log($scope.OrderData.orderDetails);
            console.log($scope.orderDetails);
        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); }

    }]);

app.controller("ModalInstanceCtrlOrderInvoice", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, order) {
        console.log("order invoice modal opened");



        $scope.OrderDetails = {};
        $scope.OrderData = {};
        var d = OrderMasterService.getDeatil();

        $scope.OrderData = d;
        $scope.orderDetails = d.orderDetails;


        $scope.Itemcount = 0;


        for (var i = 0 ; i < $scope.orderDetails.length; i++) {

            $scope.Itemcount = $scope.Itemcount + $scope.orderDetails[i].qty;
        }

        $scope.totalfilterprice = 0;
        _.map($scope.OrderData.orderDetails, function (obj) {
            console.log("count total");

            $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmt;
            console.log(obj.TotalAmt);
            console.log($scope.totalfilterprice);

        })

    }]);

app.controller("ModalInstanceCtrldeleteOrder", ["$scope", '$http', 'OrderMasterService', "$modalInstance", 'ngAuthSettings', 'order',
    function ($scope, $http, OrderMasterService, $modalInstance, ngAuthSettings, myData) {
        console.log("delete modal opened");
        function ReloadPage() {
            location.reload();
        };

        $scope.orders = [];


        if (myData) {

            $scope.orders = myData.order1;

        }
        $scope.ok = function () { $modalInstance.close(); },
        $scope.cancel = function () { $modalInstance.dismiss('canceled'); },


        $scope.deleteorders = function (dataToPost, $index) {

            console.log("Delete  controller");
            console.log(dataToPost);
            OrderMasterService.deleteorder(dataToPost).then(function (results) {
                console.log("Del");

                // myData.all.splice($index, 1);

                $modalInstance.close(dataToPost);
                //ReloadPage();

            }, function (error) {
                alert(error.data.message);
            });
        }

    }])

