'use strict';
app.controller('CreateDamageOrderController', ['$scope', "customerService", "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, customerService, $filter, $http, ngTableParams, $modal, FileUploader) {
    
    $scope.customers = [];
    $scope.Customerid = '';
    customerService.getcustomers().then(function (results) {
 
        $scope.customers = results.data;
        console.log($scope.Customerid);
    }, function (error) {
    });

    $scope.getitemMaster = function(){
    
        $scope.getDamageItem();
      
    }

    $scope.getDamageItem = function () {
        var url = serviceBase + 'api/damagestock/getall';
        $http.get(url)
        .success(function (data) {
            $scope.DamageItemData = data;
            $scope.itemss = data;
            
        });
    }
    //TotalAmount = [];
    $scope.filtitemMaster = function (data) {
        
        $scope.selecteditem = JSON.parse(data.ItemId);
        $scope.AmountCalculation($scope.selecteditem);
    };

    $scope.TotalAmount = 0.0;
    $scope.AmountCalculation = function (data) {
        
        if (data.DamageInventory != 0 && data.DamageInventory != null) {
            $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
            console.log("Total amount" + $scope.TotalAmount);
        }
        else {
                $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
                console.log("Total amount" + $scope.TotalAmount);
            }

        
    }
      

    $scope.AddDOrder = function (data) {
        
        var url = serviceBase + "api/damageorder";

        var customerData = $("#site-name").val();
        if (customerData != "") {
            var customerObject = JSON.parse(customerData);
            data.Customer = customerObject;
        }
        console.log($scope.TotalAmount);
        var dataToPost = {
            "CustomerId": data.Customer.CustomerId,
            "CustomerName": data.Customer.Name,
            "ShippingAddress": data.Customer.ShippingAddress,
            "Warehouseid": data.Warehouseid,
            "WarehouseName": data.WarehouseName,
            "DamageStockId": data.DamageStockId,
            "qty": data.DamageInventory,
            "ItemName": data.ItemName,
            "ItemNumber": data.ItemNumber,
            "ItemId": data.ItemId,
            "UnitPrice": data.UnitPrice,
            "TotalAmount": $scope.TotalAmount,
        };

        console.log(dataToPost);
        $http.post(url, dataToPost)
        .success(function (response) {
            if (response != null) {
                $scope.items = response;
                alert('Damage order  created Successfully');
                location.reload();
            }
            else {

                alert('Damage order not created');
            }
            
        });
    }



   
}]);
