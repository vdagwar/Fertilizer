'use strict';
app.controller('PurchaseOrderListController', ['$scope', 'PurchaseOrderListService', 'supplierService', "$filter", '$http', 'ngAuthSettings', "ngTableParams", '$modal', function ($scope, PurchaseOrderListService, supplierService, $filter, $http, ngAuthSettings, ngTableParams, $modal) {
    $scope.currentPageStores = {};
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            timePicker: true,
            timePickerIncrement: 5,
            timePicker12Hour: true,
            format: 'MM/DD/YYYY h:mm A',
        });
    });

    $scope.cities = [];
    PurchaseOrderListService.getcitys().then(function (results) {
        $scope.cities = results.data;
    }, function (error) {  });

    $scope.warehouse = [];
    PurchaseOrderListService.getwarehouse().then(function (results) {
        $scope.warehouse = results.data;
    }, function (error) { });

    function ReloadPage() {
        location.reload();
    };

    $scope.supplierList = [];
    $scope.suppliersearch = {};
    supplierService.getsuppliers().then(function (results) {
        $scope.supplierList = results.data;
    }, function (error) { });
    $scope.PurchaseOrder = [];
    $scope.GetPO = [];
    $scope.PurchaseList = [];

    PurchaseOrderListService.getorder(1).then(function (results) {
        $scope.InitialData = results.data;
        $scope.data = results.data;        
        $scope.table();
    }, function (error) {
    });

    $scope.table = function () {
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 25,
            reload: $scope.tableParams
        }, {
            total: $scope.data.length,
            getData: function ($defer, params) {
                var orderedData = params.sorting() ? $filter('orderBy')($scope.data, params.orderBy()) : $scope.data;
                orderedData = params.filter() ?
                        $filter('filter')(orderedData, params.filter()) :
                        orderedData;
                $defer.resolve($scope.users = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
        $scope.checkboxes = { 'checked': false, items: {} };
        $scope.$watch('checkboxes.checked', function (value) {
            angular.forEach($scope.users, function (orderDetail) {
                if (angular.isDefined(orderDetail.OrderDetailsId)) {
                    $scope.checkboxes.items[orderDetail.OrderDetailsId] = value;
                }
            });
        });
        $scope.$watch('checkboxes.items', function (values) {
            if (!$scope.users) {
                return;
            }
            var checked = 0, unchecked = 0,
                total = $scope.users.length;
            angular.forEach($scope.users, function (orderDetail) {
                checked += ($scope.checkboxes.items[orderDetail.OrderDetailsId]) || 0;
                unchecked += (!$scope.checkboxes.items[orderDetail.OrderDetailsId]) || 0;
            });
            if ((unchecked == 0) || (checked == 0)) {
                $scope.checkboxes.checked = (checked == total);
            }
            angular.element(document.getElementById("select_all")).prop("indeterminate", (checked != 0 && unchecked != 0));
        }, true);
    }

    $scope.dataforsearch = { Cityid: "", Warehouseid: "", datefrom: "", dateto: "" };

    $scope.Search = function (data) {
        var f = $('input[name=daterangepicker_start]');
        var g = $('input[name=daterangepicker_end]');
        $scope.dataforsearch.Cityid = data.Cityid;
        $scope.dataforsearch.Warehouseid = data.Warehouseid;
        if (!$('#dat').val()) {
            $scope.dataforsearch.datefrom = '';
            $scope.dataforsearch.dateto = '';
        }
        else {
            $scope.dataforsearch.datefrom = f.val();
            $scope.dataforsearch.dateto = g.val();
        }
        //---------------------------------------//
        PurchaseOrderListService.getfiltereddetails($scope.dataforsearch).then(function (results) {
            $scope.PurchaseList = [];
            $scope.InitialData = results.data;            
            $scope.data = results.data; //$scope.PurchaseList;
            console.log($scope.data);
            $scope.tableParams.reload();
            $scope.table();
        }, function (error) {
        });
    }

    $scope.reset = function () {
        $scope.suppliersearch = 0;
    };
    $scope.genrateAllPo = [];
    $scope.msg = {};

    $scope.genrateAllPo = function () {
        $scope.CkdId = [];
        var strItms = JSON.stringify($scope.checkboxes.items);
        strItms = strItms.replace("{", "");
        strItms = strItms.replace("}", "");
        var data = strItms.split(",");
        for (var i = 0; i < data.length; i++) {
            data[i] = data[i].replace("\"", "");
            var strData = data[i].split("\":");
            var id = strData[0];
            var value = strData[1];
            if (value == "true") {
                $scope.CkdId.push(id);
            }
        }
        $scope.plist = [];
        for (var j = 0; j < $scope.CkdId.length; j++) {
            _.each($scope.users, function (o2) {
                //if (o2.OrderDetailsId == $scope.CkdId[j]) {
                $scope.plist.push(o2);
                //}
            })
            $scope.PurchaseList = _.reject($scope.PurchaseList, function (o2) { return o2.OrderDetailsId == $scope.CkdId[j]; });
        }
        var dataToPost = $scope.plist;
        var url = serviceBase + "api/PurchaseOrderList";
        $http.post(url, dataToPost).success(function (data) {
            $scope.suppliersearch = 0;
            $scope.data = $scope.PurchaseList;
            alert("All Purchase Order genrated... :-)");
            location.reload();
            $scope.tableParams.reload();
            $scope.table();
        })
         .error(function (data) {
         })
    };
    
    $scope.$watch(function () { return $scope.stores }, function () { console.log("store"); });

    $scope.PurchaseOrder = function () {
     
        $("#po").prop("disabled", true);
        var modalInstance;
        $scope.CkdId = [];
        var strItms = JSON.stringify($scope.checkboxes.items);
        strItms = strItms.replace("{", "");
        strItms = strItms.replace("}", "");
        var data = strItms.split(",");
        for (var i = 0; i < data.length; i++) {
            data[i] = data[i].replace("\"", "");
            var strData = data[i].split("\":");
            var id = strData[0];
            var value = strData[1];
            if (value == "true") {
                $scope.CkdId.push(id);
            }
        }
        $scope.plist = [];
        for (var j = 0; j < $scope.CkdId.length; j++) {
            _.each($scope.users, function (o2) {
                if (o2.OrderDetailsId == $scope.CkdId[j]) {
                    $scope.plist.push(o2);
                }
            })
            $scope.PurchaseList = _.reject($scope.PurchaseList, function (o2) { return o2.OrderDetailsId == $scope.CkdId[j]; });
        }
        modalInstance = $modal.open(
            {
                templateUrl: "myputmodal.html",
                controller: "PurchaseOrdeSaveController", resolve: { object: function () { return $scope.plist } }
            }), modalInstance.result.then(function (selectedItem) {

                $scope.coupons.push(selectedItem);
                _.find($scope.coupons, function (coupons) {
                    if (coupons.id == selectedItem.id) {
                        coupons = selectedItem;
                    }
                });

                $scope.coupons = _.sortBy($scope.coupons, 'Id').reverse();
                $scope.selected = selectedItem;
            },
            function () {

            })
    }; 
}]);

app.controller("PurchaseOrdeSaveController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", "PurchaseOrderListService", '$modal', function ($scope, $http, ngAuthSettings, $modalInstance, object, PurchaseOrderListService, $modal) {
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    if (object) {
        $scope.saveData = object;
    }

    $scope.coupons = [];
    $scope.ok = function () {
        $modalInstance.close();
        window.location.reload();
    },
    $scope.cancel = function () {
        $modalInstance.dismiss('canceled');
        window.location.reload();
    },
    $scope.up = function (orderDetail) {
        orderDetail.finalqty += 1;
    };

    $scope.save = function () {
      
        $("#svpo").prop("disabled", true);
        var dataToPost = $scope.saveData;
        for (var i = 0 ; i < $scope.saveData.length; i++){
            dataToPost[i].qty = (dataToPost[i].conversionfactor * dataToPost[i].finalqty);
        } 
        var url = serviceBase + "api/PurchaseOrderList" + "?a=ab";
        $http.post(url, dataToPost).success(function (data) {
            $scope.suppliersearch = 0;
            $scope.data = $scope.PurchaseList;
            alert("All Purchase Order genrated... :-)");
            location.reload();
            $scope.tableParams.reload();
            $scope.table();
        })
         .error(function (data) {
         })
    };
}]);
