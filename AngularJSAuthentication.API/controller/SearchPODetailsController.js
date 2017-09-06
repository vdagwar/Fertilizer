'use strict';
app.controller('SearchPODetailsController', ['$scope', 'SearchPOService', 'supplierService', 'PurchaseODetailsService', "$filter", '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams",'$modal',
    function ($scope, SearchPOService, supplierService, PurchaseODetailsService, $filter, $http, $window, $timeout, ngAuthSettings, ngTableParams, $modal) {
        
        console.log("PODetailsController start loading PODetailsService");

        $scope.currentPageStores = {};
        $scope.PurchaseorderDetails = {};
        $scope.PurchaseOrderData = [];
        var d = SearchPOService.getDeatil();
        console.log(d);
        $scope.PurchaseOrderData = d;
        console.log("PurchaseOrderData");
        console.log($scope.PurchaseOrderData);

        supplierService.getsuppliersbyid($scope.PurchaseOrderData.SupplierId).then(function (results) {
            console.log("ingetfn");
            console.log(results);
            $scope.supaddress = results.data.BillingAddress;
            $scope.SupContactPerson = results.data.ContactPerson;
            $scope.supMobileNo = results.data.MobileNo;
        }, function (error) {

        });

        SearchPOService.getWarehousebyid($scope.PurchaseOrderData.Warehouseid).then(function (results) {
            console.log("get warehouse id");
            console.log(results);
            $scope.WhAddress = results.data.Address;
            $scope.WhCityName = results.data.CityName;
            $scope.WhPhone = results.data.Phone;
        }, function (error) {
        });

        PurchaseODetailsService.getPODetalis($scope.PurchaseOrderData.PurchaseOrderId).then(function (results) {
            $scope.PurchaseorderDetails = results.data;
            
            console.log("orders..........");
            console.log($scope.PurchaseorderDetails);
            $scope.totalfilterprice = 0;
            _.map($scope.PurchaseorderDetails, function (obj) {
                
                console.log(obj);
                
              $scope.totalfilterprice = $scope.totalfilterprice + ((obj.Price) * (obj.TotalQuantity));
               // $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmountIncTax;
                console.log("$scope.OrderData");
                console.log($scope.totalfilterprice);
                

            })
            $scope.callmethod();
        }, function (error) {
        });

        $scope.callmethod = function () {

            var init;
            return $scope.stores = $scope.PurchaseorderDetails,

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

        //----------------------------------------------------------------------------------------------------
       
        $scope.kot = function () {
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "kot.html",
                    controller: "Kotpopupctrls", resolve: { object: function () { return $scope.items } }
                }), modalInstance.result.then(function (selectedItem) {
                    $scope.currentPageStores.push(selectedItem);
                },
                function () {
                })
            //print
            //setTimeout(function () {
            //    window.print();
            //}, 100);

        }
                
        //-----------------------------------------------------------------------------------------------------

        $scope.open = function () {
            var modalInstance;
            var data = {}
            data = $scope.PurchaseOrderData;
            modalInstance = $modal.open(
                {
                    templateUrl: "myputmodal.html",
                    controller: "PurchaseOrdeADDController", resolve: { object: function () { return data } }
                }), modalInstance.result.then(function (selectedItem) {

                },
                function () {
                })
        };

        $scope.invoice = function (invoice) {
            console.log("in invoice Section");
            console.log(invoice);
        };

    }]);

app.controller("PurchaseOrdeADDController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", 'PurchaseODetailsService', "object", '$modal', function ($scope, $http, ngAuthSettings, $modalInstance, PurchaseODetailsService, object, $modal) {
    
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    if (object) {
        $scope.data = object;
    }

    $scope.ok = function () {
        $modalInstance.close();
        window.location.reload();
    },
    $scope.cancel = function () {
        $modalInstance.dismiss('canceled');
        window.location.reload();
    },
        $scope.idata = {};
    PurchaseODetailsService.GetItemMaster($scope.data).then(function (results) {
        $scope.itemMasterrr = results.data;
        $scope.idata = angular.copy($scope.itemMasterrr);
    });

    $scope.iidd = 0;
    $scope.Minqty = function (key) {
        
        $scope.itmdata = [];
        $scope.iidd = Number(key);
        for (var c = 0; c < $scope.idata.length; c++) {
            if ($scope.idata.length != null) {
                if ($scope.idata[c].ItemId == $scope.iidd) {
                    $scope.itmdata.push($scope.idata[c]);
                }
            }
            else {
            }
        }
    }



    $scope.Put = function (item) {
        

        if (item.Noofset != null || item.Noofset != undefined && Number(item.PurchaseMinOrderQty)!=undefined) {

            $scope.qQty = item.Noofset * Number(item.PurchaseMinOrderQty);

            var taxamt = 0;
            var obj = $scope.itmdata[0];
            //var quantity = JSON.parse(quantity);
            var quantity = $scope.qQty;
            //  var totalqty = (quantity * obj.PurchaseMinOrderQty);
            var totalqty = $scope.qQty;
            try {
                // taxamt = ((obj.PurchasePrice * quantity * obj.PurchaseMinOrderQty * obj.TotalTaxPercentage) / 100).toFixed(2);

                taxamt = ((obj.PurchasePrice * totalqty * obj.TotalTaxPercentage) / 100).toFixed(2);
            }
            catch (exe) {
                taxamt = 0;
            }
            var dataToPost = {
                PurchaseOrderId: $scope.data.PurchaseOrderId,
                ItemId: obj.ItemId,
                name: obj.PurchaseUnitName,
                ItemName: obj.ItemName,
                PurchaseSku: obj.PurchaseSku,
                Supplier: $scope.data.SupplierName,
                SupplierId: $scope.data.SupplierId,
                WareHouseId: obj.warehouse_id,
                WareHouseName: obj.WarehouseName,
                conversionfactor: obj.PurchaseMinOrderQty,
                finalqty: quantity,
                qty: totalqty,
                Price: obj.PurchasePrice,
                CityId: obj.Cityid,
                TaxAmount: taxamt,
                CityName: obj.CityName,
                itemNumber: obj.Number
            }
            console.log(dataToPost);
            var url = serviceBase + "api/PurchaseOrderList" + "?a=ab&b=1";
            $http.post(url, dataToPost).success(function (data) {
                $scope.data = $scope.PurchaseList;
                alert("All Purchase Order genrated... :-)");
                $modalInstance.close();
                window.location = "#/SearchPurchaseOrder";
            })
         .error(function (data) {
         })
        }
        else {

            alert('Please select no. of sets with Moq selection');
        }

    }


}]);

app.controller("Kotpopupctrls", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", "getset", '$rootScope', 'SearchPOService', 'supplierService', 'PurchaseODetailsService', function ($scope, $http, ngAuthSettings, $modalInstance, object, getset, $rootScope, SearchPOService, supplierService, PurchaseODetailsService) {
    
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () {        
        $modalInstance.dismiss('canceled');
    }
    console.log("PODetailsController start loading PODetailsService");

    $scope.currentPageStores = {};
    $scope.PurchaseorderDetails = {};
    $scope.PurchaseOrderData = [];
    var d = SearchPOService.getDeatil();
    console.log(d);
 // alert(d);
    $scope.PurchaseOrderData = d;
    console.log("PurchaseOrderData");
    console.log($scope.PurchaseOrderData);

    supplierService.getsuppliersbyid($scope.PurchaseOrderData.SupplierId).then(function (results) {
        console.log("ingetfn");
        console.log(results);
        $scope.supaddress = results.data.BillingAddress;
        $scope.SupContactPerson = results.data.ContactPerson;
        $scope.supMobileNo = results.data.MobileNo;
    }, function (error) {

    });

    SearchPOService.getWarehousebyid($scope.PurchaseOrderData.Warehouseid).then(function (results) {
        console.log("get warehouse id");
        console.log(results);
        $scope.WhAddress = results.data.Address;
        $scope.WhCityName = results.data.CityName;
        $scope.WhPhone = results.data.Phone;
    }, function (error) {

    });


    PurchaseODetailsService.getPODetalis($scope.PurchaseOrderData.PurchaseOrderId).then(function (results) {
      
        $scope.PurchaseorderDetails = results.data;
        console.log("orders..........");
        console.log($scope.PurchaseorderDetails);
        $scope.totalfilterprice = 0;
        _.map($scope.PurchaseorderDetails, function (obj) {

            $scope.totalfilterprice = $scope.totalfilterprice + obj.TotalAmountIncTax;
            console.log("$scope.OrderData");
            console.log($scope.totalfilterprice);

            console.log($scope.totalfilterprice);
        })
      //  $scope.callmethod();
    }, function (error) {
    });
  
    //setTimeout(function () {
    //    window.print();
    //}, 1000);
  
   
   
}])


