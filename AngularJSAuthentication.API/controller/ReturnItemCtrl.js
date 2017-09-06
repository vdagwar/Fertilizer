'use strict';
app.controller('ReturnItemCtrl', ['$scope',"$filter", '$http', '$window', '$timeout', 'ngAuthSettings', "ngTableParams", '$modal',
    function ($scope, $filter, $http, $window, $timeout, ngAuthSettings, ngTableParams, $modal) {
        console.log("ReturnItemCtrl start loading ReturnItem");
        
        $scope.PurchaseOrderData = [];
        $http.get(serviceBase + "api/ReturnPurchaseItem").success(function (data) {
            if (data != null) {
                $scope.PurchaseOrderData=data;
            }
        })
     .error(function (data) {
     })
        //----------------------------------------------------------------------------------------------------       
        $scope.kot = function () {
            var modalInstance;
            modalInstance = $modal.open(
                {
                    templateUrl: "kot.html",
                    controller: "Kotpopupctrl", resolve: { object: function () { return $scope.items } }
                }), modalInstance.result.then(function (selectedItem) {
                    $scope.currentPageStores.push(selectedItem);
                },
                function () {
                })
        }                
        //----------------------------------------------------------------------------------------------------
        $scope.open = function () {
            var modalInstance;
            var data = {}
            data = $scope.PurchaseOrderData;
            modalInstance = $modal.open(
                {
                    templateUrl: "myputmodal.html",
                    controller: "ReturnItemAddController", resolve: { object: function () { return data } }
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

app.controller("ReturnItemAddController", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", '$modal', 'PurchaseODetailsService', 'supplierService', function ($scope, $http, ngAuthSettings, $modalInstance, object, $modal, PurchaseODetailsService, supplierService) {    
    $scope.itemMasterrr = {};
    $scope.saveData = [];
    $scope.itemSupplier = [];
    if (object) { $scope.saveData = object; }
      
    supplierService.getsuppliers().then(function (results) {
        $scope.supplier = results.data;
    });

    $scope.ok = function () { $modalInstance.close(); window.location.reload(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); window.location.reload(); },

    $scope.supplietItems = function (supp) {
        var obj = JSON.parse(supp);
        $scope.itemSupplier.supplierCode = obj.SUPPLIERCODES;
        $scope.itemSupplier.supplierName = obj.Name;
        $scope.itemSupplier.supplierId = obj.SupplierId;
        obj.Warehouseid = 1;
        PurchaseODetailsService.GetItemMaster(obj).then(function (results) {
            $scope.itemMasterrr = results.data;
        });
    }
    $scope.AddItem = function (item, quantity, Price) {
        
        var obj = JSON.parse(item);
        var TotalQuantity = JSON.parse(quantity);
        var price = JSON.parse(Price);
        var Qyantity = parseInt(quantity / obj.PurchaseMinOrderQty);
       
        var dataToPost = {
            supplierCode: $scope.itemSupplier.supplierCode,
            supplierId: $scope.itemSupplier.supplierId,
            supplierName: $scope.itemSupplier.supplierName,
            WarehouseId: obj.warehouse_id,
            UnitPrice: price,
            TotalQuantity: TotalQuantity,
            TotalAmount: (TotalQuantity * price),
            Qyantity: Qyantity,
            Status:"Process",
            ItemId: obj.ItemId,
            itemname: obj.PurchaseUnitName,
            PurchaseSku: obj.PurchaseSku,
            itemNumber: obj.Number
        }
        console.log(dataToPost);
        var url = serviceBase + "api/ReturnPurchaseItem/add";
        $http.post(url, dataToPost).success(function (data) {
            if(data != null)
            {
                $scope.saveData.push(data);
                alert("All Return Order genrated... :-)");
                $modalInstance.close();
                window.location = "#/returnitem";
            }
            else {

            }
            })
     .error(function (data) {
     })
    };
}]);

app.controller("Kotpopupctrl", ["$scope", '$http', 'ngAuthSettings', "$modalInstance", "object", "getset", '$rootScope', 'supplierService', function ($scope, $http, ngAuthSettings, $modalInstance, object, getset, $rootScope, SearchPOService) {
   
    $scope.ok = function () { $modalInstance.close(); },
    $scope.cancel = function () { $modalInstance.dismiss('canceled'); }
    console.log("PODetailsController start loading PODetailsService");

    $scope.currentPageStores = {};
    $scope.PurchaseOrderData = [];
}])